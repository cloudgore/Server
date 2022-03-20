using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraSApplication.DB
{
    public class UserPrioirity
    {

        public int ID { get; set; }

        public byte PriorityID { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
