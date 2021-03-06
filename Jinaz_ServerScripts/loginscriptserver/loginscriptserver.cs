using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;



namespace loginscriptserver
{
    public class loginscriptserver : BaseScript
    {
        private string connStr = "server=localhost;user=client;database=fivem_server_db;port=3306;password=fivemClientDBPW";
        public loginscriptserver()
        {

            EventHandlers["loginscrserver:startlogin"] += new Action<Player>(retrieveNameIDCash);

            EventHandlers["loginscrserver:login"] += new Action<Player>(login);
            EventHandlers["loginscrserver:getChars"] += new Action<Player>(getAllChars);
            
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
        }

        

        public void OnResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            displayNumberOfPlayer();

            Debug.WriteLine($"Resource {resourceName} loaded!");

        }

        //utils
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
        //utils
        private async Task<long> numOfCharas(Player source)
        {

            MySqlConnection conn = new MySqlConnection(connStr);
            long messagesNo = 0;
            try
            {
                await conn.OpenAsync();

                string testSQL = $"SELECT count(0) from charactertable where steamID='{getSteamID(source)}'";
                MySqlCommand command = new MySqlCommand(testSQL, conn);

                messagesNo = (long)await command.ExecuteScalarAsync();

                Debug.WriteLine($"number of entries in database: {messagesNo} ");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($" {ex} ");
            }

            conn.Close();

            return messagesNo;

        }

        private async void getAllChars([FromSource]Player source)
        {
            string steamid = getSteamID(source);

            await SelectCharacterIDS(source);
        }

        
        //utils
        public static string getBetween(string strSource, string searchString)
        {
            if (strSource.Contains(searchString))
            {
                return strSource.Remove(0, searchString.Length);
            }

            return "";
        }

        

        private async Task SelectCharacterIDS(Player source)
        {
            

            string sqlstatement = $"select * from charactertable where steamID='{getSteamID(source)}';";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {

                await conn.OpenAsync();

                int charanum = Convert.ToInt32(await numOfCharas(source));

                //SQL string
                string sql = sqlstatement;
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //reader
                DbDataReader rdr = (DbDataReader)await cmd.ExecuteReaderAsync();
                //rdr.
                source.TriggerEvent("loginscr:response", "start of reader");
                int i = 0;
                while (await rdr.ReadAsync())
                {
                    CharacterInterface.CharacterAtts ca = new CharacterInterface.CharacterAtts();
                    ca.steamcharid = rdr["steamID_chara"].ToString();
                    ca.steamid = rdr["steamID"].ToString();
                    ca.CharacterName = rdr["CharaName"].ToString();
                    ca.isjailed = Convert.ToBoolean(rdr["isjailed"].ToString());
                    ca.canspawnboat = Convert.ToBoolean(rdr["canspawnboat"].ToString());
                    ca.canspawnplane = Convert.ToBoolean(rdr["canspawnplane"].ToString());
                    ca.moneyBank = Convert.ToInt32(rdr["moneybank"].ToString());
                    ca.moneyHand = Convert.ToInt32(rdr["moneyhand"].ToString());
                    ca.job = rdr["job"].ToString();
                    ca.ispolice = Convert.ToBoolean(rdr["ispolice"].ToString());
                    ca.canspawntow = Convert.ToBoolean(rdr["canspawntow"].ToString());
                    ca.isEmc = Convert.ToBoolean(rdr["isEMC"].ToString());


                    string jsonobject = JsonConvert.SerializeObject(ca);

                    source.TriggerEvent("loginscr:displayCharas", jsonobject, i);
                    

                    i++;
                }
                if (i == 0) source.TriggerEvent("loginscr:newPlayer");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            
        }

        

        //templates
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
            
            Debug.WriteLine(getSteamID(source));
            SelectCharacterIDS(source);

            //use steamid to setup SQL select


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

        private async void retrieveNameIDCash([FromSource]Player source)
        {
            //long charanum = await numOfCharas(obj);

            string sqlstatement = $"select * from charactertable where steamID='{getSteamID(source)}';";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {

                await conn.OpenAsync();

                int charanum = Convert.ToInt32(await numOfCharas(source));

                //SQL string
                string sql = sqlstatement;
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //reader
                DbDataReader rdr = (DbDataReader)await cmd.ExecuteReaderAsync();
                //rdr.
                source.TriggerEvent("loginscr:response", "start of reader");
                int i = 0;
                //source.TriggerEvent("loginscr:response", rdr.);

                //init 2D array
                string[][] Values = new string[charanum][];
                for (int x = 0; x < charanum; x++)
                {
                    Values[x] = new string[12];
                }

                while (await rdr.ReadAsync())
                {

                    Values[i][0] = rdr["steamID_chara"].ToString();
                    Values[i][1] = rdr["steamID"].ToString();
                    Values[i][2] = rdr["CharaName"].ToString();
                    Values[i][3] = rdr["isjailed"].ToString();
                    Values[i][4] = rdr["canspawnboat"].ToString();
                    Values[i][5] = rdr["canspawnplane"].ToString();
                    Values[i][6] = rdr["moneybank"].ToString();
                    Values[i][7] = rdr["moneyhand"].ToString();
                    Values[i][8] = rdr["job"].ToString();
                    Values[i][9] = rdr["ispolice"].ToString();
                    Values[i][10] = rdr["canspawntow"].ToString();
                    Values[i][11] = rdr["isEMC"].ToString();

                    i++;
                }

                //peddict[i].Add();
                rdr.Close();



                if (charanum > 0)
                {
                    for (i = 0; i < charanum; i++)
                        if (i == charanum - 1)
                            source.TriggerEvent("loginscr:displayCharas", true, Values[i][2], Values[i][8], Values[i][6], Values[i][7], Values[i][0]);
                        else
                            source.TriggerEvent("loginscr:displayCharas", false, Values[i][2], Values[i][8], Values[i][6], Values[i][7], Values[i][0]);
                }
                else if (charanum == 0)
                {
                    source.TriggerEvent("loginscr:newPlayer");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

    }

}

