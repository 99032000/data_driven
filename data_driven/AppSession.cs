using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient; //sql
using System.Configuration; //webconfig

namespace week2
{
    public class AppSession
    {
        public const string SESSION_QUESTION_NUMBER = "questionNumber";
        public const string SESSION_EXTRA_QUESTIONS = "extraQuestions";

        public static int getCurrentQuestionID()
        {
            //is there not a current question id stored in session for this client/user
            if (HttpContext.Current.Session[SESSION_QUESTION_NUMBER] == null)
            {
                using (SqlConnection connection = GetConnection())
                {
                    string commandStr = "SELECT MIN(ID) FROM Question ";
                    SqlCommand command = new SqlCommand(commandStr, connection);

                    //run command, collect results into reader
                    //SqlDataReader reader = command.ExecuteReader();
                    // When you select the maximum id you shouldn't use a SqlDataReader - the query returns just one item, 
                    // which by default is unnamed so your existing query breaks because it expects a result named 
                    HttpContext.Current.Session[SESSION_QUESTION_NUMBER] = Convert.ToInt32(command.ExecuteScalar());

                    connection.Close();
                }
            }
            return (int)HttpContext.Current.Session[SESSION_QUESTION_NUMBER];
        } 

        public static void setCurrentQuestionID(int q)
        {
            HttpContext.Current.Session[SESSION_QUESTION_NUMBER] = q;
        }

        public static void incrementLabelNumber()
        {
            int q = getQuestionID();
            q++;
            //save new number over the old number we had in session
            HttpContext.Current.Session["labelNumber"] = q;
        }

        public static List<int> getExtraQuestionsList()
        {
            if (HttpContext.Current.Session[SESSION_EXTRA_QUESTIONS] != null)
                return (List<int>)HttpContext.Current.Session[SESSION_EXTRA_QUESTIONS];

            return new List<int>();
        }
        public static void setExtraQuestionsList(List<int> extraQuestions)
        {
            HttpContext.Current.Session[SESSION_EXTRA_QUESTIONS] = extraQuestions;
        }
        // check extraQuestionList then back to normal code
        private static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SurveyConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }

        public static int getQuestionID()
        {
            if (HttpContext.Current.Session["labelNumber"] == null)
            {
                HttpContext.Current.Session["labelNumber"] = 1;
            }
            return (int)HttpContext.Current.Session["labelNumber"];
        }


    }
}