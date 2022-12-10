using System.ComponentModel.DataAnnotations;

namespace ConnectMediaEF.Models
{
    public class userTasks
    {
        [Key]
        [Required] 
        public int Id { get; set; }

        public DateTime selectedDay { get; set; }

        public int userId { get; set; }
        public int ProjectId { get; set; }

        public List<Tasks> tasks { get; set; }

    }
}
