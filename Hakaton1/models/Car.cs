using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class Car
    {
        public Car()
        {

        }
        public Car(int idCar, string number, int idModel)
        {
            this.idCar = idCar;
            this.number = number;
            this.idModel = idModel;
        }

        public int idCar { get; set; }
        public string number { get; set; }
        public int idModel { get; set; }
    }
}
