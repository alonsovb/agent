using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agent.Objects
{
    /// <summary>
    /// Representa una actividad y sus propiedades.
    /// </summary>
    public class AActivity
    {
        /// <summary>
        /// ID de la actividad.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Título de la actividad.
        /// </summary>
        public string Title {get; set;}
        /// <summary>
        /// Fecha en que se debe realizar la actividad.
        /// </summary>
        public Nullable<DateTime> Date { get; set; }
        /// <summary>
        /// Fecha en que se creó esta actividad.
        /// </summary>
        public Nullable<DateTime> Created { get; set; }
        /// <summary>
        /// Fecha y hora de recordatorio de esta actividad.
        /// </summary>
        public Nullable<DateTime> Reminder { get; set; }
        /// <summary>
        /// Prioridad de la actividad.
        /// </summary>
        public byte Priority { get; set; }
        /// <summary>
        /// Indica si la actividad ya está completada o no.
        /// </summary>
        public bool Completed { get; set; }
        /// <summary>
        /// Indica el identificador de la actividad padre de esta.
        /// </summary>
        public int Parent { get; set; }
        /// <summary>
        /// Indica el identificador del proyecto al que pertenece.
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// Título del proyecto al que pertenece esta actividad.
        /// </summary>
        public string ProjectTitle { get; set; }

        /// <summary>
        /// Crea una nueva actividad a partir de los datos dados.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <param name="title">Título de la actividad.</param>
        /// <param name="date">Fecha de la actividad.</param>
        /// <param name="reminder">Recordatorio de la actividad.</param>
        /// <param name="priority">Prioridad de la actividad.</param>
        /// <param name="completed">Indica si está completada o no.</param>
        public AActivity(int id, string title, DateTime date, DateTime reminder, byte priority, bool completed)
        {
            ID = id;
            Title = title;
            Date = date;
            Reminder = reminder;
            Priority = priority;
            Completed = completed;
        }

    }

}