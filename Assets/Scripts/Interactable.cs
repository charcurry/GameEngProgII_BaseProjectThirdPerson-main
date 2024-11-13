using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool wasInteracted = false;

    [SerializeField] private Pickup pickup;

    public enum InteractionType
    {
        Door,
        Button,
        Pickup
    }

    public InteractionType type;

    public void Activate()
    {
        if (type == InteractionType.Pickup)
        {
            Debug.Log(pickup.ShowName() + " was activated");
        }
        else
        {
            Debug.Log(name + " was activated");
        }
        wasInteracted = true;

    }
}
