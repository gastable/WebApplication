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
        // GET: Orders
        public ActionResult Index()
        {
            string sql = "select o.*,c.CompanyName from Orders as o inner join customers as c on o.CustomerID=c.CustomerID";

            var orders = gd.TableQuery(sql);
            orders.Columns[8].ColumnName = "收件人姓名";//顯示中文欄名稱，但view那邊用欄位抓資料就要對應修改
            return View(orders);
        }

        public ActionResult Display(int id)
        {
            string sql = "SELECT Orders.OrderID, Orders.OrderDate, Orders.RequiredDate, Orders.ShippedDate, Orders.Freight, Orders.ShipName, Orders.ShipAddress, Orders.ShipCity, Orders.ShipRegion, Orders.ShipPostalCode, Orders.ShipCountry, Customers.CustomerID, Customers.CompanyName, Customers.ContactName, Customers.ContactTitle, Employees.EmployeeID, Employees.LastName, Employees.FirstName, Shippers.CompanyName AS ShipCompany,Shippers.Phone FROM Orders INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID INNER JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID INNER JOIN Shippers ON Orders.ShipVia = Shippers.ShipperID ";
            sql+= "where orders.OrderID=@id";

            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("id",id)
            };


            var order = gd.TableQuery(sql,list);
            return View(order);
        }
    }
}