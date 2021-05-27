using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Econtact.econtactClasses
{
    class contactClass
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        
        static string myconntstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //Select data from Database
        public DataTable select()
        {
            SqlConnection conn = new SqlConnection(myconntstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "select * from tbl_contact";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adpater = new SqlDataAdapter(cmd);
                conn.Open();
                adpater.Fill(dt);
            }
            catch(Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Insert Data
        public bool Insert(contactClass contact)
        {
            //Creating a default return type
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconntstring);
           
            try
            {
                string sql = "Insert into tbl_contact(FirstName,LastName,ContactNo,Address,Gender) values(@FirstName,@LastName,@ContactNo,@Address,@Gender)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
                cmd.Parameters.AddWithValue("@LastName", contact.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", contact.ContactNo);
                cmd.Parameters.AddWithValue("@Address", contact.Address);
                cmd.Parameters.AddWithValue("@Gender", contact.Gender);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }

        //Update Data
        public bool Update(contactClass contact)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconntstring);
            try
            {
                string sql = "Update tbl_contact SET FirstName=@FirstName,LastName=@LastName,ContactNo=@ContactNo,Address=@Address,Gender=@Gender Where ContactID=@ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
                cmd.Parameters.AddWithValue("@LastName", contact.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", contact.ContactNo);
                cmd.Parameters.AddWithValue("@Address", contact.Address);
                cmd.Parameters.AddWithValue("@Gender", contact.Gender);
                cmd.Parameters.AddWithValue("@ContactID", contact.ContactID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception exx)
            {

            }
            finally
            {
                conn.Close();
            }


            return isSuccess;
        }

        //Delete Data
        public bool Delete(contactClass contact)
        {
            //Creating a default return type
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconntstring);

            try
            {
                string sql = "Delete from tbl_contact where ContactID=@ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", contact.ContactID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception exxx)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }

    }
}
