using IntegraSApplication.DB.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntegraSApplication.Pages
{
   public class Service : INotifyPropertyChanged
    {   
        [Key]
        public int ID { get; set; }
        [Required,MaxLength(64)]
        public string NameService { get; set; }

        public decimal Cost { get; set; }
        [Required,MaxLength(100)]
        public string Description { get; set; }

        public double? Discount { get; set; }

        public bool isDicount => Discount != null;

        public decimal DicountCost
        {
            get
            {
                if (Discount != null)
                    return Cost - (Cost / 100) * (decimal)Discount;
                return Cost;
            }
        }

        public virtual Category Category { get; set; }

        public int CategoryID { get; set; }

        private byte[] _MainImage;

        public event PropertyChangedEventHandler PropertyChanged;

        public byte[] MainImage
        {
            get { return _MainImage; }
            set { _MainImage = value; ChangeProp(); }
        }

        public void ChangeProp([CallerMemberName] string str = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(str));
        }



    }
}
