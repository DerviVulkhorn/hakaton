using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class Companies
    {
        public Companies()
        {

        }
        public Companies(int idCompany, string nameCompany)
        {
            this.idCompany = idCompany;
            this.nameCompany = nameCompany;
        }

        public int idCompany { get; set; }
        public string nameCompany { get; set; }
    }
}
