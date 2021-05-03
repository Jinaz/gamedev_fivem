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

        bool inventoryinit = false;
        Ped playerped;
        Inventory inv;
        Timer t1;

        string hunger_key = "_hunger_key";
        string thirst_key = "_thirst_key";

        bool initialized = false;

        public CharacterInterface()
        {
            
            hunger = 0.75f;
            thirst = 0.75f;

            t1 = new Timer(0);
            t1.Limit = 1000;
            playerped = Game.PlayerPed;
            EntityDecoration.RegisterProperty(hunger_key, DecorationType.Float);
            EntityDecoration.RegisterProperty(thirst_key, DecorationType.Float);

            Tick += OnTick;
            Tick += ItemUsage;

            //SetNuiFocus(false, false);

            RegisterNuiCallbackType("exit");
            EventHandlers["__cfx_nui:exit"] += new Action<IDictionary<string, object>, CallbackDelegate>(error);


        }

        private void error(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = false
            });
            SendNuiMessage(jsonstring);
            SetNuiFocus(false, false);
        }

        private async Task ItemUsage()
        {
            inv.items[0] = ItemsData.SM_WATER;
            Console.WriteLine(ItemsData.SM_WATER.ToString("g"));
            if (!playerped.IsInVehicle() 
                || (playerped.IsInVehicle() 
                && playerped.CurrentVehicle.Driver != playerped))
            if ( Game.IsControlJustReleased(0, Control.Talk))
            {
                    
                    //open UI

                    string jsonstring;
                    jsonstring = JsonConvert.SerializeObject(new
                    {
                        toggle = true
                    });
                    //SendNuiMessage(jsonstring);

                    int slotnumber = 0;
                    SetNuiFocus(true, true);
                    UseItem(inv.items[0], slotnumber);
            }
        }

        private void UseItem(ItemsData itemnum, int slotnumber)
        {
            if (itemnum == ItemsData.SM_WATER)
            {
                inv.items[slotnumber] = ItemsData.EMPTY;
                thirst = thirst + 0.25f;
                thirst = thirst > 1f ? 1f : thirst;
            }
            if (itemnum == ItemsData.SM_FOOD)
            {
                inv.items[slotnumber] = ItemsData.EMPTY;
                hunger = hunger + 0.25f;
                hunger = hunger > 1f ? 1f : hunger;
            }

        }

        private static void DisplayText(float x, float y, string text)
        {
            BeginTextCommandDisplayText("STRING");
            AddTextComponentString(text);
            SetTextScale(1f, .5f);
            SetTextCentre(true);
            EndTextCommandDisplayText(x, y);
        }

        private async Task OnTick()
        {
            if (!inventoryinit)
            {
                initInv(playerped);

                inventoryinit = true;
            }

            

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

            DisplayText(.95f, .55f, $"thirst: {thirst}");
            DisplayText(.95f, .5f, $"hunger: {hunger}");

            await Task.FromResult(0);
        }

        private void initInv(Ped playerped)
        {
            inv = new Inventory();
        }

        private Ped setLook(Ped playerped )
        {/* some doc of the enum PedComponent
                 Face = 0,
        Head = 1,
        Hair = 2,
        Torso = 3,
        Legs = 4,
        Hands = 5,
        Shoes = 6,
        Special1 = 7,
        Special2 = 8,
        Special3 = 9,
        Textures = 10,
        Torso2 = 11
                 */


            /*PedProp
             * 
             * "Hats = 0,
    Glasses = 1,
    EarPieces = 2,
    Unknown3 = 3,
    Unknown4 = 4,
    Unknown5 = 5,
    Watches = 6,
    Wristbands = 7,
    Unknown8 = 8,
    Unknown9 = 9"*/
            
            playerped.Style[PedComponents.Face].SetVariation(0,0);
            Debug.WriteLine(playerped.Style[PedComponents.Face].Count.ToString());
            Debug.WriteLine(playerped.Style[PedComponents.Face].Count.ToString());
            return playerped;
        }

        
    }
}
