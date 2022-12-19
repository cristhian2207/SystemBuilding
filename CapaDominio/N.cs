using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad.Cache;

namespace CapaDominio
{
    public class N
    {
        D Datos = new D();

        //Atributos de usuario
        private int idUser;
        private string username;
        private string password;
        private string firstName;
        private string lastName;
        private string usertype;
        private string email;

        //Atributos de visitante
        private string name;
        private string last;
        private string career;
        private string mail;
        private DateTime entrytime;
        private DateTime outtime;
        private string reason;
        private int building;
        private int place;


        public N(int idUser, string username, string password, string firstName, string lastName, string usertype, string email)
        {
            this.idUser = idUser;
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.usertype = usertype;
            this.email = email;
        }

        public N(string username, string password, string firstName, string lastName, string usertype, string email)
        {
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.usertype = usertype;
            this.email = email;
        }
        public N()
        {

        }

        public N(string name, string last, string career, string mail, DateTime entrytime, DateTime outtime, string reason, int building, int place)
        {
            this.name = name;
            this.last = last;
            this.career = career;
            this.mail = mail;
            this.entrytime = entrytime;
            this.outtime = outtime;
            this.reason = reason;
            this.building = building;
            this.place = place;
        }

        public int IdUser { get => idUser; set => idUser = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Usertype { get => usertype; set => usertype = value; }
        public string Email { get => email; set => email = value; }

        public string editUserProfile() {
            try
            {
                Datos.editUser(IdUser, Username, Password, FirstName, LastName, Usertype, Email);
                LoginUser(Username, Password);
                return "Your profile has been updated succesfully";
            }
            catch (Exception ex)
            {
                return "Username is already registered, try another one" + ex;
            }
        }

        public string insertUserProfile() {
            try
            {
                Datos.insertUser(Username, Password, FirstName, LastName, Usertype, Email);
                LoginUser(Username, Password);
                return "New user added!";
            }
            catch (Exception ex)
            {

                return "New user couldn't be added" + ex;
            }
        }

        public string insertVisitor()
        {
            try
            {
                Datos.insertVisitor(name,last,career,mail,entrytime,outtime,reason,building,place);
                return "New visitor added!";
            }
            catch (Exception ex)
            {
                return "The visitor couldn't be added" + ex;
            }
        }

        public bool LoginUser(string user, string pass)
        {
            return Datos.Login(user, pass);
        }

        public void Autenticacion()
        {
            if (UserLoginCache.Position == Users.Administrator)
            {

            }
            if (UserLoginCache.Position == Users.General)
            {

            }
        }
    }


}
