using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;


namespace garagescript
{
    public static class ParkingGarages
    {

        public static Vector3[] positions;

        public static void InitViaCode()
        {
            positions = new Vector3[12];

            positions[0] = new Vector3(55.5f,18.9f,69.0f);
            positions[1] = new Vector3(-773.9092f, 373.1312f, 87.46f);
            positions[2] = new Vector3(-806.47f, 372.545f, 87.46f);
            positions[3] = new Vector3(-747.79f, 374.11f, 87.46f);
            positions[4] = new Vector3(-339f, 288f, 85f);
            positions[5] = new Vector3(-313f, -881f, 30f);
            positions[6] = new Vector3(285f, -338f, 44f);
            positions[7] = new Vector3(268f, -322f, 44f);
            positions[8] = new Vector3(-704f, -1405f, 4.5f);
            positions[9] = new Vector3(-678f, -2383f, 13f);
            positions[10] = new Vector3(911f, -54.52f, 78.3f);
            positions[11] = new Vector3(875.3f, -30.67f, 78.3f);
            //aircraft
            //big airstrip
            //positions[7] = new Vector3(-989.337f, -3149.713f, 13.53039f);
            //helipads
            //positions[7] = new Vector3(-1145f,-2863f,13.5f);
            //positions[8] = new Vector3(-724f, -1443f, 4f);
            //spawnpoint player: -777.0, 311.94, 85.69
        }
    }
}
