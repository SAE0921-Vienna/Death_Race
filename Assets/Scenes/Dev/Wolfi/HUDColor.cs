using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HUDColor_", menuName = "Scriptables/HUDColor")]
public class HUDColor : ScriptableObject
{
    public new string name;
    public int SkinID;
    [Range(0,255)]
    public float R;
    [Range(0, 255)]
    public float G;
    [Range(0, 255)]
    public float B;
    [Range(0, 1)]
    public float A;
    
}
