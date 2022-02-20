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
    [Authorize]
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

        /// <summary>
        /// Vraća listu svih uplata za javno nadmetanje
        /// </summary>
        /// <returns>Lista uplata za javno nadmetanje</returns>
        /// <response code="200">Vraća listu uplata za javno nadmetanje</response>
        /// <response code="404">Nije pronađena ni jedna jedina uplata za javno nadmetanje</response>
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

        /// <summary>
        /// Vraća uplatu za javno nadmetanje na osnovu ID
        /// </summary>
        /// <param name="paymentId">ID uplate za javno nadmetanje</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu uplatu za javno nadmetanje</response>
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

        /// <summary>
        /// Kreira novu uplatu za javno nadmetanje.
        /// </summary>
        /// <param name="paymentCreation">Model uplate za javno nadmetanje</param>
        /// <returns>Potvrdu o kreiranoj uplati.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove uplate \
        /// POST /api/application/payment \
        /// {     \
        ///     "accountNumber": 158, \
        ///     "referenceNumber": 8875, \
        ///     "purposeOfPayment": "Svrha uplate", \
        ///     "dateOfPayment": "2020-11-15T10:30:00" \
        ///     "publicBiddingId": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreiranu uplatu za javno nadmetanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom uplate za javno nadmetanje</response>
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
                string location = linkGenerator.GetPathByAction("GetPaymentById", "PaymentForApplication", new { paymentId = confirmation.PaymentId });
                return Created(location, mapper.Map<PaymentConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Vrši brisanje jedne uplate za javno nadmetanje na osnovu ID-ja prijave.
        /// </summary>
        /// <param name="paymentId">ID uplate za javno nadmetanje</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Uplata za javno nadmetanje uspešno obrisana</response>
        /// <response code="404">Nije pronađena uplata za javno nadmetanje za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja uplate za javno nadmetanje</response>
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

        /// <summary>
        /// Ažurira jednu uplatu za javno nadmetanje.
        /// </summary>
        /// <param name="paymentUpdateDto">Model uplate za javno nadmetanje koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj uplati.</returns>
        /// <response code="200">Vraća ažuriranu uplatu za javno nadmetanje</response>
        /// <response code="400">Uplata za javno nadmetanje koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja uplate za javno nadmetanje</response>
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

        /// <summary>
        /// Vraća opcije za rad sa prijavama ispita
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
