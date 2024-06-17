using AutoMapper;
using Hangfire;
using HelperPlan.DTO;
using HelperPlan.DTO.dto;
using HelperPlan.Models;
using HelperPlan.Repository;
//using HelperPlan.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System.Security.Claims;

namespace HelperPlan.Controllers
{
     

    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
       private UserManager<ApplicationUser> user;
        private IUnitOFWork unitOfWork;
        private readonly IMapper autoMapper;
        private readonly IWebHostEnvironment webHostEnvironment;
       

        public CandidateController(UserManager<ApplicationUser> user, IUnitOFWork unitOfWork , IMapper autoMapper,IWebHostEnvironment webHostEnvironment)
        {


            this.user = user;
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
            this.webHostEnvironment = webHostEnvironment;
            
        }

        [HttpGet("/Cadidate/GetFilteredCandidates")]
        public ActionResult<IEnumerable<Candidate>> GetFilteredCandidates([FromQuery]filtercandidateDTO fcd)
        {
           
            List<Candidate>? filteredcandidates = unitOfWork.CandidateRepository.filteredcandidate(fcd , "Languages,MainSkills").ToList();
           
            var candidate = unitOfWork.CandidateRepository.mapandget(filteredcandidates);
            return Ok(candidate);
        }

        [HttpGet("/Candidate/GetById/{id:int}")]
        public ActionResult<candsDTO> GetById(int id)
        {
            Candidate cadidate = unitOfWork.CandidateRepository.Get(C => C.Id == id, "Languages,MainSkills,OtherSkills,CookingSkills,Experiences,Educations");
            var candidatesDTO = autoMapper.Map<candsDTO>(cadidate);
            return Ok(candidatesDTO);
        }

        [HttpGet("/Candidate/GetAll")]
        public ActionResult<IEnumerable<candsDTO>> GetAll()
        {
            RecurringJob.AddOrUpdate(() => CheckSubscriptionActivation(), Cron.Daily());
            IEnumerable<Candidate> allCandidates = unitOfWork.CandidateRepository.GetList( c=> c.description != "" || c.description != null && c.Position != "" || c.Position != null && c.PhotoURL != null || c.PhotoURL != "" , "Languages");
            var candidatesDTO = autoMapper.Map<IEnumerable<candsDTO>>(allCandidates);
            return Ok(candidatesDTO);
        }

        [HttpPut("/Cadidate/Update")]
        public async Task<IActionResult> Update()
        {
           
            var formData = await Request.ReadFormAsync();        
            var candidateJson = formData["cands"];
            var cand = JsonConvert.DeserializeObject<candsDTO>(candidateJson.Last());
            var file = Request.Form.Files["file"];
            var candidateFromDataBase = unitOfWork.CandidateRepository.Get(c => c.Id == cand.Id);
             autoMapper.Map(cand, candidateFromDataBase, cand.GetType(), candidateFromDataBase.GetType());
           


            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                {
                    unitOfWork.CandidateRepository.update(candidateFromDataBase);
                    unitOfWork.CandidateRepository.save();
                    return Ok();
                }

                else
                {
                    var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "Candidates");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    candidateFromDataBase.PhotoURL = "/Images/Candidates/" + uniqueFileName;
                    unitOfWork.CandidateRepository.update(candidateFromDataBase);
                    unitOfWork.CandidateRepository.save();
                    // return CreatedAtAction("GetByID", new { id = candidate.Id }, candidate);
                    return Ok();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
           

        }

        [HttpPost("/Candidate/Insert")]    // Insert with candidateDTO
        public async Task<IActionResult> Insert()
        {
            var formData = await Request.ReadFormAsync();
            var candidateJson = formData["cands"]; // Assuming "cands" is the key for candidate information
            var cand = JsonConvert.DeserializeObject<candsDTO>(candidateJson.Last());
            var file = Request.Form.Files["file"];
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            if (ModelState.IsValid)
            {

                var candidate = autoMapper.Map<Candidate>(cand);
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "Candidates");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                candidate.PhotoURL = "/Images/Candidates/" + uniqueFileName;
                unitOfWork.CandidateRepository.add(candidate);
                unitOfWork.CandidateRepository.save();
                // return CreatedAtAction("GetByID", new { id = candidate.Id }, candidate);
                return Ok();
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("/Candidate/Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Candidate candidate = unitOfWork.CandidateRepository.Get(C => C.Id == id);
                unitOfWork.CandidateRepository.remove(candidate);
                unitOfWork.CandidateRepository.save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Candidate/Count")]
        public IActionResult candidateCount()
        {
            int cadidate = unitOfWork.CandidateRepository.GetAll().Count();
            return Ok(cadidate);
        }


        [ApiExplorerSettings(IgnoreApi = true)]

        public void CheckSubscriptionActivation()
        {
            List<Subscribtion> allSubscriptions = unitOfWork.SubscribtionRepository.GetAll().ToList();
            var todayDate = DateTime.Now;
            foreach (var subscription in allSubscriptions)
            {
                if (subscription.EndDate <= todayDate)
                {
                    subscription.IsActive = false;
                }
            }
        }

    }
}
