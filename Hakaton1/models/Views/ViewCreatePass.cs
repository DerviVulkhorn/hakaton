using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.models.Views
{
    public class ViewCreatePass
    {
        public ViewCreatePass()
        {

        }

        public ViewCreatePass(int idOrderStatus, string numberPass, DateTime dateStart, DateTime dateEnd, string numberCar, string nameModel, string nameCompany, string fIO)
        {
            this.idOrderStatus = idOrderStatus;
            this.numberPass = numberPass;
            this.dateStart = dateStart;
            this.dateEnd = dateEnd;
            this.numberCar = numberCar;
            this.nameModel = nameModel;
            this.nameCompany = nameCompany;
            FIO = fIO;
        }

        public int idOrderStatus { get; set; }
        public string numberPass { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public string numberCar { get; set; }
        public string nameModel { get; set; }
        public string nameCompany { get; set; }
        public string FIO { get; set; }
    }
}
