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
    public class StudentServices
    {

        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;


        public IList<StudentModel> GetStudentList()
        {
            IList<StudentModel> getStuList = new List<StudentModel>();
            _ds = new DataSet();

            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("StudentViewOrInsert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetStuList");
                
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                
                if(_ds.Tables.Count > 0)
                {
                    for(int i=0; i<_ds.Tables[0].Rows.Count; i++)
                    {
                        StudentModel obj = new StudentModel();
                        obj.Student_ID = Convert.ToInt32(_ds.Tables[0].Rows[i]["Student_ID"]);
                        obj.Name = Convert.ToString(_ds.Tables[0].Rows[i]["Name"]);
                        obj.Address = Convert.ToString(_ds.Tables[0].Rows[i]["Address"]);
                        obj.Phone = Convert.ToString(_ds.Tables[0].Rows[i]["Phone"]);
                        obj.ID = string.IsNullOrEmpty(_ds.Tables[0].Rows[i]["ID"].ToString()) ? 0 : Convert.ToInt32(_ds.Tables[0].Rows[i]["ID"]);
                        obj.UserName = string.IsNullOrEmpty(_ds.Tables[0].Rows[i]["UserName"].ToString()) ? "-" : Convert.ToString(_ds.Tables[0].Rows[i]["UserName"]);
                        getStuList.Add(obj); 

                    }
                }
            }
            return getStuList;
        }


        public void InsertStudent(StudentModel model)
        {
            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("StudentViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "AddStudent");
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Address", model.Address);
                cmd.Parameters.AddWithValue("@Phone", model.Phone);

                cmd.ExecuteNonQuery();
            }


        }


        public StudentModel GetEditById(int Student_ID)
        {
            var model = new StudentModel();

            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("StudentViewOrInsert",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetStudentById");
                cmd.Parameters.AddWithValue("@Stu_ID", Student_ID);

                _adapter = new SqlDataAdapter(cmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                
                if(_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Student_ID = Convert.ToInt32(_ds.Tables[0].Rows[0]["Student_ID"]);
                    model.Name = Convert.ToString(_ds.Tables[0].Rows[0]["Name"]);
                    model.Address = Convert.ToString(_ds.Tables[0].Rows[0]["Address"]);
                    model.Phone = Convert.ToString(_ds.Tables[0].Rows[0]["Phone"]);
                }

            }

            return model;
        }

        public void UpdateStudent(StudentModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("StudentViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "UpdateStudent");
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Address", model.Address);
                cmd.Parameters.AddWithValue("@Phone", model.Phone);
                cmd.Parameters.AddWithValue("@Stu_ID", model.Student_ID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int Student_ID)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("StudentViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteStudent");
                cmd.Parameters.AddWithValue("@Stu_ID", Student_ID);
                cmd.ExecuteNonQuery();
            }
        }

    }
}