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
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PaymentDto>> GetAllPayments()
        {
            var paymentsEntities = paymentRepository.GetPayments();
            if(paymentsEntities == null)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PaymentDto>>(paymentsEntities));
        }

        [HttpGet("{paymentId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PaymentDto> GetPaymentById(Guid paymentId)
        {
            var paymentModel = paymentRepository.GetPaymentsById(paymentId);
            if (paymentModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PaymentDto>(paymentModel));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PaymentConfirmationDto> CreatePayment([FromBody] PaymentCreationDto paymentCreation)
        {
            try
            {
                PaymentForApplication paymentEntity = mapper.Map<PaymentForApplication>(paymentCreation);
                PaymentConfirmation confirmation = paymentRepository.CreatePayment(paymentEntity);
                paymentRepository.SaveChanges();

                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetAllPayments", "PaymentForApplication", new { paymentId = confirmation.PaymentId });
                return Created(location, mapper.Map<PaymentConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{paymentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePayment(Guid paymentId)
        {
            try
            {
                var paymentModel = paymentRepository.GetPaymentsById(paymentId);
                if (paymentModel == null)
                {
                    return NotFound();
                }
                paymentRepository.DeletePayment(paymentId);
                paymentRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PaymentConfirmationDto> UpdatePayment(PaymentUpdateDto paymentUpdateDto)
        {
            try
            {
                var oldPayment = paymentRepository.GetPaymentsById(paymentUpdateDto.PaymentId);
                if (oldPayment == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                PaymentForApplication examRegistrationEntity = mapper.Map<PaymentForApplication>(paymentUpdateDto);

                mapper.Map(examRegistrationEntity, oldPayment); //Update objekta koji treba da sačuvamo u bazi                

                paymentRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<PaymentDto>(oldPayment));
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
    }
}
