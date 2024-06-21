using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace SqlServerLib;
public class Connection //connection class will create the single connection to the database to avoid having 3 connections open for each data inquiry.
{
    private string _connectionString { get;set; } = string.Empty; //underscore typically indicates a private property
    private SqlConnection? _sqlConnection { get;set; } = null; //this is an instance of the sql connection and a property. ? to indicate this could be null. once a valid connection is made, this won't be null.
    
    public SqlConnection? GetSqlConnection() //method that returns the Sql Connection itself, not the string, the actual connection. could choose to throw an exception if the connection is not present.
    {
        return _sqlConnection;
    }

            public void Open() //this is the open method that will check to see if the connection is open. will throw an exception if it is false. 
            {
                _sqlConnection = new SqlConnection(_connectionString);
                _sqlConnection.Open(); //this is the actual piece making the connection to the dB.
                if(_sqlConnection.State != System.Data.ConnectionState.Open) 
                {
                    _sqlConnection = null;
                    throw new Exception("Connection failed to open.");
                }
            }
            public void Close()
            {
                _sqlConnection?.Close(); //this says when the _sqlConnection returns false, run "close" method.
            }
    
    public Connection(string connectionString) //this is constructor. you will pass the new connectionString variable through the instance of the Connection from the program.cs file.
    {
        _connectionString = connectionString;
    }
    public Connection()
    {
    }
}
//end of class
//connection class is an example of building somethig that supports the functionality of the greater solution that doesn't do much on its own.