using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//new
using System.Data; //datatable
using System.Data.SqlClient; //sql stuff
using System.Configuration; //access web config file

namespace week2
{
    public partial class adminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            using (SqlConnection connection = GetConnection())
            {
                string str = "Select * From Admin Where userName = @username AND password = @password";
                SqlCommand command = new SqlCommand(str, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                if(command.ExecuteReader().HasRows)
                {
                    Response.Redirect("adminPage.aspx");
                }
                else
                {
                    messageLabel.Text = "username or password incorrect";
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