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

        public dynamic GetCurrentState()
        {
            Player player = GetPlayer();
            var task = dataContext.Tasks.Single(t => t.Id == player.TaskNumber);

            return new
            {
                Task = task,
                Player = player
            };
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
                    //finish
                    return null;
                }

                player.TaskNumber++;
                dataContext.SaveChanges();

                return new { Task = nextTask, Player = player, Feedback = GetPositiveFeedback() };
            }

            return new
            {
                Task = task,
                Player = GetPlayer(token),
                Feedback = GetNegativeFeedback()
            };
        }

        readonly Random rnd = new Random();
        private readonly string[] negatives = {"Нет.", "неа", "не верно", "Ответ не правильный", "Мимо"};
        private readonly string[] positives = { "Да!", "Правильно", "Верно", "Ответ принят", "Правильно" };
        private string GetNegativeFeedback()
        {
            return negatives[rnd.Next(0, 5)];
        }

        private string GetPositiveFeedback()
        {
            return positives[rnd.Next(0, 5)];
        }

        private Player GetPlayer(string token = null)
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
}
