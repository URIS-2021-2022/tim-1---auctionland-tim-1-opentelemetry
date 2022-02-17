using AutoMapper;
using DocumentMsProject.Data;
using DocumentMsProject.Entities;
using DocumentMsProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Controllers
{
    [ApiController]
    [Route("api/document")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public DocumentController(IDocumentRepository documentRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.documentRepository = documentRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<DocumentCreationDto>> GetAllDocuments()
        {
            var documents = documentRepository.GetAllDocuments();

            //Ukoliko nismo pronašli ni jednu prijavu vratiti status 204 (NoContent)
            if (documents == null || documents.Count == 0)
            {
                return NoContent();
            }

            //Ukoliko smo našli neke prijava vratiti status 200 i listu pronađenih prijava (DTO objekti)
            return Ok(mapper.Map<List<DocumentDto>>(documents));
        }

        [HttpGet("{documentID}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<DocumentDto> GetDocumentById(Guid documentID) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            Document document = documentRepository.GetDocumentById(documentID);

            if (document == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DocumentDto>(document));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentConfirmationDto> CreateDocument([FromBody] DocumentCreationDto documentDto)
        {
            try
            {
                Document documentEntity = mapper.Map<Document>(documentDto);
                DocumentConfirmation confirmation = documentRepository.CreateDocument(documentEntity);
                documentRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetDocumentById", "Document", new { documentID = confirmation.DocumentId });

                return Ok();
                //return CreatedAtRoute(location, mapper.Map<DocumentConfirmationDto>(confirmation));
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
        public ActionResult<DocumentDto> UpdateExamRegistration(DocumentUpdateDto documentDto)
        {
            try
            {
                var oldDocument = documentRepository.GetDocumentById(documentDto.DocumentId);
                if (oldDocument == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Document documentEntity = mapper.Map<Document>(documentDto);

                mapper.Map(documentEntity, oldDocument); //Update objekta koji treba da sačuvamo u bazi                

                documentRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<DocumentDto>(oldDocument));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{documentID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteExamRegistration(Guid documentID)
        {
            try
            {
                var document = documentRepository.GetDocumentById(documentID);

                if (document == null)
                {
                    return NotFound();
                }

                documentRepository.DeleteDocument(documentID);
                documentRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
    }
}
