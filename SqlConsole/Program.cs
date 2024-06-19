using Microsoft.Data.SqlClient;
using SqlServerLib;
namespace SqlConsole;

internal class Program
{
    static void Main(string[] args)
    {
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

        var sql = "SELECT * from Customers;"; 
        var sqlcmd = new SqlCommand(sql, Conn);
        var reader = sqlcmd.ExecuteReader();
        if(!reader.HasRows) //means if there are no rows, due to ! before reader.HasRows)
        {
            Console.WriteLine("'Customer' returned no rows!");
        }
        while(reader.Read()) //means as long as the reader finds data in the rows, this will read as true, allowing you to deploy whatever using that data, (by putting it in the curly braces following the while statement).
        {
                var id = Convert.ToInt32(reader["Id"]); //reader["Id"] returns an object, which is a class that stores otther objects. but we want it to be an integet so we return this as an int. 
                var name = Convert.ToString(reader["Name"]);
                var sales = Convert.ToDecimal(reader["Sales"]);
                Console.WriteLine($"Id: {id} | Name: {name} | Sales: {sales:C}");
        }
        reader.Close(); //important to close the reader. only one reader can be open at a time. 
        Conn.Close(); //connections are expensive. close this connection when you are finished.
    }
}
