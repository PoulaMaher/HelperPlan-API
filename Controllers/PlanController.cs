using AutoMapper;
using HelperPlan.Models;
using HelperPlan.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IUnitOFWork unitOfWork;
        private readonly IMapper autoMapper;

        public PlanController(IUnitOFWork unitOfWork , IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;
            this.autoMapper = autoMapper;
        }

        // GET: api/Job
        [HttpGet("/Plan/GetAll")]
        public ActionResult<IEnumerable<Plan>> GetAll()
        {
            IEnumerable<Plan> Plans = unitOfWork.PlanRepository.GetAll();
            return Ok(Plans);
        }

        // GET api/Plan/5
        [HttpGet("/Plan/GetById/{id:int}")]
        public IActionResult GetById(int id)
        {
            Plan Plan = unitOfWork.PlanRepository.Get(P => P.ID == id);
            return Ok(Plan);
        }

        //Post api/Plan
        [HttpPost("/Plan/Insert")]
        public IActionResult Insert(Plan Plan)
        {
            if (ModelState.IsValid == true)
            {
                unitOfWork.PlanRepository.add(Plan);
                unitOfWork.PlanRepository.save();
                return CreatedAtAction("GetByID", new { id = Plan.ID }, Plan);
            }
            return BadRequest(ModelState);
        }

        // PUT api/Plan/5
        [HttpPut("/Plan/Update/{id:int}")]
        public IActionResult Update(int id, Plan newPlan)
        {
            if (ModelState.IsValid == true)
            {
                Plan PlanDB = unitOfWork.PlanRepository.Get(P => P.ID == id);

                if (PlanDB == null)
                {
                    return BadRequest("Invalid ID");
                }
                else if (PlanDB.ID != id)
                {
                    return BadRequest("Invalid ID");

                }
                PlanDB.Name = newPlan.Name;
                PlanDB.Type = newPlan.Type;
                PlanDB.Price = newPlan.Price;
                PlanDB.Subscribtion = newPlan.Subscribtion;
                unitOfWork.PlanRepository.update(PlanDB);
                unitOfWork.PlanRepository.save();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        // DELETE api/Plan/5
        [HttpDelete("/Plan/Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Plan Plan = unitOfWork.PlanRepository.Get(P => P.ID == id);
                unitOfWork.PlanRepository.remove(Plan);
                unitOfWork.PlanRepository.save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
