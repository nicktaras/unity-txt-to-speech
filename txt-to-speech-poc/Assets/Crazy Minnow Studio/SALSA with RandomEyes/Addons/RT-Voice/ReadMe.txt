-------------
Version 1.3.0
-------------
Salsa_RTVoice.cs and Salsa_RTVoice_Native.cs are designed to be used in congunction with SALSA with RandomEyes, and the RT-Voice text-to-speech system. The demonstrate using the RT-Voice Speaker.Speak and Speaker.SpeakNative methods.

Crazy Minnow Studio, LLC
CrazyMinnowStudio.com

This workflow is documented at the following URL, along with a downloadable zip file that contains the supporting files.

http://www.crazyminnowstudio.com/posts/salsa-and-rt-voice-for-runtime-lipsynced-text-to-speech/


Package Contents
----------------
Crazy Minnow Studio/SALSA with RandomEyes/Addons/
	RT-Voice
		Salsa_RTVoice.cs
			Demonstration using the RT-Voice Speaker.Speak method.
		Salsa_RTVoice_Native.cs
			Demonstration using the RT-Voice Speaker.SpeakNative method and the OnSpeakNativeCurrentViseme event.
		Salsa_RTVoice_Native_iOS.cs (Optional - requires TextSync text-to-lipsync @ https://crazyminnowstudio.com/unity-3d/lip-sync-salsa/downloads/)
			Demonstration using the RT-Voice Speaker.SpeakNative method and the OnSpeakCurrentWord event. This is the only option on iOS and requires the free TextSync asset, also available at the download link above.
		ReadMe.txt
			This readme file.


Installation Instructions
-------------------------
1. Install SALSA with RandomEyes into your project.
	Select [Window] -> [Asset Store]
	Once the Asset Store window opens, select the download icon, and download and import [SALSA with RandomEyes].

2. Install RT-Voice into your project.
	Select [Window] -> [Asset Store]
	Once the Asset Store window opens, select the download icon, and download and import [RT-Voice] or [RT-Voice PRO].

2. Import the SALSA with RandomEyes RT-Voice support package (Salsa_RTVoice).
	Select [Assets] -> [Import Package] -> [Custom Package...]
	Browse to the [Salsa-RTVoice_1.0.0.unitypackage] file and [Open].


Usage Instructions
------------------
1. Setup a Salsa2D or Salsa3D enabled character.

2. Setup the RT-Voice components:
	a. Using Speaker.Speak
		1. Add the RT-Voice [Speaker] component.
		2. Add the Salsa_RTVoice component:
			[Component] -> [Crazy Minnow Studio] -> [SALSA] -> [Addons] -> [RT-Voice] -> [Salsa_RTVoice]
	b. Using Speaker.SpeakNative with the OnSpeakNativeCurrentViseme event.
		1. Add the RT-Voice [Speaker] component.
		2. Add the RT-Voice [Live Speaker] component.
		3. Add the Salsa_RTVoice_Native component:
			[Component] -> [Crazy Minnow Studio] -> [SALSA] -> [Addons] -> [RT-Voice] -> [Salsa_RTVoice_Native]
	c. Using Speaker.SpeakNative with the OnSpeakCurrentWord event for use on iOS.
		1. Add the RT-Voice [Speaker] component.
		2. Add the RT-Voice [Live Speaker] component.
		3. Add the Salsa_RTVoice_Native_iOS component:
			[Component] -> [Crazy Minnow Studio] -> [SALSA] -> [Addons] -> [RT-Voice] -> [Salsa_RTVoice_Native_iOS]
		4. Add the TextSync component: (TextSync text-to-lipsync @ https://crazyminnowstudio.com/unity-3d/lip-sync-salsa/downloads/)
			[Component] -> [Crazy Minnow Studio] -> [SALSA] -> [Addons] -> [TextSync] -> [CM_TextSync]
		5. Set the TextSync [Words Per Minute] to 300.
3. Play the scene and press the [Speak] check box on either the [Salsa_RTVoice] or [Salsa_RTVoice_Native] component.