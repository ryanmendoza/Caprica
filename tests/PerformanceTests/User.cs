namespace PerformanceTests
{
    public class User
    {
        public User(int userId, int level, string state, string title)
        {
            UserId = userId;
            Level = level;
            State = state;
            Title = title;
        }
        
        public int UserId { get; set; }
        public int Level { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
    }
}