using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickup")]
public class Pickup : ScriptableObject
{
    public string pickupName;

    public string ShowName()
    {
        return pickupName;
    }
}
