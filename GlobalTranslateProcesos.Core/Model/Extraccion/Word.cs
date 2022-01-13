namespace GlobalTranslateProcesos.Core.Model.Extraccion
{
    /// <summary>
    /// Representa una palabra desde el proceso de extraccion de texto
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Indica las regiones de la caja que rodean a este elemento
        /// </summary>
        public string BoundingBox { get; set; }

        /// <summary>
        /// El texto leido desde Azure
        /// </summary>
        public string Text { get; set; }
    }

}
