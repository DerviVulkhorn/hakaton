using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class Status
    {
        public Status()
        {

        }
        public Status(int idStatus, string nameStatus)
        {
            this.idStatus = idStatus;
            this.nameStatus = nameStatus;
        }

        public int idStatus { get; set; }
        public string nameStatus { get; set; }
    }
}
