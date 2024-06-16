using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.dto.JobToModel
{
    public class JTMLanguagesDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int JobID { get; set; }
    }
}
