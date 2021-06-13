using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace characterUI
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

    //class to display any stat related with character
    public class characterCreationUI : BaseScript
    {

        Timer t1;
        public characterCreationUI()
        {
            EventHandlers["charinfUI:showmoney"] += new Action<int, int>(showmoney);
            EventHandlers["charinfUI:showThirst"] += new Action<float>(showThirst);
            EventHandlers["charinfUI:showHunger"] += new Action<float>(showHunger);

            //register net event to change appearance

            t1 = new Timer(0);
            t1.Limit = 10000;

            Tick += OnTick;
        }

        private static void DisplayText(float x, float y, string text)
        {
            BeginTextCommandDisplayText("STRING");
            AddTextComponentString(text);
            SetTextScale(1f, .5f);
            SetTextCentre(true);
            EndTextCommandDisplayText(x, y);
        }
        //should add a flag to undisplay the hunger and thirst one day
        private void showHunger(float hunger)
        {
            DisplayText(.95f, .5f, $"hunger: {hunger}");
        }

        private void showThirst(float thirst)
        {
            DisplayText(.95f, .55f, $"thirst: {thirst}");
        }

        //show money only for a duration of time
        private async Task OnTick()
        {
            if (t1.Expired)
            {
                showcashUI = false;
            }

            await Task.FromResult(0);
        }

        int moneyhand = 0;
        int moneybank = 0;
        bool showcashUI = false;

        private void showmoney(int hand, int bank)
        {
            moneyhand = hand;
            moneybank = bank;
            showcashUI = true;

        }


    }
}
