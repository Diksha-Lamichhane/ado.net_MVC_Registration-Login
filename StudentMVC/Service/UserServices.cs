using StudentMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentMVC.Service
{
    public class UserServices
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public void CreateUser(UserModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateDeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "AddUser");
                cmd.Parameters.AddWithValue("@Student_ID", model.Student_ID);
                cmd.Parameters.AddWithValue("@UserName", model.UserName);
                cmd.Parameters.AddWithValue("@Password", model.Password);

                cmd.ExecuteNonQuery();
            }
        }
        

        public UserModel GetEditById(int Student_ID)
        {
            var model = new UserModel();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateDeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetUserById");
                cmd.Parameters.AddWithValue("@Student_ID", Student_ID);

                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);

                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.ID = Convert.ToInt32(_ds.Tables[0].Rows[0]["ID"]);
                    model.Student_ID = Convert.ToInt32(_ds.Tables[0].Rows[0]["Student_ID"]);
                    model.UserName = Convert.ToString(_ds.Tables[0].Rows[0]["UserName"]);
                    model.Password = Convert.ToString(_ds.Tables[0].Rows[0]["Password"]);
                }
            }
            return model;
        }

        public void UpdateUser(UserModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateDeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "UpdateUser");
                cmd.Parameters.AddWithValue("@Student_ID", model.Student_ID);
                cmd.Parameters.AddWithValue("@UserName", model.UserName);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@User_ID", model.ID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int Student_ID)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateDeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteUser");
                cmd.Parameters.AddWithValue("@Student_ID", Student_ID);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
