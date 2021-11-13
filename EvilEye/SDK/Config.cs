using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EvilEye.SDK
{
    public class Config
    {
        public Config()
        {
            if (!Directory.Exists("EvilEye"))
            {
                Directory.CreateDirectory("EvilEye");
            }
            if (!File.Exists("EvilEye/Config.ini"))
            {
                File.Create("EvilEye/Config.ini");
            }
            if (!Directory.Exists("EvilEye/VRCA"))
            {
                Directory.CreateDirectory("EvilEye/VRCA");
            }
            if (!Directory.Exists("EvilEye/VRCW"))
            {
                Directory.CreateDirectory("EvilEye/VRCW");
            }
        }

        public int getConfigInt(string key, int defaultVal)
        {
            if (File.ReadAllText("EvilEye/Config.ini").Contains(key))
            {
                string[] arrLine = File.ReadAllLines("EvilEye/Config.ini");
                for (int i = 0; i < arrLine.Length; i++)
                {
                    if (arrLine[i].Contains(key))
                    {
                        return int.Parse(arrLine[i].Split('=')[1]);
                    }
                }
                return 0;
            }
            else
            {
                File.AppendAllText("EvilEye/Config.ini", "\n" + key + "=" + defaultVal);
                return defaultVal;
            }
        }

        public void setConfigBool(string key, bool state)
        {
            string[] arrLine = File.ReadAllLines("EvilEye/Config.ini");
            for (int i = 0; i < arrLine.Length; i++)
            {
                if (arrLine[i].Contains(key))
                {
                    arrLine[i] = key + "=" + state;
                    break;
                }
            }
            File.WriteAllLines("EvilEye/Config.ini", arrLine);
        }

        public bool getConfigBool(string key)
        {
            if (File.ReadAllText("EvilEye/Config.ini").Contains(key))
            {
                string[] arrLine = File.ReadAllLines("EvilEye/Config.ini");
                for (int i = 0; i < arrLine.Length; i++)
                {
                    if (arrLine[i].Contains(key))
                    {
                        return arrLine[i].Split('=')[1] == "True";
                    }
                }

                return false;
            }
            else
            {
                File.AppendAllText("EvilEye/Config.ini", "\n" + key + "=False");
                return false;
            }
        }

        public Color getConfigColor(string key, Color defaultVal)
        {
            if (File.ReadAllText("EvilEye/Config.ini").Contains(key))
            {
                string[] arrLine = File.ReadAllLines("EvilEye/Config.ini");
                for (int i = 0; i < arrLine.Length; i++)
                {
                    if (arrLine[i].Contains(key))
                    {
                        string[] rgb = arrLine[i].Split('=')[1].Split(',');
                        try
                        {
                            return new Color(float.Parse(rgb[0]), float.Parse(rgb[1]), float.Parse(rgb[2]), float.Parse(rgb[3]));
                        }
                        catch
                        {
                            MelonLoader.MelonLogger.Msg(ConsoleColor.Red, "[Config] [Error] colors not saved as nummbers");
                            return defaultVal;
                        }

                    }
                }
                return defaultVal;
            }
            else
            {
                File.AppendAllText("EvilEye/Config.ini", "\n" + key + "=" + defaultVal.r + "," + defaultVal.g + "," + defaultVal.b + "," + defaultVal.a);
                MelonLoader.MelonLogger.Msg("[Config] created color " + key);
                return defaultVal;
            }
        }
    }
}