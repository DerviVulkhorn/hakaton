using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class CarUser
    {
        public CarUser()
        {

        }
        public CarUser(int idCarUser, int idCar, int idUser)
        {
            this.idCarUser = idCarUser;
            this.idCar = idCar;
            this.idUser = idUser;
        }

        public int idCarUser { get; set; }
        public int idCar { get; set; }
        public int idUser { get; set; }
    }
}
