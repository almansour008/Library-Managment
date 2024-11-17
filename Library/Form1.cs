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
    public partial class Form1 : Form
    {
        //this variables for move form without border
        int move;
        int movx;
        int movy;
        // this variables for sql connection
        SqlConnection con=new SqlConnection();
        SqlDataAdapter da=new SqlDataAdapter();
        SqlCommand cmd=new SqlCommand();
        List<String> list = new List<String>();
       

        //private string connectionString = "Server=DESKTOP-PQ3002Q\\SQLEXPRESS01; Database=DBBOOK; Integrated Security=True;";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (WindowState== FormWindowState.Normal)
            {
                WindowState= FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            movx = e.X;
            movy=e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(move==1)
            {
                this.SetDesktopLocation(MousePosition.X - movx, MousePosition.Y - movy);
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            //con.ConnectionString = (@"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\D\\The Fourth Year\\Data_Base_2\\Library\\Library\\DBBOOK.mdf\;Integrated Security=True");
            //con.ConnectionString = (@"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\The Fourth Year\Data_Base_2\Library\Library\DBBOOK.mdf;Integrated Security=True");
            // con.ConnectionString = (@"Data Source=DESKTOP-PQ3002Q\SQLEXPRESS01;Initial Catalog=DBBOOK;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            DataTable dt = new DataTable();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\The Fourth Year\\Data_Base_2\\Library\\Library\\DBBOOK.mdf\";Integrated Security=True";
            var sql = "SELECT Id,Title,Auther,Price,Category FROM Books";
            da = new SqlDataAdapter(sql,con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "التسلسل";
            dataGridView1.Columns[1].HeaderText = "العنوان";
            dataGridView1.Columns[2].HeaderText = "المؤلف";
            dataGridView1.Columns[3].HeaderText = "السعر";
            dataGridView1.Columns[4].HeaderText = "الصنف";

        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            FormAdd formadd = new FormAdd();
            formadd.btnadd.ButtonText = "إضافة";
            formadd.state = 0;
            bunifuTransition1.ShowSync(formadd);
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            FormAdd formadd = new FormAdd();
            formadd.btnadd.ButtonText = "تعديل";
            formadd.state =Convert.ToInt16( dataGridView1.CurrentRow.Cells[0].Value);
            bunifuTransition1.ShowSync(formadd);
            try
            {
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\The Fourth Year\\Data_Base_2\\Library\\Library\\DBBOOK.mdf\";Integrated Security=True";
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT Title,Auther,Price,Category,Date,Rate FROM Books where ID=@ID";
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value));
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(Convert.ToString(rd[0]));
                    list.Add(Convert.ToString(rd[1]));
                    list.Add(Convert.ToString(rd[2]));
                    list.Add(Convert.ToString(rd[3]));
                    list.Add(Convert.ToString(rd[4]));
                    list.Add(Convert.ToString(rd[5]));
                }
              // FormAdd frmAdd=new FormAdd();
                formadd.txtBookName.Text = list[0];
                formadd.txtBookAuther.Text = list[1];
                formadd.txtBookPrice.Text = list[2];
                formadd.txtCategory.Text = list[3];
                formadd.txtDate.Text =/*Convert.ToDateTime(list[4]);*/list[4];
                formadd.txtRate.Value =Convert.ToInt16( list[5]);
                con.Close();
                //Read Image from database
                con.Open();
                cmd.CommandText = "SELECT Cover FROM Books WHERE ID=@IDIMAGE";
                cmd.Parameters.AddWithValue("@IDIMAGE", Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value));
                byte[] img = (byte[])cmd.ExecuteScalar();
                MemoryStream ma = new MemoryStream();
                ma.Write(img,0,img.Length);
                formadd.txtPicture.Image=Image.FromStream(ma);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            cmd.Parameters.Clear();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\The Fourth Year\\Data_Base_2\\Library\\Library\\DBBOOK.mdf\";Integrated Security=True";
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "DELETE FROM Books WHERE ID=@ID";
            cmd.Parameters.AddWithValue("@ID", dataGridView1.CurrentRow.Cells[0].Value);
            cmd.ExecuteNonQuery();
            con.Close() ;
            FormDialogDelete formDialogDelete = new FormDialogDelete();
            formDialogDelete.Show();
            cmd.Parameters.Clear();

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            FormDetails formdetails = new FormDetails();
            bunifuTransition1.ShowSync(formdetails);
            try
            {
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\The Fourth Year\\Data_Base_2\\Library\\Library\\DBBOOK.mdf\";Integrated Security=True";
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT Title,Auther,Price,Category,Date,Rate FROM Books where ID=@ID";
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value));
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(Convert.ToString(rd[0]));
                    list.Add(Convert.ToString(rd[1]));
                    list.Add(Convert.ToString(rd[2]));
                    list.Add(Convert.ToString(rd[3]));
                    list.Add(Convert.ToString(rd[4]));
                    list.Add(Convert.ToString(rd[5]));
                }
                // FormAdd frmAdd=new FormAdd();
                formdetails.txtBookName.Text = list[0];
                formdetails.txtBookAuther.Text = list[1];
                formdetails.txtBookPrice.Text = list[2];
                formdetails.txtCategory.Text = list[3];
                formdetails.txtDate.Text =/*Convert.ToDateTime(list[4]);*/list[4];
                formdetails.txtRate.Value =Convert.ToInt16(list[5]);
                con.Close();
                //Read Image from database
                con.Open();
                cmd.CommandText = "SELECT Cover FROM Books WHERE ID=@IDIMAGE";
                cmd.Parameters.AddWithValue("@IDIMAGE", Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value));
                byte[] img = (byte[])cmd.ExecuteScalar();
                MemoryStream ma = new MemoryStream();
                ma.Write(img, 0, img.Length);
                formdetails.txtPicture.Image = Image.FromStream(ma);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            cmd.Parameters.Clear();
        }
    }
}
