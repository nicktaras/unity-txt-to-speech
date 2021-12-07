// TextToSpeechManager.cs
// Subscribes to Socket Events and invokes RT Speech plugin component.

using UnityEngine;
using Crosstales.RTVoice;

public class TextToSpeechManager : MonoBehaviour {

    public AudioSource source;

    void OnEnable()
    {
        SocketManager.DispatchSpeakEvent += SpeakEvent;
    }

    void OnDisable()
    {
        SocketManager.DispatchSpeakEvent -= SpeakEvent;
    }

    private void Start()
    {
    }

    void SpeakEvent(string str)
    {
        Speaker.Speak(str, source, Speaker.VoiceForName("Microsoft Zira Desktop"));
    }

}

