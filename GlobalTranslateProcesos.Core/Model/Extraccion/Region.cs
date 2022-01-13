using System.Collections.Generic;

namespace GlobalTranslateProcesos.Core.Model.Extraccion
{
    /// <summary>
    /// Indica una region detectada por el proceso de extraccion de texto
    /// </summary>
    public class Region
    {
        /// <summary>
        /// Indica las proporciones de la caja de esta region
        /// </summary>
        public string BoundingBox { get; set; }

        /// <summary>
        /// Lista de lineas para esta region
        /// </summary>
        public List<Line> Lines { get; set; }
    }
}
