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


    public class CharacterInterface : BaseScript
    {
        float hunger;
        float thirst;

        bool inventoryinit = false;
        Ped playerped;
        Inventory inv;

        string hunger_key = "_hunger_key";
        string thirst_key = "_thirst_key";

        public CharacterInterface()
        {
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);

            hunger = 0.75f;
            thirst = 0.75f;


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
            float consumedThirst = 0f;
            if (playerped.IsRunning)
            {
                consumedThirst += 
            }


        }

        private void initInv(Ped playerped)
        {
            inv = new Inventory();
            Ped ped;

        }

        private void OnResourceStart(string obj)
        {
            playerped = Game.PlayerPed;

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
                }



                /*
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


                /*"Hats = 0,
        Glasses = 1,
        EarPieces = 2,
        Unknown3 = 3,
        Unknown4 = 4,
        Unknown5 = 5,
        Watches = 6,
        Wristbands = 7,
        Unknown8 = 8,
        Unknown9 = 9"*/
            }
            ), false);
        }
    }
}
