using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalTranslateProcesos.Core.Model;
using GlobalTranslateProcesos.Core.Model.Deteccion;
using GlobalTranslateProcesos.Core.Model.Traduccion;
using GlobalTranslateProcesos.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTranslateProcesos.Controllers
{
    [Route("api/[controller]")]
    public class TraductorController : Controller
    {
        private readonly TraductorService _traductorService;

        public TraductorController(TraductorService traductorService)
        {
            _traductorService = traductorService;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> Traducir([FromBody]string texto, Idiomas idiomaObjetivo = Idiomas.Spanish)
        {
            if (texto.Length > 5000)
            {
                ModelState.AddModelError(string.Empty,"No puedes introducir mas de 5000 caracteres");
                return BadRequest(ModelState);
            }

            string textoIdiomaObj;

            switch (idiomaObjetivo)
            {
                case Idiomas.Spanish:
                    textoIdiomaObj = "es";
                    break;
                case Idiomas.English:
                    textoIdiomaObj = "en";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(idiomaObjetivo), idiomaObjetivo, null);
            }

            //ObjetoDeteccion detecc = await _traductorService.DetectarLenguaje(texto);
            ObjetoTraduccion respuesta = await _traductorService.RealizarTraduccion(texto, textoIdiomaObj);

            string textoTraducido = respuesta.Translations.Any()
                ? respuesta.Translations.First().Text
                : "No se pudo traducir el texto :(";

            return Ok(textoTraducido);
        }
    }
}