using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class Product
    {
        public Product()
        {

        }
        public Product(int idProduct, string nameProduct, int idSensor)
        {
            this.idProduct = idProduct;
            this.nameProduct = nameProduct;
            this.idSensor = idSensor;
        }

        public int idProduct { get; set; }
        public string nameProduct { get; set; }
        public int idSensor { get; set; }

    }
}
