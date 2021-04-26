using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;



namespace loginscriptserver
{
    public class loginscriptserver : BaseScript
    {

        public loginscriptserver()
        {
            EventHandlers["cbc:login"] += new Action<Player>(login);
        }
        public static string getBetween(string strSource, string searchString)
        {
            if (strSource.Contains(searchString))
            {
                return strSource.Remove(0, searchString.Length);
            }

            return "";
        }
        private void login([FromSource]Player source)
        {
            Debug.WriteLine(source.Handle);
            Debug.WriteLine(GetNumPlayerIdentifiers(source.Handle).ToString());

            //int ia = 1;
            //string ids = GetPlayerIdentifier(source.Handle, ia);
            //Debug.WriteLine(ids);
            string steamid = "";
            for (int i = 0; i < GetNumPlayerIdentifiers(source.Handle) - 1; i++)
            {
                string id = GetPlayerIdentifier(source.Handle, i);
                Debug.WriteLine(id);
                if (id.Contains("live:"))
                {
                    steamid = getBetween(id, "live:");
                    source.TriggerEvent("charainter:response", steamid);
                }
                //Debug.WriteLine($"{steamid}");
            }

            //use steamid to setup SQL select


        }


    }
}
