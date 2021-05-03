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
    class CharacterAtts : BaseScript
    {
        public string CharacterName;
        public bool isjailed;
        public bool canspawnboat;
        public bool canspawnplane;
        public bool canspawntow;
        public int moneyHand;
        public int moneyBank;
        public string job;
        public bool ispolice;
        public bool isEmc;

        public CharacterAtts()
        {
            CharacterName="";
        isjailed=false;
        canspawnboat=false;
        canspawnplane=false;
        canspawntow=false;
         moneyHand=0;
        moneyBank=0;
        job="";
        ispolice=false;
        isEmc=false;
    }

        public void update()
        {
        }

    }
}
