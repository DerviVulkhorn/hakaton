using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class Roles
    {
        public Roles()
        {

        }
        public Roles(int idRole, string nameRole)
        {
            this.idRole = idRole;
            this.nameRole = nameRole;
        }

        public int idRole { get; set; }
        public string nameRole { get; set; }
    }
}
