using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CapaEntidad.Cache;

namespace CapaDatos
{
    public class D
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        //METODO PARA INSERTAR VISITANTE
        public void insertVisitor(string firstName, string lastName, string career, string email, DateTime entrytime, DateTime outtime,
            string reason, int building, int place)
        {
            conexion.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = conexion;
                command.CommandText = "SP_INSERTARVISITANTES";
                command.Parameters.AddWithValue("@Nombre", firstName);
                command.Parameters.AddWithValue("@Apellido", lastName);
                command.Parameters.AddWithValue("@Carrera", career);
                command.Parameters.AddWithValue("@Correo", email);
                command.Parameters.AddWithValue("@Hora_Entrada", entrytime);
                command.Parameters.AddWithValue("@Hora_Salida", outtime);
                command.Parameters.AddWithValue("@Motivo_visita", reason);
                command.Parameters.AddWithValue("@Edificio", building);
                command.Parameters.AddWithValue("@Lugar", place);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteReader();
            }
            conexion.Close();
        }

        //METODO PARA EDITAR USUARIO
        public void editUser(int id, string userName, string password, string name, string lastname,  string usertype, string email){
            conexion.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = conexion;
                command.CommandText = "SP_EDITARUSUARIOS";
                command.Parameters.AddWithValue("@ID_USUARIOS", id);
                command.Parameters.AddWithValue("@Username", userName);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@FirstName", name);
                command.Parameters.AddWithValue("@LastName", lastname);
                command.Parameters.AddWithValue("@UserType", usertype);
                command.Parameters.AddWithValue("@Email", email);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteReader();
            }
            conexion.Close();
        }

        //METODO PARA INSERTAR USUARIO
        public void insertUser(string userName, string password, string name, string lastname, string usertype, string email)
        {
            conexion.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = conexion;
                command.CommandText = "SP_INSERTARUSUARIOS";
                command.Parameters.AddWithValue("@Username", userName);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@FirstName", name);
                command.Parameters.AddWithValue("@LastName", lastname);
                command.Parameters.AddWithValue("@UserType", usertype);
                command.Parameters.AddWithValue("@Email", email);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteReader();
            }
            conexion.Close();
        }

        //METODO PARA EL LOGIN
        public bool Login(string user, string pass)
        {
            conexion.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = conexion;
                command.CommandText = "select * from Users where Username=@user and Password=@pass";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@pass", pass);
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserLoginCache.IdUser = reader.GetInt32(0);
                        UserLoginCache.Username = reader.GetString(1);
                        UserLoginCache.Password = reader.GetString(2);
                        UserLoginCache.FirstName = reader.GetString(3);
                        UserLoginCache.LastName = reader.GetString(4);
                        UserLoginCache.Position = reader.GetString(5);
                        UserLoginCache.Email = reader.GetString(6);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #region ListarUsuarios
        //public List<UserLoginCache> ListarUsuarios(string buscar)
        //{
        //    SqlDataReader LeerFilas;
        //    SqlCommand cmd = new SqlCommand("SP_MOSTRARUSUARIOS", conexion);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    conexion.Open();

        //    cmd.Parameters.AddWithValue("@BUSCAR", buscar);
        //    LeerFilas = cmd.ExecuteReader();

        //    List<UserLoginCache> Listar = new List<UserLoginCache>();
        //    while (LeerFilas.Read())
        //    {
        //        Listar.Add(new UserLoginCache
        //        {
        //            IdUser = LeerFilas.GetInt32(0),
        //            FirstName = LeerFilas.GetString(1),
        //            LastName = LeerFilas.GetString(2),
        //            Position = LeerFilas.GetString(3),
        //            Email = LeerFilas.GetString(4)
        //        });
        //    }
        //    conexion.Close();
        //    LeerFilas.Close();
        //    return Listar;
        //}
        #endregion

    }
}
