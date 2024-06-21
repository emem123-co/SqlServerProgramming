using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
namespace SqlServerLib;
public class OrderController
{
     private SqlConnection? _sqlConnection { get; set; } = null;
     //public List<Orders> GetAll()
     //{
     //   var sqlOrdersTable = "Select * from Orders;";
     //   var sqlcmd = new SqlCommand(sqlOrdersTable, _sqlConnection);
     //   var reader = sqlcmd.ExecuteReader(); 
     //   List<Orders> orders = new List<Orders>(); 
     //   while(reader.Read())
     //   {
     //       var order = new Orders();
     //       order.Id = Convert.ToInt32(reader["Id"]);
     //       order.CustomerId = Convert.ToInt32 (reader["CustId"]);
     //       order.Date = Convert.ToString(reader["Date"])!;
     //       order.Description = Convert.ToString(reader["Description"])!; 
     //       orders.Add(order);
     //   }
     //   reader.Close(); 
     //   return orders;
     //}
     public List<OrdersOrders> GetAll()
     {
        var sqlOrdersTable = "Select * from Orders;";
        var sqlcmd = new SqlCommand(sqlOrdersTable, _sqlConnection);
        var reader = sqlcmd.ExecuteReader(); 
        if(!reader.HasRows)
        {
            Console.WriteLine("'Customer' returned no rows!");
        }
        List<OrdersOrders> orders = new List<OrdersOrders>(); 
        while(reader.Read())
        {
            var order = new OrdersOrders();
            order.Id = Convert.ToInt32(reader["Id"]);
            order.CustomerId = Convert.ToInt32 (reader["CustomerId"]);
            order.Date = Convert.ToString(reader["Date"])!;
            order.Description = Convert.ToString(reader["Description"])!; 
            orders.Add(order);
        }
        reader.Close(); 
        return orders;
     }




     //constructor
     public OrderController(Connection connection)
     {
        if(connection.GetSqlConnection() != null)
        {
            _sqlConnection = connection.GetSqlConnection()!;
        }
     }


} //end of class
