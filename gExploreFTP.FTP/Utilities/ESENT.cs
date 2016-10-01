using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Isam.Esent.Collections.Generic;
using Google.Documents;

namespace gExploreFTP.FTP.Utilities
{
    class ESENT
    {

        private static PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>("files");

        /*public static void Open()
        {
            if (PersistentDictionaryFile.Exists("files") == true)
            {
                dictionary = new PersistentDictionary<string, string>("files"); 
            }
            else
            {
                dictionary = new PersistentDictionary<string, string>("files");
            }
        }*/

        public static void Add(string gmail, string title, string resourceID) 
        {

            string key = gmail + ":" + title;

            if (dictionary.ContainsKey(key) == false)
            {
                dictionary.Add(key, resourceID);
            }
            else
            {
                dictionary[key] = resourceID;
            }

            //Console.WriteLine("Adding " + key + "       " + resourceID);

            // Write it to disk.
            // TODO: Don't flush every time something is added.
            dictionary.Flush();

        }

        public static string Get(string gmail, string title)
        {

            string key = gmail + ":" + title;

            //Console.WriteLine("Getting " + key);

            if (dictionary.ContainsKey(key) == true)
            {
                //Console.WriteLine("Returning " + dictionary[key]);
                return dictionary[key];
            }
            else
            {
                //Console.WriteLine("Not found");
                return null;
            }
            
        }


    }

}
