using DocumentMicroservice.Models;
using DocumentMicroservice.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Controllers
{
    [ApiController]
    [Route("api/document")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService documentService;

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ResponseDocumentDto>> GetAllDocuments()
        {
            //fale provere da li je nocontent
            //Ukoliko smo našli neke prijava vratiti status 200 i listu pronađenih prijava (DTO objekti)
            return Ok(documentService.GetAllDocuments());
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{examRegistrationId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public ActionResult<ResponseDocumentDto> GetDocument(Guid documentID) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var document = documentService.GetDocumentById(documentID);

            if (document == null)
            {
                return NotFound();
            }
            return Ok(documentService.GetDocumentById(documentID));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ResponseDocumentDto> CreateDocument([FromBody] RequestDocumentDto documentDto)
        {
            try
            {
                return documentService.CreateDocument(documentDto);
            }
            catch (Exception ex)
            {
                //TODO: Logovati ex
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ResponseDocumentDto> UpdateExamRegistration(RequestDocumentDto documentDto)
        {
            try
            {
                return Ok(documentService.UpdateDocument(documentDto));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{examRegistrationId}")]
        public IActionResult DeleteExamRegistration(Guid documentID)
        {
            //TODO: Dodati logiku da se studentu vrate sredstva na račun ukoliko se obriše prijava ispita
            try
            {
                var document = documentService.GetDocumentById(documentID);

                if (document == null)
                {
                    return NotFound();
                }

                documentService.DeleteDocument(documentID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
    }
}
