using System.IO;
using System.Threading.Tasks;
using GlobalTranslateProcesos.Core.Model.Extraccion;
using GlobalTranslateProcesos.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTranslateProcesos.Controllers
{
    [Route("api/[controller]")]
    public class ExtractorController : Controller
    {
        private readonly ExtractorService _extractorService;

        public ExtractorController(ExtractorService extractorService)
        {
            _extractorService = extractorService;
        }

        /// <summary>
        /// La imagen debe cumplir las siguientes condiciones:Debe ser tipo png, jpg o bmp,La resolucion debe estar entre 50x50 y 4200x4200 y No debe pesar mas de 4Mb
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> Extraer(IFormFile archivo)
        {

            //Validaciones
            if (archivo.ContentType != "image/png" && archivo.ContentType != "image/jpeg" && archivo.ContentType != "image/bmp")
            {
                ModelState.AddModelError(string.Empty, "Solo se adminten archivos de tipo png,jpge y bmp");
                return BadRequest(ModelState);
            }

            if (archivo.Length > 4000000)
            {
                ModelState.AddModelError(string.Empty, "No puedes agregar imagenes que pesen mas de 4Mb");
                return BadRequest(ModelState);
            }

            ObjetoExtracionTexto respuesta;
            using (var ms = new MemoryStream())
            {
                archivo.CopyTo(ms);
                byte[] contenido = ms.ToArray();
                respuesta = await _extractorService.RealizarExtraccion(contenido);
            }
            
            return Ok(respuesta.GetTextoPlano());
        }
    }
}