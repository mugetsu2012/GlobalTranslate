using System.Collections.Generic;

namespace GlobalTranslateProcesos.Core.Model.Extraccion
{
    /// <summary>
    /// Objeto root de la extraccion del texto
    /// </summary>
    public class ObjetoExtracionTexto
    {
        /// <summary>
        /// Indica el lenguaje detectado
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Indica la orientacion del archivo
        /// </summary>
        public string Orientation { get; set; }

        /// <summary>
        /// Indica el angulo de orientacion
        /// </summary>
        public double TextAngle { get; set; }

        /// <summary>
        /// Lista todas las regiones encontradas
        /// </summary>
        public List<Region> Regions { get; set; }

        public string GetTextoPlano()
        {
            string texto = string.Empty;
            foreach (Region region in Regions)
            {
                foreach (Line line in region.Lines)
                {
                    foreach (Word word in line.Words)
                    {
                        texto = texto + word.Text + " ";
                    }
                }
            }

            return texto;
        }
    }
}
