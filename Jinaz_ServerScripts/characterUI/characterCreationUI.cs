using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace characterUI
{
    public class characterCreationUI : BaseScript
    {
        public characterCreationUI()
        {
            EventHandlers["loginscr:createChar"] += new Action(createChar);

            //register net event to change appearance
        }

        private void createChar()
        {
            Debug.WriteLine("ALL ped things");

            Ped playerped = Game.PlayerPed;

            /*
             public int Count { get; }
        public int Index { get; set; }
        public int TextureCount { get; }
        public int TextureIndex { get; set; }
        public bool HasVariations { get; }
        public bool HasTextureVariations { get; }
        public bool HasAnyVariations { get; }

        public bool IsVariationValid(int index, int textureIndex = 0);
        public bool SetVariation(int index, int textureIndex = 0);
        public override string ToString();
             */
            PedComponent[] pcs = playerped.Style.GetAllComponents();
            Debug.WriteLine("pcs"+pcs.Length.ToString());

            foreach (PedComponent pc in pcs)
            {
                Debug.WriteLine(pc.ToString());
                Debug.WriteLine(pc.Count.ToString());
                for (int i = 0; i < pc.Count; i++)
                {
                    Debug.WriteLine($"Component {pc.ToString()} with index {i.ToString()}");
                    //pc.Index = i;
                    Debug.WriteLine($"has {pc.TextureCount.ToString()} Textures");
                }


            }
            PedProp[] pps = playerped.Style.GetAllProps();
            Debug.WriteLine("pps"+pps.Length.ToString());
            foreach (PedProp pp in pps)
            {
                Debug.WriteLine(pp.ToString());
                Debug.WriteLine(pp.Count.ToString());
                for (int i = 0; i < pp.Count; i++)
                {
                    Debug.WriteLine($"Prop {pp.ToString()} with index {i.ToString()}");
                    //pp.Index = i;
                    Debug.WriteLine($"has {pp.TextureCount.ToString()} Textures");
                }


            }

            Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            bool valid = playerped.Style[PedComponents.Special2].IsVariationValid(12);
            Debug.WriteLine(valid.ToString());
            if (valid) playerped.Style[PedComponents.Special2].SetVariation(12);
            /*
            Dictionary<int, List<int>>[] components = new Dictionary<int, List<int>>[12];
            for (int i = 0; i< 12; i++)
            {
                components[i] = new Dictionary<int, List<int>>();
            }
            foreach (PedComponents pc in (PedComponents[])Enum.GetValues(typeof(PedComponents)))
            {
                int drawable = 361;
                for (int i = 0; i < drawable; i++)
                {
                    int variation = 200;
                    List<int> variations = new List<int>();
                    for (int j = 0; j < variation; j++)
                    {
                        //Debug.WriteLine("j:" + j);
                        if (playerped.Style[pc].IsVariationValid(i, j))
                        {
                            //Debug.WriteLine("valid found");
                            variations.Add(j);
                        }
                    }
                    components[i] = new Dictionary<int, List<int>>() { { i, variations } };
                    //Debug.WriteLine(i + " done.");
                }
                //Debug.WriteLine(pc.ToString()+" done.");

            }
            
            for (int i = 0; i < 12; i++)
            Debug.WriteLine(components[i].ToString());*/

            //
            //Debug.WriteLine(playerped.Style[PedComponents.Torso2].IsVariationValid(269).ToString());

            foreach (PedProps pc in (PedProps[])Enum.GetValues(typeof(PedProps)))
            {
                //Debug.WriteLine(pc.ToString() + " :Index: " + playerped.Style[pc].Index.ToString());

                PedProp pedprop = playerped.Style[pc];
                //Debug.WriteLine($"{pc.ToString()} : {pedprop.Count.ToString()}");

                if (pedprop.HasTextureVariations)
                {
                    //Debug.WriteLine($"{pc.ToString()} : {pedprop.TextureCount} Variations");
                    
                }
            }

            //playerped.Style.SetDefaultClothes();
            //uint modelhashed = (uint)GetHashKey("mp_f_freemode_01");
            //RequestModel(modelhashed);
            //while (!HasModelLoaded(modelhashed))
            //RequestModel(modelhashed);
            //SetPlayerModel(Game.Player.ServerId, modelhashed);
            Debug.WriteLine(Game.Player.ServerId.ToString());
            //trigger UI
            //throw new NotImplementedException();
        }
    }
}
