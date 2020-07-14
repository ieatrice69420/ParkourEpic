using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon")]
public class PickUpableSO : ScriptableObject
{
    public int pickUpIndex;
    public int ammo;
    public new string name;
}