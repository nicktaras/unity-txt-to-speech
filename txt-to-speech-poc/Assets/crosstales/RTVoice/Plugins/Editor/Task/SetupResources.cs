using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorTask
{
    /// <summary>Copies all resources to 'Editor Default Resources'.</summary>
    [InitializeOnLoad]
    public static class SetupResources
    {

        #region Constructor

        static SetupResources()
        {
            // set the miniumum API-level for Android to 15/16
            int androidVersion;

            if (int.TryParse(PlayerSettings.Android.minSdkVersion.ToString().Substring("AndroidApiLevel".Length), out androidVersion))
            {
                // if (androidVersion < 16)
                // {
                    // PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel16;
                // }
            }

            // set the miniumum version for iOS to 8.0
            float iOSVersion;

#if UNITY_5_5_OR_NEWER
            if (float.TryParse(PlayerSettings.iOS.targetOSVersionString.ToString(), out iOSVersion)) {
                if (iOSVersion < 8f) {
                    PlayerSettings.iOS.targetOSVersionString = "8.0";
                }
            }
#else
            if (float.TryParse(PlayerSettings.iOS.targetOSVersion.ToString().Substring("iOS_".Length).Replace("_", "."), out iOSVersion))
            {
                if (iOSVersion < 8f)
                {
                    PlayerSettings.iOS.targetOSVersion = iOSTargetOSVersion.iOS_8_0;
                }
            }
#endif

#if !rtv_ignore_setup

            string path = Application.dataPath + "/";
            string assetpath = path + "crosstales/RTVoice/";
            string source = assetpath + "Icons/";
            string target = path + "Editor Default Resources/RTVoice/";
            string metafile = assetpath + "Icons.meta";

            try
            {
                if (System.IO.Directory.Exists(source))
                {
                    if (!System.IO.Directory.Exists(target))
                    {
                        System.IO.Directory.CreateDirectory(target);
                    }

                    var dirSource = new System.IO.DirectoryInfo(source);

                    foreach (var file in dirSource.GetFiles("*"))
                    {
                        if (System.IO.File.Exists(target + file.Name))
                        {
                            System.IO.File.Delete(target + file.Name);
                        }

                        file.MoveTo(target + file.Name);

                        if (Util.Config.DEBUG)
                            Debug.Log("File moved: " + file);
                    }

                    dirSource.Delete();

                    if (System.IO.File.Exists(metafile))
                    {
                        System.IO.File.Delete(metafile);
                    }

                    AssetDatabase.Refresh();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Could not move all files: " + ex);
            }
#endif
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)