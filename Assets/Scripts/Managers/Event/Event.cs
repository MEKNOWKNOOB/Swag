using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Event : MonoBehaviour
{
    // Name of the event
    [SerializeField] private string eventName;
    // Time when the event was last triggered
    private float timeSinceLastTrigger;
    private bool canTrigger;

    void Awake()
    {
        canTrigger = true;
    }

    public void DisplayEventName(TextMeshPro eventTextfield)
    {
        eventTextfield.text = eventName;
    }

    public virtual void TriggerEvent()
    {
        if(canTrigger){
            Debug.Log("Event: " + eventName + " was triggered.");
            timeSinceLastTrigger = Time.time;
            canTrigger = false;
        }
    }
}
