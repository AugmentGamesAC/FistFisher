using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KeyCodeOrDirectionCode
{
    public KeyCode key = KeyCode.None;
    public ALInput.DirectionCode direction = ALInput.DirectionCode.Unset;
}


/// <summary>
/// this class is a list of all the inputs as action definitions and their associated buttons
/// </summary>
public class KeyConfiguration //: MonoBehaviour
{

    public string m_ConfigName = "Default Config Name";


    //how do we get this if keycodes and axis codes are different? - requires another layer or different functions?
    public Dictionary<ActionDefinition, KeyCodeOrDirectionCode> m_allTheInputs = new Dictionary<ActionDefinition, KeyCodeOrDirectionCode>(); 


    //function here to get the key associated with the action def given the context
    //this is called and returned to LayerBetweenGameAndALInput (needs to be renamed) so that it can then call ALInput to get the button down


    //stores a list of all actionIDs and the keys/axis associated with them


    protected KeyCodeOrDirectionCode GetKeyOrAxis(ActionDefinition AD)
    {
        KeyCodeOrDirectionCode code;
        m_allTheInputs.TryGetValue(AD, out code);
        return code;
    }

    protected ActionDefinition GetActionDefinitionFromDictionary(ActionID AD)
    {
        foreach(var a in m_allTheInputs)
        {
            if (a.Key.InternalID == AD)
                return a.Key;
        }


        return null;
    }


    public bool IsThisPressed(ActionID actionID)
    {

        ActionDefinition AD = GetActionDefinitionFromDictionary(actionID);
        if (AD == null)
            return false;

        KeyCodeOrDirectionCode code = GetKeyOrAxis(AD);

        if(code.key != KeyCode.None)
        {
            return ALInput.GetKeyDown(code.key);
        }


        if (code.direction != ALInput.DirectionCode.Unset)
        {
            Vector3 v = ALInput.GetDirection(code.direction);
            if (v != Vector3.zero)
                return true;
        }


        return false;
    }




}
