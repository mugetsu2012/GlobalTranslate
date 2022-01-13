using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using GlobalTranslateProcesos.Core.Model.Extraccion;
using Newtonsoft.Json;

namespace GlobalTranslateProcesos.Servicios
{
    public class ExtractorService
    {
        private readonly string _llave;
        private readonly string _endpoint;

        public ExtractorService(string llave, string endpoint)
        {
            _llave = llave;
            _endpoint = endpoint;
        }

        public async Task<ObjetoExtracionTexto> RealizarExtraccion(byte[] datosBinarios)
        {
            //Declaramos un nuevo cliente, el que hace las peticiones http
            HttpClient cliente = new HttpClient();
            cliente.Timeout = TimeSpan.FromMinutes(30);

            //Creamos un nuevo objeto para alcenar nuestro queryString
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            //Agregamos los headers, agregamos la llave
            cliente.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _llave);

            //Agregamos los parametros
            queryString["language"] = "unk"; //Seteamos el valor unk para que lea desde cualquier lenguaje
            queryString["detectOrientation "] = "true"; //Para que lea documentos pandos

            string uri = _endpoint + "/ocr?" + queryString; //Creamos la Uri a la cual vamos a postear

            HttpResponseMessage response;

            using (ByteArrayContent content = new ByteArrayContent(datosBinarios))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await cliente.PostAsync(uri, content);
            }

            response.EnsureSuccessStatusCode(); //Va a explotar si no fue exitoso el proceso

            //Extraemos el string
            string responseBody = await response.Content.ReadAsStringAsync();

            //Mandamos a contruir el objeto
            ObjetoExtracionTexto objetoRetorno = JsonConvert.DeserializeObject<ObjetoExtracionTexto>(responseBody);

            return objetoRetorno;
        }
    }
}
