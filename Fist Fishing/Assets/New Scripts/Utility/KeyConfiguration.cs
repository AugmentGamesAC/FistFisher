using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is a list of all the inputs as action definitions and their associated buttons
/// </summary>
public class KeyConfiguration : MonoBehaviour
{
    //or dictionary?
    protected List<ActionDefinition> m_allTheInputs;


    //function here to get the key associated with the action def given the context
    //this is called and returned to LayerBetweenGameAndALInput (needs to be renamed) so that it can then call ALInput to get the button down

    //how do we get this if keycodes and axis codes are different? - requires another layer or different functions?





}
