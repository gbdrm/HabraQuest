using System;
using System.Linq;
using HabraQuest.Models;
using Microsoft.AspNet.Mvc;

namespace HabraQuest.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class QuestController : Controller
    {
        // GET api/MainQuest
        [HttpGet]
        public MainQuestViewModel Get(string token, string taskNumberString, string answer)
        {
            int taskNumber;
            if (token == "null") token = null;
            if (!string.IsNullOrEmpty(taskNumberString) && int.TryParse(taskNumberString, out taskNumber))
            {
                using (var db = new ApplicationDbContext())
                {
                    if (taskNumber == -1)
                    {
                        if (string.IsNullOrEmpty(token))
                        {
                            token = Guid.NewGuid().ToString();
                            db.Progress.Add(new Progress { TaskNumber = 1, Token = token });
                            db.SaveChanges();

                            return new MainQuestViewModel
                            {
                                Task = new Task
                                {
                                    Number = 1,
                                    Title = "Первый",
                                    Content = "Какой уровень следующий?"
                                },
                                Token = token
                            };
                        }
                    }

                    if (string.IsNullOrEmpty(token)) return null;

                    var current = db.Progress.FirstOrDefault(_ => _.Token == token);
                    if (current == null || (current.TaskNumber != taskNumber && taskNumber != -1))
                    {
                        return new MainQuestViewModel
                        {
                            IsAnswerRight = false,
                            Task = null,
                            Token = null
                        };
                    }

                    // if user already finished
                    if (current.TaskNumber == 999)
                        return new MainQuestViewModel
                        {
                            IsAnswerRight = true,
                            Task = null,
                            Token = token
                        };

                    // check if the answer is right or get last task for user
                    var isRight = taskNumber == -1;
                    if (!isRight)
                    {
                        var task = db.QuestTask.SingleOrDefault(_ => _.Number == taskNumber);
                        if (task != null)
                        {
                            isRight = task.Answer.Split(',').Contains(answer?.ToLower() ?? "");
                        }
                    }

                    var nextTask = GetNextTask(db, isRight, taskNumber, current);

                    var progress = db.Progress.SingleOrDefault(_ => _.Token == token);
                    if (progress == null) return null;

                    // save user progress
                    if (isRight)
                    {
                        progress.TaskNumber = nextTask?.Number ?? 999;
                        db.SaveChanges();
                    }

                    return new MainQuestViewModel
                    {
                        IsAnswerRight = isRight,
                        Task = nextTask,
                        Token = token
                    };
                }
            }

            return null;
        }

        // POST api/MainQuest
        [HttpPost]
        public void Post([FromBody]StartAgainRequest startAgainRequest)
        {
            if (startAgainRequest != null && startAgainRequest.StartAgaing)
            {
                using (var db = new ApplicationDbContext())
                {
                    var progress = db.Progress.SingleOrDefault(_ => startAgainRequest != null && _.Token == startAgainRequest.Token);
                    if (progress != null)
                    {
                        progress.TaskNumber = 1;
                        db.SaveChanges();
                    }
                }
            }
        }

        private Task GetNextTask(ApplicationDbContext db, bool isRight, int taskNumber, Progress current)
        {
            QuestTask nextTask = null;
            if (taskNumber == -1)
            {
                nextTask = db.QuestTask.SingleOrDefault(_ => _.Number == current.TaskNumber);
            }
            else if (isRight)
            {
                nextTask = db.QuestTask.FirstOrDefault(t => t.Id > taskNumber);
            }

            return nextTask != null
                ? new Task
                {
                    Number = nextTask.Number,
                    Title = nextTask.Title,
                    Content = nextTask.Content
                }
                : null;
        }
    }

    public class StartAgainRequest
    {
        public string Token { get; set; }
        public bool StartAgaing { get; set; }
    }

    public class MainQuestViewModel
    {
        public bool IsAnswerRight { get; set; }
        public Task Task { get; set; }
        public string Token { get; set; }
    }

    public class Task
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
