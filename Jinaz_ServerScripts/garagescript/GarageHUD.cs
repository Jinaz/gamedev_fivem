using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;
using System.Drawing;
using TinyTween;

namespace garagescript
{
    public class GarageHUD
    {
        public GarageHUD()
        {

        }
        protected Scaleform buttons = new Scaleform("instructional_buttons");
        public void InstructOpenMenu()
        {
            buttons.CallFunction("CLEAR_ALL");
            buttons.CallFunction("TOGGLE_MOUSE_BUTTONS", 0);
            buttons.CallFunction("CREATE_CONTAINER");

            buttons.CallFunction("SET_DATA_SLOT", 0, Function.Call<string>((Hash)0x0499D7B09FC9B407, 2, (int)Control.VehicleFlyRollLeftOnly, false), "Open Menu");
            buttons.CallFunction("SET_DATA_SLOT", 1, Function.Call<string>((Hash)0x0499D7B09FC9B407, 2, (int)Control.VehicleFlyRollRightOnly, false), "Store Car");

            buttons.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", -1);
        }

        public void RenderInstructions()
        {
            buttons.Render2D();
        }


    }

    
}
