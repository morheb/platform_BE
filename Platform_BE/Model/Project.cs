namespace ConnectMediaEF.Models
{
    public class Project
    {
        public int Id { get; set; } 
        public string projectName { get; set; } = string.Empty;
        public string projectImage { get; set; } = string.Empty;
        public string projectDescription { get; set; } = string.Empty;
        public string projectDay { get; set; } = string.Empty;
        public double PercentageComplete { get; set; }
        public string Department { get; set; } = string.Empty;
        public int userId { get; set; }
        public List<Tasks> tasks { get; set; }
    }
}
