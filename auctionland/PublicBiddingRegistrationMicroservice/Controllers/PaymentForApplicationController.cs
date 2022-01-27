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
    }
}
