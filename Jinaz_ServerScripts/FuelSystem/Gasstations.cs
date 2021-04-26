using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;


namespace fuels
{
    public static class GasStations
    {
        public static Vector3[] positions;
        public static Vector3[][] pumps;

        private static bool AreGasStationsLoaded = false;

        /// <summary>
        /// Loads the 'positions' and 'pumps' arrays with the data from 'GasStations.json'.
        /// Could use some improvements in the future, but it works for now.
        /// </summary>
        public static void LoadGasStations()
        {
            if (!AreGasStationsLoaded)
            {
                // load the GasStations.json file.
                string jsonString = LoadResourceFile(GetCurrentResourceName(), "GasStations.json");
                if (string.IsNullOrEmpty(jsonString))
                {
                    // Do not continue if the file is empty or it's null.
                    Debug.WriteLine(" An error occurred while loading the gas stations file.");
                    return;
                }

                // Convert the json into an object.
                Newtonsoft.Json.Linq.JObject jsonData = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(jsonString);

                int i = 0;

                Newtonsoft.Json.Linq.JArray gasStations = (Newtonsoft.Json.Linq.JArray)jsonData["GasStations"];

                Debug.WriteLine("gas stations loaded");

                // Initialize the 'positions' and 'pumps' Vector3 Arrays.
                positions = new Vector3[gasStations.Count];
                pumps = new Vector3[gasStations.Count][];
                
                // Go through every gas station in the json data, and create a location entry for it.
                // Then go through all the pumps for that gas station and add all the pump vector3's to the pumps Array.
                foreach (var gasStation in gasStations)
                {
                    Vector3 location = new Vector3(
                        float.Parse(gasStation["coordinates"]["X"].ToString()),
                        float.Parse(gasStation["coordinates"]["Y"].ToString()),
                        float.Parse(gasStation["coordinates"]["Z"].ToString())
                        );

                    positions[i] = location;

                    Newtonsoft.Json.Linq.JArray pumpsList = (Newtonsoft.Json.Linq.JArray)gasStation["pumps"];
                    pumps[i] = new Vector3[pumpsList.Count];
                    for (int p = 0; p < pumpsList.Count; p++)
                    {
                        pumps[i][p] = new Vector3(
                            float.Parse(pumpsList[p]["X"].ToString()),
                            float.Parse(pumpsList[p]["Y"].ToString()),
                            float.Parse(pumpsList[p]["Z"].ToString())
                            );
                    }
                    i++;
                }
                Debug.WriteLine("pumps and positions set");
                // Prevent this function from being accidentally called twice for whatever reason.
                AreGasStationsLoaded = true;
            }
        }

        public static void InitViaCode()
        {
            if (!AreGasStationsLoaded)
            {
                positions = new Vector3[29];
                pumps = new Vector3[29][];

                Vector3 gas0 = new Vector3(49.41872024536133f, 2778.79296875f, 58.043949127197266f);
                positions[0] = gas0;
                pumps[0] = new Vector3[1];
                pumps[0][0] = new Vector3(49.499210357666016f, 2778.912109375f, 58.04399108886719f);
                Vector3 gas1 = new Vector3(263.8948974609375f, 2606.462890625f, 44.98339080810547f);
                positions[1] = gas1;
                pumps[1] = new Vector3[2];
                pumps[1][0] = new Vector3(263.1731872558594f, 2606.514892578125f, 44.9852409362793f);
                pumps[1][1] = new Vector3(265.07391357421875f, 2606.89990234375f, 44.9852409362793f);
                Vector3 gas2 = new Vector3(1039.9580078125f, 2671.134033203125f, 39.55091094970703f);
                positions[2] = gas2;
                pumps[2] = new Vector3[4];
                pumps[2][0] = new Vector3(1043.2860107421875f, 2668.31591796875f, 39.6953010559082f);
                pumps[2][1] = new Vector3(1035.779052734375f, 2667.884033203125f, 39.598419189453125f);
                pumps[2][2] = new Vector3(1035.363037109375f, 2674.14599609375f, 39.6953010559082f);
                pumps[2][3] = new Vector3(1043.22802734375f, 2674.72705078125f, 39.692588806152344f);
                Vector3 gas3 = new Vector3(1207.260009765625f, 2660.175048828125f, 37.899959564208984f);
                positions[3] = gas3;
                pumps[3] = new Vector3[3];
                pumps[3][0] = new Vector3(1208.802978515625f, 2659.409912109375f, 38.29294967651367f);
                pumps[3][1] = new Vector3(1209.3819580078125f, 2658.550048828125f, 38.29296112060547f);
                pumps[3][2] = new Vector3(1206.1639404296875f, 2662.242919921875f, 38.29296112060547f);
                Vector3 gas4 = new Vector3(2539.68505859375f, 2594.19189453125f, 37.944881439208984f);
                positions[4] = gas4;
                pumps[4] = new Vector3[1];
                pumps[4][0] = new Vector3(2540.0458984375f, 2594.929931640625f, 37.941139221191406f);
                Vector3 gas5 = new Vector3(2679.85791015625f, 3263.946044921875f, 55.240570068359375f);
                positions[5] = gas5;
                pumps[5] = new Vector3[2];
                pumps[5][0] = new Vector3(2680.89208984375f, 3266.343994140625f, 55.15650939941406f);
                pumps[5][1] = new Vector3(2678.446044921875f, 3262.31201171875f, 55.15681838989258f);
                Vector3 gas6 = new Vector3(2005.0550537109375f, 3773.886962890625f, 32.4039306640625f);
                positions[6] = gas6;
                pumps[6] = new Vector3[4];
                pumps[6][0] = new Vector3(2009.2080078125f, 3776.83203125f, 32.147579193115234f);
                pumps[6][1] = new Vector3(2006.240966796875f, 3775.010009765625f, 32.1514892578125f);
                pumps[6][2] = new Vector3(2003.9210205078125f, 3773.583984375f, 32.14501953125f);
                pumps[6][3] = new Vector3(2001.4840087890625f, 3772.196044921875f, 32.14670181274414f);
                Vector3 gas7 = new Vector3(1687.156005859375f, 4929.39208984375f, 42.07809066772461f);
                positions[7] = gas7;
                pumps[7] = new Vector3[2];
                pumps[7][0] = new Vector3(1684.635986328125f, 4931.69580078125f, 41.92953109741211f);
                pumps[7][1] = new Vector3(1690.1689453125f, 4927.81591796875f, 41.919490814208984f);
                Vector3 gas8 = new Vector3(1701.31396484375f, 6416.02783203125f, 32.76395034790039f);
                positions[8] = gas8;
                pumps[8] = new Vector3[3];
                pumps[8][0] = new Vector3(1701.72900390625f, 6416.4228515625f, 32.98830032348633f);
                pumps[8][1] = new Vector3(1697.7020263671875f, 6418.27587890625f, 32.396610260009766f);
                pumps[8][2] = new Vector3(1705.75f, 6414.47607421875f, 32.471309661865234f);
                Vector3 gas9 = new Vector3(179.8572998046875f, 6602.8388671875f, 31.8681697845459f);
                positions[9] = gas9;
                pumps[9] = new Vector3[3];
                pumps[9][0] = new Vector3(172.11669921875f, 6603.4599609375f, 31.767589569091797f);
                pumps[9][1] = new Vector3(179.74920654296875f, 6604.9619140625f, 31.75048065185547f);
                pumps[9][2] = new Vector3(187.0438995361328f, 6606.25390625f, 31.75101089477539f);
                Vector3 gas10 = new Vector3(-94.46199035644531f, 6419.59423828125f, 31.489519119262695f);
                positions[10] = gas10;
                pumps[10] = new Vector3[2];
                pumps[10][0] = new Vector3(-97.03368377685547f, 6416.826171875f, 31.38680076599121f);
                pumps[10][1] = new Vector3(-91.3159408569336f, 6422.505859375f, 31.342670440673828f);
                Vector3 gas11 = new Vector3(-2554.99609375f, 2334.402099609375f, 33.07802963256836f);
                positions[11] = gas11;
                pumps[11] = new Vector3[6];
                pumps[11][0] = new Vector3(-2551.4208984375f, 2327.216064453125f, 33.01744079589844f);
                pumps[11][1] = new Vector3(-2558.01806640625f, 2327.195068359375f, 33.078041076660156f);
                pumps[11][2] = new Vector3(-2558.60791015625f, 2334.410888671875f, 32.963539123535156f);
                pumps[11][3] = new Vector3(-2552.719970703125f, 2334.7060546875f, 32.97264862060547f);
                pumps[11][4] = new Vector3(-2552.409912109375f, 2341.948974609375f, 33.00519943237305f);
                pumps[11][5] = new Vector3(-2558.843017578125f, 2340.989013671875f, 33.010990142822266f);
                Vector3 gas12 = new Vector3(-1800.375f, 803.6619262695312f, 138.6511993408203f);
                positions[12] = gas12;
                pumps[12] = new Vector3[6];
                pumps[12][0] = new Vector3(-1796.2939453125f, 811.601806640625f, 138.50579833984375f);
                pumps[12][1] = new Vector3(-1790.8709716796875f, 806.3740844726562f, 138.20289611816406f);
                pumps[12][2] = new Vector3(-1797.1510009765625f, 800.720703125f, 138.38909912109375f);
                pumps[12][3] = new Vector3(-1802.280029296875f, 806.3079223632812f, 138.37510681152344f);
                pumps[12][4] = new Vector3(-1808.656982421875f, 799.9904174804688f, 138.427001953125f);
                pumps[12][5] = new Vector3(-1803.636962890625f, 794.5114135742188f, 138.40969848632812f);
                Vector3 gas13 = new Vector3(-1437.6219482421875f, -276.7475891113281f, 46.20771026611328f);
                positions[13] = gas13;
                pumps[13] = new Vector3[4];
                pumps[13][0] = new Vector3(-1444.3399658203125f, -274.1885986328125f, 46.11930847167969f);
                pumps[13][1] = new Vector3(-1435.3900146484375f, -284.62548828125f, 46.12236022949219f);
                pumps[13][2] = new Vector3(-1428.98095703125f, -278.9674987792969f, 46.108089447021484f);
                pumps[13][3] = new Vector3(-1438.0030517578125f, -268.3987121582031f, 46.07535171508789f);
                Vector3 gas14 = new Vector3(-2096.242919921875f, -320.2867126464844f, 13.168569564819336f);
                positions[14] = gas14;
                pumps[14] = new Vector3[9];
                pumps[14][0] = new Vector3(-2089.239990234375f, -327.372802734375f, 13.028949737548828f);
                pumps[14][1] = new Vector3(-2088.4560546875f, -320.83160400390625f, 12.974220275878906f);
                pumps[14][2] = new Vector3(-2087.032958984375f, -312.7973937988281f, 12.906490325927734f);
                pumps[14][3] = new Vector3(-2095.93310546875f, -311.9273986816406f, 12.90725040435791f);
                pumps[14][4] = new Vector3(-2096.466064453125f, -320.4183044433594f, 13.028849601745605f);
                pumps[14][5] = new Vector3(-2097.3359375f, -326.397705078125f, 12.88916015625f);
                pumps[14][6] = new Vector3(-2105.950927734375f, -325.5889892578125f, 12.935210227966309f);
                pumps[14][7] = new Vector3(-2105.10302734375f, -319.0184020996094f, 12.877900123596191f);
                pumps[14][8] = new Vector3(-2104.419921875f, -311.0090026855469f, 12.933449745178223f);
                Vector3 gas15 = new Vector3(-724.6192016601562f, -935.1630859375f, 19.21385955810547f);
                positions[15] = gas15;
                pumps[15] = new Vector3[6];
                pumps[15][0] = new Vector3(-715.043212890625f, -932.5637817382812f, 19.07505989074707f);
                pumps[15][1] = new Vector3(-715.4774169921875f, -939.2255859375f, 19.35049057006836f);
                pumps[15][2] = new Vector3(-723.8599853515625f, -939.2935791015625f, 18.862829208374023f);
                pumps[15][3] = new Vector3(-723.7554931640625f, -932.4473876953125f, 19.402450561523438f);
                pumps[15][4] = new Vector3(-732.3931274414062f, -932.5628051757812f, 19.41345977783203f);
                pumps[15][5] = new Vector3(-732.469970703125f, -939.5462036132812f, 18.94506072998047f);
                Vector3 gas16 = new Vector3(-526.019775390625f, -1211.0030517578125f, 18.184829711914062f);
                positions[16] = gas16;
                pumps[16] = new Vector3[8];
                pumps[16][0] = new Vector3(-518.4993286132812f, -1209.4429931640625f, 18.077829360961914f);
                pumps[16][1] = new Vector3(-521.2747192382812f, -1208.4019775390625f, 18.061979293823242f);
                pumps[16][2] = new Vector3(-526.128173828125f, -1206.4019775390625f, 18.06817054748535f);
                pumps[16][3] = new Vector3(-528.5460205078125f, -1204.93798828125f, 18.089929580688477f);
                pumps[16][4] = new Vector3(-532.3411865234375f, -1212.7740478515625f, 18.075939178466797f);
                pumps[16][5] = new Vector3(-529.4605712890625f, -1213.782958984375f, 18.075889587402344f);
                pumps[16][6] = new Vector3(-524.92578125f, -1216.4420166015625f, 18.039810180664062f);
                pumps[16][7] = new Vector3(-522.1807250976562f, -1217.3709716796875f, 18.07600975036621f);
                Vector3 gas17 = new Vector3(-70.21484375f, -1761.7919921875f, 29.534019470214844f);
                positions[17] = gas17;
                pumps[17] = new Vector3[6];
                pumps[17][0] = new Vector3(-63.78422927856445f, -1767.8070068359375f, 29.5849609375f);
                pumps[17][1] = new Vector3(-61.2121696472168f, -1760.782958984375f, 29.573970794677734f);
                pumps[17][2] = new Vector3(-69.46559143066406f, -1758.156982421875f, 29.255090713500977f);
                pumps[17][3] = new Vector3(-72.02877807617188f, -1765.1300048828125f, 29.238740921020508f);
                pumps[17][4] = new Vector3(-80.31096649169922f, -1762.1650390625f, 29.50827980041504f);
                pumps[17][5] = new Vector3(-77.66983032226562f, -1755.0770263671875f, 29.527690887451172f);
                Vector3 gas18 = new Vector3(265.6484069824219f, -1261.3089599609375f, 29.292940139770508f);
                positions[18] = gas18;
                pumps[18] = new Vector3[9];
                pumps[18][0] = new Vector3(273.8891906738281f, -1268.60595703125f, 29.508960723876953f);
                pumps[18][1] = new Vector3(273.9101867675781f, -1261.3409423828125f, 29.458410263061523f);
                pumps[18][2] = new Vector3(273.9552001953125f, -1253.5550537109375f, 29.004629135131836f);
                pumps[18][3] = new Vector3(265.0881042480469f, -1253.458984375f, 29.534889221191406f);
                pumps[18][4] = new Vector3(264.59759521484375f, -1261.260986328125f, 29.443119049072266f);
                pumps[18][5] = new Vector3(265.1925964355469f, -1268.5030517578125f, 29.069480895996094f);
                pumps[18][6] = new Vector3(256.46160888671875f, -1268.6259765625f, 29.551509857177734f);
                pumps[18][7] = new Vector3(256.51739501953125f, -1261.2869873046875f, 28.948049545288086f);
                pumps[18][8] = new Vector3(256.4725036621094f, -1253.448974609375f, 29.557689666748047f);
                Vector3 gas19 = new Vector3(819.65380859375f, -1028.845947265625f, 26.403419494628906f);
                positions[19] = gas19;
                pumps[19] = new Vector3[6];
                pumps[19][0] = new Vector3(826.7512817382812f, -1026.1650390625f, 26.357280731201172f);
                pumps[19][1] = new Vector3(826.7982177734375f, -1030.967041015625f, 26.429569244384766f);
                pumps[19][2] = new Vector3(819.14111328125f, -1030.9969482421875f, 26.229820251464844f);
                pumps[19][3] = new Vector3(819.1500854492188f, -1026.3690185546875f, 26.181209564208984f);
                pumps[19][4] = new Vector3(810.8203735351562f, -1026.366943359375f, 26.15118980407715f);
                pumps[19][5] = new Vector3(810.8690185546875f, -1031.196044921875f, 26.158199310302734f);
                Vector3 gas20 = new Vector3(1208.9510498046875f, -1402.5670166015625f, 35.22418975830078f);
                positions[20] = gas20;
                pumps[20] = new Vector3[4];
                pumps[20][0] = new Vector3(1210.22705078125f, -1407.06494140625f, 35.11444854736328f);
                pumps[20][1] = new Vector3(1213.0069580078125f, -1404.0789794921875f, 35.09584045410156f);
                pumps[20][2] = new Vector3(1207.0810546875f, -1398.2960205078125f, 35.15727996826172f);
                pumps[20][3] = new Vector3(1204.208984375f, -1401.1009521484375f, 35.131858825683594f);
                Vector3 gas21 = new Vector3(1181.3809814453125f, -330.84710693359375f, 69.31651306152344f);
                positions[21] = gas21;
                pumps[21] = new Vector3[6];
                pumps[21][0] = new Vector3(1186.4560546875f, -338.1484069824219f, 69.5254135131836f);
                pumps[21][1] = new Vector3(1179.0550537109375f, -339.394287109375f, 69.6856689453125f);
                pumps[21][2] = new Vector3(1177.467041015625f, -331.177490234375f, 68.97178649902344f);
                pumps[21][3] = new Vector3(1184.803955078125f, -329.9715881347656f, 69.48990631103516f);
                pumps[21][4] = new Vector3(1183.2239990234375f, -321.3689880371094f, 69.19593811035156f);
                pumps[21][5] = new Vector3(1175.6429443359375f, -322.26959228515625f, 68.98219299316406f);
                Vector3 gas22 = new Vector3(620.8433837890625f, 269.10089111328125f, 103.0895004272461f);
                positions[22] = gas22;
                pumps[22] = new Vector3[6];
                pumps[22][0] = new Vector3(629.555419921875f, 263.8569030761719f, 103.02239990234375f);
                pumps[22][1] = new Vector3(629.3790893554688f, 273.95458984375f, 102.99870300292969f);
                pumps[22][2] = new Vector3(620.789794921875f, 273.88861083984375f, 102.9988021850586f);
                pumps[22][3] = new Vector3(612.3482055664062f, 274.0846862792969f, 103.00430297851562f);
                pumps[22][4] = new Vector3(612.2713012695312f, 263.88848876953125f, 102.9917984008789f);
                pumps[22][5] = new Vector3(620.9271240234375f, 263.8310852050781f, 103.02510070800781f);
                Vector3 gas23 = new Vector3(2581.321044921875f, 362.039306640625f, 108.46880340576172f);
                positions[23] = gas23;
                pumps[23] = new Vector3[6];
                pumps[23][0] = new Vector3(2588.462890625f, 358.53900146484375f, 108.39579772949219f);
                pumps[23][1] = new Vector3(2589.12890625f, 363.9043884277344f, 108.39949798583984f);
                pumps[23][2] = new Vector3(2581.26611328125f, 364.2455139160156f, 108.3998031616211f);
                pumps[23][3] = new Vector3(2581.087890625f, 358.8944091796875f, 108.37239837646484f);
                pumps[23][4] = new Vector3(2573.717041015625f, 359.0278015136719f, 108.36150360107422f);
                pumps[23][5] = new Vector3(2573.843994140625f, 364.69720458984375f, 108.39579772949219f);
                Vector3 gas24 = new Vector3(1785.363037109375f, 3330.3720703125f, 41.38188171386719f);
                positions[24] = gas24;
                pumps[24] = new Vector3[2];
                pumps[24][0] = new Vector3(1785.89501953125f, 3330.16796875f, 41.345619201660156f);
                pumps[24][1] = new Vector3(1785.14501953125f, 3331.251953125f, 41.381229400634766f);
                Vector3 gas25 = new Vector3(-319.69000244140625f, -1471.6099853515625f, 30.030000686645508f);
                positions[25] = gas25;
                pumps[25] = new Vector3[6];
                pumps[25][0] = new Vector3(-310.3699951171875f, -1472.030029296875f, 30.719999313354492f);
                pumps[25][1] = new Vector3(-315.4599914550781f, -1463.27001953125f, 30.719999313354492f);
                pumps[25][2] = new Vector3(-321.79998779296875f, -1467.030029296875f, 30.719999313354492f);
                pumps[25][3] = new Vector3(-316.67999267578125f, -1475.93994140625f, 30.719999313354492f);
                pumps[25][4] = new Vector3(-324.2200012207031f, -1480.1700439453125f, 30.719999313354492f);
                pumps[25][5] = new Vector3(-329.30999755859375f, -1471.3499755859375f, 30.719999313354492f);
                Vector3 gas26 = new Vector3(174.8800048828125f, -1562.449951171875f, 28.739999771118164f);
                positions[26] = gas26;
                pumps[26] = new Vector3[4];
                pumps[26][0] = new Vector3(169.64999389648438f, -1562.6800537109375f, 29.31999969482422f);
                pumps[26][1] = new Vector3(176.4199981689453f, -1556.280029296875f, 29.31999969482422f);
                pumps[26][2] = new Vector3(181.38999938964844f, -1561.56005859375f, 29.31999969482422f);
                pumps[26][3] = new Vector3(174.63999938964844f, -1567.68994140625f, 29.31999969482422f);
                Vector3 gas27 = new Vector3(1246.47998046875f, -1485.449951171875f, 34.900001525878906f);
                positions[27] = gas27;
                pumps[27] = new Vector3[2];
                pumps[27][0] = new Vector3(1246.1600341796875f, -1488.1500244140625f, 34.900001525878906f);
                pumps[27][1] = new Vector3(1246.47998046875f, -1482.760009765625f, 34.900001525878906f);
                Vector3 gas28 = new Vector3(-66.33000183105469f, -2532.570068359375f, 6.139999866485596f);
                positions[28] = gas28;
                pumps[28] = new Vector3[2];
                pumps[28][0] = new Vector3(-64.25f, -2533.89990234375f, 6.139999866485596f);
                pumps[28][1] = new Vector3(-68.72000122070312f, -2530.7099609375f, 6.139999866485596f);
                Debug.WriteLine("pumps and positions set");
                AreGasStationsLoaded = true;
            }

            }
    }
}
