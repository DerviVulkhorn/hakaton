using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models.Views
{
    public class ViewOrderList
    {
        public ViewOrderList(string number_pass, DateTime date_start, DateTime date_end, string name_company, string fio, string name_model, string name_product, decimal weight, string name_type_weight, bool is_finished)
        {
            this.number_pass = number_pass;
            this.date_start = date_start;
            this.date_end = date_end;
            this.name_company = name_company;
            this.fio = fio;
            this.name_model = name_model;
            this.name_product = name_product;
            this.weight = weight;
            this.name_type_weight = name_type_weight;
            this.is_finished = is_finished;
        }

        public ViewOrderList()
        {

        }

        public int id { get; set; }
        public string number_pass { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }

        public string name_company { get; set; }
        public string fio { get; set; }
        public string name_model { get; set; }
        public string name_product { get; set; }
        public decimal weight { get; set; }

        public string name_type_weight { get; set; }
        public Boolean is_finished { get; set; }
    }
}
