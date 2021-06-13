using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;

namespace ClothesShopServer
{  
    //keep this stateless!!!
    public class ClothesShopDBConnector : BaseScript
    {
        private string connStr = "server=localhost;user=client;database=fivem_server_db;port=3306;password=fivemClientDBPW";
        public ClothesShopDBConnector()
        {

            //get looks TODO
            EventHandlers["loginscrserver:CharaCreationDone"] += new Action<Player, string, string, int>(saveFirstChar);
            //update look TODO
            //TODO loginprocedure: char selected
            EventHandlers["clthssvr:selectchar"] += new Action<Player, string>(selectChar);


            var a = JsonConvert.DeserializeObject<ClothesShop.ClothesClass[]>("");

            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
        }

        private void selectChar([FromSource]Player arg1, string arg2)
        {
            throw new NotImplementedException();

            //loadskin
            //setskin 
        }

        public void OnResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
            
            Debug.WriteLine($"Resource {resourceName} loaded!");

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

        //utils
        private string getSteamID(Player source)
        {
            string steamid = "";
            for (int i = 0; i < GetNumPlayerIdentifiers(source.Handle) - 1; i++)
            {
                string id = GetPlayerIdentifier(source.Handle, i);
                
                if (id.Contains("live:"))
                {
                    steamid = getBetween(id, "live:");
                    source.TriggerEvent("loginscr:response", steamid);
                }
                
            }
            return steamid;
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

                
                sqlstatement = $"insert into CharacterLookTable " +
                    $"(lookID,gender,lookname,active,steamID_chara,face,face_variation,head,head_variation,hair,hair_variation,hair_color_prim,hair_color_sec,torso,torso_variation,legs,legs_variation,hands,hands_variation,shoes,shoes_variation,special1,special1_variation,special2,special2_variation,special3,special3_variation,textures,textures_variation,torso2_variation,torso2,hats,hats_variation,glasses,glasses_variation,Unknown3,Unknown3_variation,Unknown4,Unknown4_variation,Unknown5,Unknown5_variation,Watches,Watches_variation,Wristbands,Wristbands_variation,Unknown8,Unknown8_variation,Unknown9,Unknown9_variation,noseWidth,noseHeight,noseLength,noseBridge,noseTip,noseBridgeShift,browHeight,browWidth,cheekboneHeight,cheekboneWidth,cheeksWidth,eyes,eyes_color,lips_,jawWidth,jawHeight,chinLength,chinPosition,chinWidth,chinShape,neckWidth)" +
                    $"values (@lookID,@gender,@lookname,@active,@steamID_chara,@face,@face_variation,@head,@head_variation,@hair,@hair_variation,@hair_color_prim,@hair_color_sec,@torso,@torso_variation,@legs,@legs_variation,@hands,@hands_variation,@shoes,@shoes_variation,@special1,@special1_variation,@special2,@special2_variation,@special3,@special3_variation,@textures,@textures_variation,@torso2_variation,@torso2,@hats,@hats_variation,@glasses,@glasses_variation,@Unknown3,@Unknown3_variation,@Unknown4,@Unknown4_variation,@Unknown5,@Unknown5_variation,@Watches,@Watches_variation,@Wristbands,@Wristbands_variation,@Unknown8,@Unknown8_variation,@Unknown9,@Unknown9_variation,@noseWidth,@noseHeight,@noseLength,@noseBridge,@noseTip,@noseBridgeShift,@browHeight,@browWidth,@cheekboneHeight,@cheekboneWidth,@cheeksWidth,@eyes,@eyes_color,@lips_,@jawWidth,@jawHeight,@chinLength,@chinPosition,@chinWidth,@chinShape,@neckWidth);";
                cmd = new MySqlCommand(sqlstatement, conn);
                cmd.Parameters.AddWithValue("@lookID", Convert.ToInt32(messagesNo));

                if (gender == 0)
                    cmd.Parameters.AddWithValue("@gender", "m");
                else
                    cmd.Parameters.AddWithValue("@gender", "f");
                cmd.Parameters.AddWithValue("@lookname", 0);
                cmd.Parameters.AddWithValue("@active", true);
                cmd.Parameters.AddWithValue("@steamID_chara", $"{getSteamID(source).ToString()}_{messagesNo.ToString()}");
                cmd.Parameters.AddWithValue("@face", cc.head);
                cmd.Parameters.AddWithValue("@face_variation", cc.head_variation);
                cmd.Parameters.AddWithValue("@head", cc.masks);
                cmd.Parameters.AddWithValue("@head_variation", cc.masks_variation);
                cmd.Parameters.AddWithValue("@hair", cc.hair);
                cmd.Parameters.AddWithValue("@hair_variation", cc.hair_variation);
                cmd.Parameters.AddWithValue("@hair_color_prim", 0);
                cmd.Parameters.AddWithValue("@hair_color_sec", 0);
                cmd.Parameters.AddWithValue("@torso", cc.torso);
                cmd.Parameters.AddWithValue("@torso_variation", cc.torso_variation);
                cmd.Parameters.AddWithValue("@legs", cc.legs);
                cmd.Parameters.AddWithValue("@legs_variation", cc.legs_variation);
                cmd.Parameters.AddWithValue("@hands", cc.bags);
                cmd.Parameters.AddWithValue("@hands_variation", cc.bags_variation);
                cmd.Parameters.AddWithValue("@shoes", cc.shoes);
                cmd.Parameters.AddWithValue("@shoes_variation", cc.shoes_variation);
                cmd.Parameters.AddWithValue("@special1", cc.accessories);
                cmd.Parameters.AddWithValue("@special1_variation", cc.accessories_variation);
                cmd.Parameters.AddWithValue("@special2", cc.undershirts);
                cmd.Parameters.AddWithValue("@special2_variation", cc.undershirts_variation);
                cmd.Parameters.AddWithValue("@special3", cc.bodyArmor);
                cmd.Parameters.AddWithValue("@special3_variation", cc.bodyArmor_variation);
                cmd.Parameters.AddWithValue("@textures", cc.decals);
                cmd.Parameters.AddWithValue("@textures_variation", cc.decals_variation);
                cmd.Parameters.AddWithValue("@torso2_variation", cc.tops_variation);
                cmd.Parameters.AddWithValue("@torso2", cc.tops);
                cmd.Parameters.AddWithValue("@hats", cc.hats);
                cmd.Parameters.AddWithValue("@hats_variation", cc.hats_variation);
                cmd.Parameters.AddWithValue("@glasses", cc.glasses);
                cmd.Parameters.AddWithValue("@glasses_variation", cc.glasses_variation);
                cmd.Parameters.AddWithValue("@Unknown3", 0);
                cmd.Parameters.AddWithValue("@Unknown3_variation", 0);
                cmd.Parameters.AddWithValue("@Unknown4", 0);
                cmd.Parameters.AddWithValue("@Unknown4_variation", 0);
                cmd.Parameters.AddWithValue("@Unknown5", 0);
                cmd.Parameters.AddWithValue("@Unknown5_variation", 0);
                cmd.Parameters.AddWithValue("@Watches", cc.watches);
                cmd.Parameters.AddWithValue("@Watches_variation", cc.watches_variation);
                cmd.Parameters.AddWithValue("@Wristbands", cc.bracelets);
                cmd.Parameters.AddWithValue("@Wristbands_variation", cc.bracelets_variation);
                cmd.Parameters.AddWithValue("@Unknown8", 0);
                cmd.Parameters.AddWithValue("@Unknown8_variation", 0);
                cmd.Parameters.AddWithValue("@Unknown9", 0);
                cmd.Parameters.AddWithValue("@Unknown9_variation", 0);
                cmd.Parameters.AddWithValue("@noseWidth", cc.noseWidth);
                cmd.Parameters.AddWithValue("@noseHeight", cc.noseHeight);
                cmd.Parameters.AddWithValue("@noseLength", cc.noseLength);
                cmd.Parameters.AddWithValue("@noseBridge", cc.noseBridge);
                cmd.Parameters.AddWithValue("@noseTip", cc.noseTip);
                cmd.Parameters.AddWithValue("@noseBridgeShift", cc.noseBridgeShift);
                cmd.Parameters.AddWithValue("@browHeight", cc.browHeight);
                cmd.Parameters.AddWithValue("@browWidth", cc.browWidth);
                cmd.Parameters.AddWithValue("@cheekboneHeight", cc.cheekboneHeight);
                cmd.Parameters.AddWithValue("@cheekboneWidth", cc.cheekboneWidth);
                cmd.Parameters.AddWithValue("@cheeksWidth", cc.cheeksWidth);
                cmd.Parameters.AddWithValue("@eyes", cc.eyes);
                cmd.Parameters.AddWithValue("@eyes_color", 0);
                cmd.Parameters.AddWithValue("@lips_", cc.lips_);
                cmd.Parameters.AddWithValue("@jawWidth", cc.jawWidth);
                cmd.Parameters.AddWithValue("@jawHeight", cc.jawHeight);
                cmd.Parameters.AddWithValue("@chinLength", cc.chinLength);
                cmd.Parameters.AddWithValue("@chinPosition", cc.chinPosition);
                cmd.Parameters.AddWithValue("@chinWidth", cc.chinWidth);
                cmd.Parameters.AddWithValue("@chinShape", cc.chinShape);
                cmd.Parameters.AddWithValue("@neckWidth", cc.neckWidth);
                rowsNo = (long)await cmd.ExecuteNonQueryAsync();
                Debug.WriteLine($"Rows inserted: {rowsNo} ");
            }
            catch (Exception e)
            {

            }
            conn.Close();
        }
    }
}
