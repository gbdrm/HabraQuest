using System.Collections.Generic;
using System.Linq;
using HabraQuest.Model;
using Microsoft.AspNetCore.Mvc;

namespace HabraQuest.Controllers
{
    [Route("api/[controller]")]
    public class Results : Controller
    {
        private readonly DataContext dataContext;
        public Results(DataContext db)
        {
            dataContext = db;
        }

        [HttpGet("[action]")]
        public IEnumerable<ResultItem> Fetch()
        {
            var finieshed = dataContext.Players.Where(p => p.HasFinished).Take(100).Select(item => new ResultItem
            {
                Summary = item.Comment.Length>160? item.Comment.Substring(0, 150) + "..." : item.Comment,
                Name = item.Name
            });

            return finieshed;
        }

        public class ResultItem
        {
            public int Rank { get; set; }
            public string DateFormatted { get; set; }
            public string Name { get; set; }
            public string Summary { get; set; }
        }
    }
}
