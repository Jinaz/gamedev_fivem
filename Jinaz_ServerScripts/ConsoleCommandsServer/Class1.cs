using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;

namespace ConsoleCommandsServer
{
    public class Class1 : BaseScript
    {
        
        public Class1()
        {
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["bc:netCheckPermission"] += new Action<Player, string>(checkPermission);
            
            
        }

        private void OnResourceStart(string obj)
        {
            Debug.WriteLine($"Server Side started");
        }

        private void checkPermission([FromSource] Player source, string param1)
        {
            
            
            source.TriggerEvent("chat:addMessage", $"[Server] {param1.ToString()} triggered an event");
            source.TriggerEvent("client:sendback", param1.ToString());

            //TriggerClientEvent(source, "bc:sendback",  param1);
            
        }

        private bool CheckInDB()
        {
            

            return false;
        }
    }
}
