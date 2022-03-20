using IntegraSApplication.Pages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntegraSApplication.DB.Entitys
{
    public  class Category
    {
        public int ID { get; set; }
        [Required,MaxLength(64)]
        public string Name { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
