using AutoMapper;
using DocumentMsProject.Data;
using DocumentMsProject.Entities;
using DocumentMsProject.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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

        /// <summary>
        /// Vraća listu svih dokumenata u vezi sa javnim nadmetanjem
        /// </summary>
        /// <returns>Lista dokumenata u vezi sa javnim nadmetanjem</returns>
        /// <response code="200">Vraća listu dokumenata</response>
        /// <response code="404">Nije pronađen ni jedan jedini dokument</response>
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

        /// <summary>
        /// Vraća dokument u vezi sa javnim nadmetanjem na osnovu ID
        /// </summary>
        /// <param name="documentID">ID dokumenta</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženi dokument javnog nadmetanja</response>
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

        /// <summary>
        /// Kreira novi dokument.
        /// </summary>
        /// <param name="documentDto">Model dokumenta</param>
        /// <returns>Potvrdu o kreiranom dokumentu.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog dokumenta \
        /// POST /api/document \
        /// {     \
        ///     "documentSerialNumber": 0, \
        ///     "documentName": "naziv", \
        ///     "documentDate": ""2012-11-15T10:30:00" \", \
        ///     "documentSubmissionDate": ""2020-11-15T10:30:00" \", \
        ///     "listOfDocumentsID": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreirani dokument</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja dokumenta</response>
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

        /// <summary>
        /// Ažurira jedan dokument.
        /// </summary>
        /// <param name="documentDto">Model dokumenta za javno nadmetanje koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom dokumetu.</returns>
        /// <response code="200">Vraća ažurirani dokument</response>
        /// <response code="400">Dokument za javno nadmetanje koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja dokumenta za javno nadmetanje</response>
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

        /// <summary>
        /// Vrši brisanje jednog dokumenta na osnovu ID-ja dokumenta.
        /// </summary>
        /// <param name="documentID">ID dokumenta</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Dokument uspešno obrisan</response>
        /// <response code="404">Nije pronađen dokument za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja dokumenta</response>
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

        /// <summary>
        /// Vraća opcije za rad sa dokumentima
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetDocumentOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
