using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//reference namespaces
using System.Data.SqlClient; //sql
using System.Configuration; //webconfig

namespace week2
{
    public partial class QuestionPage : System.Web.UI.Page
    {
        const string TEXTBOX_ID = "textBoxControl";
        const string CHECKBOX_ID = "checkBoxControl";
        const string RADIOBOX_ID = "radioboxControl";
        // private string questionID;

        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentQuestionLabel.Text = "Question " + AppSession.getQuestionID();

            int currentQuestion = AppSession.getCurrentQuestionID();

            using (SqlConnection connection = GetConnection())
            {
                //Console.WriteLine("I am here using");
                string commandStr = "SELECT * FROM Question, Type " +
                    "WHERE Question.type = Type.ID AND Question.ID = " + currentQuestion;
                SqlCommand command = new SqlCommand(commandStr, connection);

                //run command, collect results into reader
                SqlDataReader reader = command.ExecuteReader();

                //see if we have at least 1 result
                if (reader.Read())
                {
                    //read basic info from result row
                    string questionText = (string)reader["text"];

                    string questionType = reader["Name"].ToString().ToLower();

                    if (questionType.Equals("textbox"))
                    {
                        //load up control
                        TextBoxControl textBoxControl = (TextBoxControl)LoadControl("~/TextBoxControl.ascx");

                        //set it up
                        textBoxControl.ID = TEXTBOX_ID;
                        textBoxControl.QuestionLabel.Text = questionText;

                        //add to placeholder
                        questionPlaceHolder.Controls.Add(textBoxControl);
                    }
                    else if (questionType.Equals("checkbox"))
                    {
                        CheckBoxControl checkBoxControl = (CheckBoxControl)LoadControl("~/CheckBoxControl.ascx");
                        checkBoxControl.ID = CHECKBOX_ID;
                        checkBoxControl.QuestionLabel.Text = questionText;

                        //ask DB for options associated with this question
                        SqlCommand optionCommand = new SqlCommand("SELECT * FROM Options WHERE questionID = " + currentQuestion, connection);
                        //execute into reader
                        SqlDataReader optionReader = optionCommand.ExecuteReader();

                        //loop through results(1 row at a time
                        while (optionReader.Read())
                        {
                            //build list items from results
                            ListItem item = new ListItem(optionReader["text"].ToString(), optionReader["ID"].ToString());

                            checkBoxControl.QuestionCheckBoxList.Items.Add(item);
                        }

                        //add loaded control to the placeholder
                        questionPlaceHolder.Controls.Add(checkBoxControl);
                    }
                    else if (questionType.Equals("radiochecklist"))
                    {
                        Console.WriteLine("I am here");
                        RadioBoxControl radioBoxControl = (RadioBoxControl)LoadControl("~/RadioBoxControl.ascx");
                        radioBoxControl.ID = RADIOBOX_ID;
                        radioBoxControl.QuestionLabel.Text = questionText;

                        SqlCommand optionCommand = new SqlCommand("SELECT * FROM Options WHERE questionID = " + currentQuestion, connection);

                        SqlDataReader optionReader = optionCommand.ExecuteReader();

                        //loop through results(1 row at a time
                        while (optionReader.Read())
                        {

                            ListItem item = new ListItem(optionReader["text"].ToString(), optionReader["ID"].ToString());
                            radioBoxControl.QuestionRadioBoxList.Items.Add(item);
                        }


                        questionPlaceHolder.Controls.Add(radioBoxControl);
                    }
                }

            }//closes db connection for us here








        }

        private static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SurveyConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
            int currentQuestion = AppSession.getCurrentQuestionID();
            AppSession.incrementLabelNumber();
            List<int> extraQuestions = AppSession.getExtraQuestionsList();
            if (HttpContext.Current.Session["answer"] == null)
            {
                List<Answer> answerList = new List<Answer>();
                HttpContext.Current.Session["answer"] = answerList;
            }
            //setup DB connection
            using (SqlConnection connection = GetConnection())
            {
                //check for answers user has filled in on the form

                //check to see if previously a textbox control was dynamically added to the screen
                TextBoxControl textBoxControl = (TextBoxControl)questionPlaceHolder.FindControl(TEXTBOX_ID);//find control is looking for id of something
                if (textBoxControl != null)
                {
                    //tring typedAnswer = textBoxControl.QuestionTextBox.Text;
                    //TODO: store in session so that later it can be stored into the DB
                    List<Answer> answerList = (List<Answer>)HttpContext.Current.Session["answer"];
                    Answer answer = new Answer();
                    answer.QuestionID = AppSession.getCurrentQuestionID();
                    answer.MyAnswer = textBoxControl.QuestionTextBox.Text;
                    answerList.Add(answer);
                    HttpContext.Current.Session["answer"] = answerList;

                }
                RadioBoxControl radioBoxControl = (RadioBoxControl)questionPlaceHolder.FindControl(RADIOBOX_ID);
                if (radioBoxControl != null)
                {


                    foreach (ListItem item in radioBoxControl.QuestionRadioBoxList.Items)
                    {
                        if (item.Selected)
                        {
                            //store in session and break the loop coz only one radio can be selected.
                            List<Answer> answerList = (List<Answer>)HttpContext.Current.Session["answer"];
                            Answer answer = new Answer();
                            answer.QuestionID = AppSession.getCurrentQuestionID();
                            answer.MyAnswer = item.Value;
                            answerList.Add(answer);
                            HttpContext.Current.Session["answer"] = answerList;

                            string commandStr = "Select * From Options Where ID = " + item.Value;
                            SqlCommand command = new SqlCommand(commandStr, connection);

                            //run command, collect results into reader
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                if (reader["extraQuestionID"] != System.DBNull.Value)
                                {
                                    extraQuestions.Add((int)reader["extraQuestionID"]);
                                }
                            }
                            break;
                        }
                    }
                }
                //check to see if it was a checkbox question
                CheckBoxControl checkBoxControl = (CheckBoxControl)questionPlaceHolder.FindControl(CHECKBOX_ID);
                if (checkBoxControl != null)
                {
                    //loop through the checkboxes
                    foreach (ListItem item in checkBoxControl.QuestionCheckBoxList.Items)
                    {
                        if (item.Selected)
                        {
                            //TODO: store item.Value(which is the optionID) in session to store later in DB
                            //TODO: if selected option leads to extra questions, here is where to check that
                            //      check this option against DB table and see if it has a foreign key to next question
                            //      if it does, add it to extra questions
                            //item.Value
                            List<Answer> answerList = (List<Answer>)HttpContext.Current.Session["answer"];
                            Answer answer = new Answer();
                            answer.QuestionID = AppSession.getCurrentQuestionID();
                            answer.MyAnswer = item.Value;
                            answerList.Add(answer);
                            HttpContext.Current.Session["answer"] = answerList;

                            string commandStr = "Select * From Options Where ID = " + item.Value;
                            SqlCommand command = new SqlCommand(commandStr, connection);

                            //run command, collect results into reader
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                if (reader["extraQuestionID"] != System.DBNull.Value)
                                {
                                    if (!extraQuestions.Contains((int)reader["extraQuestionID"]))
                                    {
                                        extraQuestions.Add((int)reader["extraQuestionID"]);
                                    }
                                }
                            }

                        }
                    }
                }

                //TODO: Dont hard code extra questions like this, this is purely for example
                /*
                if(currentQuestion == 1)
                {
                    extraQuestions.Add(3);
                    extraQuestions.Add(4);
                }
                */

                //GO TO NEXT QUESTION
                //===================
                if (extraQuestions.Count > 0)
                {
                    //use first question in list
                    int nextQuestion = extraQuestions[0];
                    //erase from list
                    extraQuestions.Remove(nextQuestion);
                    //store list of extra questions
                    AppSession.setExtraQuestionsList(extraQuestions);

                    //set currentQuestion in session to this value
                    AppSession.setCurrentQuestionID(nextQuestion);
                    //reload page
                    Response.Redirect("QuestionPage.aspx");

                }
                else
                {
                    //use default navigation from 1 question to the next
                    SqlCommand command = new SqlCommand("SELECT * FROM Question WHERE ID = " + currentQuestion, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    //read first row (dont care if multiple are returned)
                    if (reader.Read())
                    {
                        //get column index(ordinal) of nextQuestion
                        int nextQuestionColumnIndex = reader.GetOrdinal("nextQuestion");
                        //check if the value inside this column for this row of results is NULL or not
                        if (reader.IsDBNull(nextQuestionColumnIndex))
                        {
                            //its the end of the survey, go to thank you page or registration
                            //maybe store all answers in DB
                            // Console.WriteLine("all good yea!!!");
                            Response.Redirect("registration.aspx");
                        }
                        else
                        {
                            //if not null, use this value to help navigate to next question
                            AppSession.setCurrentQuestionID((int)reader["nextQuestion"]);
                            //reload the page so that pageLoad loads correct new question up
                            Response.Redirect("QuestionPage.aspx");
                        }
                    }
                    else
                    {
                        //cant find this question in the DB??!!??!
                    }
                    reader.Close();

                }
                

            }
            
        }
    }
}