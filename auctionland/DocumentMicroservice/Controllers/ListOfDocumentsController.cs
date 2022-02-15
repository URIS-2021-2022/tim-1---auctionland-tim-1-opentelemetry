using AutoMapper;
using DocumentMicroservice.Models;
using DocumentMicroservice.Services;
using DocumentMicroservice.Services.Implementation;
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
    [Route("api/document/listofdocuments")]
    [Produces("application/json", "application/xml")]
    public class ListOfDocumentsController : ControllerBase
    {
        private readonly IListOfDocuments listOfDocRep;
        private readonly ListOfDocumentsService listOfDocumentsService;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ListOfDocumentsController(IListOfDocuments listOfDocRep, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.listOfDocRep = listOfDocRep;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ListOfDocumentsDto>> GetAllList()
        {
            var lists = listOfDocRep.GetAllLists();

            if (lists == null || lists.Count == 0)
            {
                return NoContent();
            }
            //return Ok(mapper.Map<List<ResponseListOfDocumentsDto>>(lists));
            return listOfDocumentsService.GetAllLists();
        }
    }
}
