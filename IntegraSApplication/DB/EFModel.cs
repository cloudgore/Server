using IntegraSApplication.DB.Entitys;
using IntegraSApplication.Pages;
using MySql.Data.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

namespace IntegraSApplication.DB

{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class EFModel : DbContext
    {
        private static  EFModel Instains;
        public static EFModel Init()
        {
            if (Instains == null)
                Instains = new EFModel();

            return Instains;
        }
        public EFModel()
            : base("server=cfif31.ru;port=3306;user id=ISPr21-34_LobanovEV;password=ISPr21-34_LobanovEV;database=ISPr21-34_LobanovEV_IntegraSApp")
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPrioirity> UserPrioirities { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Order> Orders { get; set; 
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}