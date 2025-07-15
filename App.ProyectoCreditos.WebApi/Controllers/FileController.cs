using App.Service.Implementacion.Request;
using App.Service.Implementacion.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.ProyectoCreditos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        public FileController()
        {
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        /// <summary>
        /// EndPoint para subir un archivo
        /// </summary>
        /// <param name="request">información de archivo</param>
        /// <returns></returns>
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<GeneralResponseDTO>> UploadFile([FromForm] FileUploadDTO request)
        {
            if (request.File == null || request.File.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo valido.");
            }

            var filePath = Path.Combine(_uploadPath, request.File.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }
            GeneralResponseDTO result = new GeneralResponseDTO();

            result.IsSuccess = true;
            result.Message = "Archivo subido correctamente.";
            result.Title = "Exito";
            result.TipoAlerta = TipoAlerta.Informativo;
            return Ok(result);
        }
    }
}
