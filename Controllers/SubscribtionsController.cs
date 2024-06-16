using AutoMapper;
using HelperPlan.DTO.PaylinkDtos;
using HelperPlan.DTO.SubscribtionDtos;
using HelperPlan.Models;
using HelperPlan.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Security.Claims;

namespace HelperPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribtionsController : ControllerBase
    {
        private readonly IUnitOFWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> user;

        // get section Paylink from appsettings.json by i options
        private readonly EnvironmentPaylink _paylink;

        public SubscribtionsController(IUnitOFWork unitOfWork, IMapper mapper, IOptions<EnvironmentPaylink> paylink,UserManager<ApplicationUser>user)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.user = user;
            _paylink = paylink.Value;
        }
        //get sub
        [HttpGet("getstatus")]
        public IActionResult GetSub() 
        {
            int UserID = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var subscribe= _unitOfWork.SubscribtionRepository.Get(c => c.UserID == UserID);
            if (subscribe != null)
            {
                return Ok(new { IsActive = subscribe.IsActive });
            }
            return Ok(new { IsActive = false });
        }

        // create subscribtion

        [HttpPost("Create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(SubscribtionDto subscribtionDto)
        {
            if (ModelState.IsValid)
            {

                subscribtionDto.EmployerID=int.Parse( HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                subscribtionDto.UserId = subscribtionDto.EmployerID;

               var orderNumber = Guid.NewGuid().ToString();
    
                var result = await GenratePaylink(await AuthenticationPaylink(), subscribtionDto, orderNumber);

                var subscribtion = _mapper.Map<Subscribtion>(subscribtionDto);
                subscribtion.SubscribtionsStatus = result.OrderStatus;
                subscribtion.TransactionNo = result.TransactionNo;
                subscribtion.CheckUrl = result.CheckUrl;
                subscribtion.orderNumber = orderNumber;
                _unitOfWork.SubscribtionRepository.add(subscribtion);
                _unitOfWork.SubscribtionRepository.save();
                return Ok(new { Url = result.Url });
               
            }
            return BadRequest(ModelState);
        }

        // check subscribtion status
        [HttpGet("CheckStatus/{orderNumber}")]
        public async Task<IActionResult> CheckStatus(string? orderNumber)
        {
            var subscribtion = _unitOfWork.SubscribtionRepository.Get(x => x.orderNumber == orderNumber);
            int Duration = _unitOfWork.PlanRepository.Get(x => x.ID == subscribtion.PlanID).Duration;
            if (subscribtion == null)
            {
                return NotFound();
            }
            var options = new RestClientOptions(_paylink.url + $"/api/getInvoice/{subscribtion.TransactionNo}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json;charset=UTF-8");
            var token = await AuthenticationPaylink();
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await client.GetAsync(request);
            var rst=response.Content; 
            var result = JsonConvert.DeserializeObject<GatewayOrderResponse>(response.Content);

            // using swatch to check status of subscribtion
            switch (result.OrderStatus)
            {
                case "Pending":
                    subscribtion.SubscribtionsStatus = "Pending";
                    break;
                case "Paid":
                    subscribtion.SubscribtionsStatus = "Paid";
                    subscribtion.IsActive = true;
                    subscribtion.EndDate = DateTime.Now.AddDays(Duration);
                    break;
                case "Declined":
                    subscribtion.SubscribtionsStatus = "Declined";
                    break;
                default:
                    subscribtion.SubscribtionsStatus = "Cancelled ";
                    break;
            }

            _unitOfWork.SubscribtionRepository.update(subscribtion);
            _unitOfWork.SubscribtionRepository.save();
            return Ok(result);
        }

        [NonAction]
        // Authentication paylink
        public async Task<string> AuthenticationPaylink()
        {
            var options = new RestClientOptions(_paylink.url + "/api/auth");
            var client = new RestClient(options);
            var request = new RestRequest("");


            request.AddHeader("accept", "*/*");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(new
            {
                apiId = _paylink.apiId,
                secretKey = _paylink.secretKey,
                persistToken = _paylink.persistToken,
            });
            var response = await client.PostAsync(request);
            var result = response.Content;

            // i need get id_token desrialize it and return it
            var token = JsonConvert.DeserializeObject<tokenPaylink>(result);

            // return token.id_token;

            return token.id_token;

        }

        [NonAction]
        // genrate paylink
        public async Task<GatewayOrderResponse> GenratePaylink(string token, SubscribtionDto subscribtionDto, string orderNumber)
        {

            var UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userinfo =await user.FindByIdAsync(UserId);
                // _unitOfWork.EmployeerRepository.Get(x => x.Id == subscribtionDto.EmployerID);

            var plan = _unitOfWork.PlanRepository.Get(x => x.ID == subscribtionDto.PlanID);

            var options = new RestClientOptions(_paylink.url + "/api/addInvoice");
            var client = new RestClient(options);
            var request = new RestRequest("");

            request.AddHeader("accept", "application/json");
            // add token to header
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(new
            {

                amount = plan.Price, //100.0,
                clientMobile = userinfo.PhoneNumber,//"0512345678",
                clientName = string.Format("{0} {1}", userinfo.Fname, userinfo.Lname),
                //"Mohammed Ali",
                clientEmail = userinfo.Email,//"mohammed@test.com",

                orderNumber = orderNumber,// "123456789",


                callBackUrl = $"http://localhost:4200/success/{orderNumber}",
                cancelUrl = $"http://localhost:4200/fail/{orderNumber}",
                currency = "SAR",
                note = "Test invoice",

                products = new List<object>
                {
                    new
                    {

                        title = plan.Name,//"test",
                        price =plan.Price,//100.0,
                        qty = 1,
                        imageSrc = "",
                        description = "",
                        isDigital = false,
                        specificVat = 0,
                        productCost = 0,
                    }
                },
            }) ;
            var response = await client.PostAsync(request);
            // deserialize response
            var rst = response.Content;
            var result = JsonConvert.DeserializeObject<GatewayOrderResponse>(response.Content);

            return result;
        }

        

    }
}
