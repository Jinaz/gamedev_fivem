using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;


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
        float consumptionRateThirst = 0.001f;
        float consumptionRateHunger = 0.0005f;
        float consumedThirst = 0f;
        float consumedHunger = 0f;

        bool inventoryinit = false;
        Ped playerped;
        Inventory inv;
        Timer t1;

        string hunger_key = "_hunger_key";
        string thirst_key = "_thirst_key";

        public CharacterInterface()
        {
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);

            hunger = 0.75f;
            thirst = 0.75f;

            t1 = new Timer(0);
            t1.Limit = 100;
            playerped = Game.PlayerPed;
            EntityDecoration.RegisterProperty(hunger_key, DecorationType.Float);
            EntityDecoration.RegisterProperty(thirst_key, DecorationType.Float);

            Tick += OnTick;
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

        private void OnResourceStart(string obj)
        {
            playerped = Game.PlayerPed;

            TriggerServerEvent("charainter:Login");
            
            //TODO connect SQL on start
            //get player data

            //if first time login
            //if steamid in table then not first time --> open character selection
            //if not empty table
            //


            RegisterCommand("getCharaInfo", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                playerped.Style.GetAllProps();

                playerped.Style.GetAllComponents();

                foreach (PedComponent pc in playerped.Style.GetAllComponents())
                {
                    Debug.WriteLine($"{pc} : {pc.Count} : {pc.TextureCount}");
                }

                foreach (PedComponents pc in (PedComponents[])Enum.GetValues(typeof(PedComponents)))
                {
                    PedComponent pedcomp = playerped.Style[pc];
                    Debug.WriteLine($"{pc.ToString()} : {pedcomp.Count.ToString()}");
                    if (pedcomp.HasTextureVariations)
                    {
                        Debug.WriteLine($"{pc.ToString()} : {pedcomp.TextureCount} Variations");
                        for (int txi = 0; txi < pedcomp.TextureCount; txi++)
                        {

                        }
                    }
                }

                foreach (PedProps pc in (PedProps[])Enum.GetValues(typeof(PedProps)))
                {
                    PedProp pedprop = playerped.Style[pc];
                    Debug.WriteLine($"{pc.ToString()} : {pedprop.Count.ToString()}");

                    if (pedprop.HasTextureVariations)
                    {
                        Debug.WriteLine($"{pc.ToString()} : {pedprop.TextureCount} Variations");
                        for (int txi = 0; txi < pedprop.TextureCount; txi++)
                        {

                        }
                    }

                }



                
            }
            ), false);
        }
    }
}
