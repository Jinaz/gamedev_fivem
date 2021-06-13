using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;


namespace CharacterInterface
{
    class Timer
    {
        int _limit;
        int _tempLimit;
        bool _expired;

        public int Limit { set { _tempLimit = value; _limit = CitizenFX.Core.Game.GameTime + _tempLimit; } }
        public bool Expired
        {
            get
            {

                if (CitizenFX.Core.Game.GameTime > _limit)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public Timer(int handle)
        {

        }

        public void Reset()
        {
            _limit = 0;
            _tempLimit = 0;
            _expired = false;
        }
    }

    public class CharacterInterface : BaseScript
    {
        float hunger;
        float thirst;
        float consumptionRateThirst = 0.00001f;
        float consumptionRateHunger = 0.000005f;
        float consumedThirst = 0f;
        float consumedHunger = 0f;
        
        Ped playerped;

        Timer t1;

        CharacterAtts cas;

        bool initialized = false;

        //backend class for character attributes
        public CharacterInterface()
        {
            cas = new CharacterAtts();
            hunger = 0.75f;
            thirst = 0.75f;

            t1 = new Timer(0);
            t1.Limit = 1000;
            playerped = Game.PlayerPed;
            
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);


            //SetNuiFocus(false, false);

            EventHandlers["charinf:setCharInf"] += new Action<string>(setCharAtts);
            EventHandlers["charinf:consumeItem"] += new Action<string, int>(consumeItem);
            //EventHandlers["charinf:pay"] += new Action<int>(paymoneyhand);
            //EventHandlers["charinf:paybank"] += new Action<int>(paymoneybank);
            //EventHandlers["charinf:dropitem"] += new Action<int>(dropitem);
            EventHandlers["charinf:checkoutMoney"] += new Action<int, int>(checkoutMoney);

            EventHandlers["charinf:showMoney"] += new Action(showMoney);

            Tick += OnTick;
        }

        private void showMoney()
        {
            TriggerEvent("charinfUI:showmoney", cas.moneyHand, cas.moneyBank);
        }

        private void checkoutMoney(int senderid, int cost)
        {
            if (senderid == 0) {
                if (cas.moneyHand < cost)
                {
                    TriggerEvent("clths:checkoutFail", true);
                }
                else
                {
                    cas.moneyHand = cas.moneyHand - cost;
                }
            }
            
            //check money if not enough prompt message
            //depending on senderid return a message to said sender
        }

        private void consumeItem(string itemname, int amount)
        {
            if (itemname == Enum.GetName(typeof(CharacterInverntoryUI.ItemsData), 6))
            {
                
                thirst = thirst + 0.25f;
                thirst = thirst > 1f ? 1f : thirst;
            }
            if (itemname == Enum.GetName(typeof(CharacterInverntoryUI.ItemsData), 8))
            {
                
                hunger = hunger + 0.25f;
                hunger = hunger > 1f ? 1f : hunger;
            }
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            Debug.WriteLine("chinfo startup successful");
        }

        private void setCharAtts(string jsonstring)
        {
            cas = JsonConvert.DeserializeObject<CharacterAtts>(jsonstring);
            
        }
        
        

        private async Task OnTick()
        {
           
            if (playerped.IsRunning)
            {
                consumedThirst += consumptionRateThirst * 1.5f;
                consumedHunger += consumedHunger * 1.5f;
            }
            else
            {
                consumedHunger += consumptionRateHunger;
                consumedThirst += consumptionRateThirst;
            }

            //once timeperiod is over consome some thirst
            if (t1.Expired)
            {
                thirst -= consumedThirst;
                thirst = thirst < 0.0f ? 0f : thirst;
                consumedThirst = 0f;

                hunger -= consumedHunger;
                hunger = hunger < 0.0f ? 0f : hunger;
                consumedHunger = 0f;
            }


            TriggerEvent("charinfUI:showHunger", hunger);
            TriggerEvent("charinfUI:showThirst", thirst);

            await Task.FromResult(0);
        }
    }
}
