using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agent.Objects
{
    /// <summary>
    /// Representa un usuario del sistema.
    /// </summary>
    public class AUser
    {
        /// <summary>
        /// Email del usuario.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Nombre de usuario.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Crea un nuevo usuario a partir de los datos dados.
        /// </summary>
        /// <param name="email">Email del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        public AUser(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}