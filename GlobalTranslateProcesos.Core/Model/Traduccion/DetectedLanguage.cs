using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalTranslateProcesos.Core.Model.Traduccion
{
    public class DetectedLanguage
    {
        /// <summary>
        /// Indica el lenguaje detectado
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Indica que tan seguro esta del lenguaje que detecto
        /// </summary>
        public double Score { get; set; }
    }
}
