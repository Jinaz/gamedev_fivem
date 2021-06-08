using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;
using System.Globalization;

using System.Dynamic;

using CitizenFX.Core.UI;
using System.Drawing;

namespace ConsoleCommands
{
    public class VehicleHashes
    {
        uint dinghy = 0x3d961290;
        uint dinghy2 = 0x107f392c;
        uint dinghy3 = 0x1e5e54ea;
        uint dinghy4 = 0x33b47f96;
        uint jetmax = 0x33581161;
        uint marquis = 0xc1ce1183;
        uint seashark = 0xc2974024;
        uint seashark2 = 0xdb4388e4;
        uint seashark3 = 0xed762d49;
        uint speeder = 0xdc60d2b;
        uint speeder2 = 0x1a144f2a;
        uint squalo = 0x17df5ec2;
        uint submersible = 0x2dff622f;
        uint submersible2 = 0xc07107ee;
        uint suntrap = 0xef2295c9;
        uint toro = 0x3fd5aa2f;
        uint toro2 = 0x362cac6d;
        uint tropic = 0x1149422f;
        uint tropic2 = 0x56590fe9;
        uint tug = 0x82cac433;
        uint avisa = 0x9a474b5e;
        uint dinghy5 = 0xc58da34a;
        uint kosatka = 0x4faf0d70;
        uint longfin = 0x6ef89ccc;
        uint patrolboat = 0xef813606;
        uint benson = 0x7a61b330;
        uint biff = 0x32b91ae8;
        uint cerberus = 0xd039510b;
        uint cerberus2 = 0x287fa449;
        uint cerberus3 = 0x71d3b6f0;
        uint hauler = 0x5a82f9ae;
        uint hauler2 = 0x171c92c4;
        uint mule = 0x35ed670b;
        uint mule2 = 0xc1632beb;
        uint mule3 = 0x85a5b471;
        uint mule4 = 0x73f4110e;
        uint packer = 0x21eee87d;
        uint phantom = 0x809aa4cb;
        uint phantom2 = 0x9dae1398;
        uint phantom3 = 0xa90ed5c;
        uint pounder = 0x7de35e7d;
        uint pounder2 = 0x6290f15b;
        uint stockade = 0x6827cf72;
        uint stockade3 = 0xf337ab36;
        uint teruint = 0x897afc65;
        uint asbo = 0x42aca95f;
        uint blista = 0xeb70965f;
        uint brioso = 0x5c55cb39;
        uint club = 0x82e47e85;
        uint dilettante = 0xbc993509;
        uint dilettante2 = 0x64430650;
        uint kanjo = 0x18619b7e;
        uint issi2 = 0xb9cb3b69;
        uint issi3 = 0x378236e1;
        uint issi4 = 0x256e92ba;
        uint issi5 = 0x5ba0ff1e;
        uint issi6 = 0x49e25ba1;
        uint panto = 0xe644e480;
        uint prairie = 0xa988d3a2;
        uint rhapsody = 0x322cf98f;
        uint brioso2 = 0x55365079;
        uint weevil = 0x61fe4d6a;
        uint cogcabrio = 0x13b57d8a;
        uint exemplar = 0xffb15b5e;
        uint f620 = 0xdcbcbe48;
        uint felon = 0xe8a8bda8;
        uint felon2 = 0xfaad85ee;
        uint jackal = 0xdac67112;
        uint oracle = 0x506434f6;
        uint oracle2 = 0xe18195b2;
        uint sentinel = 0x50732c82;
        uint sentinel2 = 0x3412ae2d;
        uint windsor = 0x5e4327c8;
        uint windsor2 = 0x8cf5cae1;
        uint zion = 0xbd1b39c3;
        uint zion2 = 0xb8e2ae18;
        uint bmx = 0x43779c54;
        uint cruiser = 0x1aba13b5;
        uint fixter = 0xce23d3bf;
        uint scorcher = 0xf4e1aa15;
        uint tribike = 0x4339cd69;
        uint tribike2 = 0xb67597ec;
        uint tribike3 = 0xe823fb48;
        uint ambulance = 0x45d56ada;
        uint fbi = 0x432ea949;
        uint fbi2 = 0x9dc66994;
        uint firetruk = 0x73920f8e;
        uint lguard = 0x1bf8d381;
        uint pbus = 0x885f3671;
        uint police = 0x79fbb0c5;
        uint police2 = 0x9f05f101;
        uint police3 = 0x71fa16ea;
        uint police4 = 0x8a63c7b9;
        uint policeb = 0xfdefaec3;
        uint polmav = 0x1517d4d9;
        uint policeold1 = 0xa46462f7;
        uint policeold2 = 0x95f4c618;
        uint policet = 0x1b38e955;
        uint pranger = 0x2c33b46e;
        uint predator = 0xe2e7d4ab;
        uint riot = 0xb822a1aa;
        uint riot2 = 0x9b16a3b4;
        uint sheriff = 0x9baa707c;
        uint sheriff2 = 0x72935408;
        uint akula = 0x46699f47;
        uint annihilator = 0x31f0b376;
        uint buzzard = 0x2f03547b;
        uint buzzard2 = 0x2c75f0dd;
        uint cargobob = 0xfcfcb68b;
        uint cargobob2 = 0x60a7ea10;
        uint cargobob3 = 0x53174eef;
        uint cargobob4 = 0x78bc1a3c;
        uint frogger = 0x2c634fbd;
        uint frogger2 = 0x742e9ac0;
        uint havok = 0x89ba59f5;
        uint hunter = 0xfd707ede;
        uint maverick = 0x9d0450ca;
        uint savage = 0xfb133a17;
        uint seasparrow = 0xd4ae63d9;
        uint skylift = 0x3e48bf23;
        uint supervolito = 0x2a54c47d;
        uint supervolito2 = 0x9c5e5644;
        uint swift = 0xebc24df2;
        uint swift2 = 0x4019cb4c;
        uint valkyrie = 0xa09e15fd;
        uint valkyrie2 = 0x5bfa5c4b;
        uint volatus = 0x920016f1;
        uint annihilator2 = 0x11962e49;
        uint seasparrow2 = 0x494752f7;
        uint seasparrow3 = 0x5f017e6b;
        uint bulldozer = 0x7074f39d;
        uint cutter = 0xc3fba120;
        uint dump = 0x810369e2;
        uint flatbed = 0x50b0215a;
        uint guardian = 0x825a9f4c;
        uint handler = 0x1a7fcefa;
        uint mixer = 0xd138a6bb;
        uint mixer2 = 0x1c534995;
        uint rubble = 0x9a5b1dcc;
        uint tiptruck = 0x2e19879;
        uint tiptruck2 = 0xc7824e5e;
        uint apc = 0x2189d250;
        uint barracks = 0xceea3f4b;
        uint barracks2 = 0x4008eabb;
        uint barracks3 = 0x2592b5cf;
        uint barrage = 0xf34dfb25;
        uint chernobog = 0xd6bc7523;
        uint crusader = 0x132d5a1a;
        uint halftrack = 0xfe141da6;
        uint khanjali = 0xaa6f980a;
        uint minitank = 0xb53c6c52;
        uint rhino = 0x2ea68690;
        uint scarab = 0xbba2a2f7;
        uint scarab2 = 0x5beb3ce0;
        uint scarab3 = 0xdd71bfeb;
        uint thruster = 0x58cdaf30;
        uint trailersmall2 = 0x8fd54ebb;
        uint vetir = 0x780ffbd2;
        uint akuma = 0x63abade7;
        uint avarus = 0x81e38f7f;
        uint bagger = 0x806b9cc3;
        uint bati = 0xf9300cc5;
        uint bati2 = 0xcadd5d2d;
        uint bf400 = 0x5283265;
        uint carbonrs = 0xabb0c0;
        uint chimera = 0x675ed7;
        uint cliffhanger = 0x17420102;
        uint daemon = 0x77934cee;
        uint daemon2 = 0xac4e93c9;
        uint defiler = 0x30ff0190;
        uint deathbike = 0xfe5f0722;
        uint deathbike2 = 0x93f09558;
        uint deathbike3 = 0xae12c99c;
        uint diablous = 0xf1b44f44;
        uint diablous2 = 0x6abdf65e;
        uint _double = 0x9c669788;
        uint enduro = 0x6882fa73;
        uint esskey = 0x794cb30c;
        uint faggio = 0x9229e4eb;
        uint faggio2 = 0x350d1ab;
        uint faggio3 = 0xb328b188;
        uint fcr = 0x25676eaf;
        uint fcr2 = 0xd2d5e00e;
        uint gargoyle = 0x2c2c2324;
        uint hakuchou = 0x4b6c568a;
        uint hakuchou2 = 0xf0c2a91f;
        uint hexer = 0x11f76c14;
        uint innovation = 0xf683eaca;
        uint lectro = 0x26321e67;
        uint manchez = 0xa5325278;
        uint nemesis = 0xda288376;
        uint nightblade = 0xa0438767;
        uint oppressor = 0x34b82784;
        uint oppressor2 = 0x7b54a9d3;
        uint pcj = 0xc9ceaf06;
        uint ratbike = 0x6facdf31;
        uint ruffian = 0xcabd11e8;
        uint rrocket = 0x36a167e0;
        uint sanchez = 0x2ef89e46;
        uint sanchez2 = 0xa960b13e;
        uint sanctus = 0x58e316c7;
        uint shotaro = 0xe7d2a16e;
        uint sovereign = 0x2c509634;
        uint stryder = 0x11f58a5a;
        uint thrust = 0x6d6f8f43;
        uint vader = 0xf79a00f7;
        uint vindicator = 0xaf599f01;
        uint vortex = 0xdba9dbfc;
        uint wolfsbane = 0xdb20a373;
        uint zombiea = 0xc3d7c72b;
        uint zombieb = 0xde05fb87;
        uint manchez2 = 0x40c332a3;
        uint blade = 0xb820ed5e;
        uint buccaneer = 0xd756460c;
        uint buccaneer2 = 0xc397f748;
        uint chino = 0x14d69010;
        uint chino2 = 0xaed64a63;
        uint clique = 0xa29f78b0;
        uint coquette3 = 0x2ec385fe;
        uint deviant = 0x4c3fff49;
        uint dominator = 0x4ce68ac;
        uint dominator2 = 0xc96b73d9;
        uint dominator3 = 0xc52c6b93;
        uint dominator4 = 0xd6fb0f30;
        uint dominator5 = 0xae0a3d4f;
        uint dominator6 = 0xb2e046fb;
        uint dukes = 0x2b26f456;
        uint dukes2 = 0xec8f7094;
        uint dukes3 = 0x7f3415e3;
        uint faction = 0x81a9cddf;
        uint faction2 = 0x95466bdb;
        uint faction3 = 0x866bce26;
        uint ellie = 0xb472d2b5;
        uint gauntlet = 0x94b395c5;
        uint gauntlet2 = 0x14d22159;
        uint gauntlet3 = 0x2b0c4dcd;
        uint gauntlet4 = 0x734c5e50;
        uint gauntlet5 = 0x817afaad;
        uint hermes = 0xe83c17;
        uint hotknife = 0x239e390;
        uint hustler = 0x23ca25f2;
        uint impaler = 0xb2e046fb;
        uint impaler2 = 0x3c26bd0c;
        uint impaler3 = 0x8d45df49;
        uint impaler4 = 0x9804f4c7;
        uint imperator = 0x1a861243;
        uint imperator2 = 0x619c1b82;
        uint imperator3 = 0xd2f77e37;
        uint lurcher = 0x7b47a6a7;
        uint moonbeam = 0x1f52a43f;
        uint moonbeam2 = 0x710a2b9b;
        uint nightshade = 0x8c2bd0dc;
        uint peyote2 = 0x9472cd24;
        uint phoenix = 0x831a21d5;
        uint picador = 0x59e0fbf3;
        uint ratloader = 0xd83c13ce;
        uint ratloader2 = 0xdce1d9f7;
        uint ruiner = 0xf26ceff9;
        uint ruiner2 = 0x381e10bd;
        uint ruiner3 = 0x2e5afd37;
        uint sabregt = 0x9b909c94;
        uint sabregt2 = 0xd4ea603;
        uint slamvan = 0x2b7f9de3;
        uint slamvan2 = 0x31adbbfc;
        uint slamvan3 = 0x42bc5e19;
        uint slamvan4 = 0x8526e2f5;
        uint slamvan5 = 0x163f8520;
        uint slamvan6 = 0x67d52852;
        uint stalion = 0x72a4c31e;
        uint stalion2 = 0xe80f67ee;
        uint tampa = 0x39f9c898;
        uint tampa3 = 0xb7d9f7f1;
        uint tulip = 0x56d42971;
        uint vamos = 0xfd128dfd;
        uint vigero = 0xcec6b9b7;
        uint virgo = 0xe2504942;
        uint virgo2 = 0xca62927a;
        uint virgo3 = 0xfdffb0;
        uint voodoo = 0x779b4f2d;
        uint voodoo2 = 0x1f3766e3;
        uint yosemite = 0x6f946279;
        uint yosemite2 = 0x64f49967;
        uint yosemite3 = 0x0409d787;
        uint bfinjection = 0x432aa566;
        uint bifta = 0xeb298297;
        uint blazer = 0x8125bcf9;
        uint blazer2 = 0xfd231729;
        uint blazer3 = 0xb44f0582;
        uint blazer4 = 0xe5ba6858;
        uint blazer5 = 0xa1355f67;
        uint bodhi2 = 0xaa699bb6;
        uint brawler = 0xa7ce1bc5;
        uint bruiser = 0x27d79225;
        uint bruiser2 = 0x9b065c9e;
        uint bruiser3 = 0x8644331a;
        uint brutus = 0x7f81a829;
        uint brutus2 = 0x8f49ae28;
        uint brutus3 = 0x798682a2;
        uint caracara = 0x4abebf23;
        uint caracara2 = 0xaf966f3c;
        uint dloader = 0x698521e3;
        uint dubsta3 = 0xb6410173;
        uint dune = 0x9cf21e0f;
        uint dune2 = 0x1fd824af;
        uint dune3 = 0x711d4738;
        uint dune4 = 0xceb28249;
        uint dune5 = 0xed62bfa9;
        uint everon = 0x97553c28;
        uint freecrawler = 0xfcc2f483;
        uint hellion = 0xea6a047f;
        uint insurgent = 0x9114eada;
        uint insurgent2 = 0x7b7e56f0;
        uint insurgent3 = 0x8d4b7a8a;
        uint kalahari = 0x5852838;
        uint kamacho = 0xf8c2e0e7;
        uint marshall = 0x49863e9c;
        uint mesa3 = 0x84f42e51;
        uint monster = 0xcd93a7db;
        uint monster3 = 0x669eb40a;
        uint monster4 = 0x32174afc;
        uint monster5 = 0xd556917c;
        uint menacer = 0x79dd18ae;
        uint outlaw = 0x185e2ff3;
        uint nightshark = 0x19dd9ed1;
        uint rancherxl = 0x6210cbb0;
        uint rancherxl2 = 0x7341576b;
        uint rebel = 0xb802dd46;
        uint rebel2 = 0x8612b64b;
        uint rcbandito = 0xeef345ec;
        uint riata = 0xa4a4e453;
        uint sandking = 0xb9210fd0;
        uint sandking2 = 0x3af8c345;
        uint technical = 0x83051506;
        uint technical2 = 0x4662bcbb;
        uint technical3 = 0x50d4d19f;
        uint trophytruck = 0x612f4b6;
        uint trophytruck2 = 0xd876dbe2;
        uint vagrant = 0x2c1fea99;
        uint zhaba = 0x4c8dba51;
        uint verus = 0x11cbc051;
        uint winky = 0xf376f1e6;
        uint formula = 0x1446590a;
        uint formula2 = 0x8b213907;
        uint openwheel1 = 0x58f77553;
        uint openwheel2 = 0x4669d038;
        uint alphaz1 = 0xa52f6866;
        uint avenger = 0x81bd2ed0;
        uint avenger2 = 0x18606535;
        uint besra = 0x6cbd1d6d;
        uint blimp = 0xf7004c86;
        uint blimp2 = 0xdb6b4924;
        uint blimp3 = 0xeda4ed97;
        uint bombushka = 0xfe0a508c;
        uint cargoplane = 0x15f27762;
        uint cuban800 = 0xd9927fe3;
        uint dodo = 0xca495705;
        uint duster = 0x39d6779e;
        uint howard = 0xc3f25753;
        uint hydra = 0x39d6e83f;
        uint jet = 0x3f119114;
        uint lazer = 0xb39b0ae6;
        uint luxor = 0x250b0c5e;
        uint luxor2 = 0xb79f589e;
        uint mammatus = 0x97e55d11;
        uint microlight = 0x96e24857;
        uint miljet = 0x9d80f93;
        uint mogul = 0xd35698ef;
        uint molotok = 0x5d56f01b;
        uint nimbus = 0xb2cf7250;
        uint nokota = 0x3dc92356;
        uint pyro = 0xad6065c0;
        uint rogue = 0xc5dd6967;
        uint seabreeze = 0xe8983f9f;
        uint shamal = 0xb79c1bf5;
        uint starling = 0x9a9eb7de;
        uint strikeforce = 0x64de07a1;
        uint stunt = 0x81794c70;
        uint titan = 0x761e2ad3;
        uint tula = 0x3e2e4f8a;
        uint velum = 0x9c429b6a;
        uint velum2 = 0x403820e8;
        uint vestra = 0x4ff77e37;
        uint volatol = 0x1aad0ded;
        uint alkonost = 0xea313705;
        uint baller = 0xcfca3668;
        uint baller2 = 0x8852855;
        uint baller3 = 0x6ff0f727;
        uint baller4 = 0x25cbe2e2;
        uint baller5 = 0x1c09cf5e;
        uint baller6 = 0x27b4e6b0;
        uint bjxl = 0x32b29a4b;
        uint cavalcade = 0x779f23aa;
        uint cavalcade2 = 0xd0eb2be5;
        uint contender = 0x28b67aca;
        uint dubsta = 0x462fe277;
        uint dubsta2 = 0xe882e5f6;
        uint fq2 = 0xbc32a33b;
        uint granger = 0x9628879c;
        uint gresley = 0xa3fc0f4d;
        uint habanero = 0x34b7390f;
        uint huntley = 0x1d06d681;
        uint landstalker = 0x4ba4e8dc;
        uint landstalker2 = 0xce0b9f22;
        uint mesa = 0x36848602;
        uint mesa2 = 0xd36a4b44;
        uint novak = 0x92f5024e;
        uint patriot = 0xcfcfeb3b;
        uint patriot2 = 0xe6e967f8;
        uint radi = 0x9d96b45b;
        uint rebla = 0x4f48fc4;
        uint rocoto = 0x7f5c91f1;
        uint seminole = 0x48ceced3;
        uint seminole2 = 0x94114926;
        uint serrano = 0x4fb1a214;
        uint toros = 0xba5334ac;
        uint xls = 0x47bbcf2e;
        uint xls2 = 0xe6401328;
        uint squaddie = 0xf9e67c05;
        uint asea = 0x94204d89;
        uint asea2 = 0x9441d8d5;
        uint asterope = 0x8e9254fb;
        uint cog55 = 0x360a438e;
        uint cog552 = 0x29fcd3e4;
        uint cognoscenti = 0x86fe0b60;
        uint cognoscenti2 = 0xdbf2d57a;
        uint emperor = 0xd7278283;
        uint emperor2 = 0x8fc3aadc;
        uint emperor3 = 0xb5fcf74e;
        uint fugitive = 0x71cb2ffb;
        uint glendale = 0x47a6bc1;
        uint glendale2 = 0xc98bbad6;
        uint ingot = 0xb3206692;
        uint uintruder = 0x34dd8aa1;
        uint limo2 = 0xf92aec4d;
        uint premier = 0x8fb66f9b;
        uint primo = 0xbb6b404f;
        uint primo2 = 0x86618eda;
        uint regina = 0xff22d208;
        uint romero = 0x2560b2fc;
        uint stafford = 0x1324e960;
        uint stanier = 0xa7ede74d;
        uint stratum = 0x66b4fc45;
        uint stretch = 0x8b13f083;
        uint superd = 0x42f2ed16;
        uint surge = 0x8f0e3594;
        uint tailgater = 0xc3ddfdce;
        uint warrener = 0x51d83328;
        uint washington = 0x69f06b57;
        uint airbus = 0x4c80eb0e;
        uint brickade = 0xedc6f847;
        uint bus = 0xd577c962;
        uint coach = 0x84718d34;
        uint pbus2 = 0x149bd32a;
        uint rallytruck = 0x829a3c44;
        uint rentalbus = 0xbe819c63;
        uint taxi = 0xc703db5f;
        uint tourbus = 0x73b1c3cb;
        uint trash = 0x72435a19;
        uint trash2 = 0xb527915c;
        uint wastelander = 0x8e08ec82;
        uint alpha = 0x2db8d1aa;
        uint banshee = 0xc1e908d2;
        uint bestiagts = 0x4bfcf28b;
        uint blista2 = 0x3dee5eda;
        uint blista3 = 0xdcbc1c3b;
        uint buffalo = 0xedd516c6;
        uint buffalo2 = 0x2bec3cbe;
        uint buffalo3 = 0xe2c013e;
        uint carbonizzare = 0x7b8ab45f;
        uint comet2 = 0xc1ae4d16;
        uint comet3 = 0x877358ad;
        uint comet4 = 0x5d1903f9;
        uint comet5 = 0x276d98a3;
        uint coquette = 0x67bc037;
        uint coquette4 = 0x98f65a5e;
        uint drafter = 0x28eab80f;
        uint deveste = 0x5ee005da;
        uint elegy = 0xbba2261;
        uint elegy2 = 0xde3d9d22;
        uint feltzer2 = 0x8911b9f5;
        uint flashgt = 0xb4f32118;
        uint furoregt = 0xbf1691e0;
        uint fusilade = 0x1dc0ba53;
        uint futo = 0x7836ce2f;
        uint gb200 = 0x71cbea98;
        uint hotring = 0x42836be5;
        uint komoda = 0xce44c4b9;
        uint imorgon = 0xbc7c0a00;
        uint issi7 = 0x6e8da4f7;
        uint italigto = 0xec3e3404;
        uint jugular = 0xf38c4245;
        uint jester = 0xb2a716a3;
        uint jester2 = 0xbe0e6126;
        uint jester3 = 0xf330cb6a;
        uint khamelion = 0x206d1b68;
        uint kuruma = 0xae2bfe94;
        uint kuruma2 = 0x187d938d;
        uint locust = 0xc7e55211;
        uint lynx = 0x1cbdc10b;
        uint massacro = 0xf77ade32;
        uint massacro2 = 0xda5819a3;
        uint neo = 0x9f6ed5a2;
        uint neon = 0x91ca96ee;
        uint ninef = 0x3d8fa25c;
        uint ninef2 = 0xa8e38b01;
        uint omnis = 0xd1ad4937;
        uint paragon = 0xe550775b;
        uint paragon2 = 0x546d8eee;
        uint pariah = 0x33b98fe2;
        uint penumbra = 0xe9805550;
        uint penumbra2 = 0xda5ec7da;
        uint raiden = 0xa4d99b7d;
        uint rapidgt = 0x8cb29a14;
        uint rapidgt2 = 0x679450af;
        uint raptor = 0xd7c56d39;
        uint revolter = 0xe78cc3d9;
        uint ruston = 0x2ae524a8;
        uint schafter2 = 0xb52b5113;
        uint schafter3 = 0xa774b5a6;
        uint schafter4 = 0x58cf185c;
        uint schafter5 = 0xcb0e7cd9;
        uint schafter6 = 0x72934be4;
        uint schlagen = 0xe1c03ab0;
        uint schwarzer = 0xd37b7976;
        uint sentinel3 = 0x41d149aa;
        uint seven70 = 0x97398a4b;
        uint specter = 0x706e2b40;
        uint specter2 = 0x400f5147;
        uint streiter = 0x67d2b389;
        uint sugoi = 0x3adb9758;
        uint sultan = 0x39da2754;
        uint sultan2 = 0x3404691c;
        uint surano = 0x16e478c1;
        uint tampa2 = 0xc0240885;
        uint tropos = 0x707e63a4;
        uint verlierer2 = 0x41b77fa4;
        uint vstr = 0x56cdee7d;
        uint zr380 = 0x20314b42;
        uint zr3802 = 0xbe11efc6;
        uint zr3803 = 0xa7dcc35c;
        uint italirsx = 0xbb78956a;
        uint veto = 0xcce5c8fa;
        uint veto2 = 0xa703e4a9;
        uint ardent = 0x97e5533;
        uint btype = 0x6ff6914;
        uint btype2 = 0xce6b35a4;
        uint btype3 = 0xdc19d101;
        uint casco = 0x3822bdfe;
        uint cheetah2 = 0xd4e5f4d;
        uint coquette2 = 0x3c4e2113;
        uint deluxo = 0x586765fb;
        uint dynasty = 0x127e90d5;
        uint fagaloa = 0x6068ad86;
        uint feltzer3 = 0xa29d6d10;
        uint gt500 = 0x8408f33a;
        uint infernus2 = 0xac33179c;
        uint jb700 = 0x3eab5555;
        uint jb7002 = 0x177da45c;
        uint mamba = 0x9cfffc56;
        uint manana = 0x81634188;
        uint manana2 = 0x665f785d;
        uint michelli = 0x3e5bd8d9;
        uint monroe = 0xe62b361b;
        uint nebula = 0xcb642637;
        uint peyote = 0x6d19ccbc;
        uint peyote3 = 0x4201a843;
        uint pigalle = 0x404b6381;
        uint rapidgt3 = 0x7a2ef5e4;
        uint retinue = 0x6dbd6c0a;
        uint retinue2 = 0x79178f0a;
        uint savestra = 0x35ded0dd;
        uint stinger = 0x5c23af9b;
        uint stingergt = 0x82e499fa;
        uint stromberg = 0x34dba661;
        uint swinger = 0x1dd4c0ff;
        uint torero = 0x59a9e570;
        uint tornado = 0x1bb290bc;
        uint tornado2 = 0x5b42a5c4;
        uint tornado3 = 0x690a4153;
        uint tornado4 = 0x86cf7cdd;
        uint tornado5 = 0x94da98ef;
        uint tornado6 = 0xa31cb573;
        uint turismo2 = 0xc575df11;
        uint viseris = 0xe8a8ba94;
        uint z190 = 0x3201dd49;
        uint ztype = 0x2d3bd401;
        uint zion3 = 0x6f039a67;
        uint cheburek = 0xc514aae0;
        uint toreador = 0x56c8a5ef;
        uint adder = 0xb779a091;
        uint autarch = 0xed552c74;
        uint banshee2 = 0x25c5af13;
        uint bullet = 0x9ae6dda1;
        uint cheetah = 0xb1d95da0;
        uint cyclone = 0x52ff9437;
        uint entity2 = 0x8198aedc;
        uint entityxf = 0xb2fe5cf9;
        uint emerus = 0x4ee74355;
        uint fmj = 0x5502626c;
        uint furia = 0x3944d5a0;
        uint gp1 = 0x4992196c;
        uint infernus = 0x18f25ac7;
        uint italigtb = 0x85e8e76b;
        uint italigtb2 = 0xe33a477b;
        uint krieger = 0xd86a0247;
        uint le7b = 0xb6846a55;
        uint nero = 0x3da47243;
        uint nero2 = 0x4131f378;
        uint osiris = 0x767164d6;
        uint penetrator = 0x9734f3ea;
        uint pfister811 = 0x92ef6e04;
        uint prototipo = 0x7e8f677f;
        uint reaper = 0xdf381e5;
        uint s80 = 0xeca6b6a3;
        uint sc1 = 0x5097f589;
        uint scramjet = 0xd9f0503d;
        uint sheava = 0x30d3f6d8;
        uint sultanrs = 0xee6024bc;
        uint t20 = 0x6322b39a;
        uint taipan = 0xbc5dc07e;
        uint tempesta = 0x1044926f;
        uint tezeract = 0x3d7c6410;
        uint thrax = 0x3e3d1f59;
        uint tigon = 0xaf0b8d48;
        uint turismor = 0x185484e1;
        uint tyrant = 0xe99011c2;
        uint tyrus = 0x7b406efb;
        uint vacca = 0x142e0dc3;
        uint vagner = 0x7397224c;
        uint vigilante = 0xb5ef4c33;
        uint visione = 0xc4810400;
        uint voltic = 0x9f4b77be;
        uint voltic2 = 0x3af76f4a;
        uint xa21 = 0x36b4a8a9;
        uint zentorno = 0xac5df515;
        uint zorrusso = 0xd757d97d;
        uint armytanker = 0xb8081009;
        uint armytrailer = 0xa7ff33f5;
        uint armytrailer2 = 0x9e6b14d6;
        uint baletrailer = 0xe82ae656;
        uint boattrailer = 0x1f3d44b5;
        uint cablecar = 0xc6c3242d;
        uint docktrailer = 0x806efbee;
        uint freighttrailer = 0xd1abb666;
        uint grauintrailer = 0x3cc7f596;
        uint proptrailer = 0x153e1b0a;
        uint raketrailer = 0x174cb172;
        uint tr2 = 0x7be032c6;
        uint tr3 = 0x6a59902d;
        uint tr4 = 0x7cab34d0;
        uint trflat = 0xaf62f6b2;
        uint tvtrailer = 0x967620be;
        uint tanker = 0xd46f4737;
        uint tanker2 = 0x74998082;
        uint trailerlarge = 0x5993f939;
        uint trailerlogs = 0x782a236d;
        uint trailersmall = 0x2a72beab;
        uint trailers = 0xcbb2be0e;
        uint trailers2 = 0xa1da3c91;
        uint trailers3 = 0x8548036d;
        uint trailers4 = 0xbe66f5aa;
        uint freight = 0x3d6aaa9b;
        uint freightcar = 0x0afd22a6;
        uint freightcont1 = 0x36dcff98;
        uint freightcont2 = 0x0e512e79;
        uint freightgrain = 0x264d9262;
        uint metrotrain = 0x33c9e158;
        uint tankercar = 0x22eddc30;
        uint airtug = 0x5d0aac8f;
        uint caddy = 0x44623884;
        uint caddy2 = 0xdff0594c;
        uint caddy3 = 0xd227bdbb;
        uint docktug = 0xcb44b1ca;
        uint forklift = 0x58e49664;
        uint mower = 0x6a4bd8f6;
        uint ripley = 0xcd935ef9;
        uint sadler = 0xdc434e51;
        uint sadler2 = 0x2bc345d1;
        uint scrap = 0x9a9fd3df;
        uint towtruck = 0xb12314e0;
        uint towtruck2 = 0xe5a2d6c6;
        uint tractor = 0x61d6ba8c;
        uint tractor2 = 0x843b73de;
        uint tractor3 = 0x562a97bd;
        uint utillitruck = 0x1ed0a534;
        uint utillitruck2 = 0x34e6bf6b;
        uint utillitruck3 = 0x7f2153df;
        uint slamtruck = 0xc1a8a914;
        uint bison = 0xfefd644f;
        uint bison2 = 0x7b8297c5;
        uint bison3 = 0x67b3f020;
        uint bobcatxl = 0x3fc5d440;
        uint boxville = 0x898eccea;
        uint boxville2 = 0xf21b33be;
        uint boxville3 = 0x07405e08;
        uint boxville4 = 0x1a79847a;
        uint boxville5 = 0x28ad20e1;
        uint burrito = 0xafbb2ca4;
        uint burrito2 = 0xc9e8ff76;
        uint burrito3 = 0x98171bd3;
        uint burrito4 = 0x353b561d;
        uint burrito5 = 0x437cf2a0;
        uint camper = 0x6fd95f68;
        uint gburrito = 0x97fa4f36;
        uint gburrito2 = 0x11aa0e14;
        uint journey = 0xf8d48e7a;
        uint minivan = 0xed7eada4;
        uint minivan2 = 0xbcde91f0;
        uint paradise = 0x58b3979c;
        uint pony = 0xf8de29a8;
        uint pony2 = 0x38408341;
        uint rumpo = 0x4543b74d;
        uint rumpo2 = 0x961afef7;
        uint rumpo3 = 0x57f682af;
        uint speedo = 0xcfb3870c;
        uint speedo2 = 0x2b6dc64a;
        uint speedo4 = 0xd17099d;
        uint surfer = 0x29b0da97;
        uint surfer2 = 0xb1d80e06;
        uint taco = 0x744ca80d;
        uint youga = 0x03e5f6b8;
        uint youga2 = 0x3d29cd2b;
        uint youga3 = 0x6b73a9be;
    }

    public class Class1 : BaseScript
    {
        public Class1()
        {
            Tick += OnTick;
            Tick += DrawRectangle;

            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            EventHandlers["client:sendback"] += new Action<string>(sendback);
            EventHandlers["client:applyLooks"] += new Action<Dictionary<string, int>>(applyLooks);

            //Debug.WriteLine("console commands started");

            
        }

        int cam = -1;
        string zoom = "ropa";
        bool isCameraActive = false;
        float camHeading = 0.0f;
        float angle = 0.0f;
        float camOffset = 1.8f;
        float zoomOffset  = 0.0f;

        private void applyLooks(Dictionary<string, int> obj)
        {
            //Wait(2);

        }



        private void msg(List<string> args)
        {

            foreach (string element in args)
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 255, 0 },
                    args = new[] { "[Server]", $"^*{element}" }
                });

        }
        private async Task spawnCarAsync(List<object> args)
        {
            // account for the argument not being passed
            var model = "adder";
            if (args.Count > 0)
            {
                model = args[0].ToString();
            }

            // check if the model actually exists
            // assumes the directive `using static CitizenFX.Core.Native.API;`
            var hash = (uint)GetHashKey(model);
            if (!IsModelInCdimage(hash) || !IsModelAVehicle(hash))
            {

                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[CarSpawner]", $"It might have been a good thing that you tried to spawn a {model}. Who even wants their spawning to actually ^*succeed?" }
                });
                return;
            }

            // create the vehicle
            var vehicle = await World.CreateVehicle(model, Game.PlayerPed.Position, Game.PlayerPed.Heading);

            // set the player ped into the vehicle and driver seat
            Game.PlayerPed.SetIntoVehicle(vehicle, VehicleSeat.Driver);

            // tell the player
            TriggerEvent("chat:addMessage", new
            {
                color = new[] { 255, 0, 0 },
                args = new[] { "[CarSpawner]", $"Woohoo! Enjoy your new ^*{model}!" }
            });
        }

        private void sendback(string allowed)
        {
            TriggerEvent("chat:addMessage", new
            {
                color = new[] { 0, 0, 250 },
                args = new[] { "[Server]", $"^*{allowed} triggered a command locally" }
            });
        }

        private void OnClientResourceStart(string resourceName)
        {


            if (GetCurrentResourceName() != resourceName) return;

            RegisterCommand("TP", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                try
                {
                    float x = float.Parse(args[0].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    float y = float.Parse(args[1].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    float z = float.Parse(args[2].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    if (args.Count > 0)
                    {
                        teleportPlayer(new Vector3(x, y, z));
                        Debug.WriteLine("player teleported");
                    }
                }
                catch (Exception e)
                {
                    TriggerEvent("chat:addMessage", new
                    {
                        color = new[] { 255, 0, 0 },
                        args = new[] { "[TP]", $"cannot convert to float" }
                    });
                    return;
                }


            })
            , false);

            RegisterCommand("triggerCam", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                /**
                 * local cam = -1							-- Camera control
local zoom = "ropa"					-- Define which tab is shown first (Default: Head)
local isCameraActive
local camHeading = 0.0
local angulo = 0
local camOffset, zoomOffset = 1.8, 0.0
                 * 
                 * 
                 * function CreateSkinCam()
	if not DoesCamExist(cam) then
		cam = CreateCam('DEFAULT_SCRIPTED_CAMERA', true)
	end

	SetCamActive(cam, true)
	RenderScriptCams(true, true, 500, true, true)

	local playerPed = PlayerPedId()
	local playerHeading = GetEntityHeading(playerPed)
	if playerHeading + 94 < 360.0 then
		camHeading = playerHeading + 94.0
	elseif playerHeading + 94 >= 360.0 then
		camHeading = playerHeading - 266.0 --194
	end
	angulo = camHeading
	isCameraActive = true
	SetCamCoord(cam, GetEntityCoords(GetPlayerPed(-1)))
end

function DeleteSkinCam()
	isCameraActive = false
	SetCamActive(cam, false)
	RenderScriptCams(false, true, 500, true, true)
	cam = nil
end
                 * 
                 */
                var cam = CreateCam("DEFAULT_SCRIPTED_CAMERA", true);
                SetCamActive(cam, true);
                RenderScriptCams(true, true, 500, true, true);

                int playerPed = Game.PlayerPed.Handle;
                float playerHeading = GetEntityHeading(playerPed);
                if (playerHeading + 94 < 360.0f)
                {
                    camHeading = playerHeading + 94.0f;
                }else if (playerHeading + 94 >= 360.0f)
                {
                    camHeading = playerHeading - 266.0f;
                }
                angle = camHeading;
                isCameraActive = true;
                var coords = GetEntityCoords(Game.PlayerPed.Handle, true);
                SetCamCoord(cam, coords.X, coords.Y, coords.Z);


            }), false);

            RegisterCommand("untogglecam", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                isCameraActive = false;

                SetCamActive(cam, false);

                RenderScriptCams(false, true, 500, true, true);

                cam = -1;
            }), false);


            RegisterCommand("chatM", new Action<int, List<object>, string>(async (source, args, raw) =>
            {

                //List<string> mylist = new List<string>(new string[] { resourceName, source.ToString(), args.ToString(), raw });
                //msg(new List<string>(new string[] { resourceName, source.ToString(), args.ToString(), raw, Game.Player.Name }));
                msg(new List<string>(new string[] { "chatM called by", Game.Player.Name }));
                //TriggerEvent("client:sendback", "internal call");
                TriggerServerEvent("bc:netCheckPermission", Game.Player.Name);
            }

            ), false);

            RegisterCommand("car", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                spawnCarAsync(args);
            }), false);

            RegisterCommand("entershop", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                entershop();
            }), false);


        }

        private void entershop()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                Vehicle veh = Game.PlayerPed.LastVehicle;
                veh.Position = new Vector3(-333.4315f,-135.5258f,38.57364f);
                veh.Heading = 88.82297f;

                if (veh.IsDamaged)
                {
                    Debug.WriteLine(veh.IsFrontBumperBrokenOff.ToString());
                    Debug.WriteLine(veh.IsLeftHeadLightBroken.ToString());
                    Debug.WriteLine(veh.IsRearBumperBrokenOff.ToString());
                    Debug.WriteLine(veh.IsRightHeadLightBroken.ToString());
                    Debug.WriteLine(veh.Windows.AreAllWindowsIntact.ToString());
                    foreach (VehicleDoorIndex vdi in (VehicleDoorIndex[])Enum.GetValues(typeof(VehicleDoorIndex)))
                        Debug.WriteLine(veh.Doors.HasDoor(vdi).ToString());
        
                }
            }
        }

        private void teleportPlayer(Vector3 position)
        {
            //Game.PlayerPed.CanBeDraggedOutOfVehicle = false;
            Game.PlayerPed.Position = position;
        }



        /// <summary>
        /// Function to display a text
        /// </summary>
        /// <param name="x"></param> value range 0.0-1.0 relative to screen width
        /// <param name="y"></param> value range 0.0-1.0 relative to screen height
        /// <param name="text"></param> string to be displayed
        private static void DisplayText(float x, float y, string text)
        {
            BeginTextCommandDisplayText("STRING");
            AddTextComponentString(text);
            SetTextScale(1f, .5f);
            SetTextCentre(true);
            EndTextCommandDisplayText(x,y);
        }
        /// <summary>
        /// displays a rectangle , for other doc see DisplayText()
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="alpha"></param>
        private static void DisplayRect(float x, float y, float width, float height, int r, int g, int b, int alpha)
        {
            DrawRect(x,y,width,height,r,g,b,alpha);
        }

        public static void DebugDisplay(/*Text text*/)
        {
            float textx = Screen.Resolution.Width / 2f;
            float texty = Screen.Resolution.Height / 2f;

            DrawRect(.95f, .3f, .02f, .02f, 250, 50, 50, 200);

            BeginTextCommandDisplayText("STRING");
            AddTextComponentString("text1");
            SetTextScale(1f, .5f);
            SetTextCentre(true);
            SetTextColour(250, 50, 50, 200);
            EndTextCommandDisplayText(.95f, .3f);

            //Text text = new Text("text2", new PointF(textx, texty), .5f);
            //text.Alignment = Alignment.Center;
            //text.Draw();
        }


        
        private async Task OnTick()
        {
            //await Task.Delay(1);
            //ThreadMethod();
            DisplayRect(.95f, .3f, .02f, .02f, 250, 50, 50, 200);

            //float fuel = Game.PlayerPed.CurrentVehicle.FuelLevel;

            //DisplayText(.95f, .5f, "fuel:" + fuel.ToString());

        }





        private async Task DrawRectangle()
        {
            //Rectangle(50.0f, 50.0f, 2.0f, 2.0f, 255, 0, 0, 255, 0, 0);
            //DebugDisplay();

            
            DisplayText(.95f, .3f, "X:"+Game.PlayerPed.Position.X.ToString());
            DisplayText(.95f, .35f, "Y:" + Game.PlayerPed.Position.Y.ToString());
            DisplayText(.95f, .4f, "Z:" + Game.PlayerPed.Position.Z.ToString());

            DisplayText(.95f, .45f, "H:" + Game.PlayerPed.Heading.ToString());
            //Game.PlayerPed.
            //DisplayText(.95f, .35f, "Y:" + Game.PlayerPed.Position.Y.ToString());
            //DisplayText(.95f, .4f, "Z:" + Game.PlayerPed.Position.Z.ToString());



        }


    }
}
