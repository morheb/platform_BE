using System.ComponentModel.DataAnnotations;

namespace ConnectMediaEF.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirebaseId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
        public double UserRank { get; set; }
        public int AlluserTask { get; set; }
        public int PendinguserTask { get; set; }
        public string UserAbout { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public int Usertype { get; set; }
        public int UserAge { get; set; }
        public string FireBaseToken { get; set; } = string.Empty;

    }
}
