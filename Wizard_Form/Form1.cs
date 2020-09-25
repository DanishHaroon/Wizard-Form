using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Wizard_Form
{
    public partial class Form1 : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        dbConnection dbcon = new dbConnection();
        bool checkCancel = true;
        public Form1()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public string GetSelectedRadioButtonText(RadioButton[] radioButtons)
        {
            // possible additional checks: check multiple selected, check if at least one is selected and generate more descriptive exception.

            // works for above cases, but throws a generic InvalidOperationException if it fails
            return radioButtons.Single(r => r.Checked).Text;
        }

        private void signup_Click(object sender, EventArgs e)
        {
            string email = txtemail.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            try
            {
                if (txtpass.Text != txtconfirmpass.Text)
                {
                    MessageBox.Show("Password didn't Match", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if ((txtfirst.Text == string.Empty) || (txtlast.Text == string.Empty))
                {
                    MessageBox.Show("Please Enter First or Last Name", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if ((txtemail.Text == string.Empty) || (txtpass.Text == string.Empty))
                {
                    MessageBox.Show("Please Enter Must Email Or Password", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (match.Success)
                {

                }
                else
                {
                    MessageBox.Show("Please Enter Correct Email", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cn.Open();
                cm = new SqlCommand("insert into tbldata(firstname,lastname,email,password,gender,dob,contact,cnic)values(@firstname,@lastname,@email,@password,@gender, @dob, @contact, @cnic)", cn);
                cm.Parameters.AddWithValue("@firstname", txtfirst.Text);
                cm.Parameters.AddWithValue("@lastname", txtlast.Text);
                cm.Parameters.AddWithValue("@email", txtemail.Text);
                cm.Parameters.AddWithValue("@password", txtpass.Text);
                cm.Parameters.AddWithValue("@gender", this.GetSelectedRadioButtonText(new[] { male, female }));
                cm.Parameters.AddWithValue("@dob", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cm.Parameters.AddWithValue("@contact", txtcontactnumber.Text);
                cm.Parameters.AddWithValue("@cnic", txtcnic.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Record Has been Saved!", "Sign Up!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                LoadRecords();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);


            }

        }

        public void clear()
        {

            txtfirst.Clear();
            txtlast.Clear();
            txtemail.Clear();
            txtpass.Clear();
            txtconfirmpass.Clear();
            txtcnic.Clear();
            dateTimePicker1.Value = DateTime.Now;
            txtcontactnumber.Clear();


        }

        private void next_Click(object sender, EventArgs e)
        {
            checkCancel = false;
            form.SelectTab("tabPage2");
        }

        private void previous_Click(object sender, EventArgs e)
        {

            checkCancel = false;
            form.SelectTab("tabPage1");
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRecords();
        }
        public void LoadRecords()
        {
            try
            {
                int i = 0;
                cn.Open();
                cm = new SqlCommand("Select * from tblData", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void c(object sender, PaintEventArgs e)
        {

        }

        private void female_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void male_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtcontactnumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtfirst_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcnic_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtconfirmpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtlast_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SeignIn_Click(object sender, EventArgs e)
        {

        }

        private void SeignIn_Click_1(object sender, EventArgs e)
        {
           

                try
                {
                Int32 index = dataGridView1.Rows.Count - 1;
                if ((txtemail1.Text == string.Empty) || (txtpassword.Text == string.Empty))
                {
                    MessageBox.Show("Please Enter Email & Password !");
                    return;
                }
                    cn.Open();
                    cm = new SqlCommand("select * from tbldata where email = @email and password = @password", cn);
                    cm.Parameters.AddWithValue("@email", txtemail1.Text);
                    cm.Parameters.AddWithValue("@password", txtpassword.Text);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if(dr.HasRows)
                    {

                    checkCancel = false;
                    form.SelectTab("tabPage3");
                        dataGridView1.Rows[index].Selected = true;

                }
                else
                    {
                    
                    cn.Close();
                    MessageBox.Show("Invalid email and password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    dr.Close();
                    cn.Close();

                }catch (Exception ex)

                {
                    cn.Close();
                    MessageBox.Show(ex.Message);
                }
            }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void form_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void form_Selecting(object sender, TabControlCancelEventArgs e)
        {

            e.Cancel = checkCancel;
            checkCancel = true;
        }
    }

    }

