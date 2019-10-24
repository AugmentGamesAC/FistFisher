using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class InspectorDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {


        TKey tempKey = keys.LastOrDefault();
        TValue tempValue = values.LastOrDefault();
        bool keysLonger = keys.Count > values.Count;
        bool valuesLonger = keys.Count < values.Count;
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
        if (keysLonger)
            keys.Add(tempKey);
        if (valuesLonger)
            values.Add(tempValue);
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        //if (keys.Count != values.Count)
        //    throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable.", keys.Count, values.Count));

        int matchedCount = Mathf.Min(keys.Count, values.Count);


        for (int i = 0; i < matchedCount; i++)
            this.Add(keys[i], values[i]);
    }
}
