using HelperPlan.Models;
using HelperPlan.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace HelperPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IUnitOFWork unitOfWork;
        private readonly IMapper autoMapper;

        public EmployerController(IUnitOFWork unitOFWork , IMapper autoMapper)
        {
            this.unitOfWork = unitOFWork;
            this.autoMapper = autoMapper;
        }

        // GET api/EmployerController/5
        [HttpGet("/Employer/GetById/{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(unitOfWork.EmployeerRepository.Get(E => E.Id == id));
        }

        // GET: api/EmployerController
        [HttpGet("/Employer/GetAll")]
        public ActionResult<IEnumerable<Employer>> GetAll()
        {
            IEnumerable<Employer> employers = unitOfWork.EmployeerRepository.GetAll();
            return Ok(employers);
        }

        // POST: api/EmployerController
        [HttpPost("/Employer/Insert")]
        public IActionResult Insert(Employer emp)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.EmployeerRepository.add(emp);
                unitOfWork.EmployeerRepository.save();
                return CreatedAtAction("GetById", new { id = emp.Id }, emp);
            }
            return BadRequest(ModelState);

        }

        // PUT api/EmployerController/5
        [HttpPut("/Employer/Update/{id:int}")]
        public IActionResult Update(int id , Employer newEmp)
        {
            if (ModelState.IsValid == true)
            {
                Employer EmpDB = unitOfWork.EmployeerRepository.Get(E => E.Id == id);

                if (EmpDB == null)
                {
                    return BadRequest("Invalid ID");
                }
                else if (EmpDB.Id != newEmp.Id)
                {
                    return BadRequest("Invalid ID");

                }

                EmpDB.Fname = newEmp.Fname;
                EmpDB.Lname = newEmp.Lname;
                EmpDB.description = newEmp.description;
                EmpDB.title = newEmp.title;
                EmpDB.PhoneNumber = newEmp.PhoneNumber;
                EmpDB.Email = newEmp.Email;
                EmpDB.Location = newEmp.Location;
                EmpDB.DayOff = newEmp.DayOff;
                EmpDB.Accomodation = newEmp.Accomodation;
                EmpDB.Salary = newEmp.Salary;
                EmpDB.KidsNo = newEmp.KidsNo;
                EmpDB.AdultNo = newEmp.AdultNo;
                EmpDB.HasBet = newEmp.HasBet;

                unitOfWork.EmployeerRepository.update(EmpDB);
                unitOfWork.EmployeerRepository.save();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        // DELETE api/EmployerController/5
        [HttpDelete("/Employer/Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Employer emp = unitOfWork.EmployeerRepository.Get(E => E.Id == id);
                unitOfWork.EmployeerRepository.remove(emp);
                unitOfWork.EmployeerRepository.save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




    }
}
