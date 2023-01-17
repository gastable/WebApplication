using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MCSDD09.Models
{

    public class MCSDD09Initializer : DropCreateDatabaseAlways<MCSDD09Context>
    {
        protected override void Seed(MCSDD09Context db)
        {
            base.Seed(db);//如果要建立空資料庫，之後在view新增資料，就寫到這裡為止


            //建立某些資料表的基礎資料
            List<Shippers> shippers = new List<Shippers>
            {
            new Shippers
            {
                ShipVia="到店取貨"
            },
            new Shippers
            {
                ShipVia="宅配到府"
            },
            new Shippers
            {
                ShipVia="郵寄"
            }
            };
            //把db裡面的資料讀出來放到
            shippers.ForEach(s => db.Shippers.Add(s));
            db.SaveChanges();

            List<PayTypes> payTypes = new List<PayTypes>
            {
                new PayTypes
                {
                    PayTypeName="現金"
                },
                 new PayTypes
                {
                    PayTypeName="信用卡"
                },
                 new PayTypes
                {
                    PayTypeName="LinePay"
                }

            };
            payTypes.ForEach(s => db.PayTypes.Add(s));
            db.SaveChanges();
            List<Employees> employees = new List<Employees>
            { 
                new Employees
                {
                    EmployeeName="王大明",
                    CreatedDate=DateTime.Today,
                    Account="admin",
                    Password="12345678"
                }
            };
            employees.ForEach(s => db.Employees.Add(s));
            db.SaveChanges();
            List<Members> Members = new List<Members>
            {
                new Members
                {
                    MemberName="李筱華",
                    MemberBirthdy=Convert.ToDateTime("1991-10-10"),
                    CreatedDate=DateTime.Today,
                    //會員帳號預設最少5碼，最多20碼
                    Account="hua1991",
                    Password="12345678"
                }
            };
            Members.ForEach(s => db.Members.Add(s));
            db.SaveChanges();
        }
    }
}