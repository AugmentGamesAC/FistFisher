using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuePool<T> : IPool<T>
{
    Func<T> produce;
    int capacity;
    T[] objects;
    int index;

    public QueuePool(Func<T> factoryMethod, int maxSize)
    {
        produce = factoryMethod;
        capacity = maxSize;
        index = -1;
        objects = new T[maxSize];
    }

    public T GetInstance()
    {
        index = (index + 1) % capacity;

        if (objects[index] == null)
            objects[index] = produce();

        return objects[index];
    }
}
