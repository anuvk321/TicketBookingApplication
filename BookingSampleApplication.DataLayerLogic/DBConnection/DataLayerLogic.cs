using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

namespace BookingSampleApplication.DataAccess
{
    public class DataLayerLogic
    {
        SqlDataAdapter da;
        SqlCommand cmd;
        SqlConnection con;
        DataSet ds;
        DataTable dt;
        String ConStr = ConfigurationManager.AppSettings["connectionstring"];

        //Open Connection
        private void openConnection()
        {
            try
            {
                if (ConStr == "")
                {
                    throw new Exception("Connection String is not found in application Configuration file");
                }
                else if (con == null)
                {
                    con = new SqlConnection(ConStr);
                }
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (SqlException ex)
            {
                throw (ex);
            }
        }

        //Close Connection
        private void closeConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        //Dispose connection
        private void disposeConnection()
        {
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }

        //Get DataSet that accept procedure having no parameter
        public DataSet getDataSetProcedureNoParameter(string ProcedureStr)
        {
            SqlTransaction trans = null;
            try
            {
                openConnection();
                trans = con.BeginTransaction();
                cmd = new SqlCommand();
                ds = new DataSet();
                da = new SqlDataAdapter();
                cmd.Transaction = trans;
                cmd.CommandText = ProcedureStr;
                cmd.Connection = con;
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(ds);
                trans.Commit();
                closeConnection();
                disposeConnection();
                return ds;
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw (ex);
            }
        }

        //Get DataSet that accept procedure having parameter
        public DataSet getDataSetProcedure(Dictionary<string, object> HT, string ProcedureStr)
        {
            SqlTransaction trans = null;
            try
            {
                openConnection();
                trans = con.BeginTransaction();
                cmd = new SqlCommand();
                ds = new DataSet();
                da = new SqlDataAdapter();
                cmd.Transaction = trans;
                cmd.CommandText = ProcedureStr;
                cmd.Connection = con;
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (string myKey in HT.Keys)
                {
                    cmd.Parameters.AddWithValue(myKey, HT[myKey]);
                }
                cmd.Prepare();
                da.SelectCommand = cmd;
                da.Fill(ds);
                trans.Commit();
                closeConnection();
                disposeConnection();
                return ds;
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw (ex);
            }
        }

        //Execute Procedure with parameter 
        public int executeQueryProcedure(Dictionary<string, string> HT, string ProcedureStr)
        {
            SqlTransaction trans = null;
            try
            {
                openConnection();
                trans = con.BeginTransaction();
                cmd = new SqlCommand();
                cmd.Transaction = trans;
                cmd.CommandText = ProcedureStr;
                cmd.Connection = con;
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (string myKey in HT.Keys)
                {
                    cmd.Parameters.AddWithValue(myKey.ToString().Trim(), HT[myKey].ToString().Trim());
                }
                cmd.Prepare();
                int result = cmd.ExecuteNonQuery();
                trans.Commit();
                closeConnection();
                disposeConnection();
                return result;
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw (ex);
            }
        }

        //Execute DataReader with Procedure having no parameter
        public SqlDataReader executeReaderProcedureNoParameter(String ProcedureStr)
        {
            try
            {
                openConnection();
                cmd = new SqlCommand();
                cmd.CommandText = ProcedureStr;
                cmd.Connection = con;
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (SqlException ex)
            {
                throw (ex);
            }
        }

        //Execute DataReader with Procedure having parameter
        public SqlDataReader executeReaderProcedure(Dictionary<string,string> HT, String ProcedureStr)
        {
            try
            {
                openConnection();
                cmd = new SqlCommand();
                cmd.CommandText = ProcedureStr;
                cmd.Connection = con;
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (string myKey in HT.Keys)
                {
                    cmd.Parameters.AddWithValue(myKey.ToString().Trim(), HT[myKey].ToString().Trim());
                }
                cmd.Prepare();
                SqlDataReader dr;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (SqlException ex)
            {
                throw (ex);
            }
        }

        // Close sqlDataReader
        public void closeDataReader(SqlDataReader dr)
        {
            if (dr.IsClosed == false)
            {
                dr.Close();
            }
        }

        //Get DataTable with procedure having no parameter
        public DataTable getDataTableProcedureNoParameter(String ProcedureStr)
        {
            SqlTransaction trans = null;
            try
            {
                openConnection();
                trans = con.BeginTransaction();
                dt = new DataTable();
                da = new SqlDataAdapter();
                cmd = new SqlCommand();
                cmd.Transaction = trans;
                cmd.CommandText = ProcedureStr;
                cmd.Connection = con;
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                trans.Commit();
                closeConnection();
                disposeConnection();
                return dt;
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw (ex);
            }
        }

        //Get DataTable with procedure having parameter
        public DataTable getDataTableProcedure(Dictionary<string, object> HT, String ProcedureStr)
        {
            SqlTransaction trans = null;
            try
            {
                openConnection();
                trans = con.BeginTransaction();
                dt = new DataTable();
                da = new SqlDataAdapter();
                cmd = new SqlCommand();
                cmd.Transaction = trans;
                cmd.CommandText = ProcedureStr;
                cmd.Connection = con;
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (string myKey in HT.Keys)
                {
                    cmd.Parameters.AddWithValue(myKey, HT[myKey]);
                }
                cmd.Prepare();
                da.SelectCommand = cmd;
                da.Fill(dt);
                trans.Commit();
                closeConnection();
                disposeConnection();
                return dt;
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw (ex);
            }
        }
    }
}
