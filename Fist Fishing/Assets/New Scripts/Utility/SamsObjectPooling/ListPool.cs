using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//where T: class ensures we pass in a nullable type.
public class ListPool<T> : IPool<T> 
{
    Func<T> produce;
    int capacity;
    List<T> objects;
    Func<T, bool> useTest;
    bool expandable;

    public ListPool(Func<T> factoryMethod, int maxSize, Func<T, bool> inUse, bool expandable = true)
    {
        produce = factoryMethod;
        capacity = maxSize;
        objects = new List<T>(maxSize);
        useTest = inUse;
        this.expandable = expandable;
    }

    public T GetInstance()
    {
        var count = objects.Count;

        foreach (var item in objects)
        {
            if (!useTest(item))
                return item;
        }

        if (count >= capacity && !expandable)
        {
            return default;
        }
        var obj = produce();
        objects.Add(obj);
        return obj;
    }
}
