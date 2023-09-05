using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_hueta.model
{
    public class ViewPathWarehouse
    {
        public int id { get; set; }
        public DateTime date_start { get; set; }
        public string name_status { get; set; }
        public string number_pass { get; set; }

        public string number_car { get; set; }
        public string model_car { get; set; }
        public string name_product { get; set; }
        public decimal weight { get; set; }
        public string FIO { get; set; }

        public ViewPathWarehouse()
        {

        }

        public ViewPathWarehouse(int id, DateTime date_start, string name_status, string number_pass, string number_car, string model_car, string name_product, decimal weight, string fIO)
        {
            this.id = id;
            this.date_start = date_start;
            this.name_status = name_status;
            this.number_pass = number_pass;
            this.number_car = number_car;
            this.model_car = model_car;
            this.name_product = name_product;
            this.weight = weight;
            FIO = fIO;
        }
    }
}