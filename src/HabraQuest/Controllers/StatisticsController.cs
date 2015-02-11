using System;
using System.Globalization;
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
        public Finisher[] Post([FromBody]SaveNameRequest data)
        {
            if (data == null) return Array.Empty<Finisher>();
            var name = data.Name;
            var token = data.Token;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var current = db.Progress.FirstOrDefault(_ => _.Token == token);
                if (current != null && current.TaskNumber == 999)
                {
                    var alreadyAdded = db.Finishers.Any(_ => _.Token == token);
                    if (!alreadyAdded)
                    {
                        var finisher = new Finisher()
                        {
                            Name = name,
                            Token = token,
                            Time = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)
                        };
                        db.Finishers.Add(finisher);
                        db.SaveChanges();
                    }

                    return db.Finishers.ToArray();
                }
                return null;
            }
        }
    }

    public class SaveNameRequest
    {
        public string Token { get; set; }
        public string Name { get; set; }
    }

    public class StatisticsResult
    {
        public string Ok { get; } = "OK";
        public int Watched { get; set; }
        public int Done { get; set; }
    }
}
