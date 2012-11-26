using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Objects
{
    /// <summary>
    /// Representa una imagen y su nombre original.
    /// </summary>
    public class AImage
    {
        /// <summary>
        /// Identificador de la imagen.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Bytes del archivo de imagen.
        /// </summary>
        public byte[] FileData { get; set; }
        /// <summary>
        /// Nombre de archivo de la imagen.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Identificador de la actividad a la que pertenece esta imagen.
        /// </summary>
        public int ActivityID { get; set; }
    }
}
