using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HabraQuest.Controllers
{
    [Route("api/[controller]")]
    public class Results : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<ResultItem> Fetch()
        {
            int r = 1;
            // TODO: change from Mock data, max = 100 ?
            return Enumerable.Range(1, 6).Select(index => new ResultItem
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                Summary = "Ololo",
                Rank = r++,
                Name = "Test Player"
            });
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
