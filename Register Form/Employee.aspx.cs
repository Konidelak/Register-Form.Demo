using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register_Form
{
    public partial class Employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                GetData();
            }
        }
        void GetData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());

            SqlDataAdapter da = new SqlDataAdapter("getemp", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "employee");
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string eno = TextBox1.Text;
            string ename = TextBox2.Text;
            string gender;
            if (RadioButton1.Checked == true)
            {
                gender = RadioButton1.Text;
            }
            else
            {
                gender = RadioButton2.Text;
            }
            string qualification = DropDownList1.SelectedItem.Text;
            string hobbies = "";
            if (CheckBox1.Checked == true)
            {
                hobbies = CheckBox1.Text;
            }
            if (CheckBox2.Checked == true)
            {
                hobbies = hobbies + ", " + CheckBox2.Text;
            }
            if (CheckBox3.Checked == true)
            {
                hobbies = hobbies + ", " + CheckBox3.Text;
            }
            if (CheckBox4.Checked == true)
            {
                hobbies = hobbies + ", " + CheckBox4.Text;
            }
            if (Button1.Text == "Save")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("saveemp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eno", eno);
                cmd.Parameters.AddWithValue("@ename", ename);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@qualification", qualification);
                cmd.Parameters.AddWithValue("@hobbies", hobbies);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                GetData();
                Clear();
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("updateemp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eno", eno);
                cmd.Parameters.AddWithValue("@ename", ename);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@qualification", qualification);
                cmd.Parameters.AddWithValue("@hobbies", hobbies);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                GetData();
                Button1.Text = "Save";
                Clear();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdedit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label l1 = (Label)row.FindControl("Label1");
                Label l2 = (Label)row.FindControl("Label2");
                Label l3 = (Label)row.FindControl("Label3");
                if (l3.Text == "Male")
                {
                    RadioButton1.Checked = true;
                }
                else

                {
                    RadioButton2.Checked = true;
                }
                Label l4 = (Label)row.FindControl("Label4");
                Label l5 = (Label)row.FindControl("Label5");


                TextBox1.Text = l1.Text;
                TextBox2.Text = l2.Text;
                DropDownList1.SelectedItem.Text = l4.Text;



                Button1.Text = "Update";
            }
            else
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label l1 = (Label)row.FindControl("Label1");
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("deleteemp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eno", l1.Text);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                GetData();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Clear();

        }
        public void Clear()
        {

            TextBox1.Text = "";
            TextBox2.Text = "";
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            DropDownList1.SelectedItem.Text = "--Select Qualification--";
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
        }
    }
}