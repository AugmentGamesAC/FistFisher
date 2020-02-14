using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this class exists to instantiate a list of prefabs into a scrollable list
/// </summary>
public class ActionDefinitionListController : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> m_actionDefinitionPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        foreach (GameObject AD in m_actionDefinitionPrefabs) //loop through list
        {
            GameObject g = Instantiate(AD) as GameObject;
            ActionDefinition def = g.GetComponent<ActionDefinition>();
            if (def == default)
            {
                Debug.LogError("failed to instatniate " + g.name);
            }
            else
            {
                //g.GetComponentInChildren<Text>().text = def.m_humanReadableID; //may need something like this in the future unless all text is set up in prefab (which is preferable)

                g.SetActive(true);
                g.transform.SetParent(gameObject.transform, false);
            }
            count++;
        }
    }
}
