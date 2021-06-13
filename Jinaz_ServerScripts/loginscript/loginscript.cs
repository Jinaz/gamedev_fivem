using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;


using CitizenFX.Core.UI;
using System.Drawing;
using Newtonsoft.Json;

namespace loginscript
{
    class UIcard
    {
        public string steamCharID;
        public string name;
        public string job;
        public int moneyhand;
        public int moneybank;

        public UIcard(string nameU, string jobU, int moneyhandU, int moneybankU, string steamCharIDU)
        {
            name = nameU;
            job = jobU;
            moneyhand = moneyhandU;
            moneybank = moneybankU;
            steamCharID = steamCharIDU;
        }

    }

    public class loginscript : BaseScript
    {

        Ped playerped;
        bool initialized = false;
        
        CharacterInterface.CharacterAtts[] cas = new CharacterInterface.CharacterAtts[5];
        public loginscript()
        {
            EventHandlers["loginscr:newPlayer"] += new Action(newPlayer);
            EventHandlers["onClientResourceStart"] += new Action<string>(OnResourceStartAsync);
            //EventHandlers["loginscr:response"] += new Action<string>(responsePrint);
            //EventHandlers["loginscr:respondPeds"] += new Action<Dictionary<string, string>[]>(respPeds);

            EventHandlers["loginscr:displayCharas"] += new Action<string, int>(createUI);
            //SetNuiFocus(true,true);

            RegisterNuiCallbackType("createChar");
            EventHandlers["__cfx_nui:createChar"] += new Action<IDictionary<string, object>, CallbackDelegate>(createTriggered);

            RegisterNuiCallbackType("selectChar");
            EventHandlers["__cfx_nui:selectChar"] += new Action<IDictionary<string, object>, CallbackDelegate>(selectChar);


        }

        private void selectChar(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {

            int choice = Convert.ToInt32(arg1["chosenchar"].ToString());
            
            TriggerEvent("charinf:setCharInf", JsonConvert.SerializeObject(cas[choice]));
            string steamidid = cas[choice].steamcharid;
            //find skin and apply with steamid_id
            TriggerServerEvent("clthssvr:selectchar", steamidid);
            //trigger server event then set skin
            TriggerEvent("loginscr:tpToSpawn");
            throw new NotImplementedException();
            //Trigger tp script after setting chara
        }

        private void createTriggered(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            //not really need the slotnum, just in case some bug will occur
            int slotnum = Convert.ToInt32(arg1["slotnum"].ToString());
            TriggerEvent("clths:createChar", 0 );
        }

        private void newPlayer()
        {
            string jss = JsonConvert.SerializeObject(new
            {
                trigger = true,
                cardnumber = 5
            });

            SendNuiMessage(jss);
            SetNuiFocus(true, true);
        }
        
        private void createUI(string jsonstring, int index)
        {
            CharacterInterface.CharacterAtts ca = JsonConvert.DeserializeObject<CharacterInterface.CharacterAtts>(jsonstring);
            cas[index] = ca;

            string jss = JsonConvert.SerializeObject(new {
                trigger = true,
                cardnumber = index,
                charactername = ca.CharacterName,
                hand = ca.moneyHand,
                bank = ca.moneyBank,
                job = ca.job

            });

            SendNuiMessage(jss);
            SetNuiFocus(true, true);
        }

        private async Task defaultModel()
        {
            var model = new Model(PedHash.FreemodeMale01);
            bool x = await Game.Player.ChangeModel(model);
            //playerped.Style.SetDefaultClothes();
            Debug.WriteLine(x.ToString());
            foreach (PedComponents pc in (PedComponents[])Enum.GetValues(typeof(PedComponents)))
            {
                Game.PlayerPed.Style[pc].SetVariation(0);
            }
            foreach (PedProps pc in (PedProps[])Enum.GetValues(typeof(PedProps)))
            {
                Game.PlayerPed.Style[pc].SetVariation(0);
            }

        }

        private void OnResourceStartAsync(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            playerped = Game.PlayerPed;


            if (!initialized)
            {
                TriggerServerEvent("loginscrserver:login");

                Debug.WriteLine("server event triggered");

                defaultModel();
                initialized = true;
            }

            

            RegisterCommand("triggerlogin", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Console.WriteLine("login triggered");
                TriggerServerEvent("loginscrserver:startlogin");
            }), false);

            RegisterCommand("createChar", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Console.WriteLine("char creation triggered");
                TriggerEvent("clths:createChar");
            }), false);

            RegisterCommand("dm", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                //set model to be changable
                defaultModel();
               
                }), false);

            ;
        }
    }
}
