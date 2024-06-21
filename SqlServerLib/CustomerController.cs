using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace SqlServerLib;
public class CustomerController
{
    private SqlConnection? _sqlConnection { get; set; } = null; //setting to null because below we are placing the confirmed not null connection into the _sqlConnection property declared here.
    
    public List<Customer> GetAll()
    {
        var sqlCustomerTable = "Select * from Customers;";
        var sqlcmd = new SqlCommand(sqlCustomerTable, _sqlConnection); //select statement, then the prperty holding the connection.
        var reader = sqlcmd.ExecuteReader(); //this is choosing the reader to the SqlCommand statement and capturing the results in the variable reader.
        List<Customer> customers = new List<Customer>(); //intializing the list to put the data from the reader into. before the while loop. 
        while(reader.Read())
        {
            var customer = new Customer();
            ConvertToCustomer(customer, reader);    
            customers.Add(customer);
        }
        reader.Close(); //remember to close the reader or else we cant open another one elsewhere. 
        return customers;  //return the newly created list of customers. 
    }
    public Customer? GetByPK(int Id)
    {
        var sqlCustomerTable = "Select * from Customers;";
        var sqlcmd = new SqlCommand(sqlCustomerTable, _sqlConnection);
        var reader = sqlcmd.ExecuteReader(); 
        if(!reader.HasRows) 
        {
            reader.Close();
            return null;
        }
        reader.Read();
        var customer = new Customer();
        ConvertToCustomer(customer, reader);
        reader.Close();
        return customer;
    }
    public bool Create(Customer customer) 
    {
        //var sql = $" INSERT Customers (Name, City, State, Sales, Active) " + $" VALUES ('{customer.Name}', '{customer.City}', '{customer.State}', {customer.Sales}, {(customer.Active ? 1 : 0)}) ";
        var sql = " INSERT Customers (Name, City, State, Sales, Active) VALUES " +
                                  "  (@Name, @City, @State, @Sales, @Active);";
        var sqlcmd = new SqlCommand(sql, _sqlConnection);
        CustomersParameters(sqlcmd, customer);           
        var rowsAffected = sqlcmd.ExecuteNonQuery(); //check select statement if you get an syntax error here. 
        return rowsAffected == 1 ? true : false;
    }
    public bool Change(Customer customer)
    {
        var sql = " UPDATE Customers Set " +
                  " Name = @Name, " +
                  " City = @City, " +
                  " State = @State, " +
                  " Sales = @Sales, " +
                  " Active = @Active " +
                  " Where Id = @Id; ";
        var sqlcmd = new SqlCommand(sql, _sqlConnection);
        CustomersParameters(sqlcmd, customer);
        sqlcmd.Parameters.AddWithValue("@Id", customer.Id);

        var rowsUpdated = sqlcmd.ExecuteNonQuery();
        return rowsUpdated == 1 ? true : false;
    }
    public bool Remove(int Id)
    {
        var sql = " DELETE Customers " +
                  " Where Id = @Id;";
        var sqlcmd = new SqlCommand(sql, _sqlConnection);
        sqlcmd.Parameters.AddWithValue("@Id", Id);            
        var rowsUpdated = sqlcmd.ExecuteNonQuery();
        return rowsUpdated == 1 ? true : false;
    }

    
    
    
    
    
    
    //Refactory object (for paramters)
    private void CustomersParameters(SqlCommand sqlcmd, Customer customer)
    {
        sqlcmd.Parameters.AddWithValue("@Name", customer.Name);          //add sql parameters  
        sqlcmd.Parameters.AddWithValue("@City", customer.City);
        sqlcmd.Parameters.AddWithValue("@State", customer.State);
        sqlcmd.Parameters.AddWithValue("@Sales", customer.Sales);
        sqlcmd.Parameters.AddWithValue("@Active", customer.Active);
    }
    
    //Refactory object (if changes to columns, change here.)
    private void ConvertToCustomer(Customer customer, SqlDataReader reader)
    {
        customer.Id = Convert.ToInt32(reader["Id"]); 
        customer.Name = Convert.ToString(reader["Name"])!;
        customer.City = Convert.ToString(reader["City"])!; 
        customer.State = Convert.ToString(reader["State"])!; 
        customer.Sales = Convert.ToDecimal(reader["Sales"]); 
        customer.Active = Convert.ToBoolean(reader["Active"]); 
    }
    
    //constructor
    public CustomerController(Connection connection) //this is passing the connection through the controller
    {
        if(connection.GetSqlConnection() != null)
        {
            _sqlConnection = connection.GetSqlConnection()!; //add ! because this wouldn't be null.
        }
    }
    
    //end of constructor

}//end of class

//NOTE: list properties first, then methods, then constructors at the very end.