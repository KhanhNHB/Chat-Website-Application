using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat
{
    public class Utils
    {
        private Utils() { }

        private static Utils instance;

        public static Utils Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Utils();

                }
                return instance;
            }
            set { instance = value; }
        }

        public string getConnectionString()
        {
            string strConnection = "server=.;database=ChatDB;uid=sa;pwd=123456";
            return strConnection;
        }

        public bool IsExist(string query)
        {
            bool check = false;
            SqlConnection cnn = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                check = true;
            }
            sdr.Close();
            cnn.Close();
            return check;
        }

        public bool ExecuteQuery(string query)
        {
            int i = 0;
            SqlConnection cnn = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            i = cmd.ExecuteNonQuery();
            cnn.Close();
            if (i > 0)
                return true;
            else
                return false;
        }

        public string GetColumValue(string query, string columName)
        {
            string Val = "";
            SqlConnection cnn = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(query, cnn);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Val = sdr[columName].ToString();
                break;
            }
            sdr.Close();
            cnn.Close();
            return Val;
        }

        public int ExecuteNonQuery(string query, params object[] parameters)
        {
            int row = 0;

            using (SqlConnection con = new SqlConnection(getConnectionString()))
            {
                con.Open();

                SqlCommand command = new SqlCommand(query, con);


                if (parameters != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (var item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameters[i]);
                            i++;
                        }
                    }
                }

                row = command.ExecuteNonQuery();

            }

            return row;
        }

        public object ExecuteScalar(string query, params object[] parameters)
        {
            object data = 0;

            using (SqlConnection con = new SqlConnection(getConnectionString()))
            {
                con.Open();

                SqlCommand command = new SqlCommand(query, con);

                if (parameters != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (var item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameters[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();
            }

            return data;
        }

        public string GetEncrypting(string password)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";
            foreach (var item in hasData)
            {
                hasPass += item;
            }
            
            //var list = hasData.ToString();
            //list.Reverse();

            return hasPass;
        }

        public DataTable ExecuteQuery(string query, params object[] parameters)
        {
            DataTable data = new DataTable();
            using (SqlConnection con = new SqlConnection(getConnectionString()))
            {
                con.Open();

                SqlCommand command = new SqlCommand(query, con);

                if (parameters != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (var item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameters[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);
            }

            return data;
        }
    }
}