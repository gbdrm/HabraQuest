using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public string RegisterNewPlayer()
        {
            Player player = new Player
            {
                Token = Guid.NewGuid()
            };

            dataContext.Add(player);
            dataContext.SaveChanges();

            return player.Token.ToString();
        }
    }
}
