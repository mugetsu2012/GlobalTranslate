using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalTranslateProcesos.Core.Model.Deteccion
{
   public class Alternative
    {
        /// <summary>
        /// Lenguaje detectado
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Indica una puntuacion entre 1 y 0 acerca de que tan seguro esta acerca de que sea el lenguaje que dijo
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Indica si soporta la traduccion a este lenguaje
        /// </summary>
        public bool IsTranslationSupported { get; set; }

        /// <summary>
        /// Indica si la transliteracion esta soportada
        /// </summary>
        public bool IsTransliterationSupported { get; set; }
    }
}
