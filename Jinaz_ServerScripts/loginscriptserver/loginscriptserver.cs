using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using MySql.Data.MySqlClient;
using static CitizenFX.Core.Native.API;



namespace loginscriptserver
{
    public class loginscriptserver : BaseScript
    {
        private string connStr = "server=localhost;user=client;database=fivem_server_db;port=3306;password=fivemClientDBPW";
        public loginscriptserver()
        {
            EventHandlers["cbc:login"] += new Action<Player>(login);
            EventHandlers["cbc:getChars"] += new Action<Player>(getAllChars);
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
        }

        public void OnResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            displayNumberOfPlayer();

            Debug.WriteLine($"Resource {resourceName} loaded!");

        }

        private void getAllChars([FromSource]Player source)
        {
            List<string> playerpedsids = new List<string>();

        }

        private async Task displayNumberOfPlayer()
        {

            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                await conn.OpenAsync();

                string testSQL = "SELECT count(0) from charactertable";
                MySqlCommand command = new MySqlCommand(testSQL, conn);

                long messagesNo = (long)await command.ExecuteScalarAsync();

                Debug.WriteLine($"number of entries in database: {messagesNo} ");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($" {ex} ");
            }

            conn.Close();

        }

        public static string getBetween(string strSource, string searchString)
        {
            if (strSource.Contains(searchString))
            {
                return strSource.Remove(0, searchString.Length);
            }

            return "";
        }

        private async Task selectCharacterLooks(Player source, List<string> playerpedsids)

        {
            Dictionary<string, string>[] peddict = new Dictionary<string, string>[] {
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>(),
                new Dictionary<string, string>()
            };
            foreach (string characterid in playerpedsids)
            {
                int charaid = Int16.Parse(characterid.Split('_')[1]);
                string sqlstatement = $"select * from characterlooktable where steamID_chara='{characterid}';";
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {

                    await conn.OpenAsync();

                    if (sqlstatement.ToLower().Contains("select"))
                    {

                        //SQL string
                        string sql = sqlstatement;
                        MySqlCommand cmd = new MySqlCommand(sql, conn);

                        //reader
                        DbDataReader rdr = (DbDataReader)await cmd.ExecuteReaderAsync();
                        source.TriggerEvent("loginscr:response", rdr.ToString());
                        int i = 0;
                        //Ped ped = new Ped(charaid);
                        while (await rdr.ReadAsync())
                        {
                            peddict[charaid].Add(rdr[0].ToString(), rdr[1].ToString());
                        }
                        rdr.Close();

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private async Task selectCharacterIDS(Player source)
        {
            List<string> playerpedsids = new List<string>();

            string sqlstatement = $"select * from charactertable where steamID='{getSteamID(source)}';";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {

                await conn.OpenAsync();

                if (sqlstatement.ToLower().Contains("select"))
                {

                    //SQL string
                    string sql = sqlstatement;
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    //reader
                    DbDataReader rdr = (DbDataReader)await cmd.ExecuteReaderAsync();
                    source.TriggerEvent("loginscr:response", rdr.ToString());
                    int i = 0;
                    while (await rdr.ReadAsync())
                    {
                        if (rdr[0] == "steamID_chara")
                        {
                            playerpedsids.Add(rdr[1].ToString());
                        }
                        string printed = $"{rdr[0]} -- {rdr[1]}";
                        Console.WriteLine(printed);
                        source.TriggerEvent("loginscr:response", printed);
                        i++;
                    }
                    rdr.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private async Task insertPlayerPed(Player source)
        {
            string sqlstatement = $"select * from charactertable where steamID='{getSteamID(source)}';";
            MySqlConnection conn = new MySqlConnection(connStr);
            if (sqlstatement.ToLower().Contains("insert"))
            {
                //template code will be changed for insert...
                //string insertStmt = "insert into message (player_name, message_text) values (@player_name, @message)";
                //MySqlCommand cmd = new MySqlCommand(insertStmt, conn);
                //cmd.Parameters.AddWithValue("@player_name", playerName);
                //cmd.Parameters.AddWithValue("@message", message);


                string sql = "";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                long messagesNo = (long)await cmd.ExecuteScalarAsync();
            }
        }

        private async Task insertPlayer(Player source, List<string> playerpedsids)
        {

            MySqlConnection conn = new MySqlConnection(connStr);


            string sqlstatement = $"insert into charactertable (steamID_chara, steamID, CharaName, isjailed, canspawnboat, canspawnplane, moneybank, moneyhand, job, ispolice, canspawntow, isEMC) values (@steamID_chara, @steamID, @CharaName, @isjailed, @canspawnboat, @canspawnplane, @moneybank, @moneyhand, @job, @ispolice, @canspawntow, @isEMC);";
            //(@steamID_chara, @steamID, @CharaName, @isjailed, @canspawnboat, @canspawnplane, @moneybank, @moneyhand, @job, @ispolice, @canspawntow, @isEMC)
            MySqlCommand cmd = new MySqlCommand(sqlstatement, conn);
            cmd.Parameters.AddWithValue("@steamID_chara", $"{getSteamID(source)}_{playerpedsids.Count}");


            //template code will be changed for insert...
            //string insertStmt = "insert into message (player_name, message_text) values (@player_name, @message)";
            //MySqlCommand cmd = new MySqlCommand(insertStmt, conn);
            //cmd.Parameters.AddWithValue("@player_name", playerName);
            //cmd.Parameters.AddWithValue("@message", message);




        }

        private async Task execSQL(Player source)
        {
            Console.WriteLine("execSQL");
            string sqlstatement = $"select * from charactertable where steamID='{getSteamID(source)}';";

            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("connection defined");
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                await conn.OpenAsync();

                if (sqlstatement.ToLower().Contains("select"))
                {

                    //SQL string
                    string sql = sqlstatement;
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    //reader
                    DbDataReader rdr = (DbDataReader)await cmd.ExecuteReaderAsync();
                    source.TriggerEvent("loginscr:response", rdr.ToString());
                    while (await rdr.ReadAsync())
                    {
                        string printed = $"{rdr[0]} -- {rdr[1]}";
                        Console.WriteLine(printed);
                        source.TriggerEvent("loginscr:response", printed);
                    }
                    rdr.Close();

                    //return "a";
                }
                if (sqlstatement.ToLower().Contains("update"))
                {
                    //SQL string
                    string sql = sqlstatement;
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    long rowsNo = (long)await cmd.ExecuteNonQueryAsync();

                    Debug.WriteLine($"");
                }

                if (sqlstatement.ToLower().Contains("insert"))
                {
                    //template code will be changed for insert...
                    //string insertStmt = "insert into message (player_name, message_text) values (@player_name, @message)";
                    //MySqlCommand cmd = new MySqlCommand(insertStmt, conn);
                    //cmd.Parameters.AddWithValue("@player_name", playerName);
                    //cmd.Parameters.AddWithValue("@message", message);


                    string sql = "";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    long messagesNo = (long)await cmd.ExecuteScalarAsync();
                }

                if (sqlstatement.ToLower().Contains("count"))
                {
                    //SQL string
                    string sql = sqlstatement;
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    long messagesNo = (long)await cmd.ExecuteScalarAsync();

                    Debug.WriteLine($"");

                }

                conn.Close();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());

            }


        }

        private string getSteamID(Player source)
        {
            string steamid = "";
            for (int i = 0; i < GetNumPlayerIdentifiers(source.Handle) - 1; i++)
            {
                string id = GetPlayerIdentifier(source.Handle, i);
                //Debug.WriteLine(id);
                if (id.Contains("live:"))
                {
                    steamid = getBetween(id, "live:");
                    source.TriggerEvent("loginscr:response", steamid);
                }
                //Debug.WriteLine($"{steamid}");
            }
            return steamid;
        }

        private void login([FromSource]Player source)
        {
            //Debug.WriteLine(source.Handle);
            //Debug.WriteLine(GetNumPlayerIdentifiers(source.Handle).ToString());

            //int ia = 1;
            //string ids = GetPlayerIdentifier(source.Handle, ia);
            //Debug.WriteLine(ids);


            //load via SQL character look
            Debug.WriteLine(getSteamID(source));
            selectCharacterIDS(source);

            //use steamid to setup SQL select


        }



    }


}

