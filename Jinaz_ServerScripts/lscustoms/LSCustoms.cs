using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace lscustoms
{
    class lscLocations : BaseScript
    {

        public Vector3[] locations;
        public Vector3[] tplocation;
        public float[] heading;

        public lscLocations()
        {
            locations = new Vector3[6];
            tplocation = new Vector3[6];
            heading = new float[6];
        }

        private void loadLSClocations()
        {
            //tuning spot -333.4315f,-135.5258f,38.57364f H 88.82297f
            tplocation[0] = new Vector3(-333.4315f, -135.5258f, 38.57364f);
            locations[0] = new Vector3(-341.2744f, -137.7952f, 38.57351f);
            heading[0] = 88.82297f;

            //tuning spot 733.2429f, -1082.693f, 21.67369f H 145.3428f
            locations[1] = new Vector3(727.7993f, -1087.986f, 21.67351f);
            tplocation[1] = new Vector3(733.2429f, -1082.693f, 21.67369f);
            heading[1] = 145.3428f;

            //tuning spot -1158.363 -2013.537 12.68451 327.9541
            locations[2] = new Vector3(-1149.964f, -1995.74f, 12.68468f);
            tplocation[2] = new Vector3(-1158.363f, -2013.537f, 12.68451f);
            heading[2] = 327.9541f;

            //tuning spot -211.5419 -1327.059 30.39517 336.9796
            locations[3] = new Vector3(-205.9641f, -1313.703f, 30.55551f);
            tplocation[3] = new Vector3(-211.5419f, -1327.059f, 30.39517f);
            heading[3] = 336.9796f;

            //tuning spot 1176.05 2639.556 37.34248 12.58688
            locations[4] = new Vector3(1175.823f, 2650.369f, 37.39545f);
            tplocation[4] = new Vector3(1176.05f, 2639.556f, 37.34248f);
            heading[4] = 12.58688f;

            //tuning spot 109.7344 6627.865 31.37505 240.78
            locations[5] = new Vector3(118.7251f, 6619.716f, 31.4472f);
            tplocation[5] = new Vector3(109.7344f, 6627.865f, 31.37505f);
            heading[5] = 240.78f;

        }

        //LS car dealer: -47.17583,-1080.66,26.23973,71.305

        //paleto vault
        //cashier
        //-113.2106 6470.312 31.64673
        //door
        //-105.7967 6471.165 31.62673
        //inner
        //-103.9631 6477.874 31.62673

        //fleeca 1
        //1175.009 2706.848 38.09408

        //atms
        //1172.388 2702.448 38.1748
        //1171.535 2702.492 38.17547


        //guns
        //-331.0344 6083.64 31.45477
        //-1118.206 2698.67 18.55415

        //hair locations:
        //-277.5684 6226.772 31.69554 121.781

        //tatoo locations
        //-294.4682 6200.624 31.48774 213.7269

        //clothes locations:
        //10.57499 6514.151 31.87785 114.0505
        //-1108.631 2709.62 19.10787
        //613.81 2752.859 42.0881

        //shop locations : 
        //1728.792 6414.235 35.03722
        //547.7766 2671.348 42.15649
        //1165.87 2709.358 38.15771

    }
}
