using event_planner.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace event_planner.Services
{
    public class AccountService
    {
        string connStr = @"server=LAPTOP-OBTQNT2O\SQLEXPRESS;initial catalog = EventPlanner; user id = local; pwd=sqlpass1";

        public int AddAccount(Account model)
        {
            int retVal = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string cmdStr = "Accounts_Insert";
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@AccountId";
                    param.SqlDbType = System.Data.SqlDbType.Int;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    retVal = (int)cmd.Parameters["@AccountId"].Value;
                }
            }
            return retVal;
        }

        public List<Account> GetAccounts()
        {
            List<Account> accList = new List<Account>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string cmdStr = "Accounts_SelectAll";
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Account acc = MapAccount(reader);
                            accList.Add(acc);
                        }
                    }
                    conn.Close();
                }
            }
            return accList;
        }

        public Account GetAccountById(int id)
        {
            Account retAcct = new Account();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string cmdStr = "Accounts_SelectById";
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountId", id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retAcct = MapAccount(reader);
                        }
                    }
                    conn.Close();
                }
            }
            return retAcct;
        }

        public int UpdateAccount(Account model)
        {
            int updatedRows = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string cmdStr = "Accounts_Update";
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountId", model.AccountId);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);
                    conn.Open();
                    updatedRows = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return updatedRows;
        }

        public int DeleteAccount(int id)
        {
            int updatedRows = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string cmdStr = "Accounts_Delete";
                using(SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountId", id);
                    conn.Open();
                    updatedRows = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return updatedRows;
        }

        private Account MapAccount(SqlDataReader reader)
        {
            Account acc = new Account();
            acc.AccountId = (int)reader["AccountId"];
            acc.Email = (string)reader["Email"];
            acc.Password = (string)reader["Password"];
            acc.CreatedDate = (DateTime)reader["CreatedDate"];
            acc.ModifiedDate = (DateTime)reader["ModifiedDate"];
            return acc;
        }
    }
}
