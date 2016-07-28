using System;
using System.Collections.Generic;
using System.Linq;
using HabraQuest.Model;
using Microsoft.AspNetCore.Mvc;

namespace HabraQuest.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext dataContext;

        public HomeController(DataContext db)
        {
            dataContext = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public dynamic Finish([FromBody]FinishForm form)
        {
            var token = Request.Cookies["playerToken"];
            if (token == null)
            {
                return BadRequest("something went wrong");
            }

            var player = dataContext.Players.SingleOrDefault(p => p.Token.ToString() == token);
            if (player == null || !player.HasFinished || !string.IsNullOrEmpty(player.Name))
            {
                return BadRequest("something went wrong");
            }

            player.Name = form.Name;
            player.Comment = form.Message;
            player.Email = form.Email;
            dataContext.SaveChanges();

            return new OkResult();
        }

        public dynamic GetCurrentState()
        {
            try
            {
                Player player = GetOrInitPlayer();
                var task = dataContext.Tasks.Single(t => t.Id == player.TaskNumber);

                return new ReturnResult(task, player);
            }
            catch (Exception ex)
            {
                //to do: log error
                return Json(new { status = "error", message = ex.Message });
            }

        }

        public dynamic SubmitAnswer(string answer)
        {
            var token = Request.Cookies["playerToken"];
            if (token == null)
            {
                return BadRequest("something went wrong");
            }

            var player = dataContext.Players.Single(p => p.Token.ToString() == token);
            var task = dataContext.Tasks.Single(t => t.Id == player.TaskNumber);
            if (!string.IsNullOrEmpty(answer) && task.Answers.Split(',').Contains(answer.ToLower()))
            {
                var nextTask = dataContext.Tasks.SingleOrDefault(t => t.Id == task.Id + 1);
                if (nextTask == null)
                {
                    player.HasFinished = true;
                    dataContext.SaveChanges();

                    return new ReturnResult(task, player);
                }

                player.TaskNumber++;
                dataContext.SaveChanges();

                return new ReturnResult(nextTask, player, GetPositiveFeedback());
            }

            return new ReturnResult(task, player, GetNegativeFeedback());
        }

        readonly Random rnd = new Random();
        private readonly string[] negatives = { "Нет.", "неа", "не верно", "Ответ не правильный", "Мимо" };
        private readonly string[] positives = { "Да!", "Правильно", "Верно", "Ответ принят", "Правильно" };
        private string GetNegativeFeedback()
        {
            return negatives[rnd.Next(0, 5)];
        }

        private string GetPositiveFeedback()
        {
            return positives[rnd.Next(0, 5)];
        }

        private Player GetOrInitPlayer(string token = null)
        {
            Player player = null;
            if (token == null) token = Request.Cookies["playerToken"];
            if (token == null)
            {
                player = new Player
                {
                    Token = Guid.NewGuid(),
                    TaskNumber = 1
                };

                dataContext.Add(player);
                dataContext.SaveChanges();
            }

            return player ?? dataContext.Players.Single(p => p.Token.ToString() == token);
        }
    }

    public class ReturnResult
    {
        public dynamic Task { get; set; }
        public dynamic Player { get; set; }
        public string Feedback { get; set; }
        public ReturnResult(QuestTask task, Player player, string feedback = "")
        {
            Task = new
            {
                task.Content, task.Title
            };

            Player = new
            {
                player.Name,
                player.Comment,
                player.HasFinished,
                player.TaskNumber,
                player.Token
            };

            Feedback = feedback;
        }
    }

    public class FinishForm
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
    }
}
