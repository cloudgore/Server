using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IntegraSApplication.DB
{
    public class User : INotifyPropertyChanged
    {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(64)]
        public string Name { get; set; }

        [Required, MaxLength(64)]
        public string SurName { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now;

        public string Email { get; set; }

        [Required, MaxLength(64)]
        public string Login { get; set; }
        [Required, MaxLength(64)]
        public string Password { get; set; }
        public virtual UserPrioirity UserPrioirity { get; set; }

        public bool Admins => UserPrioirity.PriorityID == 1;

        public int UserPrioirityID { get; set; }

        public static User userAunt;

        public event PropertyChangedEventHandler PropertyChanged;

        [Required, MaxLength(64), Index(IsUnique = true)]
        public string UserName { get; set; }

        private byte[] _MainImage;

        public byte[] MainImage
        {
            get { return _MainImage; }
            set { _MainImage = value; PropChanged(); }
        }



        public static User AutroizationUser(string login, string password)
        {
            try
            {
                User user = EFModel.Init().Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
                User.userAunt = user;
            }
            catch (DbEntityValidationException exp)
            {
                MessageBox.Show(string.Join(",", exp.EntityValidationErrors.Last().ValidationErrors.Select(u => u.ErrorMessage)));
            }
            return User.userAunt;
        }

        public void PropChanged([CallerMemberName] string msg = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(msg));
        }
    }
}
