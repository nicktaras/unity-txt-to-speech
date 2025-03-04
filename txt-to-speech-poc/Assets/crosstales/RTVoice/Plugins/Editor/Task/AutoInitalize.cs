﻿using UnityEditor;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
#endif
using Crosstales.RTVoice.EditorUtil;

namespace Crosstales.RTVoice.EditorTask
{
    /// <summary>Automatically adds the neccessary RTVoice-prefabs to the current scene.</summary>
    [InitializeOnLoad]
    public class AutoInitalize
    {

        #region Variables

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
        private static Scene currentScene;
#else
        private static string currentScene;
#endif

        #endregion


        #region Constructor

        static AutoInitalize()
        {
            EditorApplication.hierarchyWindowChanged += hierarchyWindowChanged;
        }

        #endregion


        #region Private static methods

        private static void hierarchyWindowChanged()
        {
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
            if (currentScene != EditorSceneManager.GetActiveScene())
            {
#else
            if (currentScene != EditorApplication.currentScene)
            {
#endif
                if (EditorConfig.PREFAB_AUTOLOAD)
                {
                    if (!EditorHelper.isRTVoiceInScene)
                        EditorHelper.InstantiatePrefab("RTVoice");

                }

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
                currentScene = EditorSceneManager.GetActiveScene();
#else
                currentScene = EditorApplication.currentScene;
#endif
            }
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)