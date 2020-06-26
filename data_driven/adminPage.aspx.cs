using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient; //sql
using System.Configuration; //webconfig

namespace week2
{
    public partial class adminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            userGrid.DataSource = ShowUser();
            userGrid.DataBind();
            ageRangeDropdown.DataSource = ShowAge();
            ageRangeDropdown.DataTextField = "text";
            ageRangeDropdown.DataValueField = "optionID";
            
            ageRangeDropdown.DataBind();
            ageRangeDropdown.Items.Insert(0, new ListItem("", ""));
            StateDropdown.DataSource = ShowState();
            StateDropdown.DataTextField = "text";
            StateDropdown.DataValueField = "optionID";
            StateDropdown.DataBind();
            StateDropdown.Items.Insert(0, new ListItem("", ""));
            BankusedCheckBoxList.Items.Clear();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand optionCommand = new SqlCommand("SELECT * FROM Options WHERE questionID = 6", connection);
                //execute into reader
                SqlDataReader optionReader = optionCommand.ExecuteReader();
                while (optionReader.Read())
                {
                    //build list items from results
                    ListItem item = new ListItem(optionReader["text"].ToString(), optionReader["ID"].ToString());
                    BankusedCheckBoxList.Items.Add(item);

                }
                BankusedCheckBoxList.DataBind();
            }

        }
        private static void ShowBankused(object sender, EventArgs e)
        {
           
        }
        private static DataTable ShowState()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("optionID", typeof(Int32));
            dt.Columns.Add("text", typeof(String));
            DataRow row = dt.NewRow();
            row["optionID"] = 7;
            row["text"] = "NSW";
            dt.Rows.Add(row);
            DataRow row1 = dt.NewRow();
            row1["optionID"] = 8;
            row1["text"] = "VIC";

            dt.Rows.Add(row1);
            DataRow row2 = dt.NewRow();
            row2["optionID"] = 9;
            row2["text"] = "WA";
            dt.Rows.Add(row2);
            DataRow row3 = dt.NewRow();
            row3["optionID"] = 10;
            row3["text"] = "SA";
            dt.Rows.Add(row3);
            DataRow row4 = dt.NewRow();
            row4["optionID"] = 11;
            row4["text"] = "QLD";
            dt.Rows.Add(row4);
            DataRow row5 = dt.NewRow();
            row5["optionID"] = 12;
            row5["text"] = "TAS";
            dt.Rows.Add(row5);
            DataRow row6 = dt.NewRow();
            row6["optionID"] = 13;
            row6["text"] = "ACT";
            dt.Rows.Add(row6);
            DataRow row7 = dt.NewRow();
            row7["optionID"] = 14;
            row7["text"] = "NT";
            dt.Rows.Add(row7);
            return dt;
        }
        private static DataTable ShowAge()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("optionID", typeof(Int32));
            dt.Columns.Add("text", typeof(String));
            DataRow row = dt.NewRow();
            row["optionID"] = 4;
            row["text"] = "under 18";
            dt.Rows.Add(row);
            DataRow row1 = dt.NewRow();
            row1["optionID"] = 5;
            row1["text"] = "18-45";
            
            dt.Rows.Add(row1);
            DataRow row2 = dt.NewRow();
            row2["optionID"] = 6;
            row2["text"] = "45 +";
            dt.Rows.Add(row2);
            return dt;
        }

        private static DataTable ShowUser()
        {
            using (SqlConnection connection = GetConnection())
            {
                string str = "Select * From [User]";
                SqlCommand command = new SqlCommand(str, connection);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                //setup columns
                dt.Columns.Add("UserID", typeof(Int32));
                dt.Columns.Add("FirstName", typeof(String));
                dt.Columns.Add("LastName", typeof(String));
                dt.Columns.Add("DOB", typeof(DateTime));
                dt.Columns.Add("PhoneNo", typeof(String));
                dt.Columns.Add("IP", typeof(String));
                dt.Columns.Add("DateCreate", typeof(DateTime));

                while (reader.Read())//works with 1 row at a time
                {
                    //generate empty row for table
                    DataRow row = dt.NewRow();
                    //copy values across
                    row["userID"] = reader["UserID"];
                    row["FirstName"] = reader["firstName"];
                    row["LastName"] = reader["lastName"];
                    row["DOB"] = reader["DOB"];
                    row["phoneNo"] = reader["phoneNo"];
                    row["IP"] = reader["IP"];
                    row["DateCreate"] = reader["dateCreate"];
                    //add this row to our data table
                    dt.Rows.Add(row);
                }

                return dt;

            }
        }
        private static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SurveyConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }

        protected void Search_Click(object sender, EventArgs e)
        {

        }

        protected void ShowAllUser_Click(object sender, EventArgs e)
        {

        }
    }
}