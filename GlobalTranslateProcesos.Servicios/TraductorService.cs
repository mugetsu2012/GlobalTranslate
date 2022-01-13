using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GlobalTranslateProcesos.Core.Model.Deteccion;
using GlobalTranslateProcesos.Core.Model.Traduccion;
using Newtonsoft.Json;

namespace GlobalTranslateProcesos.Servicios
{
    public class TraductorService
    {
        private readonly string _hostTraduccion;
        private readonly string _llave;
        private readonly string _pathTraduccion;
        private readonly string _pathDeteccion;

        public TraductorService(string hostTraduccion, string pathTraduccion, string pathDeteccion, string llave)
        {
            _hostTraduccion = hostTraduccion;
            _pathDeteccion = pathDeteccion;
            _pathTraduccion = pathTraduccion;
            _llave = llave;
        }



        public async Task<ObjetoTraduccion> RealizarTraduccion(string texto, string idiomaObjetivo)
        {
            string uri = _hostTraduccion + _pathTraduccion + "&to=" + idiomaObjetivo;
            System.Object[] body = new System.Object[] { new { Text = texto } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                client.Timeout = TimeSpan.FromMinutes(30);
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", _llave);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                ObjetoTraduccion[] objetoTraduccion = JsonConvert.DeserializeObject<ObjetoTraduccion[]>(responseBody);
                return objetoTraduccion.First();

            }
        }

        public async Task<ObjetoDeteccion> DetectarLenguaje(string texto)
        {
            System.Object body = new System.Object[] {new {Text = texto}};

            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(_hostTraduccion + _pathDeteccion );
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", _llave);

                    var response = await client.SendAsync(request);

                    var responseBody = await response.Content.ReadAsStringAsync();
                    ObjetoDeteccion[] objetoDeteccion = JsonConvert.DeserializeObject<ObjetoDeteccion[]>(responseBody);
                    return objetoDeteccion.First();
                }
            }
        }
    }
}
