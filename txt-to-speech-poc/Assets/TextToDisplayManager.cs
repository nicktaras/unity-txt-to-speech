// TextToDisplayManager
// Subscribes to Socket Events and projects text recieved to the display.

using System.Text.RegularExpressions;
using UnityEngine;

public class TextToDisplayManager : MonoBehaviour {

    VTextInterface vi;

    // Adjust this value as required when making changes to the size of the Voxon Box 
    public int maxCharactersPerLine = 90;

    void OnEnable()
    {
        SocketManager.DispatchSpeakEvent += SpeakEvent;
    }

    void OnDisable()
    {
        SocketManager.DispatchSpeakEvent -= SpeakEvent;
    }

    private void Start(){
        vi = GetComponent<VTextInterface>();
    }

    // Define Curve in degree
    void SetCurveRadius(string str)
    {
        int strLength = str.Length;
        int maxRadius = maxCharactersPerLine;
        vi.layout.EndRadius = 360 * strLength / maxCharactersPerLine;
    }

    // Render the 3D text to view
    void RenderText(string str)
    {
        vi.RenderText = str;
    }

    // Utility method to split the text up into lines.
    public static string SpliceText(string text, int lineLength)
    {
        return Regex.Replace(text, "(.{" + lineLength + "})", "$1" + System.Environment.NewLine);
    }

    // Manages speech events within class
    // Current steps:
    // 1. Define radius to draw text
    // 2. When text exceeds a multiple of n create new line
    // 3. Render transformed text to view
    void SpeakEvent(string str)
    {
        SetCurveRadius(str);
        string textOutputTransformed = SpliceText(str, maxCharactersPerLine);
        RenderText(textOutputTransformed);
    }

}

