using System.ComponentModel.DataAnnotations;

namespace DemoLogging.Data
{
    public class Todo
    {
        [Key]
        public int UserId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }
    }
}
