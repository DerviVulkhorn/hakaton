using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class StatusOrder
    {
        public StatusOrder()
        {

        }
        public StatusOrder(int idStatusOrder, int idStatus, int idOrder, DateTime dateStart, DateTime dateEnd)
        {
            this.idStatusOrder = idStatusOrder;
            this.idStatus = idStatus;
            this.idOrder = idOrder;
            this.dateStart = dateStart;
            this.dateEnd = dateEnd;
        }

        public int idStatusOrder { get; set; }
        public int idStatus { get; set; }
        public int idOrder { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
    }
}
