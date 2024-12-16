using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace example1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private void FillData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from Employee", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Employee");
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { FillData(); }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            TextBox l1 = (TextBox)row.FindControl("TextBox1");
            //Label l1 = (Label)row.FindControl("label1");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            con.Open();
            string query = "delete from Employee where eno='" + l1.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillData();

        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            FillData();

        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            TextBox t1 = (TextBox)row.FindControl("TextBox1");
            TextBox t2 = (TextBox)row.FindControl("TextBox2");
            TextBox t3 = (TextBox)row.FindControl("TextBox3");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            con.Open();
            string query = "update Employee set ename='" + t2.Text + "',salary='" + t3.Text + "' where eno='" + t1.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            FillData();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            FillData();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.FooterRow;
            TextBox t1 = (TextBox)row.FindControl("txteno");
            TextBox t2 = (TextBox)row.FindControl("txtename");
            TextBox t3 = (TextBox)row.FindControl("txtsalary");

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            con.Open();
            string query = "insert into Employee values('" + t1.Text + "','" + t2.Text + "','" + t3.Text + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillData();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            FillData();

        }
        

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow row = GridView1.SelectedRow;
            
            /*Label L1 =(Label)row.FindControl("Label1");
            Label l2 = (Label)row.FindControl("Label2");
            Label l3 = (Label)row.FindControl("Label3");
            TextBox7.Text = l1.Text;
            TextBox8.Text = l2.Text;
            TextBox9.Text = l3.Text;*/

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            con.Open();
            string query = "update Employee set ename='" + TextBox7.Text + "' salary='" + TextBox8.Text + "' where eno='" + TextBox9.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            FillData();
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
        }

    }
}