using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalTranslateProcesos.Core.Model.Traduccion
{
    public class Translation
    {
        /// <summary>
        /// El texto ya traducido
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Idioma del texto
        /// </summary>
        public string To { get; set; }
    }
}
