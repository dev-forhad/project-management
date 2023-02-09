using System.ComponentModel.DataAnnotations.Schema;

namespace project_management_api.Model
{
    public class ProjectInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public User Admin { get; set; }
        public ICollection<User> Developers { get; set; }
        public ICollection<TaskInformation> Tasks { get; set; }
    }
}
