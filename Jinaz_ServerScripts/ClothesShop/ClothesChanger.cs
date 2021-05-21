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


            RegisterNuiCallbackType("creationDone");
            EventHandlers["__cfx_nui:creationDone"] += new Action<IDictionary<string, object>, CallbackDelegate>(creationDone);

            RegisterNuiCallbackType("charUpdate");
            EventHandlers["__cfx_nui:charUpdate"] += new Action<IDictionary<string, object>, CallbackDelegate>(charChange);

            EventHandlers["clths:setClths"] += new Action<string, int, int>(setClothesCall);

            RegisterNuiCallbackType("exit");
            EventHandlers["__cfx_nui:exit"] += new Action<IDictionary<string, object>, CallbackDelegate>(exit);

            cmodel = Game.PlayerPed.Model;
            Tick += OnTick;
        }
        private bool lockHCamera = false;
        private bool lockVCamera = false;
        private Model cmodel;
        private async Task OnTick()
        {
            if (lockHCamera) SetGameplayCamRelativeHeading(-1 * GetGameplayCamRelativeHeading());
            if (lockVCamera) SetGameplayCamRelativePitch(GetGameplayCamRelativePitch(), 0.0f);
        }

        private void creationDone(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = 1
            });
            SendNuiMessage(jsonstring);
            SetNuiFocus(false, false);
            lockVCamera = false;
            lockHCamera = false;
            playername = arg1["name"].ToString();

            Ped ped = Game.PlayerPed;
            ClothesClass cc = new ClothesClass(head: ped.Style[PedComponents.Face].Index,
             masks: ped.Style[PedComponents.Head].Index,
             hair: ped.Style[PedComponents.Hair].Index,
             torso: ped.Style[PedComponents.Torso].Index,
             legs: ped.Style[PedComponents.Legs].Index,
             bags: ped.Style[PedComponents.Hands].Index,
             shoes: ped.Style[PedComponents.Shoes].Index,
             accessories: ped.Style[PedComponents.Special1].Index,
             undershirts: ped.Style[PedComponents.Special2].Index,
             bodyArmor: ped.Style[PedComponents.Special3].Index,
             decals: ped.Style[PedComponents.Textures].Index,
             tops: ped.Style[PedComponents.Torso2].Index,
             hats: ped.Style[PedProps.Hats].Index,
             glasses: ped.Style[PedProps.Glasses].Index,
             ears: ped.Style[PedProps.EarPieces].Index,
             watches: ped.Style[PedProps.Watches].Index,
             bracelets: ped.Style[PedProps.Wristbands].Index,
                head_variation: ped.Style[PedComponents.Face].TextureIndex,
                masks_variation: ped.Style[PedComponents.Head].TextureIndex,
                hair_variation: ped.Style[PedComponents.Hair].TextureIndex,
                torso_variation: ped.Style[PedComponents.Torso].TextureIndex,
                legs_variation: ped.Style[PedComponents.Legs].TextureIndex,
                bags_variation: ped.Style[PedComponents.Hands].TextureIndex,
                shoes_variation: ped.Style[PedComponents.Shoes].TextureIndex,
                accessories_variation: ped.Style[PedComponents.Special1].TextureIndex,
                undershirts_variation: ped.Style[PedComponents.Special2].TextureIndex,
                bodyArmor_variation: ped.Style[PedComponents.Special3].TextureIndex,
                decals_variation: ped.Style[PedComponents.Textures].TextureIndex,
                tops_variation: ped.Style[PedComponents.Torso2].TextureIndex,
                hats_variation: ped.Style[PedProps.Hats].TextureIndex,
                glasses_variation: ped.Style[PedProps.Glasses].TextureIndex,
                ears_variation: ped.Style[PedProps.EarPieces].TextureIndex,
                watches_variation: ped.Style[PedProps.Watches].TextureIndex,
                bracelets_variation: ped.Style[PedProps.Wristbands].TextureIndex,
                noseWidth: GetPedFaceFeature(ped.Handle, 0),
                noseHeight: GetPedFaceFeature(ped.Handle, 1),
                noseLength: GetPedFaceFeature(ped.Handle, 2),
                noseBridge: GetPedFaceFeature(ped.Handle, 3),
                noseTip: GetPedFaceFeature(ped.Handle, 4),
                noseBridgeShift: GetPedFaceFeature(ped.Handle, 5),
                browHeight: GetPedFaceFeature(ped.Handle, 6),
                browWidth: GetPedFaceFeature(ped.Handle, 7),
                cheekboneHeight: GetPedFaceFeature(ped.Handle, 8),
                cheekboneWidth: GetPedFaceFeature(ped.Handle, 9),
                cheeksWidth: GetPedFaceFeature(ped.Handle, 10),
                eyes: GetPedFaceFeature(ped.Handle, 11),
                lips_: GetPedFaceFeature(ped.Handle, 12),
                jawWidth: GetPedFaceFeature(ped.Handle, 13),
                jawHeight: GetPedFaceFeature(ped.Handle, 14),
                chinLength: GetPedFaceFeature(ped.Handle, 15),
                chinPosition: GetPedFaceFeature(ped.Handle, 16),
                chinWidth: GetPedFaceFeature(ped.Handle, 17),
                chinShape: GetPedFaceFeature(ped.Handle, 18),
                neckWidth: GetPedFaceFeature(ped.Handle, 19));
            Debug.WriteLine("A");
            var jss = JsonConvert.SerializeObject(cc);
            Debug.WriteLine(jss);
            //send to SQLDB
            TriggerServerEvent("cbc:SaveLook", jss, playername,cgender);
            //trigger TP script
        }

        int cgender = 0;
        string playername = "";

        private async void charChange(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {

            /*
             "head":0,\
        "masks":0, \
        "hair":0, \
        "torso":0, \
        "legs":0, \
        "bags":0, \
        "shoes":0, \
        "accessories":0, \
        "undershirts":0, \
        "bodyArmor":0, \
        "decals":0, \
        "tops":0, \
        "head_variation":0,\
        "masks_variation":0, \
        "hair_variation":0, \
        "torso_variation":0, \
        "legs_variation":0, \
        "bags_variation":0, \
        "shoes_variation":0, \
        "accessories_variation":0, \
        "undershirts_variation":0, \
        "bodyArmor_variation":0, \
        "decals_variation":0, \
        "tops_variation":0, \
        "hats":0, \
        "glasses":0, \
        "ears":0, \
        "watches":0, \
        "bracelets":0, \
        "hats_variation":0, \
        "glasses_variation":0, \
        "ears_variation":0, \
        "watches_variation":0, \
        "bracelets_variation":0, \
        "noseWidth":0.0,\
        "noseHeight":0.0,\
        "noseLength":0.0,\
        "noseBridge":0.0,\
        "noseTip":0.0,\
        "noseBridgeShift":0.0,\
        "browHeight":0.0,\
        "browWidth":0.0,\
        "cheekboneHeight":0.0,\
        "cheekboneWidth":0.0,\
        "cheeksWidth":0.0,\
        "eyes":0.0,\
        "lips_":0.0,\
        "jawWidth":0.0,\
        "jawHeight":0.0,\
        "chinLength":0.0,\
        "chinPosition":0.0,\
        "chinWidth":0.0,\
        "chinShape":0.0,\
        "neckWidth":0.0\
             */

            //SetPedEyeColor();
            //SetPedHeadOverlay();
            //Game.PlayerPed.


            int pedid = Game.PlayerPed.Handle;
            Debug.WriteLine(arg1["head"].ToString());
            int faceShape = Convert.ToInt32(arg1["head"].ToString());
            int skincolor = Convert.ToInt32(arg1["head_variation"].ToString());
            SetPedHeadBlendData(pedid, 0, 0, faceShape, 0, 0, skincolor, 0.0f, 0.0f, 1.0f, false);

            Debug.WriteLine(arg1["gender"].ToString());

            if (Convert.ToInt32(arg1["gender"].ToString()) != cgender)
            {
                if (Convert.ToInt32(arg1["gender"].ToString()) == 0)
                {

                    var model = new Model(PedHash.FreemodeMale01);
                    bool x = await Game.Player.ChangeModel(model);
                    cgender = 0;
                    foreach (PedComponents pc in (PedComponents[])Enum.GetValues(typeof(PedComponents)))
                    {
                        Game.PlayerPed.Style[pc].SetVariation(0);
                    }
                    foreach (PedProps pc in (PedProps[])Enum.GetValues(typeof(PedProps)))
                    {
                        Game.PlayerPed.Style[pc].SetVariation(0);
                    }
                }
                else
                {

                    var model = new Model(PedHash.FreemodeFemale01);
                    bool x = await Game.Player.ChangeModel(model);
                    cgender = 1;
                    foreach (PedComponents pc in (PedComponents[])Enum.GetValues(typeof(PedComponents)))
                    {
                        Game.PlayerPed.Style[pc].SetVariation(0);
                    }
                    foreach (PedProps pc in (PedProps[])Enum.GetValues(typeof(PedProps)))
                    {
                        Game.PlayerPed.Style[pc].SetVariation(0);
                    }
                }
            }

            int masks = Convert.ToInt32(arg1["masks"].ToString());
            int masksvariation = Convert.ToInt32(arg1["masks_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Head].IsVariationValid(masks, masksvariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Head].IsVariationValid(masks, masksvariation))
                Game.PlayerPed.Style[PedComponents.Head].SetVariation(masks, masksvariation);
            int hair = Convert.ToInt32(arg1["hair"].ToString());
            int hairvariation = Convert.ToInt32(arg1["hair_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Hair].IsVariationValid(hair, hairvariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Hair].IsVariationValid(hair, hairvariation))
                Game.PlayerPed.Style[PedComponents.Hair].SetVariation(hair, hairvariation);
            int torso = Convert.ToInt32(arg1["torso"].ToString());
            int torsovariation = Convert.ToInt32(arg1["torso_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Torso].IsVariationValid(torso, torsovariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Torso].IsVariationValid(torso, torsovariation))
                Game.PlayerPed.Style[PedComponents.Torso].SetVariation(torso, torsovariation);
            int legs = Convert.ToInt32(arg1["legs"].ToString());
            int legsvariation = Convert.ToInt32(arg1["legs_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Legs].IsVariationValid(legs, legsvariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Legs].IsVariationValid(legs, legsvariation))
                Game.PlayerPed.Style[PedComponents.Legs].SetVariation(legs, legsvariation);
            int bags = Convert.ToInt32(arg1["bags"].ToString());
            int bagsvariation = Convert.ToInt32(arg1["bags_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Hands].IsVariationValid(bags, bagsvariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Hands].IsVariationValid(bags, bagsvariation))
                Game.PlayerPed.Style[PedComponents.Hands].SetVariation(bags, bagsvariation);
            int shoes = Convert.ToInt32(arg1["shoes"].ToString());
            int shoesvariation = Convert.ToInt32(arg1["shoes_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Shoes].IsVariationValid(shoes, shoesvariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Shoes].IsVariationValid(shoes, shoesvariation))
                Game.PlayerPed.Style[PedComponents.Shoes].SetVariation(shoes, shoesvariation);
            int accessories = Convert.ToInt32(arg1["accessories"].ToString());
            int accessoriesvariation = Convert.ToInt32(arg1["accessories_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Special1].IsVariationValid(accessories, accessoriesvariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Special1].IsVariationValid(accessories, accessoriesvariation))
                Game.PlayerPed.Style[PedComponents.Special1].SetVariation(accessories, accessoriesvariation);
            int undershirts = Convert.ToInt32(arg1["undershirts"].ToString());
            int undershirtsvariation = Convert.ToInt32(arg1["undershirts_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Special2].IsVariationValid(undershirts, undershirtsvariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Special2].IsVariationValid(undershirts, undershirtsvariation))
                Game.PlayerPed.Style[PedComponents.Special2].SetVariation(undershirts, undershirtsvariation);
            int bodyArmor = Convert.ToInt32(arg1["bodyArmor"].ToString());
            int bodyArmorvariation = Convert.ToInt32(arg1["bodyArmor_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Special3].IsVariationValid(bodyArmor, bodyArmorvariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Special3].IsVariationValid(bodyArmor, bodyArmorvariation))
                Game.PlayerPed.Style[PedComponents.Special3].SetVariation(bodyArmor, bodyArmorvariation);
            int decals = Convert.ToInt32(arg1["decals"].ToString());
            int decalsvariation = Convert.ToInt32(arg1["decals_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Textures].IsVariationValid(decals, decalsvariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Textures].IsVariationValid(decals, decalsvariation))
                Game.PlayerPed.Style[PedComponents.Textures].SetVariation(decals, decalsvariation);
            int tops = Convert.ToInt32(arg1["tops"].ToString());
            int topsvariation = Convert.ToInt32(arg1["tops_variation"].ToString());
            Debug.WriteLine(Game.PlayerPed.Style[PedComponents.Torso2].IsVariationValid(tops, topsvariation).ToString());
            if (Game.PlayerPed.Style[PedComponents.Torso2].IsVariationValid(tops, topsvariation))
                Game.PlayerPed.Style[PedComponents.Torso2].SetVariation(tops, topsvariation);

            int hats = Convert.ToInt32(arg1["hats"].ToString());
            int hats_variation = Convert.ToInt32(arg1["hats_variation"].ToString());
            if (Game.PlayerPed.Style[PedProps.Hats].IsVariationValid(hats, hats_variation))
                Game.PlayerPed.Style[PedProps.Hats].SetVariation(hats, hats_variation);
            int glasses = Convert.ToInt32(arg1["glasses"].ToString());
            int glasses_variation = Convert.ToInt32(arg1["glasses_variation"].ToString());
            if (Game.PlayerPed.Style[PedProps.Glasses].IsVariationValid(glasses, glasses_variation))
                Game.PlayerPed.Style[PedProps.Glasses].SetVariation(glasses, glasses_variation);
            int ears = Convert.ToInt32(arg1["ears"].ToString());
            int ears_variation = Convert.ToInt32(arg1["ears_variation"].ToString());
            if (Game.PlayerPed.Style[PedProps.EarPieces].IsVariationValid(ears, ears_variation))
                Game.PlayerPed.Style[PedProps.EarPieces].SetVariation(ears, ears_variation);
            int watches = Convert.ToInt32(arg1["watches"].ToString());
            int watches_variation = Convert.ToInt32(arg1["watches_variation"].ToString());
            if (Game.PlayerPed.Style[PedProps.Watches].IsVariationValid(watches, watches_variation))
                Game.PlayerPed.Style[PedProps.Watches].SetVariation(watches, watches_variation);
            int bracelets = Convert.ToInt32(arg1["bracelets"].ToString());
            int bracelets_variation = Convert.ToInt32(arg1["bracelets_variation"].ToString());
            if (Game.PlayerPed.Style[PedProps.Wristbands].IsVariationValid(bracelets, bracelets_variation))
                Game.PlayerPed.Style[PedProps.Wristbands].SetVariation(bracelets, bracelets_variation);

            float noseWidth = float.Parse(arg1["noseWidth"].ToString());
            SetPedFaceFeature(pedid, 0, noseWidth);
            float noseHeight = float.Parse(arg1["noseHeight"].ToString());
            SetPedFaceFeature(pedid, 1, noseHeight);
            float noseLength = float.Parse(arg1["noseLength"].ToString());
            SetPedFaceFeature(pedid, 2, noseLength);
            float noseBridge = float.Parse(arg1["noseBridge"].ToString());
            SetPedFaceFeature(pedid, 3, noseBridge);
            float noseTip = float.Parse(arg1["noseTip"].ToString());
            SetPedFaceFeature(pedid, 4, noseTip);
            float noseBridgeShift = float.Parse(arg1["noseBridgeShift"].ToString());
            SetPedFaceFeature(pedid, 5, noseBridgeShift);
            float browHeight = float.Parse(arg1["browHeight"].ToString());
            SetPedFaceFeature(pedid, 6, browHeight);
            float browWidth = float.Parse(arg1["browWidth"].ToString());
            SetPedFaceFeature(pedid, 7, browWidth);
            float cheekboneHeight = float.Parse(arg1["cheekboneHeight"].ToString());
            SetPedFaceFeature(pedid, 8, cheekboneHeight);
            float cheekboneWidth = float.Parse(arg1["cheekboneWidth"].ToString());
            SetPedFaceFeature(pedid, 9, cheekboneWidth);
            float cheeksWidth = float.Parse(arg1["cheeksWidth"].ToString());
            SetPedFaceFeature(pedid, 10, cheeksWidth);
            float eyes = float.Parse(arg1["eyes"].ToString());
            SetPedFaceFeature(pedid, 11, eyes);
            float lips_ = float.Parse(arg1["lips_"].ToString());
            SetPedFaceFeature(pedid, 12, lips_);
            float jawWidth = float.Parse(arg1["jawWidth"].ToString());
            SetPedFaceFeature(pedid, 13, jawWidth);
            float jawHeight = float.Parse(arg1["jawHeight"].ToString());
            SetPedFaceFeature(pedid, 14, jawHeight);
            float chinLength = float.Parse(arg1["chinLength"].ToString());
            SetPedFaceFeature(pedid, 15, chinLength);
            float chinPosition = float.Parse(arg1["chinPosition"].ToString());
            SetPedFaceFeature(pedid, 16, chinPosition);
            float chinWidth = float.Parse(arg1["chinWidth"].ToString());
            SetPedFaceFeature(pedid, 17, chinWidth);
            float chinShape = float.Parse(arg1["chinShape"].ToString());
            SetPedFaceFeature(pedid, 18, chinShape);
            float neckWidth = float.Parse(arg1["neckWidth"].ToString());
            SetPedFaceFeature(pedid, 19, neckWidth);


            updateCharToHTML();
            
        }

        private void updateCharToHTML()
        {
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = 2,
                head_variation = 45,
                masks_variation = Game.PlayerPed.Style[PedComponents.Head].TextureCount,
                hair_variation = Game.PlayerPed.Style[PedComponents.Hair].TextureCount,
                torso_variation = Game.PlayerPed.Style[PedComponents.Torso].TextureCount,
                legs_variation = Game.PlayerPed.Style[PedComponents.Legs].TextureCount,
                bags_variation = Game.PlayerPed.Style[PedComponents.Hands].TextureCount,
                shoes_variation = Game.PlayerPed.Style[PedComponents.Shoes].TextureCount,
                accessories_variation = Game.PlayerPed.Style[PedComponents.Special1].TextureCount,
                undershirts_variation = Game.PlayerPed.Style[PedComponents.Special2].TextureCount,
                bodyArmor_variation = Game.PlayerPed.Style[PedComponents.Special3].TextureCount,
                decals_variation = Game.PlayerPed.Style[PedComponents.Textures].TextureCount,
                tops_variation = Game.PlayerPed.Style[PedComponents.Torso2].TextureCount,
                hats_variation = Game.PlayerPed.Style[PedProps.Hats].TextureCount,
                glasses_variation = Game.PlayerPed.Style[PedProps.Glasses].TextureCount,
                ears_variation = Game.PlayerPed.Style[PedProps.EarPieces].TextureCount,
                watches_variation = Game.PlayerPed.Style[PedProps.Watches].TextureCount,
                bracelets_variation = Game.PlayerPed.Style[PedProps.Wristbands].TextureCount
            });
            SendNuiMessage(jsonstring);
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
                toggle = 1
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
            Debug.WriteLine("openui");
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = 0,
                characreation = true
            });
            SendNuiMessage(jsonstring);
            SetNuiFocus(true, true);
            lockHCamera = true;
            lockVCamera = true;
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
