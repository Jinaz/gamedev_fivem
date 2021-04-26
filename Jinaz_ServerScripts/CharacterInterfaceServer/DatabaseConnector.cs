using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;


namespace CharacterInterfaceServer
{
    public class Identifiers
    {
        string steam = "";
        string ip = "";
        string discord = "";
        string license = "";
        string xbl = "";
        string live = "";

    }

    public class DatabaseConnector : BaseScript
    {
        public DatabaseConnector()
        {
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["charainter:LoadCharacters"] += new Action<Player, string>(loadCharacters);
            EventHandlers["charainter:LoadLook"] += new Action<Player, string>(loadLook);
            EventHandlers["charainter:Login"] += new Action<Player>(login);
        }



        private void login([FromSource]Player source)
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

            //load via SQL character look

            source.TriggerEvent("characters:getCharas", peds);
        }

        private void OnResourceStart(string obj)
        {
            Debug.WriteLine($"Character Server Script started");
        }
    }
}
