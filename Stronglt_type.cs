using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDemo
{
    internal class Strongly_type
    {
        private string _connectionString;
        public Strongly_type(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public SqlConnection getconnection()
        {
            SqlConnection sqlconn = new SqlConnection();
            sqlconn.ConnectionString = _connectionString;
            return sqlconn;
        }
        //search employee using id
        public Employee search(int id)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd;
            Employee p = null;
            try
            {
                sqlconn = getconnection();
                sqlconn.Open();
                sqlcmd = new SqlCommand("select * from Employee where id=@pid", sqlconn);
                sqlcmd.Parameters.AddWithValue("@pid", id);
                SqlDataReader rd = sqlcmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        // int.TryParse("rd[Id"]", out r) ;
                        p = new Employee();
                        p.Id = Convert.ToInt32(rd["Id"]);
                        p.Name = rd["Name"].ToString();
                        p.Gender = rd["Gender"].ToString();
                        p.Address= rd["Address"].ToString();
                        p.Salary = rd["Salary"].ToString();
                        break;
                    }
                }
            }
            catch (SqlException se)
            { Console.WriteLine(se.Message); }
            finally
            {
                sqlconn.Close();
            }

            return p;
        }
        //adding data to data base
        public int AddData(Employee e)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd;
            int record = 0;
            try
            {
                sqlconn = getconnection();
                sqlcmd = new SqlCommand("storedata", sqlconn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.Add("@pname", SqlDbType.NVarChar).Value = e.Name;
                sqlcmd.Parameters.Add("@pgender", SqlDbType.NVarChar).Value = e.Gender;
                sqlcmd.Parameters.Add("@paddress", SqlDbType.NVarChar).Value = e.Address;
                sqlcmd.Parameters.Add("@psalary", SqlDbType.NVarChar).Value = e.Salary;

                sqlconn.Open();
                record = sqlcmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            { Console.WriteLine(se.Message); }
            finally
            {
                sqlconn.Close();
            }
            return record;

        }
        //deleting data from data base using id
        public int Del(int id)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd;
            int no = 0;

            using (sqlconn = getconnection())
            {
                try
                {
                    sqlconn.Open();
                    sqlcmd = new SqlCommand("delete from Employee where id=@pid", sqlconn);
                    sqlcmd.Parameters.AddWithValue("@pid", id);
                    no = sqlcmd.ExecuteNonQuery();
                }
                catch (SqlException se)
                { Console.WriteLine(se.Message); }
            }


            return no;
        }
        //search all data from data base and store in list
        public List<Employee> GetList()
        {
            var listEmployee = new List<Employee>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_EMP_GET_LIST", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listEmployee.Add(new Employee
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Name = rdr["Name"].ToString(),
                            Salary = rdr["Salary"].ToString(),
                            Gender= rdr["Gender"].ToString(),
                            Address= rdr["Address"].ToString(),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listEmployee;
        }
    }
}

    