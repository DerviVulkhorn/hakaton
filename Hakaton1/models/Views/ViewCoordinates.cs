using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models.Views
{
    public class ViewCoordinates
    {
        public ViewCoordinates()
        {

        }

        public ViewCoordinates(string name_product, string x, string y, string code, bool is_actual, decimal wieth, string name_type_weight)
        {
            this.name_product = name_product;
            this.x = x;
            this.y = y;
            this.code = code;
            this.is_actual = is_actual;
            this.wieth = wieth;
            this.name_type_weight = name_type_weight;
        }

        public string name_product { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string code { get; set; }
        public bool is_actual { get; set; }
        public decimal wieth { get; set; }
        public string name_type_weight { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
