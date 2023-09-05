using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class Users
    {
        public Users()
        {

        }
        public Users(int idUser, string lastName, string firstName, string patronymic, string login, string password, int idRole)
        {
            this.idUser = idUser;
            this.lastName = lastName;
            this.firstName = firstName;
            this.patronymic = patronymic;
            this.login = login;
            this.password = password;
            this.idRole = idRole;
        }

        public int idUser { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string patronymic { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int idRole { get; set; }
    }
}
