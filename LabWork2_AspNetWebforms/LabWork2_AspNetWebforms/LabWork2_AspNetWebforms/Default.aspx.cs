using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace LabWork2_AspNetWebforms
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=EVAD-PC\SQLEXPRESS;Initial Catalog=Students;Integrated Security=True");
        SqlDataAdapter sda;
        protected System.Data.SqlClient.SqlDataAdapter da1;
        protected System.Data.SqlClient.SqlDataAdapter da2;

        protected System.Data.DataSet ds1;
        protected System.Data.SqlClient.SqlCommandBuilder b1;

        protected override void OnInitComplete(EventArgs e)
        {
            ds1 = new DataSet();
            base.OnInitComplete(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                refreshdata();
            } 
        }



        public void refreshdata()
        {
            da1 = new System.Data.SqlClient.SqlDataAdapter("SELECT DISTINCT GroupItem FROM Student ORDER BY GroupItem", this.con);

            da1.Fill(ds1, "tab1");

            da2 = new System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Student  where GroupItem = @p", this.con);
            da2.SelectCommand.Parameters.Add("@p", System.Data.SqlDbType.VarChar, 50);
            b1 = new System.Data.SqlClient.SqlCommandBuilder(da2);

            this.DropDownList1.DataSource = ds1.Tables["tab1"];
            this.DropDownList1.DataTextField = "GroupItem";
            this.DropDownList1.DataBind();
            this.DropDownList1.AutoPostBack = true;

            //  Определим параметр и заполним элемент DataGrid:
            da2.SelectCommand.Parameters["@p"].Value = this.DropDownList1.SelectedItem.ToString();
            da2.Fill(ds1, "tab2");
            this.GridView1.DataSource = ds1.Tables["tab2"];
            //    this.GridView1.DataKeyField = "номер_студента";
        //    if (!this.IsPostBack)
            {
                this.GridView1.DataBind();
            }


        }


        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            refreshdata();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
            SqlCommand cmd = new SqlCommand("delete from [Students].[dbo].[Student] where id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            refreshdata();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            refreshdata();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
            TextBox tbxlname = GridView1.Rows[e.RowIndex].FindControl("TextBox2") as TextBox;
            TextBox tbxGroup = GridView1.Rows[e.RowIndex].FindControl("TextBox3") as TextBox;
            TextBox txtSalary = GridView1.Rows[e.RowIndex].FindControl("TextBox4") as TextBox;
            TextBox tbxDate = GridView1.Rows[e.RowIndex].FindControl("TextBox5") as TextBox;
            SqlCommand cmd = new SqlCommand("update [Students].[dbo].[Student] set [LName]=@lname, [GroupItem]=@group,[Salary]=@salary,[DateItem]=@date where id =@id", con);


            cmd.Parameters.AddWithValue("@lname", tbxlname.Text);
            cmd.Parameters.AddWithValue("@group", tbxGroup.Text);
            cmd.Parameters.AddWithValue("@salary", txtSalary.Text);
            cmd.Parameters.AddWithValue("@date", tbxDate.Text);

            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            refreshdata();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataRow r = ds1.Tables["tab2"].NewRow();

            r["LName"] = "";
            r["GroupItem"] = this.DropDownList1.SelectedItem.ToString();
            r["DateItem"] = "01/01/2000";
            // TODO: 
            ds1.Tables["tab2"].Rows.Add(r);
            da2.Update(ds1, "tab2");

            refreshdata();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}