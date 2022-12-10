using System.ComponentModel.DataAnnotations;

namespace ConnectMediaEF.Models
{
    public class Tasks
    {
        [Key]
        [Required]
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public int userId { get; set; }

        public string Department { get; set; } = string.Empty;

        public DateTime selectedDay { get; set; }

        public bool doneOrNot { get; set; } = false;

        public int ProjectId { get; set; }
    }
}
