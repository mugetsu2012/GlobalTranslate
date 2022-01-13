using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GlobalTranslateProcesos.Core.Model.Deteccion;
using GlobalTranslateProcesos.Core.Model.Extraccion;
using GlobalTranslateProcesos.Core.Model.Traduccion;
using GlobalTranslateProcesos.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTranslateProcesos.Controllers
{
    [Route("api/[controller]")]
    public class ExtractorTraductorController : Controller
    {
        private readonly TraductorService _traductorService;
        private readonly ExtractorService _extractorService;

        public ExtractorTraductorController(TraductorService traductorService,
            ExtractorService extractorService)
        {
            _traductorService = traductorService;
            _extractorService = extractorService;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> ExtraerTraducir(IFormFile archivo)
        {
            ObjetoExtracionTexto respuesta;
            using (var ms = new MemoryStream())
            {
                archivo.CopyTo(ms);
                byte[] contenido = ms.ToArray();
                respuesta = await _extractorService.RealizarExtraccion(contenido);
            }

            //Ahora con la respuesta, sacar el string y detectar el idioma
            string textoPlano = respuesta.GetTextoPlano();

            ObjetoDeteccion objetoDeteccion = await _traductorService.DetectarLenguaje(textoPlano);

           

            ObjetoTraduccion objetoTraduccion;

            //Si el teto esta en ingles, lo traducimos a espanniol
            if (objetoDeteccion.Language == "en")
            {
                objetoTraduccion = await _traductorService.RealizarTraduccion(textoPlano, "es");
            }
            else if (objetoDeteccion.Language == "es")
            {
                //Si el texto esta en espanyol lo traducimos a ingles
                objetoTraduccion = await _traductorService.RealizarTraduccion(textoPlano, "en");
            }
            else
            {
                throw new NotImplementedException();
            }

            string textoTraducido = objetoTraduccion.Translations.Any()
                ? objetoTraduccion.Translations.First().Text
                : "No se pudo traducir :(";

            return Ok(textoTraducido);
        }
    }
}