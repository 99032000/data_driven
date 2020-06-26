using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;//sql
using System.Configuration; //webconfig

namespace week2
{
    public partial class AddProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            refreshList(getListOfProductsFromSession());
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product();
                p.Name = NameTextBox.Text;
                p.Description = DescriptionTextBox.Text;
                p.Price = float.Parse(PriceTextBox.Text);//maybe exception here
                //Console.Write(p.Description);
                List<Product> products = getListOfProductsFromSession();
                products.Add(p);
                //store list in session
                HttpContext.Current.Session["products"] = products;

                refreshList(products);
            }
            catch(Exception ex)
            {
                //probably thrown because typed in not a number into price textbox
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            List<Product> products = getListOfProductsFromSession();

            using(SqlConnection connection = ConnectToSqlDB())
            {
                foreach(Product p in products)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Products (name, description, price) VALUES ('"+p.Name+"','"+p.Description+"',"+p.Price+")", connection);

                    int rowsAffected = command.ExecuteNonQuery();//for insert, update, delete
                    if(rowsAffected <= 0)
                    {
                        //did not insert this product, do something about it maybe
                    }
                }
            }
            //empty product list
            products.Clear();
            HttpContext.Current.Session["products"] = products;
            refreshList(products);
        }
        private static List<Product> getListOfProductsFromSession()
        {
            if (HttpContext.Current.Session["products"] != null)
                return (List<Product>)HttpContext.Current.Session["products"];

            return new List<Product>();
        }

        private void refreshList(List<Product> products)
        {
            //clear the list
            ProductBulletedList.Items.Clear();
            //loop through products and add to list
            foreach(Product p in products)
            {
                ProductBulletedList.Items.Add(new ListItem(p.Name));
            }
        }
        private static SqlConnection ConnectToSqlDB()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }

    }
}