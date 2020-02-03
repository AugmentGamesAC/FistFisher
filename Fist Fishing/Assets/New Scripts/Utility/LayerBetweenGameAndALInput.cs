using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerBetweenGameAndALInput : MonoBehaviour
{

    /// <summary>
    /// make this a singleton
    /// </summary>


    //this allows us to know the current context to deal with the keys
    protected ActionDefinition.ContextGroup m_worldCurrentContext;
    public ActionDefinition.ContextGroup CurrentWorldContext => m_worldCurrentContext;
    public void SetCurrentWorldContext(ActionDefinition.ContextGroup context) { m_worldCurrentContext = context; }


    ///revising this - only return floats?
    /// <summary> 
    /// buttons return bool
    /// 1axis returns float
    /// 2axis returns vec2
    /// 3axis/pageselect returns vec3
    /// </summary>




    //when player asks for input, this takes the actionID or ActionDefinition, checks MagicConfig to see what button/axis is associated with it, returns if the axis or button is indeed pressed

    //how do we differentiate between the different types of value returns?
    //functions for 



    //player gives input, returns a float
    //this requires axis to be asked for one at a time
    //fuctions then should only care if it is a button or axis, and we don't care about num of axis - just need to sensure they are handled where necessary 




    public float KeyOrAxisInput(ActionDefinition action)
    {
        //get the info to determine input, input type, and context
        ActionDefinition.ActionID aid = action.InternalID;
        ActionDefinition.ContextGroup context = action.ContextGroups;
        ActionDefinition.ActionType actionType = action.InputActionType;



        return 0;
    }





















    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
