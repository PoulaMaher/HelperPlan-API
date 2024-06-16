using AutoMapper;
using HelperPlan.DTO.dto;
using HelperPlan.DTO.dto.JobToModel;
using HelperPlan.DTO.JobDTO;
using HelperPlan.Models;
using HelperPlan.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HelperPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IUnitOFWork unitOfWork;
        private readonly IMapper autoMapper;
        private readonly UserManager<ApplicationUser> userManager;
        public JobController(UserManager<ApplicationUser> UserManager, IUnitOFWork unitOfWork , IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            userManager = UserManager;
        }
        // GET: api/Job
        [HttpGet("/Job/GetJobHeadlines")]
        public ActionResult<IEnumerable<Job>> GetJobHeadlines()
        {
            IEnumerable<Job> Jobs = unitOfWork.JobRepository.GetJobHeadlines();
            return Ok(Jobs);
        }
        // GET api/Job/5
        [HttpGet("/Job/GetById/{id:int}")]
        public IActionResult GetById(int id)
       {
            Job Job = unitOfWork.JobRepository.Get(J => J.Id == id);
            Job = unitOfWork.JobRepository.IncludeDetails(Job);
            return Ok(Job);
        }
        [HttpGet("/Job/GetFilteredJobs")]
        public IActionResult GetFilteredJobs([FromQuery]FilterParams FilterParams)
        {
            List<Job> Job = unitOfWork.JobRepository.GetFilteredJobs(FilterParams);
            return Ok(Job);
        }
        //Post api/Job
        [HttpPost("/Job/Insert")]
        public IActionResult Insert(Job Job)
        {

            if (ModelState.IsValid == true)
            {
                unitOfWork.JobRepository.AddJob(Job);
                return Ok(new { Msg = "Inserted" });
            }
            return BadRequest(ModelState);
        }

        // PUT api/Job/5
        [HttpPut("/Job/Update/{id:int}")]
        public IActionResult Update(int id, Job Job)
        {
            if (ModelState.IsValid == true)
            {
                Job JobDB = unitOfWork.JobRepository.Get(J => J.Id == id);

                if (JobDB == null)
                {
                    return BadRequest("Invalid ID");
                }
                else if (JobDB.Id != Job.Id)
                {
                    return BadRequest("Invalid ID");

                }
                unitOfWork.JobRepository.update(Job);
                unitOfWork.JobRepository.save();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        // DELETE api/Job/5
        [HttpDelete("/Job/Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Job Job = unitOfWork.JobRepository.Get(J => J.Id == id);
                unitOfWork.JobRepository.remove(Job);
                unitOfWork.JobRepository.save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
