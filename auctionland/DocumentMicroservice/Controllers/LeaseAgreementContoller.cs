using AutoMapper;
using DocumentMicroservice.Data.Repository;
using DocumentMicroservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Controllers
{
    [ApiController]
    [Route("api/document/leaseAgreement")]
    [Produces("application/json", "application/xml")]
    public class LeaseAgreementContoller : ControllerBase
    {
        private readonly ILeaseAgreementRepository agreementRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public LeaseAgreementContoller(ILeaseAgreementRepository agreementRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.agreementRepository = agreementRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<LeaseAgreementCreationDto>> GetAllLeases()
        {
            var leases = agreementRepository.GetAllLeases();

            //Ukoliko nismo pronašli ni jednu prijavu vratiti status 204 (NoContent)
            if (leases == null || leases.Count == 0)
            {
                return NoContent();
            }

            //Ukoliko smo našli neke prijava vratiti status 200 i listu pronađenih prijava (DTO objekti)
            return Ok(mapper.Map<List<LeaseAgreementCreationDto>>(leases));
        }
    }
}
