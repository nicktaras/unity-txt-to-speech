﻿using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorTask
{
    /// <summary>Loads the configuration at startup.</summary>
    [InitializeOnLoad]
    public static class ConfigLoader
    {

        #region Constructor

        static ConfigLoader()
        {
            if (!Util.Config.isLoaded)
            {
                Util.Config.Load();

                if (Util.Config.DEBUG)
                    UnityEngine.Debug.Log("Config data loaded");
            }
        }

        #endregion
    }
}
// © 2017 crosstales LLC (https://www.crosstales.com)