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
    public partial class Form3 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\source\repos\ATMAPP\ATMAPP\Info.mdf;Integrated Security=True");

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = CONFIRMA1.Text + "1";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = CONFIRMA1.Text + "2";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = CONFIRMA1.Text + "3";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = CONFIRMA1.Text + "4";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = CONFIRMA1.Text + "5";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = CONFIRMA1.Text + "6";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = CONFIRMA1.Text + "7";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = CONFIRMA1.Text + "8";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = CONFIRMA1.Text + "9";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = CONFIRMA1.Text + "0";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (CONFIRMA1.Text.Length > 0)
                CONFIRMA1.Text = CONFIRMA1.Text.Substring(0, CONFIRMA1.Text.Length - 1);
        }

        private void CONFIRMA1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select UserID, Nume, Prenume From UserInfo where PIN='" + CONFIRMA1.Text + "' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                MessageBox.Show("Autentificarea a fost realizata cu succes!");
                label7.Text = dt.Rows[0][0].ToString();
                label5.Text = dt.Rows[0][1].ToString() + " " + dt.Rows[0][2].ToString();
                label5.Visible = true;
                button19.Visible = true;
                button20.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label4.Visible = true;
                label3.Visible = true;
                button21.Visible = true;
                button22.Visible = true;
                button2.Visible = false;
                label6.Visible = true;
                button3.Enabled = false;
                button1.Enabled = false;
                button1.Visible = false;
                button24.Visible = true;
                button24.Enabled = true;
                button2.Enabled = false;
                button25.Visible = true;
                button25.Enabled = true;
            }
            else
            {
                MessageBox.Show("Cod pin gresit! Va rugam incercati din nou cu atentie!");
            }
            CONFIRMA1.Text = "";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Credit, Datorii From UserInfo where UserID='" + label7.Text + "' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MessageBox.Show("Sold curent:" + dt.Rows[0][0].ToString() + ", Datorii:" + dt.Rows[0][1].ToString());
        }


        private void button20_Click(object sender, EventArgs e)
        {
            if (CONFIRMA1.Text != "")
            {
                DialogResult result = MessageBox.Show("Doriti sa efectuati operatia?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("Select Credit From UserInfo where UserID='" + label7.Text + "' ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    int sumafinala = StringToInt(CONFIRMA1.Text);
                    int cont = StringToInt(dt.Rows[0][0].ToString());

                    if (sumafinala > cont)
                    {
                        MessageBox.Show("Fonduri insuficiente!");
                        CONFIRMA1.Text = "";
                    }
                    else
                    {
                        cont -= sumafinala;
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "Update UserInfo Set Credit = @Cont Where UserID='" + label7.Text + "'";
                        cmd.Parameters.AddWithValue("@Cont", cont);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Tranzactia a fost efectuata cu succes! Fonduri:" + cont);
                        CONFIRMA1.Text = "";
                    }
                }
                else if (result == DialogResult.No) CONFIRMA1.Text = "";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            CONFIRMA1.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (CONFIRMA1.Text != "")
            {
                DialogResult result = MessageBox.Show("Doriti sa efectuati operatia?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("Select Credit From UserInfo where UserID='" + label7.Text + "' ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    int sumafinala = StringToInt(CONFIRMA1.Text) + StringToInt(dt.Rows[0][0].ToString());

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Update UserInfo Set Credit = @Cont Where UserID='" + label7.Text + "'";
                    cmd.Parameters.AddWithValue("@Cont", sumafinala);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Tranzactia a fost efectuata cu succes! Fonduri:" + sumafinala);
                    CONFIRMA1.Text = "";
                }
                else if (result == DialogResult.No) CONFIRMA1.Text = "";
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (button18.Visible == true && button23.Visible == true)
            {
                button18.Visible = false;
                button23.Visible = false;
            }
            else
            {
                button18.Visible = true;
                button23.Visible = true;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (CONFIRMA1.Text != "")
            {
                DialogResult result = MessageBox.Show("Doriti sa efectuati operatia?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("Select Credit, Datorii From UserInfo where UserID='" + label7.Text + "' ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    int datorii = StringToInt(dt.Rows[0][1].ToString());
                    int credit = StringToInt(dt.Rows[0][0].ToString());
                    int suma = StringToInt(CONFIRMA1.Text);

                    datorii += suma;
                    credit += suma;

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Update UserInfo Set Datorii = @Datorie, Credit = @Cont Where UserID='" + label7.Text + "'";
                    cmd.Parameters.AddWithValue("@Datorie", datorii);
                    cmd.Parameters.AddWithValue("@Cont", credit);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Tranzactia a fost efectuata cu succes! Datorii:" + datorii);
                    CONFIRMA1.Text = "";
                }
                else if (result == DialogResult.No) CONFIRMA1.Text = "";
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

        private void button18_Click(object sender, EventArgs e)
        {
            if (CONFIRMA1.Text != "")
            {
                DialogResult result = MessageBox.Show("Doriti sa efectuati operatia?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("Select Credit, Datorii From UserInfo where UserID='" + label7.Text + "' ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    int credit = StringToInt(dt.Rows[0][0].ToString());
                    int suma = StringToInt(CONFIRMA1.Text);
                    int datorii = StringToInt(dt.Rows[0][1].ToString());

                    if (datorii == 0) MessageBox.Show("Nu exista datorii!");
                    else
                    {
                        if (suma <= credit)
                        {
                            if (suma >= datorii)
                            {
                                credit -= datorii;
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandText = "Update UserInfo Set Datorii = @Datorie, Credit = @Cont Where UserID='" + label7.Text + "'";
                                cmd.Parameters.AddWithValue("@Datorie", 0);
                                cmd.Parameters.AddWithValue("@Cont", credit);
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                MessageBox.Show("Tranzactia a fost efectuata cu succes! Datorii:" + 0);
                            }
                            else
                            {
                                credit -= suma;
                                datorii -= suma;
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandText = "Update UserInfo Set Datorii = @Datorie, Credit = @Cont Where UserID='" + label7.Text + "'";
                                cmd.Parameters.AddWithValue("@Datorie", datorii);
                                cmd.Parameters.AddWithValue("@Cont", credit);
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                MessageBox.Show("Tranzactia a fost efectuata cu succes! Datorii:" + datorii);
                            }
                        }
                        else MessageBox.Show("Fonduri insuficiente!");
                    }
                    CONFIRMA1.Text = "";
                }
                else if(result == DialogResult.No) CONFIRMA1.Text = "";
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            DialogResult iesire;

            iesire = MessageBox.Show("Doriti sa parasiti aplicatia?", "ATM BANCA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (iesire == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(label7.Text);
            form1.ShowDialog();
        }
    }
}

