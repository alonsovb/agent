using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using Agent.Objects;

namespace Agent.Model
{
    /// <summary>
    /// Esta clase comunica la aplicación con la base de datos.
    /// </summary>
    public class DBHelper
    {
        /// <summary>
        /// Conexión a la BD SQLServer.
        /// </summary>
        SqlConnection conn;

        /// <summary>
        /// Inicia una nueva instancia, dado la cadena de conexión.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión</param>
        public DBHelper(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }

        #region Usuarios

        /// <summary>
        /// Autentifica si un usuario registrado en la BD. Indica el ID de usuario si se logró autentificar, -1 de otra forma.
        /// </summary>
        /// <param name="email">Email del usuario.</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>ID del usuario si los credenciales son correctos. -1 si no.</returns>
        public int Authenticate(string email, string password)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select [dbo].[fAuthenticate] ('" + email + "', '" + password + "')", conn);
            command.CommandType = CommandType.Text;
            int result = (int)command.ExecuteScalar();
            conn.Close();

            return result;
        }

        /// <summary>
        /// Obtiene un usuario a partir de su ID.
        /// </summary>
        /// <param name="id">ID del usuario a consultar.</param>
        /// <returns>Usuario encontrado o null</returns>
        public AUser GetUser(int id)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from [dbo].[fGetUser] (" + id + ")", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable("user");
            adapter.Fill(table);
            conn.Close();
            if (table.Rows.Count > 0)
            {
                int rowid = (int)table.Rows[0]["ID"];
                string email = table.Rows[0]["Email"].ToString();
                string password = table.Rows[0]["Password"].ToString();
                string name = table.Rows[0]["Name"].ToString();
                AUser result = new AUser(email, password);
                result.Name = name;
                result.ID = rowid;
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Registra un usuario en la BD.
        /// </summary>
        /// <param name="email">Email del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <param name="name">Nombre del usuario.</param>
        public void RegisterUser(string email, string password, string name)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spRegisterUser", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@UserEmail", SqlDbType.VarChar).Value = email;
            command.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = password;
            command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = name;
            command.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// Actualiza el nombre de un usuario.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <param name="name">Nuevo nombre.</param>
        public void UpdateName(int id, string name)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spUpdateName", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = id;
            command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = name;
            command.ExecuteNonQuery();
            conn.Close();
        }

        #endregion

        #region Proyectos

        public List<AProject> GetProjects(int id)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from [dbo].[fGetProjects] (" + id + ")", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable("projects");
            adapter.Fill(table);

            List<AProject> result = new List<AProject>();

            foreach (DataRow row in table.Rows)
            {
                int rowid = (int)row["ID"];
                string rowtitle = row["Title"].ToString();
                result.Add(new AProject(rowid, rowtitle));
            }

            conn.Close();

            return result;
        }

        public AProject GetProject(int id)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from [dbo].[fGetProject] (" + id + ")", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable("project");
            adapter.Fill(table);
            conn.Close();
            if (table.Rows.Count > 0)
            {
                int rowid = (int)table.Rows[0]["ID"];
                string rowtitle = table.Rows[0]["Title"].ToString();
                AProject result = new AProject(rowid, rowtitle);
                return result;
            }
            else
            {
                return null;
            }
        }

        public int RegisterProject(int userId, string title)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spRegisterProject", conn);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
            command.Parameters.Add("@ProjectTitle", SqlDbType.VarChar).Value = title;

            command.Parameters.Add("@NewID", SqlDbType.Int).Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            int newId = int.Parse(command.Parameters["@NewID"].Value.ToString());
            conn.Close();

            return newId;
        }

        public void UpdateProjectTitle(int projectId, string title)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spUpdateProjectTitle", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectId;
            command.Parameters.Add("@ProjectTitle", SqlDbType.Text).Value = title;
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteProject(int projectId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spDeleteProject", conn);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectId;

            command.ExecuteNonQuery();

            conn.Close();
        }

        #endregion

        #region Actividades

        public List<AActivity> GetActivities(int projectId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from [dbo].[fGetActivities] (" + projectId + ")", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable("activities");
            adapter.Fill(table);

            List<AActivity> result = new List<AActivity>();

            foreach (DataRow row in table.Rows)
            {
                int rowid = (int)row["ID"];
                string rowtitle = row["Title"].ToString();
                DateTime due = (DateTime)row["DueDate"];
                DateTime created = (DateTime)row["Created"];
                DateTime reminder = (DateTime)row["Reminder"];
                byte priority = Convert.ToByte(row["Priority"]);
                bool completed = (bool)row["Completed"];
                result.Add(new AActivity(rowid, rowtitle, due, reminder, priority, completed) { Created = created });
            }

            conn.Close();

            return result;
        }

        public List<AActivity> GetAllActivities(int userId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from [dbo].[fGetAllActivities] (" + userId + ")", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable("activities");
            adapter.Fill(table);

            List<AActivity> result = new List<AActivity>();

            foreach (DataRow row in table.Rows)
            {
                int rowid = (int)row["ID"];
                string rowtitle = row["Title"].ToString();
                DateTime created = (DateTime)row["Created"];
                DateTime reminder = (DateTime)row["Reminder"];
                DateTime due = (DateTime)row["DueDate"];
                byte priority = Convert.ToByte(row["Priority"]);
                bool completed = (bool)row["Completed"];
                result.Add(new AActivity(rowid, rowtitle, due, reminder, priority, completed)
                {
                    ProjectID = (int)row["ProjectID"],
                    ProjectTitle = row["ProjectTitle"].ToString(),
                    Created = created
                });
            }

            return result;
        }

        public AActivity GetActivity(int id)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from [dbo].[fGetActivity] (" + id + ")", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable("activity");
            adapter.Fill(table);
            conn.Close();
            if (table.Rows.Count > 0)
            {
                int rowid = (int)table.Rows[0]["ID"];
                string rowtitle = table.Rows[0]["Title"].ToString();
                DateTime due = (DateTime)table.Rows[0]["DueDate"];
                DateTime created = (DateTime)table.Rows[0]["Created"];
                DateTime reminder = (DateTime)table.Rows[0]["Reminder"];
                short priority = (short)table.Rows[0]["Priority"];
                bool completed = (bool)table.Rows[0]["Completed"];
                AActivity result = new AActivity(rowid, rowtitle, due, reminder, byte.Parse(priority.ToString()), completed) { Created = created };
                return result;
            }
            else
            {
                return null;
            }
        }

        public List<AActivity> GetChildActivities(int activityId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("select * from [dbo].[fGetChildActivities] (" + activityId + ")", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable("activities");
            adapter.Fill(table);

            List<AActivity> result = new List<AActivity>();

            foreach (DataRow row in table.Rows)
            {
                int rowid = (int)row["ID"];
                string rowtitle = row["Title"].ToString();
                DateTime created = (DateTime)row["Created"];
                DateTime reminder = (DateTime)row["Reminder"];
                byte priority = (byte)row["Priority"];
                bool completed = (bool)row["Completed"];
                result.Add(new AActivity(rowid, rowtitle, created, reminder, priority, completed));
            }

            conn.Close();

            return result;
        }

        public int RegisterActivity(int projectID, string title, DateTime due, int parentActivity, byte priority, DateTime dateReminder, bool completed)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spRegisterActivity", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectID;
            command.Parameters.Add("@ActivityTitle", SqlDbType.VarChar).Value = title;
            command.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = due;
            command.Parameters.Add("@ParentActivity", SqlDbType.Int).Value = parentActivity;
            command.Parameters.Add("@Priority", SqlDbType.SmallInt).Value = priority;
            command.Parameters.Add("@DateReminder", SqlDbType.DateTime).Value = dateReminder;
            command.Parameters.Add("@Completed", SqlDbType.Bit).Value = completed;
            command.Parameters.Add("@NewID", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();

            int newId = int.Parse(command.Parameters["@NewID"].Value.ToString());
            conn.Close();

            return newId;
        }

        public void UpdateCompleted(int activityId, bool completed)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spUpdateCompleted", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ActivityID", SqlDbType.Int).Value = activityId;
            command.Parameters.Add("@Completed", SqlDbType.Bit).Value = completed;
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdatePriority(int activityId, byte priority)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spUpdatePriority", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ActivityID", SqlDbType.Int).Value = activityId;
            command.Parameters.Add("@Priority", SqlDbType.SmallInt).Value = priority;
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateReminder(int activityId, DateTime reminder)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spUpdateReminder", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ActivityID", SqlDbType.Int).Value = activityId;
            command.Parameters.Add("@DateReminder", SqlDbType.DateTime).Value = reminder;
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteActivity(int activityId)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("spDeleteActivity", conn);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ActivityID", SqlDbType.Int).Value = activityId;

            command.ExecuteNonQuery();

            conn.Close();
        }

        #endregion
    }
}