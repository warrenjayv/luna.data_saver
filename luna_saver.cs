using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace luna
{
    namespace data_saver {
        
      public static class saver { 

            private static saved_data data; 

            static string root_path = Application.persistentDataPath + "/";

            public static void identify ( saved_data data_ref ) { data = data_ref; }

            public static saved_data obtain_data ( ) { return data; }

            public static void serialize ( string file_name ) {

                BinaryFormatter bf = new BinaryFormatter( );
                FileStream      fs = new FileStream( root_path + file_name, FileMode.OpenOrCreate, FileAccess.ReadWrite );
                bf.Serialize ( fs, data ); 
                fs.Close();

                log("file saved: " + root_path + file_name );

            }

            public static void deserialize ( string file_name ) {

                if (! File.Exists(root_path + file_name)) {
                    log("file doesn't exist: " + file_name);
                    return;
                }

                BinaryFormatter bf = new BinaryFormatter( );
                FileStream      fs = File.Open( root_path + file_name, FileMode.Open );
                data               = (saved_data)bf.Deserialize(fs);    
                fs.Close();

                log("file loaded: " + root_path + file_name);

            }

            public static void check ( string file_name ) {

                BinaryFormatter bf = new BinaryFormatter( );
                FileStream      fs = new FileStream( root_path + file_name, FileMode.Open, FileAccess.Read );
                byte [] bytes       = new byte[fs.Length];
                int len             = 0;
                
                using (MemoryStream ms = new MemoryStream()) {
                    while ((len = fs.Read(bytes, 0, bytes.Length)) > 0) {
                        ms.Write(bytes, 0, len); 
                    }
                }

                log ( "check saved file: " + root_path + file_name );
                log (" check saved file: " + len + " bytes");
            }

            public static void log ( string msg ) {
                Debug.Log(typeof(saver).Namespace + ": " + msg);
            }
      }

      // a list of objects that will be serialized and saved. 
      [Serializable]
      public class saved_data  
      {
        private List<saved_object> objects;
        //... 

        public saved_data ( ) {
            objects = new List<saved_object>(); 
        }

        public void add ( saved_object obj ) {
            objects.Add(obj);
        }

        public saved_object get ( int i ) {
            return objects[i];
        }

        public int get_count ( ) {
            return objects.Count; 
        }
      }

      /// populate these fields with your object data 
      [Serializable]
      public class saved_object {

            public player_properties status;

            public saved_object ( ) {
                status = new player_properties(); 
            }
      }

      [Serializable]
      public struct player_properties {
            public float health;
            public float ammo;
      }

    }
}



