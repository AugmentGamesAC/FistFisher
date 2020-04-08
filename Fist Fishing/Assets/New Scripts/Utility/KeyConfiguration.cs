using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is a list of all the inputs as action definitions and their associated buttons
/// obsolete as controls have undergone yet more changes
/// </summary>
public class KeyConfiguration : MonoBehaviour
{

    protected string m_ConfigName = "Default Config Name";


    //how do we get this if keycodes and axis codes are different? - requires another layer or different functions?
    protected Dictionary<ActionDefinition, KeyCode> m_allTheInputs; 


    //function here to get the key associated with the action def given the context
    //this is called and returned to LayerBetweenGameAndALInput (needs to be renamed) so that it can then call ALInput to get the button down


    //stores a list of all actionIDs and the keys/axis associated with them



}
