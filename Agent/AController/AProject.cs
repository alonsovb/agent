using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agent.Objects
{
    /// <summary>
    /// Representa un proyecto.
    /// </summary>
    public class AProject
    {
        /// <summary>
        /// Título del proyecto.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Fecha de creación del proyecto.
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Identificador del proyecto.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Crea un nuevo proyecto a partir de los datos dados.
        /// </summary>
        /// <param name="id">Identificador del proyecto.</param>
        /// <param name="title">Título del proyecto.</param>
        public AProject(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}