using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hotelli_Sivu
{
    //Huoneet
    class HUONE
    {

        Yhteys yht = new Yhteys();

        //Otaa listan huoneista mysglästä
        public DataTable huneenTyyliLisata()
        {
            MySqlCommand kasky = new MySqlCommand("SELECT * FROM `huoneet_katergoria`", yht.hankiYhdistys());
            MySqlDataAdapter Sovitin = new MySqlDataAdapter();
            DataTable poyta = new DataTable();

            Sovitin.SelectCommand = kasky;
            Sovitin.Fill(poyta);

            return poyta;
        }

        //Lisiää uuden huoneen mysqlään
        public bool lisaaHuone(int numero, int tyyli, String puhelin, String vapaa)
        {
                //Otta tiedot mysql
                MySqlCommand kasky = new MySqlCommand();
                string LisaaKysely = "INSERT INTO `huone`(`numero`, `tyyli`, `puhelin`, `vapaa`) VALUES (@num,@tp,@phn,@fr)";
                kasky.CommandText = LisaaKysely;
                kasky.Connection = yht.hankiYhdistys();

                //@num,@tp,@phn,@fr
                //Kertoo mitkä tiedot pitää hakea Hallitse_Huoneita 
                kasky.Parameters.Add("@num", MySqlDbType.Int32).Value = numero;
                kasky.Parameters.Add("@tp", MySqlDbType.Int32).Value = tyyli;
                kasky.Parameters.Add("@phn", MySqlDbType.VarChar).Value = puhelin;
                kasky.Parameters.Add("@fr", MySqlDbType.VarChar).Value = vapaa;

                //Avaaa ja sulkee yhteyden myql
                yht.avaaYhdista();

                if (kasky.ExecuteNonQuery() == 1)
                {
                    yht.suljeYhdista();
                    return true;
                }
                else
                {
                    yht.suljeYhdista();
                    return false;
                }
        }

        //Hankii listan kaikista huoneista mysqlästä
        public DataTable hankiHuone()
        {
            MySqlCommand kasky = new MySqlCommand("SELECT * FROM `huone`", yht.hankiYhdistys());
            MySqlDataAdapter Sovitin = new MySqlDataAdapter();
            DataTable poyta = new DataTable();

            Sovitin.SelectCommand = kasky;
            Sovitin.Fill(poyta);

            return poyta;
        }

        //Muokaa valittua riviä
        public bool muokaaHuoneita(int numero, int tyyli, String puhelin, String vapaa)
        {
            //Otta tiedot mysql
            MySqlCommand kasky = new MySqlCommand();
            string muokaaKysely = "UPDATE `huone` SET `tyyli`=@tp,`puhelin`=@phn,`vapaa`=@fr `numero`=@mun";
            kasky.CommandText = muokaaKysely;
            kasky.Connection = yht.hankiYhdistys();

            //@num,@tp,@phn,@fr
            //Kertoo mitkä tiedot pitää hakea Hallitse_Huoneita 
            kasky.Parameters.Add("@num", MySqlDbType.Int32).Value = numero;
            kasky.Parameters.Add("@tp", MySqlDbType.Int32).Value = tyyli;
            kasky.Parameters.Add("@phn", MySqlDbType.VarChar).Value = puhelin;
            kasky.Parameters.Add("@fr", MySqlDbType.VarChar).Value = vapaa;

            //Avaaa ja sulkee yhteyden myql
            yht.avaaYhdista();

            if (kasky.ExecuteNonQuery() == 1)
            {
                yht.suljeYhdista();
                return true;
            }
            else
            {
                yht.suljeYhdista();
                return false;
            }
        }

        //Poistaa mysqllästä huone tiedot huoneen numeroa käytäen
        public bool poistaHuone(int numero)
        {
            MySqlCommand kasky = new MySqlCommand();
            string poistaKysely = "DELETE FROM `huone` WHERE `numero`=@mun";
            kasky.CommandText = poistaKysely;
            kasky.Connection = yht.hankiYhdistys();

            //@mun
            kasky.Parameters.Add("@mun", MySqlDbType.Int32).Value = numero;

            //Avaa yhteys ja sulje
            yht.avaaYhdista();

            if (kasky.ExecuteNonQuery() == 1)
            {
                yht.suljeYhdista();
                return true;
            }
            else
            {
                yht.suljeYhdista();
                return false;
            }
        }

    }
}
