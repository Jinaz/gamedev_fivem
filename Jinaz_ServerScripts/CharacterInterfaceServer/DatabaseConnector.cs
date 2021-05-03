using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using MySql.Data.MySqlClient;
using static CitizenFX.Core.Native.API;


namespace CharacterInterfaceServer
{
   

    public class DatabaseConnector : BaseScript
    {
        public DatabaseConnector()
        {
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["cbc:LoadCharacters"] += new Action<Player, string>(loadCharacters);
            EventHandlers["cbc:LoadLook"] += new Action<Player, string>(loadLook);
            EventHandlers["cbc:SaveLook"] += new Action<Player, string>(saveLook);

        }

        private void saveLook(Player arg1, string arg2)
        {
            throw new NotImplementedException();
        }

        private void loadLook([FromSource]Player source, string SteamCharacterID)
        {
            
        }

        private Ped characterLook(string SteamCharacterID)
        {
            return null;
        }

        private void loadCharacters([FromSource]Player source, string SteamID)
        {
            Ped[] peds = new Ped[5];

            

            source.TriggerEvent("characters:getCharas", peds);
        }

        private void OnResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
            Debug.WriteLine($"Character Server Script started");
        }

        
    }
}
