using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Menu : MonoBehaviour
{
    /// <summary>
    /// Set visibility of this
    /// may be more logic in future.
    /// </summary>
    public void Show(bool activeState)
    {
        gameObject.SetActive(activeState);
        var something = GetComponentsInChildren<DisapearingMenu>().Select(x => { x.Show(false); return 1; });
    }
    
}
