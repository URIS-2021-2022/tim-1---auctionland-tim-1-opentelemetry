using AutoMapper;
using DocumentMicroservice.Data.Repository;
using DocumentMicroservice.Entities;
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
    [Route("api/document/listofdocuments")]
    [Produces("application/json", "application/xml")]
    public class ListOfDocumentsController : ControllerBase
    {
        private readonly IListOfDocumentsRepository listOfDocRep;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ListOfDocumentsController(IListOfDocumentsRepository listOfDocRep, LinkGenerator linkGenerator, IMapper mapper)
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
            return Ok(mapper.Map<List<ListOfDocumentsCreationDto>>(lists));
        }

        [HttpGet("{listId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ListOfDocumentsDto> GetListById(Guid listId)
        {
            ListOfDocuments listOfDocuments = listOfDocRep.GetListById(listId);
            if (listOfDocuments == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ListOfDocumentsDto>(listOfDocuments));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ListOfDocumentsConfirmationDto> CreateListOfDocuments([FromBody] ListOfDocumentsCreationDto listCreation) 
        { 
            try
            {
                ListOfDocuments listEntity = mapper.Map<ListOfDocuments>(listCreation);
                ListOfDocumentsConfirmation confirmation = listOfDocRep.CreateList(listEntity);
                listOfDocRep.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetListById", "ListOfDocuments", new { listOfDocumentsId = confirmation.ListOfDocumentsId });

                return CreatedAtRoute(location, mapper.Map<ListOfDocumentsConfirmationDto>(confirmation)); //Druga opcija za vraćanje kreiranog resursa i lokacije
            }
            catch (Exception ex)
            {
                //TODO: Logovati ex
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpDelete("{listId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteListOfDocuments(Guid listId)
        {
            try
            {
                var listOfDocs = listOfDocRep.GetListById(listId);
                if (listOfDocs == null)
                {
                    return NotFound();
                }
                listOfDocRep.DeleteList(listId);
                listOfDocRep.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ListOfDocumentsConfirmationDto> UpdateListOfDocuments(ListOfDocumentsUpdateDto updateDto)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldList = listOfDocRep.GetListById(updateDto.ListOfDocumentsId);
                if (oldList == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                ListOfDocuments listEntity = mapper.Map<ListOfDocuments>(updateDto);

                mapper.Map(listEntity, oldList); //Update objekta koji treba da sačuvamo u bazi                

                listOfDocRep.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<ListOfDocumentsDto>(oldList));
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
