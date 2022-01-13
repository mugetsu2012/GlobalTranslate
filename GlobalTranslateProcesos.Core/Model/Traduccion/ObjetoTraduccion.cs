using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalTranslateProcesos.Core.Model.Traduccion
{
    public class ObjetoTraduccion
    {
        /// <summary>
        /// Lenaguaje detectado
        /// </summary>
        public DetectedLanguage DetectedLanguage { get; set; }

        /// <summary>
        /// Traducciones encontradas
        /// </summary>
        public List<Translation> Translations { get; set; }
    }
}
