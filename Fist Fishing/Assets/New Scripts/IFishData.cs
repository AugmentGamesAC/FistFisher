using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IFishData 
{
    //data needed for combat
    //Health
    //UI image
    //Behaviour type.

    HealthModule HealthModule { get; set; }

    Sprite Sprite { get; }


}
