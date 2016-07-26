using System;

namespace HabraQuest.Model
{
    public class Player
    {
        public int Id { get; set; }
        public Guid Token { get; set; }
        public int TaskNumber { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool HasFinished { get; set; }
    }
}
