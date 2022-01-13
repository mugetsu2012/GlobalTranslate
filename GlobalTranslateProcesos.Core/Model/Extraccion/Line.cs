using System.Collections.Generic;

namespace GlobalTranslateProcesos.Core.Model.Extraccion
{
    /// <summary>
    /// Objeto que representa una linea de una imagen a leer por el proceso de extraccion de texto
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Indica los puntos de la caja para esta linea
        /// </summary>
        public string BoundingBox { get; set; }

        /// <summary>
        /// Lista de palabras para esta linea
        /// </summary>
        public List<Word> Words { get; set; }
    }
}
