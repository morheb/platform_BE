using System.ComponentModel.DataAnnotations;

namespace ConnectMediaEF.Models
{
    public class Attendance
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public DateTime AttendanceDay { get; set; } 
        public int userId { get; set; }
        public string AttendancestartTime { get; set; } = "--/--";
        public string AttendanceendTime { get; set; } = "--/--";


    }
}
