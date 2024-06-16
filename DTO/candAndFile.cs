using HelperPlan.DTO.dto;
using Microsoft.AspNetCore.Mvc;

namespace HelperPlan.DTO
{
    public class candAndFile
    {
        
         public  candsDTO ?Cands { get; set; }
        
        public IFormFile? file { get; set; }

    }
}
