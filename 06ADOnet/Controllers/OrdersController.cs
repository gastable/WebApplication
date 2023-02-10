using _06ADOnet.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace _06ADOnet.Controllers
{
    public class OrdersController : Controller
    {
        GetData gd = new GetData();
        NorthwindEntities db = new NorthwindEntities();

        public ActionResult Index()
        {
            string sql = "select o.*,c.CompanyName from Orders as o inner join customers as c on o.CustomerID=c.CustomerID";

            var orders = gd.TableQuery(sql);
            orders.Columns[8].ColumnName = "收件人姓名";

            return View(orders);
        }

        public ActionResult Display(int id)
        {
            string sql = "SELECT Orders.OrderID, Orders.OrderDate, Orders.RequiredDate, Orders.ShippedDate, Orders.Freight, Orders.ShipName, Orders.ShipAddress, Orders.ShipCity, Orders.ShipRegion, Orders.ShipPostalCode, Orders.ShipCountry, Customers.CustomerID, Customers.CompanyName, Customers.ContactName, Customers.ContactTitle, Employees.EmployeeID, Employees.LastName, Employees.FirstName, Shippers.CompanyName AS ShipCompany,Shippers.Phone FROM Orders INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID INNER JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID INNER JOIN Shippers ON Orders.ShipVia = Shippers.ShipperID ";
            sql += "where orders.OrderID=@id";

            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("id",id)
            };


            var order = gd.TableQuery(sql, list);
            
            return View(order);
        }

        [ChildActionOnly]
        public ActionResult _DisplayDetail(int id)
        {

            return PartialView(db.OrderDetails.Where(m => m.OrderID == id).ToList());

        }


        public ActionResult getPivotOfProducts(int year = 1996)
        {
            string sql = "Sum_for_Products_Sales_Pivot";

            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("year",year)
            };
            ViewBag.Year = year;
            var pivot=gd.TableQueryBySP(sql, list);

            return View(pivot);
        }

        public ActionResult getPivotOfProducts2(int year = 1996)
        {
            if (Request.IsAjaxRequest())  //如果不是ajax的要求，就打回去
            {
                string sql = "Sum_for_Products_Salse_Pivot";

                List<SqlParameter> list = new List<SqlParameter>
                {
                    new SqlParameter("year",year)
                };
                ViewBag.Year = year;
                var pivot = gd.TableQueryBySP(sql, list);

                return View(pivot);
            }

            //ViewBag.Year = yy;
            return View();
        }

    }
}