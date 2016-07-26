using System;
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
            if (task.Answers.Split(',').Contains(answer.ToLower()))
            {
                if (task.Id == 9)
                {
                    //finish
                    return null;
                }

                var nextTask = dataContext.Tasks.Single(t => t.Id == task.Id + 1);
                player.TaskNumber++;
                dataContext.SaveChanges();

                return new { Task = nextTask, Player = player };
            }

            return new
            {
                Task = task,
                Player = GetPlayer(token),
            };
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
                    TaskNumber = 0
                };

                dataContext.Add(player);
                dataContext.SaveChanges();
            }

            return player ?? dataContext.Players.Single(p => p.Token.ToString() == token);
        }
    }
}
