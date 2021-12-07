using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{

    public float ScreenSaverTimer = 10.0f;
    float timeLeft;

    void Start ()
    {
        timeLeft = ScreenSaverTimer;
    }

    void OnEnable()
    {
        SocketManager.DispatchSpeakEvent += SpeakEvent;
    }

    void OnDisable()
    {
        SocketManager.DispatchSpeakEvent -= SpeakEvent;
    }

    void SpeakEvent(string str)
    {
        ResetTimer();
    }

    void ResetTimer()
    {
        timeLeft = ScreenSaverTimer;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Debug.Log("Time is Up");
            // Application.LoadLevel(""); // Switch Scene?
        }
    }
}
