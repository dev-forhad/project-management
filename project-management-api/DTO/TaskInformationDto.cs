using static project_management_api.Configuration.Enumration;

namespace project_management_api.DTO
{
    public class TaskInformationDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public ProgressStatus Status { get; set; }
    }
}
