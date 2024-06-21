using Microsoft.Data.SqlClient;
using SqlServerLib;
using System.ComponentModel.DataAnnotations;
namespace SqlConsole;

internal class Program
{
    static void Main(string[] args)
    {  //  LearningCode(); //uncomment this to run code in the method below.
        
    var connStr = "server=localhost\\sqlexpress01;" + 
                    "database=SalesDb;" + 
                    "trusted_connection=true;" + 
                    "trustServerCertificate=true;"; 
    Connection connection = new Connection(connStr); //create the connection by passing the connection string through the connection class. 
    connection.Open();

        OrderController orderCtrl = new OrderController(connection);
        
        var orders = orderCtrl.GetAll();
        foreach (var order in orders)
        {
            Console.WriteLine(order.Id);
            Console.WriteLine($"Customer ID: {order.CustomerId} | Order ID: {order.Id} | OrderDate: {order.Date} | Description: {order.Description}");
        }
        //var customer = custCtrl.GetByPK(1);
        //Console.WriteLine($"Id 1 is {customer!.Name}");

        ////SqlServerLib.Customer newCustomer = new SqlServerLib.Customer //put namespace of the project you want it to pull from, in this came, the SqlServerLib.
        //{
        //    Id = 0, Name = "Emily LLC",
        //    City = "Cincinnati", State = "OH",
        //    Sales = 10000, Active = true
        //};
        //var added = custCtrl.Create(newCustomer);
        //Console.WriteLine($"Did the create suceed? {added}");
        //var id = 15;
        //var customer = custCtrl.GetByPK(id);

        //customer!.City = "Lexington";
        //customer.State = "KY";
        //var added = custCtrl.Change(customer);
        //Console.WriteLine($"Did the create suceed? {added}");
        //Console.WriteLine($"Id {id} is {customer!.Name} | {customer.City} | {customer.State}.");


        //var id = 11;
        //var customer = custCtrl.GetByPK(id);
        //var removed = custCtrl.Remove(33);
        //Console.WriteLine($"Did the create suceed? {removed}");
        //Console.WriteLine($"{customer!.Name} was removed");

        connection.Close();
    }
    static void LearningCode () {
        //need to put the connection string in a variable because you will use connection string in several places
        var ConnStr = "server=localhost\\sqlexpress01;" + //machine\\instance
                      "database=SalesDb;" + //specific database you want to access
                      "trusted_connection=true;" + //[Windows (UN & PW) or Sql suthentication] OR trusted connection
                      "trustServerCertificate=true;"; //
        var Conn = new SqlConnection(ConnStr); // this is passing the connection string through the sql connection API and assigning that to the variable Conn.
        //added package
        //added using statement to package
        Conn.Open();

        if(Conn.State != System.Data.ConnectionState.Open)
        { throw new Exception("The connection didn't open!"); }

        Console.WriteLine("Connection opened..."); 

        //var sql = "SELECT * from Customers;"; 
        //var sqlcmd = new SqlCommand(sql, Conn);
        //var reader = sqlcmd.ExecuteReader();
        //if(!reader.HasRows) //means if there are no rows, due to ! before reader.HasRows)
        //{
        //    Console.WriteLine("'Customer' returned no rows!");
        //}
        
        //Dictionary<int, Customer> customers = new Dictionary<int, Customer>(); //create the dictionary before the while statement begins so the data read in the while loop will be organized into the collection (list, dictionary, array, etc.)
        //while(reader.Read()) //means as long as the reader finds data in the rows, this will read as true, allowing you to deploy whatever using that data, (by putting it in the curly braces following the while statement).
        //{
        //        Customer customer = new Customer();
        //        customer.Id = Convert.ToInt32(reader["Id"]);
        //        customer.Name= Convert.ToString(reader["Name"])!; //add ! to say this to remove warning about null
        //        customer.City= Convert.ToString(reader["City"])!; 
        //        customer.State= Convert.ToString(reader["State"])!; 
        //        customer.Sales= Convert.ToDecimal(reader["Sales"])!; 
        //        customer.Active= Convert.ToBoolean(reader["Active"])!; 
        //        customers.Add(customer.Id, customer); //customerS is the variable for the library, plural. singular is for each row. 

               // Console.WriteLine($"Id: {Customer.Id} | Name: {Customer.Name} | Sales: {Customer.sales:C}");

              
                /*var id = Convert.ToInt32(reader["Id"]); //reader["Id"] returns an object, which is a class that stores otther objects. but we want it to be an integet so we return this as an int. 
                var name = Convert.ToString(reader["Name"]);
                var sales = Convert.ToDecimal(reader["Sales"]);
                Console.WriteLine($"Id: {id} | Name: {name} | Sales: {sales:C}");*/
        }
        //reader.Close(); //important to close the reader. only one reader can be open at a time. 
        //Conn.Close(); //connections are expensive. close this connection when you are finished.
    } //end of class
