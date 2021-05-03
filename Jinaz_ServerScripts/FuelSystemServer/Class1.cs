using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;
using MySql.Data.MySqlClient;

namespace fuelsserver
{
    public class fuelsserver : BaseScript
    {

        public fuelsserver()
        {
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["bc:netCheckPermission"] += new Action<Player, string>(checkPermission);


        }

        private async Task execSQL()
        {
            string connStr = "server=localhost;user=root;database=world;port=3306;password=******";
            MySqlConnection conn = new MySqlConnection(connStr);
            try {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                //SQL string
                string sql = "SELECT Name, HeadOfState FROM Country WHERE Continent='Oceania'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //reader
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }
                rdr.Close();

                //counter
                long messagesNo = (long)await cmd.ExecuteScalarAsync();

                Debug.WriteLine($"ChatStoreServer: >> Total messages stored in database: {messagesNo} ");

                //insert

                string insertStmt = "insert into message (player_name, message_text) values (@player_name, @message)";
                MySqlCommand command = new MySqlCommand(insertStmt, conn);
                //command.Parameters.AddWithValue("@player_name", playerName);
                //command.Parameters.AddWithValue("@message", message);

                long rowsNo = (long)await command.ExecuteNonQueryAsync();

                Debug.WriteLine($"ChatStoreServer: >> Rows inserted: {rowsNo} ");

                //
                //command.

            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
               
            }
            

                conn.Close();
            
        }

        private void OnResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
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
