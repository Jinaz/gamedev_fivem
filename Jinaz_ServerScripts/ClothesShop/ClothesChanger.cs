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
using CitizenFX.Core.Native;

namespace ClothesShop
{
    public class ClothesChanger : BaseScript
    {
        ClothesClass currentclothes;
        private bool inshop = false;
        private Model cmodel;
        protected Scaleform buttons = new Scaleform("instructional_buttons");
        public static Control viewToggleControl = Control.Aim;
        int cgender = 0;
        string playername = "";

        public ClothesChanger()
        {

            currentclothes = new ClothesClass();

            //method to set clothes as an event used in wardrobe via server
            EventHandlers["clths:setclths"] += new Action<string>(setClothesCall);

            //standard enter shop methods
            EventHandlers["clths:entershop"] += new Action(entershop);
            RegisterNuiCallbackType("checkout");
            EventHandlers["__cfx_nui:checkout"] += new Action<IDictionary<string, object>, CallbackDelegate>(checkout);
            EventHandlers["clths:checkoutFail"] += new Action<bool>(checkoutReturned);
            RegisterNuiCallbackType("exit");
            EventHandlers["__cfx_nui:exit"] += new Action<IDictionary<string, object>, CallbackDelegate>(exit);

            //update char and control camera
            RegisterNuiCallbackType("charUpdate");
            EventHandlers["__cfx_nui:charUpdate"] += new Action<IDictionary<string, object>, CallbackDelegate>(charChange);
            RegisterNuiCallbackType("unlockcam");
            EventHandlers["__cfx_nui:unlockcam"] += new Action<IDictionary<string, object>, CallbackDelegate>(unlockcam);

            //methods for wardrobe do not implement here
            //EventHandlers["clths:saveinDB"] += new Action<string>(saveInDB);
            //EventHandlers["clths:saveinDB"] += new Action<string>(loadLooks);

            //create done so save to database
            EventHandlers["clths:changeClths"] += new Action<int>(startCharacterCreation);
            RegisterNuiCallbackType("creationDone");
            EventHandlers["__cfx_nui:creationDone"] += new Action<IDictionary<string, object>, CallbackDelegate>(creationDone);
            

            
            cmodel = Game.PlayerPed.Model;
            Tick += OnTick;
        }

        private void updateCharVisual(string clothesAsJson)
        {
            var clothes = JsonConvert.DeserializeObject<ClothesClass>(clothesAsJson);


            int head = clothes.head;
            int headvariation = clothes.head_variation;
            Game.PlayerPed.Style[PedComponents.Face].SetVariation(head, headvariation);

            int masks = clothes.masks;
            int masksvariation = clothes.masks_variation;
            Game.PlayerPed.Style[PedComponents.Head].SetVariation(masks, masksvariation);

            int hair = clothes.hair;
            int hairvariation = clothes.hair_variation;
            Game.PlayerPed.Style[PedComponents.Hair].SetVariation(hair, hairvariation);

            int torso = clothes.torso;
            int torsovariation = clothes.torso_variation;
            Game.PlayerPed.Style[PedComponents.Torso].SetVariation(torso, torsovariation);

            int legs = clothes.legs;
            int legsvariation = clothes.legs_variation;
            Game.PlayerPed.Style[PedComponents.Legs].SetVariation(legs, legsvariation);

            int bags = clothes.bags;
            int bagsvariation = clothes.bags_variation;
            Game.PlayerPed.Style[PedComponents.Hands].SetVariation(bags, bagsvariation);

            int shoes = clothes.shoes;
            int shoesvariation = clothes.shoes_variation;
            Game.PlayerPed.Style[PedComponents.Shoes].SetVariation(shoes, shoesvariation);

            int accessories = clothes.accessories;
            int accessoriesvariation = clothes.accessories_variation;
            Game.PlayerPed.Style[PedComponents.Special1].SetVariation(accessories, accessoriesvariation);

            int undershirts = clothes.undershirts;
            int undershirtsvariation = clothes.undershirts_variation;
            Game.PlayerPed.Style[PedComponents.Special2].SetVariation(undershirts, undershirtsvariation);

            int bodyArmor = clothes.bodyArmor;
            int bodyArmorvariation = clothes.bodyArmor_variation;
            Game.PlayerPed.Style[PedComponents.Special3].SetVariation(bodyArmor, bodyArmorvariation);

            int decals = clothes.decals;
            int decalsvariation = clothes.decals_variation;
            Game.PlayerPed.Style[PedComponents.Textures].SetVariation(decals, decalsvariation);

            int tops = clothes.tops;
            int topsvariation = clothes.tops_variation;
            Game.PlayerPed.Style[PedComponents.Torso2].SetVariation(tops, topsvariation);

            int hats = clothes.hats;
            int hatsvariation = clothes.hats_variation;
            Game.PlayerPed.Style[PedProps.Hats].SetVariation(hats, hatsvariation);

            int glasses = clothes.glasses;
            int glassesvariation = clothes.glasses_variation;
            Game.PlayerPed.Style[PedProps.Glasses].SetVariation(glasses, glassesvariation);

            int ears = clothes.ears;
            int earsvariation = clothes.ears_variation;
            Game.PlayerPed.Style[PedProps.EarPieces].SetVariation(ears, earsvariation);

            int watches = clothes.watches;
            int watchesvariation = clothes.watches_variation;
            Game.PlayerPed.Style[PedProps.Watches].SetVariation(watches, watchesvariation);

            int bracelets = clothes.bracelets;
            int braceletsvariation = clothes.bracelets_variation;
            Game.PlayerPed.Style[PedProps.Wristbands].SetVariation(bracelets, braceletsvariation);

            int pedid = Game.PlayerPed.Handle;
            float noseWidth = clothes.noseWidth;
            SetPedFaceFeature(pedid, 0, noseWidth);
            float noseHeight = clothes.noseHeight;
            SetPedFaceFeature(pedid, 0, noseHeight);
            float noseLength = clothes.noseLength;
            SetPedFaceFeature(pedid, 0, noseLength);
            float noseBridge = clothes.noseBridge;
            SetPedFaceFeature(pedid, 0, noseBridge);
            float noseTip = clothes.noseTip;
            SetPedFaceFeature(pedid, 0, noseTip);
            float noseBridgeShift = clothes.noseBridgeShift;
            SetPedFaceFeature(pedid, 0, noseBridgeShift);
            float browHeight = clothes.browHeight;
            SetPedFaceFeature(pedid, 0, browHeight);
            float browWidth = clothes.browWidth;
            SetPedFaceFeature(pedid, 0, browWidth);
            float cheekboneHeight = clothes.cheekboneHeight;
            SetPedFaceFeature(pedid, 0, cheekboneHeight);
            float cheekboneWidth = clothes.cheekboneWidth;
            SetPedFaceFeature(pedid, 0, cheekboneWidth);
            float cheeksWidth = clothes.cheeksWidth;
            SetPedFaceFeature(pedid, 0, cheeksWidth);
            float eyes = clothes.eyes;
            SetPedFaceFeature(pedid, 0, eyes);
            float lips_ = clothes.lips_;
            SetPedFaceFeature(pedid, 0, lips_);
            float jawWidth = clothes.jawWidth;
            SetPedFaceFeature(pedid, 0, jawWidth);
            float jawHeight = clothes.jawHeight;
            SetPedFaceFeature(pedid, 0, jawHeight);
            float chinLength = clothes.chinLength;
            SetPedFaceFeature(pedid, 0, chinLength);
            float chinPosition = clothes.chinPosition;
            SetPedFaceFeature(pedid, 0, chinPosition);
            float chinWidth = clothes.chinWidth;
            SetPedFaceFeature(pedid, 0, chinWidth);
            float chinShape = clothes.chinShape;
            SetPedFaceFeature(pedid, 0, chinShape);
            float neckWidth = clothes.neckWidth;
            SetPedFaceFeature(pedid, 0, neckWidth);
        }

        private void checkoutReturned(bool failed)
        {
            if (failed)
            {
                updateCharVisual(JsonConvert.SerializeObject(currentclothes));
            }
        }

        private void checkout(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            
            //get cost from arg1
            int cost = Convert.ToInt32(arg1["price"]);

            TriggerEvent("charinf:checkoutMoney", 0, cost);

        }

        private void entershop()
        {
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = 0,
                characreation = false,
                gender = cgender,
                head = currentclothes.head,
                masks = currentclothes.masks,
                hair = currentclothes.hair,
                torso = currentclothes.torso,
                legs = currentclothes.legs,
                bags = currentclothes.bags,
                shoes = currentclothes.shoes,
                accessories = currentclothes.accessories,
                undershirts = currentclothes.undershirts,
                bodyArmor = currentclothes.bodyArmor,
                decals = currentclothes.decals,
                tops = currentclothes.tops,
                hats = currentclothes.hats,
                glasses = currentclothes.glasses,
                ears = currentclothes.ears,
                watches = currentclothes.watches,
                bracelets = currentclothes.bracelets,
                noseWidth = currentclothes.noseWidth,
                noseHeight = currentclothes.noseHeight,
                noseLength = currentclothes.noseLength,
                noseBridge = currentclothes.noseBridge,
                noseTip = currentclothes.noseTip,
                noseBridgeShift = currentclothes.noseBridgeShift,
                browHeight = currentclothes.browHeight,
                browWidth = currentclothes.browWidth,
                cheekboneHeight = currentclothes.cheekboneHeight,
                cheekboneWidth = currentclothes.cheekboneWidth,
                cheeksWidth = currentclothes.cheeksWidth,
                eyes = currentclothes.eyes,
                lips_ = currentclothes.lips_,
                jawWidth = currentclothes.jawWidth,
                jawHeight = currentclothes.jawHeight,
                chinLength = currentclothes.chinLength,
                chinPosition = currentclothes.chinPosition,
                chinWidth = currentclothes.chinWidth,
                chinShape = currentclothes.chinShape,
                neckWidth = currentclothes.neckWidth,
                head_variation = currentclothes.head_variation,
                masks_variation = currentclothes.masks_variation,
                hair_variation = currentclothes.hair_variation,
                torso_variation = currentclothes.torso_variation,
                legs_variation = currentclothes.legs_variation,
                bags_variation = currentclothes.bags_variation,
                shoes_variation = currentclothes.shoes_variation,
                accessories_variation = currentclothes.accessories_variation,
                undershirts_variation = currentclothes.undershirts_variation,
                bodyArmor_variation = currentclothes.bodyArmor_variation,
                decals_variation = currentclothes.decals_variation,
                tops_variation = currentclothes.tops_variation,
                hats_variation = currentclothes.hats_variation,
                glasses_variation = currentclothes.glasses_variation,
                ears_variation = currentclothes.ears_variation,
                watches_variation = currentclothes.watches_variation,
                bracelets_variation = currentclothes.bracelets_variation

            });
            SendNuiMessage(jsonstring);
            SetNuiFocus(true, true);
            inshop = true;
        }

        private void unlockcam(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            SetNuiFocus(false, false);
        }

        public void InstructChangeView()
        {
            buttons.CallFunction("CLEAR_ALL");
            buttons.CallFunction("TOGGLE_MOUSE_BUTTONS", 0);
            buttons.CallFunction("CREATE_CONTAINER");

            buttons.CallFunction("SET_DATA_SLOT", 0, Function.Call<string>((Hash)0x0499D7B09FC9B407, 2, (int)viewToggleControl, false), "ToggleMouse");

            buttons.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", -1);
        }
        private async Task OnTick()
        {
            //if (lockHCamera) SetGameplayCamRelativeHeading(-1 * GetGameplayCamRelativeHeading());
            //if (lockVCamera) SetGameplayCamRelativePitch(GetGameplayCamRelativePitch(), 0.0f);
            InvalidateIdleCam();
            InvalidateVehicleIdleCam();
            InstructChangeView();
            if (Game.IsControlJustReleased(0, viewToggleControl) && inshop)
            {
                SetNuiFocus(true, true);
            }
        }

        //creating a char
        private void creationDone(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = 1
            });
            SendNuiMessage(jsonstring);
            SetNuiFocus(false, false);
            inshop = false;
            playername = arg1["name"].ToString();

            Ped ped = Game.PlayerPed;
            currentclothes = new ClothesClass(head: ped.Style[PedComponents.Face].Index,
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
            var jss = JsonConvert.SerializeObject(currentclothes);
            Debug.WriteLine(jss);
            //send to SQLDB
            TriggerServerEvent("cbc:SaveLook", jss, playername,cgender);
            //trigger TP script
        }

        //UI connector class, changes live when in ui change is submitted
        private async void charChange(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            
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

        //UI connector class
        private void updateCharToHTML()
        {
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = 2,
                head_variations = 45,
                masks_variations = Game.PlayerPed.Style[PedComponents.Head].TextureCount,
                hair_variations = Game.PlayerPed.Style[PedComponents.Hair].TextureCount,
                torso_variations = Game.PlayerPed.Style[PedComponents.Torso].TextureCount,
                legs_variations = Game.PlayerPed.Style[PedComponents.Legs].TextureCount,
                bags_variations = Game.PlayerPed.Style[PedComponents.Hands].TextureCount,
                shoes_variations = Game.PlayerPed.Style[PedComponents.Shoes].TextureCount,
                accessories_variations = Game.PlayerPed.Style[PedComponents.Special1].TextureCount,
                undershirts_variations = Game.PlayerPed.Style[PedComponents.Special2].TextureCount,
                bodyArmor_variations = Game.PlayerPed.Style[PedComponents.Special3].TextureCount,
                decals_variations = Game.PlayerPed.Style[PedComponents.Textures].TextureCount,
                tops_variations = Game.PlayerPed.Style[PedComponents.Torso2].TextureCount,
                hats_variations = Game.PlayerPed.Style[PedProps.Hats].TextureCount,
                glasses_variations = Game.PlayerPed.Style[PedProps.Glasses].TextureCount,
                ears_variations = Game.PlayerPed.Style[PedProps.EarPieces].TextureCount,
                watches_variations = Game.PlayerPed.Style[PedProps.Watches].TextureCount,
                bracelets_variations = Game.PlayerPed.Style[PedProps.Wristbands].TextureCount
            });
            Debug.WriteLine(jsonstring);
            SendNuiMessage(jsonstring);
        }

        private void setClothesCall(string arg1)
        {
            currentclothes = JsonConvert.DeserializeObject<ClothesClass>(arg1);
            updateCharVisual(JsonConvert.SerializeObject(currentclothes));
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
            updateCharVisual(JsonConvert.SerializeObject(currentclothes));
        }
        //char creation method
        private void startCharacterCreation(int open)
        {
           
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = open,
                characreation = true
            });
            SendNuiMessage(jsonstring);
            SetNuiFocus(true, true);
            inshop = true;
        }

       
        

    }
}
