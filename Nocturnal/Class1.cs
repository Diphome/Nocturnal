using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;

namespace Nocturnal
{
    public class Main
    {
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();
        public static void Start()
        {
            AllocConsole();
            StreamWriter standardOutput = new StreamWriter(System.Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            System.Console.SetOut(standardOutput);

            System.Console.WriteLine("------------------------------------");
            System.Console.WriteLine("  [Nocturnal v1.0]");
            System.Console.WriteLine("------------------------------------");

            System.Console.WriteLine("Probe entered the namespace correctly...");

            Thread th = new Thread(init);
            th.Start();
        }

        public static void dumpGameObjects(StreamWriter w)
        {
            System.Console.WriteLine("Dumping all activated gameobjects of current scene.");

            w.WriteLine("***");
            w.WriteLine("dumpGameObjects report :");
            w.WriteLine("***");
            w.WriteLine("");

            Component[] components;
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

            foreach (GameObject go in allObjects)
            {
                if (go.activeInHierarchy)
                {
                    w.WriteLine("--");
                    w.WriteLine("[gameObject] -> " + go.name);

                    components = go.GetComponents(typeof(Component));
                    w.WriteLine("  [components] :");
                    foreach (Component comp in components)
                    {
                        w.WriteLine("    " + comp.GetType().ToString());
                    }
                }
            }
            w.WriteLine("--");
            w.WriteLine("");
            System.Console.WriteLine("dumpGameObjects done.");
        }

        public static GameObject findGameobject(string name)
        {
            System.Console.WriteLine("Searching gameobject with name : " + name);

            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.activeInHierarchy && go.name.Contains(name))
                {
                    System.Console.WriteLine("Gameobject found !");
                    return go;
                }
            }
            System.Console.WriteLine("Gameobject not found, returning null...");
            return null;
        }

        public static void dumpScenesInfo(StreamWriter w)
        {
            System.Console.WriteLine("Dumping all resources");

            w.WriteLine("***");
            w.WriteLine("dumpScenesInfo report :");
            w.WriteLine("***");
            w.WriteLine("");

            w.WriteLine("Current Scene name : " + SceneManager.GetActiveScene().name);
            w.WriteLine("Currently in build settings there is : " + SceneManager.sceneCountInBuildSettings + " scenes");

            w.WriteLine("");

            System.Console.WriteLine("dumpScenesInfo done.");
        }
        public static void dumpResourcesInfo(StreamWriter w)
        {
            System.Console.WriteLine("Dumping all resources");

            w.WriteLine("***");
            w.WriteLine("dumpResourcesInfo report :");
            w.WriteLine("***");
            w.WriteLine("");

            w.WriteLine("All : " + Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)).Length);
            w.WriteLine("Textures : " + Resources.FindObjectsOfTypeAll(typeof(Texture)).Length);
            w.WriteLine("AudioClips : " + Resources.FindObjectsOfTypeAll(typeof(AudioClip)).Length);
            w.WriteLine("Meshes : " + Resources.FindObjectsOfTypeAll(typeof(Mesh)).Length);
            w.WriteLine("Materials : " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
            w.WriteLine("GameObjects : " + Resources.FindObjectsOfTypeAll(typeof(GameObject)).Length);
            w.WriteLine("Components : " + Resources.FindObjectsOfTypeAll(typeof(Component)).Length);

            w.WriteLine("");

            foreach (AudioClip go in Resources.FindObjectsOfTypeAll(typeof(AudioClip)) as AudioClip[])
            {
                w.WriteLine("audioClip name : " + go.name);
            }

            w.WriteLine("");

            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                w.WriteLine("GameObject name : " + go.name);
            }

            w.WriteLine("");

            System.Console.WriteLine("dumpResourcesInfo done.");
        }

        static void init()
        {
            System.Console.WriteLine("Probe landed sucessfully in launched thread.");
            StreamWriter w = File.CreateText("Nocturnal-report.txt");

            w.WriteLine("------------------------------------");
            w.WriteLine("  [Nocturnal reporter v1.0]");
            w.WriteLine("------------------------------------");
            w.WriteLine("");

            dumpScenesInfo(w);
            dumpResourcesInfo(w);
            dumpGameObjects(w);

            w.Close();

            System.Console.WriteLine("Job done, entering slowly in the night...");
        }
    }
}
