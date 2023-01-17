using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MCSDD09.Models
{
    public class MCSDD09Context : DbContext //繼承DbContext類別的屬性和功能
    {
        public MCSDD09Context()
            : base("name=MCSDD09Connection")
        {

        }
        
        //這段是DB first要指定連線資料庫的字串
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    throw new UnintentionalCodeFirstException();
        //}

        //描述資料裡包含的資料表
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<PayTypes> PayTypes { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

    }
}