using IntegraSApplication.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraSApplication.DB.Entitys
{
    public class Order
    {
        public int ID { get; set; }
        public virtual Service Service { get; set; }
        public int ServiceID { get; set; }
        public virtual User User { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
