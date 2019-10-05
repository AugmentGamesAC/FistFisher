using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotMagic : MonoBehaviour
{
    /// <summary>
    /// This is used to decide which elemental effect is displayed.
    /// </summary>
    [System.Flags]
    public enum Elements
    {
        Fire = 0x0001, //Damage Over Time  
        Ice = 0x0002, //Takes up space 
        Lightning = 0x0004, //Instant Damage
        Aiming = 0x0008, //used for aiming
    };

}
