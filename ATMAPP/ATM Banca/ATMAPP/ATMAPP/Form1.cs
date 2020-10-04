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

namespace ATMAPP
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\source\repos\ATMAPP\ATMAPP\Info.mdf;Integrated Security=True");

        public Form1(string Str_Value)
        {
            InitializeComponent();
            label1.Text = Str_Value;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Visible == false)
            {
                textBox2.Visible = true;
                textBox4.Visible = true;
                button5.Visible = true;
                label5.Visible = true;
                label7.Visible = true;
            }
            else
            {
                textBox2.Visible = false;
                textBox4.Visible = false;
                button5.Visible = false;
                label5.Visible = false;
                label7.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible == false)
            {
                textBox1.Visible = true;
                textBox3.Visible = true;
                button4.Visible = true;
                label4.Visible = true;
                label6.Visible = true;
            }
            else
            {
                textBox1.Visible = false;
                textBox3.Visible = false;
                button4.Visible = false;
                label4.Visible = false;
                label6.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "")
            {
                DialogResult result = MessageBox.Show("Doriti sa efectuati operatia?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("Select Credit From UserInfo where CodID='" + textBox1.Text + "' ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count != 0)
                    {
                        int suma = StringToInt(textBox3.Text);
                        int contut = StringToInt(dt.Rows[0][0].ToString());

                        SqlDataAdapter rda = new SqlDataAdapter("Select Credit From UserInfo where UserID='" + label1.Text + "' ", con);
                        DataTable dtmeu = new DataTable();
                        rda.Fill(dtmeu);

                        int contmeu = StringToInt(dtmeu.Rows[0][0].ToString());

                        if (suma > contmeu)
                        {
                            MessageBox.Show("Fonduri insuficiente!");
                            textBox1.Text = textBox3.Text = "";
                        }
                        else
                        {
                            contmeu -= suma;

                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = "Update UserInfo Set Credit = @Cont Where UserID='" + label1.Text + "'";
                            cmd.Parameters.AddWithValue("@Cont", contmeu);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            contut += suma;

                            SqlCommand cmd1 = new SqlCommand();
                            cmd1.CommandText = "Update UserInfo Set Credit = @Contut Where CodID='" + textBox1.Text + "'";
                            cmd1.Parameters.AddWithValue("@Contut", contut);
                            cmd1.Connection = con;
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();

                            MessageBox.Show("Tranzactia a fost efectuata cu succes! Fonduri:" + contmeu);
                            textBox1.Text = textBox3.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cod utilizator gresit!");
                        textBox1.Text = textBox3.Text = "";
                    }
                }
                else if (result == DialogResult.No) textBox1.Text = textBox3.Text = "";
            }
        }

        int StringToInt(string SIR)
        {
            int k = SIR.Length - 1;
            int p = 1;
            int rez1 = 0;
            while (k >= 0 && SIR[k] == '0')
            {
                p = p * 10;
                --k;
            }
            for (; k >= 0; --k) rez1 = rez1 * 10 + SIR[k] - '0';
            int rezultat = 0;
            while (rez1 > 0)
            {
                rezultat = rezultat * 10 + (rez1 % 10);
                rez1 /= 10;
            }
            rezultat *= p;
            return rezultat;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
