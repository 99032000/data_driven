using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient; //sql
using System.Configuration; //webconfig

namespace week2
{
    public partial class registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            Validate("Group1");
            
            if (FirstNameValidator.IsValid && LastNameValidator.IsValid && DOBValidator.IsValid && PhoneNumberRequiredValidator.IsValid && PhoneNumberValidator.IsValid)
            {
                int userID = CreateUserWithRegister();
                MessageLabel.Text = "Thanks for your patient ";
                SaveChoice(userID);

            }
            else
            {
                MessageLabel.Text = "Please fill all the blank";
            }
        }

        protected void NoRegisterButton_Click(object sender, EventArgs e)
        {
            int userID = CreateUserWithoutRegister();
            // MessageLabel.Text = CreateUserWithoutRegister().ToString();
            SaveChoice(userID);

        }

        private int CreateUserWithoutRegister()
        {

            string userip = GetIP();
            
            DateTime date = DateTime.UtcNow;
            using (SqlConnection connection = GetConnection())
            {
                string str = "INSERT INTO [User] (IP,dateCreate) VALUES (@ip,@datecreate)";
                SqlCommand command = new SqlCommand(str, connection);
                command.Parameters.AddWithValue("@ip", userip);
                command.Parameters.AddWithValue("@datecreate", date);

                //int id = (int)command.ExecuteScalar();
                command.ExecuteReader();

                str = "Select UserID From [User] Where IP = @ip AND dateCreate = @datecreate";
                command = new SqlCommand(str,connection);
                command.Parameters.AddWithValue("@ip", userip);
                command.Parameters.AddWithValue("@datecreate", date);

                return Convert.ToInt32(command.ExecuteScalar());
               
            }

                
        }
        private int CreateUserWithRegister()
        {

            string userip = GetIP();

            DateTime date = DateTime.UtcNow;
            using (SqlConnection connection = GetConnection())
            {
                string str = "INSERT INTO [User] (IP,dateCreate,phoneNo,firstName," +
                    "lastName,DOB) VALUES " +
                    "(@ip,@datecreate,@phoneNo,@firstname,@lastname,@DOB)";
                SqlCommand command = new SqlCommand(str, connection);
                command.Parameters.AddWithValue("@ip", userip);
                command.Parameters.AddWithValue("@datecreate", date);
                command.Parameters.AddWithValue("@phoneNo", PhoneNumberTextBox.Text);
                command.Parameters.AddWithValue("@firstname", FirstNameTextBox.Text);
                command.Parameters.AddWithValue("@lastname", LastNameTextBox.Text);
                command.Parameters.AddWithValue("@DOB", Convert.ToDateTime(DOBTextBox.Text));
                //int id = (int)command.ExecuteScalar();
                command.ExecuteReader();

                str = "Select UserID From [User] Where IP = @ip AND dateCreate = @datecreate";
                command = new SqlCommand(str, connection);
                command.Parameters.AddWithValue("@ip", userip);
                command.Parameters.AddWithValue("@datecreate", date);

                return Convert.ToInt32(command.ExecuteScalar());

            }


        }
        // get IP from client device
        private static string GetIP()
        {
            string hostName = string.Empty;
            hostName = Dns.GetHostName();
            IPHostEntry myIP = Dns.GetHostEntry(hostName);
            IPAddress[] address = myIP.AddressList;
            return address[0].ToString();
        }

        // save the session to database related to UserID
        private static void SaveChoice(int UserID)
        {
            if(HttpContext.Current.Session["answer"] == null)
            {
                return;
            }
            List<Answer> answerList = (List<Answer>)HttpContext.Current.Session["answer"];
            
            foreach(Answer answer in answerList)
            {
                int questionID = answer.QuestionID;
                string text = answer.MyAnswer;
                using (SqlConnection connection = GetConnection())
                {
                    string str = "INSERT INTO Answer (userID,questionID,text)" +
                        " VALUES (@userID,@questionID,@text)";
                    SqlCommand command = new SqlCommand(str, connection);
                    command.Parameters.AddWithValue("@userID", UserID);
                    command.Parameters.AddWithValue("@questionID", questionID);
                    command.Parameters.AddWithValue("@text", text);

                    command.ExecuteNonQuery();
                }
            
            }
        }
        private static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SurveyConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}