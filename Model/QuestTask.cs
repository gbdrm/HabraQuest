namespace HabraQuest.Model
{
    public class QuestTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Watched { get; set; }
        public int Done { get; set; }
        
        /// <summary>
        /// Lower case answers, separated by ','
        /// </summary>
        public string Answers { get; set; }
    }
}