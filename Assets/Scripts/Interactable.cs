using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool wasInteracted = false;

    public enum InteractionType
    {
        Door,
        Button,
        Pickup
    }

    public InteractionType type;

    public void Activate()
    {
        wasInteracted = true;
        Debug.Log(name + " was activated");
    }
}
