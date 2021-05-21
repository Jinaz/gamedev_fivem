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

            EventHandlers["cbc:basicInfo"] += new Action<Player>(retrieveNameIDCash);

            EventHandlers["cbc:login"] += new Action<Player>(login);
            EventHandlers["cbc:getChars"] += new Action<Player>(getAllChars);
            EventHandlers["cbc:SaveLook"] += new Action<Player, string, string, int>(saveFirstChar);
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
        }

        private async void saveFirstChar([FromSource]Player source, string arg2, string arg3, int gender)
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            


            ClothesShop.ClothesClass cc = JsonConvert.DeserializeObject<ClothesShop.ClothesClass>(arg2);
            //19.05 do insert function into SQL
            
            try
            {
                await conn.OpenAsync();

                string sql = $"select * from charactertable where steamID='{getSteamID(source)}';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                var rdr = await cmd.ExecuteReaderAsync();
                int messagesNo = 0;
                while (await rdr.ReadAsync())
                {
                    messagesNo++;
                }

                Debug.WriteLine("AAAAAAA"+messagesNo.ToString());
                rdr.Close();
                string sqlstatement = $"insert into charactertable (steamID_chara, steamID, CharaName, isjailed, canspawnboat, canspawnplane, moneybank, moneyhand, job, ispolice, canspawntow, isEMC) values (@steamID_chara, @steamID, @CharaName, @isjailed, @canspawnboat, @canspawnplane, @moneybank, @moneyhand, @job, @ispolice, @canspawntow, @isEMC);";
                //(@steamID_chara, @steamID, @CharaName, @isjailed, @canspawnboat, @canspawnplane, @moneybank, @moneyhand, @job, @ispolice, @canspawntow, @isEMC)
                cmd = new MySqlCommand(sqlstatement, conn);
                cmd.Parameters.AddWithValue("@steamID", $"{getSteamID(source).ToString()}");
                cmd.Parameters.AddWithValue("@steamID_chara", $"{getSteamID(source).ToString()}_{messagesNo.ToString()}");
                cmd.Parameters.AddWithValue("@CharaName", $"{arg3.ToString()}");
                cmd.Parameters.AddWithValue("@isjailed", false);
                cmd.Parameters.AddWithValue("@canspawnboat", false);
                cmd.Parameters.AddWithValue("@canspawnplane", false);
                cmd.Parameters.AddWithValue("@moneybank", 1000);
                cmd.Parameters.AddWithValue("@moneyhand", 100);
                cmd.Parameters.AddWithValue("@job", "");
                cmd.Parameters.AddWithValue("@ispolice", false);
                cmd.Parameters.AddWithValue("@canspawntow", false);
                cmd.Parameters.AddWithValue("@isEMC", false);

                long rowsNo = (long)await cmd.ExecuteNonQueryAsync();
                Debug.WriteLine($"Rows inserted: {rowsNo} ");

                //Debug.WriteLine(cc.eyes.ToString());
                sqlstatement = $"insert into CharacterLookTable " +
                    $"(lookID,gender,lookname,active,steamID_chara,face,face_variation,head,head_variation,hair,hair_variation,hair_color_prim,hair_color_sec,torso,torso_variation,legs,legs_variation,hands,hands_variation,shoes,shoes_variation,special1,special1_variation,special2,special2_variation,special3,special3_variation,textures,textures_variation,torso2_variation,torso2,hats,hats_variation,glasses,glasses_variation,Unknown3,Unknown3_variation,Unknown4,Unknown4_variation,Unknown5,Unknown5_variation,Watches,Watches_variation,Wristbands,Wristbands_variation,Unknown8,Unknown8_variation,Unknown9,Unknown9_variation,noseWidth,noseHeight,noseLength,noseBridge,noseTip,noseBridgeShift,browHeight,browWidth,cheekboneHeight,cheekboneWidth,cheeksWidth,eyes,eyes_color,lips_,jawWidth,jawHeight,chinLength,chinPosition,chinWidth,chinShape,neckWidth)" +
                    $"values (@lookID,@gender,@lookname,@active,@steamID_chara,@face,@face_variation,@head,@head_variation,@hair,@hair_variation,@hair_color_prim,@hair_color_sec,@torso,@torso_variation,@legs,@legs_variation,@hands,@hands_variation,@shoes,@shoes_variation,@special1,@special1_variation,@special2,@special2_variation,@special3,@special3_variation,@textures,@textures_variation,@torso2_variation,@torso2,@hats,@hats_variation,@glasses,@glasses_variation,@Unknown3,@Unknown3_variation,@Unknown4,@Unknown4_variation,@Unknown5,@Unknown5_variation,@Watches,@Watches_variation,@Wristbands,@Wristbands_variation,@Unknown8,@Unknown8_variation,@Unknown9,@Unknown9_variation,@noseWidth,@noseHeight,@noseLength,@noseBridge,@noseTip,@noseBridgeShift,@browHeight,@browWidth,@cheekboneHeight,@cheekboneWidth,@cheeksWidth,@eyes,@eyes_color,@lips_,@jawWidth,@jawHeight,@chinLength,@chinPosition,@chinWidth,@chinShape,@neckWidth);";
                cmd = new MySqlCommand(sqlstatement, conn);
                cmd.Parameters.AddWithValue("@lookID",Convert.ToInt32(messagesNo));
                
                if (gender == 0)
                    cmd.Parameters.AddWithValue("@gender","m");
                else
                    cmd.Parameters.AddWithValue("@gender", "f");
                cmd.Parameters.AddWithValue("@lookname",0);
                cmd.Parameters.AddWithValue("@active",true);
                cmd.Parameters.AddWithValue("@steamID_chara", $"{getSteamID(source).ToString()}_{messagesNo.ToString()}");
                cmd.Parameters.AddWithValue("@face",cc.head);
                cmd.Parameters.AddWithValue("@face_variation",cc.head_variation);
                cmd.Parameters.AddWithValue("@head",cc.masks);
                cmd.Parameters.AddWithValue("@head_variation",cc.masks_variation);
                cmd.Parameters.AddWithValue("@hair",cc.hair);
                cmd.Parameters.AddWithValue("@hair_variation",cc.hair_variation);
                cmd.Parameters.AddWithValue("@hair_color_prim",0);
                cmd.Parameters.AddWithValue("@hair_color_sec",0);
                cmd.Parameters.AddWithValue("@torso",cc.torso);
                cmd.Parameters.AddWithValue("@torso_variation",cc.torso_variation);
                cmd.Parameters.AddWithValue("@legs",cc.legs);
                cmd.Parameters.AddWithValue("@legs_variation",cc.legs_variation);
                cmd.Parameters.AddWithValue("@hands",cc.bags);
                cmd.Parameters.AddWithValue("@hands_variation",cc.bags_variation);
                cmd.Parameters.AddWithValue("@shoes",cc.shoes);
                cmd.Parameters.AddWithValue("@shoes_variation",cc.shoes_variation);
                cmd.Parameters.AddWithValue("@special1",cc.accessories);
                cmd.Parameters.AddWithValue("@special1_variation",cc.accessories_variation);
                cmd.Parameters.AddWithValue("@special2",cc.undershirts);
                cmd.Parameters.AddWithValue("@special2_variation",cc.undershirts_variation);
                cmd.Parameters.AddWithValue("@special3",cc.bodyArmor);
                cmd.Parameters.AddWithValue("@special3_variation",cc.bodyArmor_variation);
                cmd.Parameters.AddWithValue("@textures",cc.decals);
                cmd.Parameters.AddWithValue("@textures_variation",cc.decals_variation);
                cmd.Parameters.AddWithValue("@torso2_variation",cc.tops_variation);
                cmd.Parameters.AddWithValue("@torso2",cc.tops);
                cmd.Parameters.AddWithValue("@hats",cc.hats);
                cmd.Parameters.AddWithValue("@hats_variation",cc.hats_variation);
                cmd.Parameters.AddWithValue("@glasses",cc.glasses);
                cmd.Parameters.AddWithValue("@glasses_variation",cc.glasses_variation);
                cmd.Parameters.AddWithValue("@Unknown3",0);
                cmd.Parameters.AddWithValue("@Unknown3_variation",0);
                cmd.Parameters.AddWithValue("@Unknown4",0);
                cmd.Parameters.AddWithValue("@Unknown4_variation",0);
                cmd.Parameters.AddWithValue("@Unknown5",0);
                cmd.Parameters.AddWithValue("@Unknown5_variation",0);
                cmd.Parameters.AddWithValue("@Watches",cc.watches);
                cmd.Parameters.AddWithValue("@Watches_variation",cc.watches_variation);
                cmd.Parameters.AddWithValue("@Wristbands",cc.bracelets);
                cmd.Parameters.AddWithValue("@Wristbands_variation",cc.bracelets_variation);
                cmd.Parameters.AddWithValue("@Unknown8",0);
                cmd.Parameters.AddWithValue("@Unknown8_variation",0);
                cmd.Parameters.AddWithValue("@Unknown9",0);
                cmd.Parameters.AddWithValue("@Unknown9_variation",0);
                cmd.Parameters.AddWithValue("@noseWidth",cc.noseWidth);
                cmd.Parameters.AddWithValue("@noseHeight",cc.noseHeight);
                cmd.Parameters.AddWithValue("@noseLength",cc.noseLength);
                cmd.Parameters.AddWithValue("@noseBridge",cc.noseBridge);
                cmd.Parameters.AddWithValue("@noseTip",cc.noseTip);
                cmd.Parameters.AddWithValue("@noseBridgeShift",cc.noseBridgeShift);
                cmd.Parameters.AddWithValue("@browHeight",cc.browHeight);
                cmd.Parameters.AddWithValue("@browWidth",cc.browWidth);
                cmd.Parameters.AddWithValue("@cheekboneHeight",cc.cheekboneHeight);
                cmd.Parameters.AddWithValue("@cheekboneWidth",cc.cheekboneWidth);
                cmd.Parameters.AddWithValue("@cheeksWidth",cc.cheeksWidth);
                cmd.Parameters.AddWithValue("@eyes",cc.eyes);
                cmd.Parameters.AddWithValue("@eyes_color",0);
                cmd.Parameters.AddWithValue("@lips_",cc.lips_);
                cmd.Parameters.AddWithValue("@jawWidth",cc.jawWidth);
                cmd.Parameters.AddWithValue("@jawHeight",cc.jawHeight);
                cmd.Parameters.AddWithValue("@chinLength",cc.chinLength);
                cmd.Parameters.AddWithValue("@chinPosition",cc.chinPosition);
                cmd.Parameters.AddWithValue("@chinWidth",cc.chinWidth);
                cmd.Parameters.AddWithValue("@chinShape",cc.chinShape);
                cmd.Parameters.AddWithValue("@neckWidth",cc.neckWidth);
                rowsNo = (long)await cmd.ExecuteNonQueryAsync();
                Debug.WriteLine($"Rows inserted: {rowsNo} ");
            }
            catch (Exception e)
            {

            }
            conn.Close();
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
            //List<string> playerpedsids = await SelectCharacterIDS(source);
            //source.TriggerEvent("loginscr:setPlayerAtts", );

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
                
                //for(i= 0; i < charanum; i++)
                //{
                    //for (int s1=0; s1 < 12; s1++)
                    //{
                        //source.TriggerEvent("loginscr:response", Values[i][s1]);
                    //}
                //}

                
                for (i= 0; i < charanum; i++)
                    source.TriggerEvent("loginscr:setPlayerAtts", i, Values[i][0], Values[i][1], Values[i][2], Values[i][3], Values[i][4], Values[i][5], Values[i][6], Values[i][7], Values[i][8], Values[i][9], Values[i][10], Values[i][11]);



            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            //return playerpedsids;
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
            //Debug.WriteLine(source.Handle);
            //Debug.WriteLine(GetNumPlayerIdentifiers(source.Handle).ToString());

            //int ia = 1;
            //string ids = GetPlayerIdentifier(source.Handle, ia);
            //Debug.WriteLine(ids);


            //load via SQL character look
            Debug.WriteLine(getSteamID(source));
            SelectCharacterIDS(source);

            //use steamid to setup SQL select


        }

    }

}

