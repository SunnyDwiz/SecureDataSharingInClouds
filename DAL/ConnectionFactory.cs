using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace SECDS.DAL
{
    public class ConnectionFactory
    {
        public static string connStr = "Constr";
        static SqlConnection conn;
        static SqlCommand cmd;
        static DataTable dt = new DataTable();
        static DataTable dt1 = new DataTable();

        public static SqlConnection openConnection()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn = new SqlConnection();
                    ConnectionStringSettings connStrSettings = ConfigurationManager.ConnectionStrings["Constr"];
                    conn.ConnectionString = connStrSettings.ConnectionString;
                    conn.Open();
                    return conn;

                }
                else
                {
                    conn = new SqlConnection();
                    ConnectionStringSettings connStrSettings = ConfigurationManager.ConnectionStrings["Constr"];
                    conn.ConnectionString = connStrSettings.ConnectionString;
                    conn.Open();
                    return conn;
                }
            }
            catch { throw; }
        }


        public static SqlDataReader ExecuteReader_WithParameters_WithoutCommandType(string SPName, params SqlParameter[] SpParam)
        {
            try
            {
                return ExecuteReader_WithParameters_WithCommandType(SPName, CommandType.StoredProcedure, SpParam);
            }
            catch { throw; }
        }

        public static SqlDataReader ExecuteReader_WithParameters_WithCommandType(string Text, CommandType CmdType, params SqlParameter[] SpParam)
        {
            try
            {
                SqlDataReader _reader;
                cmd = new SqlCommand();
                cmd.Connection = openConnection();
                cmd.CommandText = Text;     //Assign the SP Name to Command Object
                cmd.CommandType = CmdType;  //Assign the SP Type to Command Object

                foreach (object param in SpParam)   //Assign the SP Parameters to Command Parameters Object
                    cmd.Parameters.Add(param);                
                _reader = cmd.ExecuteReader();  //Execute the SP                
                return _reader;
            }
            catch { throw; }
        }
        public static void closeConnection(SqlConnection conn)
        {
            try
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();

                }
            }
            catch { throw; }
        }
        public static DataTable DataTable_WithParameters_WithCommandType(string Text, CommandType CmdType, params SqlParameter[] SpParam)
        {
            try
            {
                SqlDataReader _reader;
                cmd = new SqlCommand();
                cmd.Connection = openConnection();
                cmd.CommandText = Text;     //Assign the SP Name to Command Object
                cmd.CommandType = CmdType;  //Assign the SP Type to Command Object

                foreach (object param in SpParam)   //Assign the SP Parameters to Command Parameters Object
                    cmd.Parameters.Add(param);               
                _reader = cmd.ExecuteReader();  //Execute the SP 
                DataTable dt = new DataTable();
                dt.BeginLoadData();
                dt.Load(_reader);
                dt.EndLoadData();
                closeConnection(conn);
                // da.Fill(dt);
                return dt;
            }
            catch { throw; }
        }
        public static DataTable DataTable_WithParameters_WithoutCommandType(string SPName, params SqlParameter[] SpParam)
        {
            try
            {
                return DataTable_WithParameters_WithCommandType(SPName, CommandType.StoredProcedure, SpParam);
            }
            catch { throw; }
        }

        public static DataSet DataSet_WithParameters_WithoutCommandType(string CommandText, CommandType CmdType, params SqlParameter[] SpParam)
        {
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = openConnection();
                cmd.CommandText = CommandText;     //Assign the SP Name to Command Object
                cmd.CommandType = CmdType;  //Assign the SP Type to Command Object

                foreach (object param in SpParam)   //Assign the SP Parameters to Command Parameters Object
                    cmd.Parameters.Add(param);                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);               
                return ds;
            }
            catch { throw; }
        }
        public static bool IsValid(string Text, CommandType CmdType, params SqlParameter[] SpParam)
        {
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = openConnection();
                cmd.CommandText = Text;     //Assign the SP Name to Command Object
                cmd.CommandType = CmdType;  //Assign the SP Type to Command Object

                foreach (object param in SpParam)   //Assign the SP Parameters to Command Parameters Object
                    cmd.Parameters.Add(param);
                bool result = Convert.ToBoolean(cmd.ExecuteScalar()); //Execute the SP 
                return result;
            }
            catch { throw; }
        }

        public static int ExecuteCommandByInt(string Text, CommandType CmdType, params SqlParameter[] SpParam)
        {
            try
            {
                cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = openConnection();
                cmd.CommandText = Text;//Assign the SP Name to Command Object
                cmd.CommandType = CmdType; //Assign the SP Type to Command Object
                foreach (object param in SpParam)
                    cmd.Parameters.Add(param); //Assign the SP Parameters to Command Parameters Object
                int result = cmd.ExecuteNonQuery(); //Execute the SP 
                return result;
            }
            catch { throw; }
        }
        public static string ExecuteCommandByString(string Text, CommandType CmdType, params SqlParameter[] SpParam)
        {
            try
            {
                cmd = new SqlCommand();
                cmd = new SqlCommand();
                cmd.Connection = openConnection();
                cmd.CommandText = Text;//Assign the SP Name to Command Object
                cmd.CommandType = CmdType; //Assign the SP Type to Command Object
                foreach (object param in SpParam)
                    cmd.Parameters.Add(param); //Assign the SP Parameters to Command Parameters Object                 
                return (cmd.ExecuteScalar()).ToString();
            }
            catch { throw; }
        }
        public static DataTable DataTable_WithoutParameters_WithCommandType(string Text, CommandType CmdType)
        {
            try
            {
                SqlDataReader _reader;
                cmd = new SqlCommand();
                cmd.Connection = openConnection();
                cmd.CommandText = Text;     //Assign the SP Name to Command Object
                cmd.CommandType = CmdType;  //Assign the SP Type to Command Object

                _reader = cmd.ExecuteReader();  //Execute the SP 
                DataTable dt = new DataTable();
                dt.Load(_reader);
                closeConnection(conn);
                return dt;
            }
            catch { throw; }
        }

        public static SqlDataReader ExecuteReader_WithoutParameters_WithCommandType(string Text, CommandType CmdType)
        {
            try
            {
                SqlDataReader _reader;
                cmd = new SqlCommand();
                cmd.Connection = openConnection();
                cmd.CommandText = Text;     //Assign the SP Name to Command Object
                cmd.CommandType = CmdType;  //Assign the SP Type to Command Object

                _reader = cmd.ExecuteReader();  //Execute the SP                
                return _reader;
            }
            catch { throw; }
        }

        //this method for reading data from Excel file and return datatable
        public static DataTable ReturnDataTableObj_Excel(string filename)
        {
            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;
            Data Source=" + filename + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1'";
                string strSQL = "SELECT * FROM [Sheet1$]";
                OleDbConnection excelConnection = new OleDbConnection(connectionString);
                excelConnection.Open(); // This code will open excel file.
                OleDbCommand dbCommand = new OleDbCommand(strSQL, excelConnection);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(dbCommand);
                DataTable dTable = new DataTable();
                dataAdapter.Fill(dTable);
                excelConnection.Close();
                return dTable;
            }
            catch { throw; }
        }
    }
}
