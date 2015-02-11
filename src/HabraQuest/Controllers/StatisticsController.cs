using System;
using System.Linq;
using HabraQuest.Models;
using Microsoft.AspNet.Mvc;

namespace HabraQuest.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        // GET api/Statistics
        [HttpGet]
        public StatisticsResult Get(string token)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var current = db.Progress.FirstOrDefault(_ => _.Token == token);
                int taskNumber = current?.TaskNumber ?? 1;

                return new StatisticsResult
                {
                    Watched = db.Progress.Count(_ => _.TaskNumber >= taskNumber),
                    Done = db.Progress.Count(_ => _.TaskNumber > taskNumber)
                };

            }
        }

        // POST api/Statistics
        [HttpPost]
        public Finisher[] Post(string token, string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var current = db.Progress.FirstOrDefault(_ => _.Token == token);
                if (current != null && current.TaskNumber == 999)
                {
                    return null;
                }
                return null;
            }
        }
    }

    public class Finisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }

    public class StatisticsResult
    {
        public string Ok { get; } = "OK";
        public int Watched { get; set; }
        public int Done { get; set; }
    }
}
