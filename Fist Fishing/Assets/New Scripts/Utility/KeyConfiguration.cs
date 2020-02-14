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


    private static Dictionary<ALInput.AxisCode, System.Func<float>> my_Axis = new Dictionary<ALInput.AxisCode, System.Func<float>>()
    {
        { ALInput.AxisCode.FakeInOut1,()=>{return FakeAxis(KeyCode.W, KeyCode.S); } },
        { ALInput.AxisCode.FakeLeftRight1,()=>{return FakeAxis(KeyCode.A, KeyCode.D); } },
        { ALInput.AxisCode.FakeUpDown1,()=>{return FakeAxis(KeyCode.Q, KeyCode.E); } },

        { ALInput.AxisCode.FakeInOut2,()=>{return FakeAxis(KeyCode.P, KeyCode.Colon); } },
        { ALInput.AxisCode.FakeLeftRight2,()=>{return FakeAxis(KeyCode.L, KeyCode.Quote); } },
        { ALInput.AxisCode.FakeUpDown2,()=>{return FakeAxis(KeyCode.O, KeyCode.LeftCurlyBracket); } },
    };


    private static float FakeAxis(KeyCode positive, KeyCode neagitve)
    {
        return (ALInput.GetKey(positive) ? 1 : 0) - (ALInput.GetKey(neagitve) ? 1 : 0);

    }

    public static float GetAxis(ALInput.AxisCode key)
    {
      System.Func<float> fakeaxis;
        if (!my_Axis.TryGetValue(key, out fakeaxis))
            return 0;
            
        return fakeaxis();
    }





    public Vector3 AxisDirections(ActionID actionID)
    {
        ActionDefinition AD = GetActionDefinitionFromDictionary(actionID);
        if (AD == null)
            return Vector3.zero;

        KeyCodeOrDirectionCode code = GetKeyOrAxis(AD);

        if (code.direction == ALInput.DirectionCode.Unset)
            return Vector3.zero;

        return ALInput.GetDirection(code.direction);
    }

}
