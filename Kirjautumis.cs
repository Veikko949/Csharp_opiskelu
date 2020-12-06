using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotelli_Sivu
{
    public partial class Kirjautumis : Form
    {
        public Kirjautumis()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //mysql yhteys kirjautumis napiin
        private void KirjauduBT_Click(object sender, EventArgs e)
        {
            //Otaa käytäjät mysglästä
            Yhteys yht = new Yhteys();
            DataTable poyta = new DataTable();
            MySqlDataAdapter sovitin = new MySqlDataAdapter();
            MySqlCommand kasky = new MySqlCommand();
            string kysely = "SELECT * FROM `käyttäjät` WHERE `käyttäjä` =@usn AND `salasana` =@pass";

            //
            kasky.CommandText = kysely;
            kasky.Connection = yht.hankiYhdistys();

            //Otaa nimet Käyttäjä sekä salasana boxista
            kasky.Parameters.Add("@usn", MySqlDbType.VarChar).Value = KayttajaTB.Text;
            kasky.Parameters.Add("@pass", MySqlDbType.VarChar).Value = SaslasanaTB.Text;

            //
            try
            {
                sovitin.SelectCommand = kasky;
                sovitin.Fill(poyta);
            }

            catch {}

            //Jos on jo käytäjä ja salasana
            if(poyta.Rows.Count > 0)
            {
                //MessageBox.Show("Oikea käyttäjä");'
                this.Hide();
                Paasivu paas = new Paasivu();
                paas.Show();
            }
            else
            {
                //MessageBox.Show("VIRHE - Tyhjä kenttä");
                if (KayttajaTB.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Kirjoita käyttäjä", "Tyhjä Käyttäjä nimi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (SaslasanaTB.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Kirjoita salasana", "Tyhjä Käyttäjä salasana", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Tämä käyttäjä tai Salasana eivät ole olemassa", "Väärä käyttäjä", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
