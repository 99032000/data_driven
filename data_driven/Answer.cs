using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace week2
{
    public class Answer
    {
        private int ID;

        public int QuestionID
        {
            get { return ID; }
            set { ID = value; }
        }
        private string myAnswer;

        public string MyAnswer
        {
            get { return myAnswer; }
            set { myAnswer = value; }
        }


    }
}