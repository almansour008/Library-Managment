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
using System.IO;

namespace Library
{
    public partial class FormAdd : Form
    {
        // this variables for sql connection
        SqlConnection con=new SqlConnection();
        SqlCommand cmd=new SqlCommand();
        List<string> list = new List<string>();
        public int state;
        public FormAdd()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox5_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formcategory=new FormCategory();
            bunifuTransition1.ShowSync(formcategory);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuMetroTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form formcategory = new FormCategory();
            bunifuTransition1.ShowSync(formcategory);
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
           
        }

        private void FormAdd_Activated(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\The Fourth Year\\Data_Base_2\\Library\\Library\\DBBOOK.mdf\";Integrated Security=True";
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT Category FROM Category";
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(Convert.ToString(rd[0]));
                }
                int i = 0;
                while (i < list.LongCount())
                {
                    txtCategory.Items.Add(list[i]);
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (txtBookAuther.Text=="" || txtBookName.Text==""||txtBookPrice.Text==""||txtCategory.Text=="")
            {
                MessageBox.Show("أكمل معلومات الكتاب أولا");
            }
            else
            {
                if(state==0)
                {
                    //Insert
                    //for convert image to binary
                    MemoryStream ma = new MemoryStream();
                    txtPicture.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var cover = ma.ToArray();
                    con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\The Fourth Year\\Data_Base_2\\Library\\Library\\DBBOOK.mdf\";Integrated Security=True";
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO Books (Title,Auther,Price,Category,Date,Rate,Cover) VALUES (@Title,@Auther,@Price,@Category,@Date,@Rate,@Cover)";
                    cmd.Parameters.AddWithValue("@Title", txtBookName.Text);
                    cmd.Parameters.AddWithValue("@Auther", txtBookAuther.Text);
                    cmd.Parameters.AddWithValue("@Price", int.Parse(txtBookPrice.Text));
                    cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                    cmd.Parameters.AddWithValue("@Rate", txtRate.Value);
                    cmd.Parameters.AddWithValue("@Cover", cover);
                    cmd.ExecuteNonQuery();
                    // cmd.ExecuteNonQuery();
                    con.Close();
                    Form frmadd = new FormDialogAdd();
                    frmadd.Show();
                }
                else
                {
                    //Edit
                    MemoryStream ma = new MemoryStream();
                    txtPicture.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var cover = ma.ToArray();
                    con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\The Fourth Year\\Data_Base_2\\Library\\Library\\DBBOOK.mdf\";Integrated Security=True";
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE Books SET Title=@Title,Auther=@Auther,Price=@Price,Category=@Category,Date=@Date,Rate=@Rate,Cover=@Cover where ID=@ID";
                    cmd.Parameters.AddWithValue("@Title", txtBookName.Text);
                    cmd.Parameters.AddWithValue("@Auther", txtBookAuther.Text);
                    cmd.Parameters.AddWithValue("@Price", int.Parse(txtBookPrice.Text));
                    cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                    cmd.Parameters.AddWithValue("@Rate", txtRate.Value);
                    cmd.Parameters.AddWithValue("@Cover", cover);
                    cmd.Parameters.AddWithValue("@ID", state);
                    cmd.ExecuteNonQuery();
                    // cmd.ExecuteNonQuery();
                    con.Close();
                    Form frmedit = new FormDialogEdit();
                    frmedit.Show();
                }
               
            }
            cmd.Parameters.Clear();
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dia=new OpenFileDialog ();
            var result=dia.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPicture.Image=Image.FromFile (dia.FileName);
            }
        }

        private void txtPicture_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
