using System;
namespace SolfeggioAPI.Services
{
    public class JsonService
    {
        public static String GetJson()
        {
            using (StreamReader r = new StreamReader("solfeggio.json"))
            {
                string json = r.ReadToEnd();
                return json;
            }
        }

        public static String GetUsers()
        {
            using (StreamReader r = new StreamReader("users.json"))
            {
                string json = r.ReadToEnd();
                return json;
            }
        }
    }
}

