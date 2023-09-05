using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class Order
    {
        public Order()
        {

        }
        public Order(int idOrder, string numberPass, int idCompany, int idCarUser, DateTime dateStart, DateTime dateEnd, bool isFinished)
        {
            this.idOrder = idOrder;
            this.numberPass = numberPass;
            this.idCompany = idCompany;
            this.idCarUser = idCarUser;
            this.dateStart = dateStart;
            this.dateEnd = dateEnd;
            this.isFinished = isFinished;
        }

        public int idOrder { get; set; }
        public string numberPass { get; set; }
        public int idCompany { get; set; }
        public int idCarUser { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public bool isFinished { get; set; }
    }
}
