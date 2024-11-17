using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library
{
    public partial class FormCategory : Form
    {
        //this variables for sql connection
        SqlConnection con=new SqlConnection();
        SqlCommand cmd= new SqlCommand();

        public FormCategory()
        {
            InitializeComponent();
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (txtAddCategory.Text != "")
            {
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\The Fourth Year\\Data_Base_2\\Library\\Library\\DBBOOK.mdf\";Integrated Security=True";
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO Category VALUES (@Category1)";
                cmd.Parameters.AddWithValue("@Category1", txtAddCategory.Text);
                //cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                con.Close();
                Form frmadd = new FormDialogAdd();
                frmadd.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("الرجاء إدخال اسم الصنف");
            }
        }

        private void txtAddCategory_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
