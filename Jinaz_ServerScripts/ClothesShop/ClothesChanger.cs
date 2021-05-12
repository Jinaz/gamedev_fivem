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
using System.Dynamic;

namespace ClothesShop
{
    public class ClothesChanger : BaseScript
    {
        public ClothesChanger()
        {

            EventHandlers["clths:changeClths"] += new Action(openUI);

            RegisterNuiCallbackType("updateLoginscrChara");
            EventHandlers["__cfx_nui:updateLoginscrChara"] += new Action<IDictionary<string, object>, CallbackDelegate>(updateChar);
            
            EventHandlers["clths:setClths"] += new Action<string, int, int>(setClothesCall);

            RegisterNuiCallbackType("exit");
            EventHandlers["__cfx_nui:exit"] += new Action<IDictionary<string, object>, CallbackDelegate>(exit);


        }

        private void setClothesCall(string arg1, int arg2, int arg3)
        {
            foreach (PedComponents ps in (PedComponents[])Enum.GetValues(typeof(PedComponents)))
            {
                if (arg1 == ps.ToString())
                    setComponentVariation(ps, arg2, arg3);
            }

            foreach (PedProps ps in (PedProps[])Enum.GetValues(typeof(PedProps)))
            {
                if (arg1 == ps.ToString())
                    setComponentVariation(ps, arg2, arg3);
            }
        }

        private void exit(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = false
            });
            SendNuiMessage(jsonstring);
            SetNuiFocus(false, false);
        }

        private void updateChar(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            string part = arg1["part"].ToString();
            int index = Convert.ToInt32(arg1["index"]);
            int variation = Convert.ToInt32(arg1["variation"]);

            setClothesCall(part, index, variation);
        }

        private void openUI()
        {
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = false
            });
            SendNuiMessage(jsonstring);
            SetNuiFocus(true, true);
        }

        private void setComponentVariation(PedComponents pc, int index, int variation)
        {
            if (Game.PlayerPed.Style[pc].Index != index)
            {
                if (Game.PlayerPed.Style[pc].IsVariationValid(index, variation))
                    Game.PlayerPed.Style[pc].SetVariation(index, variation);
            }
        }
        private void setComponentVariation(PedProps pc, int index, int variation)
        {
            if (Game.PlayerPed.Style[pc].Index != index)
            {
                if (Game.PlayerPed.Style[pc].IsVariationValid(index, variation))
                    Game.PlayerPed.Style[pc].SetVariation(index, variation);
            }
        }
        private void setClothes(
            int face, int face_variation, int head, int head_variation,
            int hair, int hair_variation, int torso, int torso_variation,
            int legs, int legs_variation, int hands, int hands_variation,
            int shoes, int shoes_variation, int special1, int special1_variation,
            int special2, int special2_variation, int special3, int special3_variation,
            int textures, int textures_variation, int torso2, int torso2_variation,
            int hats, int hats_variation, int glasses, int glasses_variation,
            int earpieces, int earpieces_variation, int watches, int watches_variation,
            int wristbands, int wristbands_variation
            )
        {

            setComponentVariation(PedComponents.Face, face, face_variation);
            setComponentVariation(PedComponents.Hair, hair, hair_variation);
            setComponentVariation(PedComponents.Head, head, head_variation);
            setComponentVariation(PedComponents.Hands, hands, hands_variation);
            setComponentVariation(PedComponents.Legs, legs, legs_variation);
            setComponentVariation(PedComponents.Shoes, shoes, shoes_variation);
            setComponentVariation(PedComponents.Special1, special1, special1_variation);
            setComponentVariation(PedComponents.Special2, special2, special2_variation);
            setComponentVariation(PedComponents.Special3, special3, special3_variation);
            setComponentVariation(PedComponents.Textures, textures, textures_variation);
            setComponentVariation(PedComponents.Torso, torso, torso_variation);
            setComponentVariation(PedComponents.Torso2, torso2, torso2_variation);

            setComponentVariation(PedProps.EarPieces, earpieces, earpieces_variation);
            setComponentVariation(PedProps.Glasses, glasses, glasses_variation);
            setComponentVariation(PedProps.Hats, hats, hats_variation);
            setComponentVariation(PedProps.Watches, watches, watches_variation);
            setComponentVariation(PedProps.Wristbands, wristbands, wristbands_variation);

        }

    }
}
