using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class ModelCar
    {
        public ModelCar()
        {

        }
        public ModelCar(int idModel, string nameModel, decimal tonnage)
        {
            this.idModel = idModel;
            this.nameModel = nameModel;
            this.tonnage = tonnage;
        }

        public int idModel { get; set; }
        public string nameModel { get; set; }
        public decimal tonnage { get; set; }
    }
}
