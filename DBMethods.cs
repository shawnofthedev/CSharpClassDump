using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

//*************************************************************************************************
//Author:           Shawn Votaw 
//Written:          April 23, 2014
//Last Revised:     April 23, 2014
//Purpose: DbMethods class provides database interface capabilities
//         All methods are static and used throughout the other classes
//*************************************************************************************************

public class DbMethods
{
    //Return a populated DataTable given an sql Command
    public static DataTable CreateTableObj(string sqlString)
    {
        //Get connection string from web.config/app.config
        //<connectionStrings>
        //  <add name="FinalProjectConnectionString" connectionString="Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\FinalProject.mdf;Integrated Security=True;Connect Timeout=30"
        //      providerName="System.Data.SqlClient" />
        //</connectionStrings>
        string conString = ConfigurationManager.ConnectionStrings["FinalProjectConnectionString"].ConnectionString;

        //Create connection and command objects
        SqlConnection dbConnObj = new SqlConnection(conString);
        SqlCommand cmdObj = new SqlCommand(sqlString, dbConnObj);

        //Create DataAdapter and DataTable
        SqlDataAdapter adapterObj = new SqlDataAdapter();
        DataTable tableObj = new DataTable();

        //Execute SQL to fill DataTable using DataAdapter
        adapterObj.SelectCommand = cmdObj;
        adapterObj.Fill(tableObj);
        return tableObj;
    }

    //Return a populated DataTable given an SQL command and parameter list
    public static DataTable CreateTableObj(string sqlString, string[,] sqlArgs)
    {
        int i;

        //Get connection string 
        string conString = ConfigurationManager.ConnectionStrings["FinalProjectConnectionString"].ConnectionString;

        //Create connection and command objects
        SqlConnection dbConnObj = new SqlConnection(conString);
        SqlCommand cmdObj = new SqlCommand(sqlString, dbConnObj);

        //Add parameter list to the SQL command
        for (i = 0; i <= sqlArgs.GetUpperBound(0); i++)
        {
            cmdObj.Parameters.AddWithValue(sqlArgs[i, 0], sqlArgs[i, 1]);
        }

        //Create DataAdapter and DataTable
        SqlDataAdapter adapterObj = new SqlDataAdapter();
        DataTable tableObj = new DataTable();

        //Execute SQL to fill DataTable using DataAdapter
        adapterObj.SelectCommand = cmdObj;
        adapterObj.Fill(tableObj);
        return tableObj;
    }

    //Return a populated DataTable given an SQL command and parameter list
    public static void ChangeTableObj(string sqlString, string[,] sqlArgs)
    {
        int i;

        //Get connection string 
        string conString = ConfigurationManager.ConnectionStrings["FinalProjectConnectionString"].ConnectionString;

        //Create connection and command objects
        SqlConnection dbConnObj = new SqlConnection(conString);
        SqlCommand cmdObj = new SqlCommand(sqlString, dbConnObj);

        //Add parameter list to the SQL command
        for (i = 0; i <= sqlArgs.GetUpperBound(0); i++)
        {
            cmdObj.Parameters.AddWithValue(sqlArgs[i, 0], sqlArgs[i, 1]);
        }

        //Execute a NonQuery to change the underlying table
        cmdObj.Connection.Open();
        cmdObj.ExecuteNonQuery();
        cmdObj.Connection.Close();
    } 
}
