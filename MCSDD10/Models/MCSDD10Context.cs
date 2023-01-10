using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MCSDD10.Models
{
    public class MCSDD10Context:DbContext   //繼承DB Context類別，「:」是繼承的語法
    {
        public MCSDD10Context()       //建構子，一個類別裡可有多個建構子
            : base("name=MCSDD10Connection")   //指定連續資料庫的connection string的name
        {
        }

        //指定連線資料庫的字串(DB First用的)
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    throw new UnintentionalCodeFirstException();
        //}

        //描述資料庫裡面的資料表，dbfirst沒裝entity framework，要再去Nuget裝，裝完才會自動using System.Data.Entity
        public virtual DbSet<Employees> Employess { get; set; }

        public virtual DbSet<Members> Members { get; set; }

        public virtual DbSet<Products> Products { get; set; }

        public virtual DbSet<Shippers> Shippers { get; set; }

        public virtual DbSet<PayTypes> PayTypes { get; set; }

        public virtual DbSet<Orders> Orders { get; set; }

        public virtual DbSet<OrderDetails> OrderDetails { get; set; }



    }
}