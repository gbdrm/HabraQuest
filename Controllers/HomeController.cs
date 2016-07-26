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

        public Player InitializePlayer()
        {
            Player player = null;
            var token = Request.Cookies["playerToken"];
            if (token == null)
            {
                player = new Player
                {
                    Token = Guid.NewGuid()
                };

                dataContext.Add(player);
                dataContext.SaveChanges();
            }

            return player ?? dataContext.Players.Single(p => p.Token.ToString() == token);
        }

        public dynamic GetCurrentState()
        {
            return new
            {
                Task = new QuestTask
                {
                    Id = 2,
                    Title = "Шта?",
                    Content = "Превед, креведко"
                },
                Player = InitializePlayer(),
            };
        }

        public dynamic SubmitAnswer()
        {
            return new
            {
                Task = new QuestTask
                {
                    Id = 3,
                    Title = "Шта?2",
                    Content = "Превед, креведко. ololo"
                },
                Player = InitializePlayer(),
            };
        }
    }

    public class QuestTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Watched { get; set; }
        public int Done { get; set; }
    }
}
