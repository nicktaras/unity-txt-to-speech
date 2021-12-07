using UnityEngine;
using System;
using System.Collections.Generic;
using Crosstales.RTVoice;
using CrazyMinnow.SALSA;

namespace CrazyMinnow.SALSA.RTVoice
{
    /// <summary>
    /// SALSA / RT-Voice integration for implementations using RT-Voice in native mode.
    /// 
    /// This script subscribes to the OnSpeakNativeCurrentViseme event, and categorizes
    /// the 21 SAPI visemes into SALSA's four mouth shapes.
    /// 
    /// There are 21 SAPI visemes:
    /// 	0 = rest
    /// 	1 = ax, ah, uh
    /// 	2 = aa
    /// 	3 = ao
    /// 	4 = ey, eh, ae
    /// 	5 = er
    /// 	6 = y, iy, ih, ix
    /// 	7 = w, uw, u
    /// 	8 = ow
    /// 	9 = aw
    /// 	10 = oy
    /// 	11 = ay
    /// 	12 = h
    /// 	13 = r
    /// 	14 = l
    /// 	15 = s, z, ts
    /// 	16 = sh, ch, jh, zh
    /// 	17 = th, dh
    /// 	18 = f, v
    /// 	19 = d, t, dx, n
    /// 	20 = k, g, ng
    /// 	21 = p, b, m
    ///
    /// Sort the 21 SAPI visemes into SALSA's four shapes
    ///     sayRest     0, 17, 18, 21
    ///     saySmall    6, 12, 13, 16, 19, 20
    ///     sayMedium   4, 11, 14, 15
    ///     sayLarge    1, 2, 3, 5, 7, 8, 9, 10
    /// </summary>
    [AddComponentMenu("Crazy Minnow Studio/SALSA/Addons/RT-Voice/Salsa_RTVoice_Native")]
    public class Salsa_RTVoice_Native : MonoBehaviour
    {
        public GameObject salsa; // GameObject that contains Salsa2D or Salsa3D
        public string speakText = "This is a test using SALSA with RT-Voice in native mode"; // Text to pass to SpeakNative
        public bool speak; // Speak trigger for editor testing

        private Salsa3D salsa3D; // Salsa3D component
        private Salsa2D salsa2D; // Salsa2D component
        private int currentViseme = 0; // current viseme index
        private List<int> sayRest = new List<int> { 0, 17, 18, 21 }; // Visemes for SALSA sayRest
        private List<int> saySmall = new List<int> { 6, 12, 13, 16, 19, 20 }; // Visemes for SALSA saySmall
        private List<int> sayMedium = new List<int> { 4, 11, 14, 15 }; // Visemes for SALSA sayMedium
        private List<int> sayLarge = new List<int> { 1, 2, 3, 5, 7, 8, 9, 10 }; // Visemes for SALSA sayLarge

        /// <summary>
        /// Subscrive to the OnSpeakNativeCurrentViseme event OnEnable
        /// </summary>
        void OnEnable()
        {
            Speaker.OnSpeakCurrentViseme += Speaker_OnSpeakCurrentViseme;
        }

        /// <summary>
        /// Unsubscrive from the OnSpeakCurrentViseme event OnDisable
        /// </summary>
        void OnDisable()
        {
            Speaker.OnSpeakCurrentViseme -= Speaker_OnSpeakCurrentViseme;
        }
        /// <summary>
        /// Unsubscrive from the OnSpeakCurrentViseme event OnDestroy
        /// </summary>
        void OnDestroy()
        {
            Speaker.OnSpeakCurrentViseme -= Speaker_OnSpeakCurrentViseme;
        }

    /// <summary>
    /// Listenter for the OnSpeakCurrentViseme event.
    /// </summary>
    /// <param name="wrapper"></param>
    /// <param name="viseme"></param>
    private void Speaker_OnSpeakCurrentViseme(Crosstales.RTVoice.Model.Wrapper wrapper, string viseme)
    {
            if (viseme == "")
                currentViseme = 0;
            else
                currentViseme = Convert.ToInt32(viseme);

            // Set SALSA average value based on the current viseme index
            if (salsa2D || salsa3D)
            {
                if (sayRest.Contains(currentViseme))
                {
                    if (salsa2D) salsa2D.average = 0;
                    else if (salsa3D) salsa3D.average = 0;
                }
                else if (saySmall.Contains(currentViseme))
                {
                    if (salsa2D) salsa2D.average = salsa2D.saySmallTrigger;
                    else if (salsa3D) salsa3D.average = salsa3D.saySmallTrigger;
                }
                else if (sayMedium.Contains(currentViseme))
                {
                    if (salsa2D) salsa2D.average = salsa2D.sayMediumTrigger;
                    else if (salsa3D) salsa3D.average = salsa3D.sayMediumTrigger;
                }
                else if (sayLarge.Contains(currentViseme))
                {
                    if (salsa2D) salsa2D.average = salsa2D.sayLargeTrigger;
                    else if (salsa3D) salsa3D.average = salsa3D.sayLargeTrigger;
                }
            }
        }

        /// <summary>
        /// Get the Salsa2D or Salsa3D component
        /// </summary>
        void Awake()
        {
            if (!salsa)
            {
                salsa2D = GetComponent<Salsa2D>();
                if (salsa2D)
                    salsa = salsa2D.gameObject;
                else
                    salsa3D = GetComponent<Salsa3D>();

                if (salsa3D)
                    salsa = salsa3D.gameObject;
            }
        }

        /// <summary>
        /// This is only used for testing and can be deleted in an implementation where you 
        /// make your own call's to [Speaker.SpeakNative]. Click [Speak] in this inspector
        /// to demonstrate send the [speakText] to the [Speaker.SpeakNative] RT-Voice method.
        /// </summary>
        private void LateUpdate()
        {
            if (speak)
            {
                speak = false;
                Speaker.SpeakNative(speakText, Speaker.VoiceForCulture("en"), 1.0f);
            }
        }
    }
}