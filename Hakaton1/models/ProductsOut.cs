using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class ProductsOut
    {
        public ProductsOut()
        {

        }
        public ProductsOut(int idProduct, string nameProduct, decimal weight, int idType, string nameType)
        {
            this.idProduct = idProduct;
            this.nameProduct = nameProduct;
            this.weight = weight;
            this.idType = idType;
            this.nameType = nameType;
        }

        public int idProduct { get; set; }
        public string nameProduct { get; set; }
        public decimal weight { get; set; }
        public int idType { get; set; }
        public string nameType { get; set; }
    }
}
