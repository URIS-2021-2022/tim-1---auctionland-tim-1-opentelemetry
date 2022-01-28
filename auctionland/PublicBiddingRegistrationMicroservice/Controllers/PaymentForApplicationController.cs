using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicBiddingRegistrationMicroservice.Data;
using PublicBiddingRegistrationMicroservice.Models;
using PublicBiddingRegistrationMicroservice.Entities;

namespace PublicBiddingRegistrationMicroservice.Controllers
{
    [ApiController]
    [Route("api/application/payment")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class PaymentForApplicationController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        public PaymentForApplicationController(IPaymentRepository paymentRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<PaymentDto>> GetAllPayments()
        {
            List<PaymentForApplication> paymentsEntities = paymentRepository.GetPayments();
            if(paymentsEntities == null)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PaymentDto>>(paymentsEntities));
        }

        [HttpGet("{paymentId}")]
        public ActionResult<PaymentDto> GetPaymentById(Guid paymentId)
        {
            PaymentForApplication paymentModel = paymentRepository.GetPaymentsById(paymentId);
            if (paymentModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PaymentDto>(paymentModel));
        }

        [HttpPost]
        public ActionResult<PaymentConfirmationDto> CreatePayment([FromBody] PaymentCreationDto paymentCreation)
        {
            try
            {
                bool modelValid = ValidatePayment(paymentCreation);

                if (!modelValid)
                {
                    return BadRequest("The exam is not in the scope of the students enrolled year or current semester");
                }

                PaymentForApplication pay = mapper.Map<PaymentForApplication>(paymentCreation);

                PaymentConfirmation confirmation = paymentRepository.CreatePayment(pay);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetExamRegistration", "ExamRegistration", new { paymentId = confirmation.PaymentId });
                return Created(location, mapper.Map<PaymentConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{paymentId}")]
        public IActionResult DeletePayment(Guid paymentId)
        {
            try
            {
                PaymentForApplication paymentModel = paymentRepository.GetPaymentsById(paymentId);
                if (paymentModel == null)
                {
                    return NotFound();
                }
                paymentRepository.DeletePayment(paymentId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<PaymentConfirmationDto> UpdatePayment(PaymentUpdateDto paymentUpdateDto)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (paymentRepository.GetPaymentsById(paymentUpdateDto.PaymentId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                PaymentForApplication paymentEntity = mapper.Map<PaymentForApplication>(paymentUpdateDto);
                PaymentConfirmation confirmation = paymentRepository.UpdatePayment(paymentEntity);
                return Ok(mapper.Map<PaymentConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        // Validira da student ne moze da prijavi ispit u visoj godini nego sto je prijavljen
        private bool ValidatePayment(PaymentCreationDto paymentCreationDto)
        {
            if (paymentCreationDto.AccountNumber.Equals(0))
            {
                return false;
            }
            if (paymentCreationDto.PurposeOfPayment == null)
            {
                return false;
            }
            return true;
        }
    }
}
