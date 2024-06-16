using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.Models
{
    public class AspNetFaceBookUsers
    {
        public int ID { get; set; }
        public string FaceBookUserId { get; set; }
        public int OriginalApplicationUserId { get; set; }
        [ForeignKey(nameof(OriginalApplicationUserId))]
        public ApplicationUser? applicationUser { get; set; }

    }
}
