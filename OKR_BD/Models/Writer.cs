using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace OKR_BD
{
    class Writer
    {
        private int id;
        private string psevdanim;
        private string first_name;
        private string last_name;
        private int birth_year;
        private int death_year;

        public Writer() { }
        public int ID { get; set; }
        public string Psevdanim { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public int Birth_year { get; set; }
        public int Death_year { get; set; }


        public static int SearchCountPsevdanin(string a)
        {
            string connStr = "server=************;" +
                "user=************;" +
                "database=************;" +
                "password=************;";

            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();

            string query = $"SELECT COUNT(*) FROM Писатель WHERE Псевдоним LIKE '{a}%'";

            MySqlCommand command = new MySqlCommand(query, conn);
 
            object result = command.ExecuteScalar();
            int r = 0;
            if (result != null)
            {
                 r = Convert.ToInt32(result);
            }
            conn.Close();
            return r;

        }

        public static List<Writer> CheckList()
        {
            string connStr = "server=************;" +
                "user=************;" +
                "database=************;" +
                "password=************;";

            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();

            string query = "SELECT DISTINCT * FROM Писатель";

            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();


            List<Writer> writer = new List<Writer>();
            while (reader.Read())
            {
                Debug.WriteLine($"{reader[1]}, {reader[2]}, {reader[3]}, {reader[4]}, {reader[5]}");
                writer.Add(new Writer()
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    Psevdanim = Convert.ToString(reader[1].ToString()),
                    First_name = Convert.ToString(reader[2].ToString()),
                    Last_name = Convert.ToString(reader[3].ToString()),
                    Birth_year = Convert.ToInt32(reader[4]),
                    Death_year = Convert.ToInt32(reader[5])
                });
            }
            reader.Close();
            conn.Close();
            return writer;

        }


        public static bool DeleteID(string id_str)
        {
            string query = $"DELETE FROM `Писатель` WHERE id={id_str}";
            string connStr = "server=************;" + "user=************;" + "database=************;" + "password=************;";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();
            conn.Close();
            return true;
        }

        public static List<Writer> SearchName(string name)
        {
            string connStr = "server=************;" + "user=************;" + "database=************;" + "password=************;";

            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();

            string query = $"SELECT * FROM Писатель WHERE Имя LIKE '{name}'";

            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();


            List<Writer> writer = new List<Writer>();
            while (reader.Read())
            {
                Debug.WriteLine($"{reader[1]}, {reader[2]}, {reader[3]}, {reader[4]}, {reader[5]}");
                writer.Add(new Writer()
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    Psevdanim = Convert.ToString(reader[1].ToString()),
                    First_name = Convert.ToString(reader[2].ToString()),
                    Last_name = Convert.ToString(reader[3].ToString()),
                    Birth_year = Convert.ToInt32(reader[4]),
                    Death_year = Convert.ToInt32(reader[5])
                });
            }
            reader.Close();
            conn.Close();
            return writer;

        }
        public static List<string> CheckOld()
        {
            string connStr = "server=************;" + "user=************;" + "database=************;" + "password=************;";

            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();

            string query = "SELECT `Писатель`.`Псевдоним`,(`Год_смерти`-`Год_рождения`) as Возраст FROM `Писатель` WHERE `Год_смерти`-`Год_рождения`=(SELECT MAX(`Писатель`.`Год_смерти`-`Год_рождения`) FROM `Писатель`)";

            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();


            List<string> psevdanim = new List<string> { "", "" };
            while (reader.Read())
            {
                psevdanim[0] = Convert.ToString(reader[0]);
                psevdanim[1] = Convert.ToString(reader[1]);
            }
            reader.Close();
            conn.Close();
            return psevdanim;

        }
        public static List<string> CheckYoung()
        {
            string connStr = "server=************;" + "user=************;" + "database=************;" + "password=************;";

            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();

            string query = "SELECT `Писатель`.`Псевдоним`,(`Год_смерти`-`Год_рождения`)as Возраст FROM `Писатель` WHERE `Год_смерти`-`Год_рождения`=(SELECT MIN(`Писатель`.`Год_смерти`-`Год_рождения`) FROM `Писатель`)";

            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();


            List<string> psevdanim = new List<string>{"", ""};
            while (reader.Read())
            {
                psevdanim[0] = Convert.ToString(reader[0]);
                psevdanim[1]= Convert.ToString(reader[1]);
            }
            reader.Close();
            conn.Close();
            return psevdanim;

        }

        public static List<Writer> CheckListSort()
        {
            string connStr = "server=************;" + "user=************;" + "database=************;" + "password=************;";

            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();

            string query = "SELECT DISTINCT * FROM Писатель ORDER BY Отчество";

            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();


            List<Writer> writer = new List<Writer>();
            while (reader.Read())
            {
                Debug.WriteLine($"{reader[1]}, {reader[2]}, {reader[3]}, {reader[4]}, {reader[5]}");
                writer.Add(new Writer()
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    Psevdanim = Convert.ToString(reader[1].ToString()),
                    First_name = Convert.ToString(reader[2].ToString()),
                    Last_name = Convert.ToString(reader[3].ToString()),
                    Birth_year = Convert.ToInt32(reader[4]),
                    Death_year = Convert.ToInt32(reader[5])
                });
            }
            reader.Close();
            conn.Close();
            return writer;

        }
        public static int CountFullDB()
        {
            string connStr = "server=************;" + "user=************;" + "database=************;" + "password=************;";

            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();

            string query = $"SELECT COUNT(*) FROM Писатель";

            MySqlCommand command = new MySqlCommand(query, conn);

            object result = command.ExecuteScalar();
            int r = 0;
            if (result != null)
            {
                r = Convert.ToInt32(result);
            }
            conn.Close();
            return r;

        }
    }
    
}
