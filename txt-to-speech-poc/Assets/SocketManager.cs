// SocketManager
// Connects to Socket.IO then dispatches recieved text via the Speak Action Delegate Event.

using UnityEngine;
using UnitySocketIO;
using UnitySocketIO.Events;
using Newtonsoft.Json;

public class SocketManager : MonoBehaviour
{

    // String to manage data.
    private string ChatInput = "";

    // Socket IO plugin component controller.
    public SocketIOController io;

    // Dispatch definitions for Delegate Event and Dispatched data.
    public delegate void SpeakAction(string str);
    public static event SpeakAction DispatchSpeakEvent;

    // Lock allows application to function within its single thread (remain syncronous)
    // If there is a new message recieved the chat log String length will be greater than 1.
    // We then dispatch the data and clear ChatInput ready for the next socket event. 
    void Update()
    {
        lock (ChatInput)
        {
            if (ChatInput.Length > 0)
            {
                if (DispatchSpeakEvent != null)
                    DispatchSpeakEvent(ChatInput);

                ChatInput = "";

            }
        }
    }

    void Start()
    {

        io.On("connect", (SocketIOEvent e) =>
        {
            Debug.Log("SocketIO connected");
            lock (ChatInput)
            {
                ChatInput = "Ready";
            }
        });

        io.Connect();

        // Subscibes to msg socket channel and updates the ChatInput string.
        io.On("msg", (SocketIOEvent e) =>
        {
            Debug.Log("Msg Received");
            string str = e.data.ToString();

            lock (ChatInput)
            {
                ChatInput = str;
            }
        });

    }

}
