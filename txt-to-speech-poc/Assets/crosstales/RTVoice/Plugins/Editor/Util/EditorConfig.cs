﻿using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorUtil
{
    /// <summary>Editor configuration for the asset.</summary>
    [InitializeOnLoad]
    public static class EditorConfig
    {

        #region Variables

        /// <summary>Enable or disable update-checks for the asset.</summary>
        public static bool UPDATE_CHECK = EditorConstants.DEFAULT_UPDATE_CHECK;

        /// <summary>Open the UAS-site when an update is found.</summary>
        public static bool UPDATE_OPEN_UAS = EditorConstants.DEFAULT_UPDATE_OPEN_UAS;

        /// <summary>Enable or disable reminder-checks for the asset.</summary>
        public static bool REMINDER_CHECK = EditorConstants.DEFAULT_REMINDER_CHECK;

        /// <summary>Enable or disable anonymous telemetry data.</summary>
        public static bool TELEMETRY = EditorConstants.DEFAULT_TELEMETRY;

        /// <summary>Automatically load and add the prefabs to the scene.</summary>
        public static bool PREFAB_AUTOLOAD = EditorConstants.DEFAULT_PREFAB_AUTOLOAD;

        /// <summary>Enable or disable the icon in the hierarchy.</summary>
        public static bool HIERARCHY_ICON = EditorConstants.DEFAULT_HIERARCHY_ICON;

        /// <summary>Is the configuration loaded?</summary>
        public static bool isLoaded = false;

        private static string assetPath = null;
        private const string idPath = "Documentation/id/";
        private readonly static string idName = EditorConstants.ASSET_UID + ".txt";

        #endregion


        #region Constructor

        static EditorConfig()
        {
            if (!isLoaded)
            {
                Load();
            }
        }

        #endregion


        #region Properties

        /// <summary>Returns the path to the asset inside the Unity project.</summary>
        /// <returns>The path to the asset inside the Unity project.</returns>
        public static string ASSET_PATH
        {
            get
            {
                if (assetPath == null)
                {
                    try
                    {
                        if (System.IO.File.Exists(Application.dataPath + EditorConstants.DEFAULT_ASSET_PATH + idPath + idName))
                        {
                            Util.Config.ASSET_PATH = assetPath = EditorConstants.DEFAULT_ASSET_PATH;
                        }
                        else
                        {
                            string[] files = System.IO.Directory.GetFiles(Application.dataPath, idName, System.IO.SearchOption.AllDirectories);

                            if (files.Length > 0)
                            {
                                string name = files[0].Substring(Application.dataPath.Length);
                                Util.Config.ASSET_PATH = assetPath = name.Substring(0, name.Length - idPath.Length - idName.Length).Replace("\\", "/");
                            }
                            else
                            {
                                Debug.LogWarning("Could not locate the asset! File not found: " + idName);
                                Util.Config.ASSET_PATH = assetPath = EditorConstants.DEFAULT_ASSET_PATH;
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogWarning("Could not locate asset: " + ex);
                    }
                }

                return assetPath;
            }
        }

        /// <summary>Returns the path of the prefabs.</summary>
        /// <returns>The path of the prefabs.</returns>
        public static string PREFAB_PATH
        {
            get
            {
                return ASSET_PATH + EditorConstants.PREFAB_SUBPATH;
            }
        }

        #endregion


        #region Public static methods

        /// <summary>Resets all changable variables to their default value.</summary>
        public static void Reset()
        {
            UPDATE_CHECK = EditorConstants.DEFAULT_UPDATE_CHECK;
            UPDATE_OPEN_UAS = EditorConstants.DEFAULT_UPDATE_OPEN_UAS;
            REMINDER_CHECK = EditorConstants.DEFAULT_REMINDER_CHECK;
            TELEMETRY = EditorConstants.DEFAULT_TELEMETRY;
            PREFAB_AUTOLOAD = EditorConstants.DEFAULT_PREFAB_AUTOLOAD;
            HIERARCHY_ICON = EditorConstants.DEFAULT_HIERARCHY_ICON;
        }

        /// <summary>Loads all changable variables.</summary>
        public static void Load()
        {
            if (Util.CTPlayerPrefs.HasKey(EditorConstants.KEY_UPDATE_CHECK))
            {
                UPDATE_CHECK = Util.CTPlayerPrefs.GetBool(EditorConstants.KEY_UPDATE_CHECK);
            }

            if (Util.CTPlayerPrefs.HasKey(EditorConstants.KEY_UPDATE_OPEN_UAS))
            {
                UPDATE_OPEN_UAS = Util.CTPlayerPrefs.GetBool(EditorConstants.KEY_UPDATE_OPEN_UAS);
            }

            if (Util.CTPlayerPrefs.HasKey(EditorConstants.KEY_REMINDER_CHECK))
            {
                REMINDER_CHECK = Util.CTPlayerPrefs.GetBool(EditorConstants.KEY_REMINDER_CHECK);
            }

            if (Util.CTPlayerPrefs.HasKey(EditorConstants.KEY_TELEMETRY))
            {
                TELEMETRY = Util.CTPlayerPrefs.GetBool(EditorConstants.KEY_TELEMETRY);
            }

            if (Util.CTPlayerPrefs.HasKey(EditorConstants.KEY_PREFAB_AUTOLOAD))
            {
                PREFAB_AUTOLOAD = Util.CTPlayerPrefs.GetBool(EditorConstants.KEY_PREFAB_AUTOLOAD);
            }

            if (Util.CTPlayerPrefs.HasKey(EditorConstants.KEY_HIERARCHY_ICON))
            {
                HIERARCHY_ICON = Util.CTPlayerPrefs.GetBool(EditorConstants.KEY_HIERARCHY_ICON);
            }

            isLoaded = true;
        }

        /// <summary>Saves all changable variables.</summary>
        public static void Save()
        {
            Util.CTPlayerPrefs.SetBool(EditorConstants.KEY_UPDATE_CHECK, UPDATE_CHECK);
            Util.CTPlayerPrefs.SetBool(EditorConstants.KEY_UPDATE_OPEN_UAS, UPDATE_OPEN_UAS);
            Util.CTPlayerPrefs.SetBool(EditorConstants.KEY_REMINDER_CHECK, REMINDER_CHECK);
            Util.CTPlayerPrefs.SetBool(EditorConstants.KEY_TELEMETRY, TELEMETRY);
            Util.CTPlayerPrefs.SetBool(EditorConstants.KEY_PREFAB_AUTOLOAD, PREFAB_AUTOLOAD);
            Util.CTPlayerPrefs.SetBool(EditorConstants.KEY_HIERARCHY_ICON, HIERARCHY_ICON);

            Util.CTPlayerPrefs.Save();
        }

        #endregion
    }
}
// © 2017 crosstales LLC (https://www.crosstales.com)