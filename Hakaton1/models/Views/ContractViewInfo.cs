using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models.Views
{
    public class ContractViewInfo
    {
        public ContractViewInfo(ViewOrderList view)
        {
            this.id = view.id;
            this.number_pass = view.number_pass;
            this.date_start = view.date_start;
            this.date_end = view.date_end;
            this.name_company = view.name_company;
            this.fio = view.fio;
            this.name_model = view.name_model;
            this.name_product = view.name_product;
            this.weight = view.weight;
            this.name_type_weight = view.name_type_weight;
            if(view.is_finished == true)
            {
                this.finished = "Готов";
            } 
            else if(view.is_finished == false)
            {
                this.finished = "Выполняется";
            }
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
        public string finished { get; set; }
    }
}
