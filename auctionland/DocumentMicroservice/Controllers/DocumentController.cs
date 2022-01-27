using AutoMapper;
using DocumentMicroservice.Models;
using DocumentMicroservice.Services.Implementation;
using DocumentMicroservice.Services.Repository;
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
    //[Authorize]
    public class DocumentController : ControllerBase
    {
        //private readonly DocumentService _documentService;
        private readonly IDocumentRepository documentRepository;
        private readonly IMapper mapper;

        public DocumentController(IDocumentRepository documentRepository, IMapper mapper)
        {
            this.documentRepository = documentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ResponseDocumentDto>> GetAllDocuments()
        {
            var documents = documentRepository.GetAllDocuments();

            //Ukoliko nismo pronašli ni jednu prijavu vratiti status 204 (NoContent)
            if (documents == null || documents.Count == 0)
            {
                return NoContent();
            }

            //Ukoliko smo našli neke prijava vratiti status 200 i listu pronađenih prijava (DTO objekti)
            return Ok(mapper.Map<List<ResponseDocumentDto>>(documents));
        }
        /*
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{documentID}")] //Dodatak na rutu koja je definisana na nivou kontrolera
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
        [HttpDelete("{documentID}")]
        public IActionResult DeleteExamRegistration(Guid documentID)
        {
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
        }*/
    }
}
