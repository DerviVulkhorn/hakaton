using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models
{
    public class TypeWeight
    {
        public TypeWeight()
        {

        }
        public TypeWeight(int idType, string nameType)
        {
            this.idType = idType;
            this.nameType = nameType;
        }

        public int idType { get; set; }
        public string nameType { get; set; }
    }
}
