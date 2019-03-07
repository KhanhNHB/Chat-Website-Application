using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.DAO
{
    public class UserDAO
    {
        public UserDTO CheckLogin(string email, string password)
        {

            password = Utils.Instance.GetEncrypting(password);

            string query = "EXEC USP_CheckLogin @Email , @Password";

            UserDTO userDTO = GetUser(query, new object[] { email, password });


            return userDTO;
        }

        private UserDTO GetUser(string query, params object[] parameters)
        {
            using (SqlConnection con = new SqlConnection(Utils.Instance.getConnectionString()))
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

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    string id, userName, email, password, avatar, loginTime;
                    bool role; // True: Admin, False: User


                    if (reader.Read())
                    {
                        id = reader.GetString(0);
                        userName = reader.GetString(1);
                        email = reader.GetString(2);
                        password = reader.GetString(3);
                        avatar = reader.GetString(4);
                        role = reader.GetBoolean(5);
                        loginTime = reader.GetString(6);

                        UserDTO userDTO = new UserDTO {
                            Id = id,
                            UserName = userName,
                            Email = email,
                            Password = password,
                            Avatar = avatar,
                            LoginTime = loginTime,
                            Role = role
                        };

                        return userDTO;
                    }

                }
            }

            return null;
        }
    }
}
