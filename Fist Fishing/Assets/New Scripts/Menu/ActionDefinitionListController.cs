using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionDefinitionListController : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> m_actionDefinitionPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        foreach (GameObject AD in m_actionDefinitionPrefabs)
        {
            GameObject g = Instantiate(AD) as GameObject;
            ActionDefinition def = g.GetComponent<ActionDefinition>();
            if (def == default)
            {
                Debug.LogError("failed to instatniate " + g.name);
            }
            else
            {
                def.m_humanReadableID += count.ToString();
                g.GetComponentInChildren<Text>().text = def.m_humanReadableID;

                g.SetActive(true);
                g.transform.SetParent(gameObject.transform, false);
            }
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
