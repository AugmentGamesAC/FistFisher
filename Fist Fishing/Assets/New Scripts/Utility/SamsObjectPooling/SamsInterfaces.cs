using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


interface Iitem
{
    string Name { get; set; }
    int GoldValue { get; set; }


    void Equip();
    void Sell();
}

interface IDamagingItem : Iitem
{
    int Damage { get; }
}


public delegate void LowDur();

//no point to making this T, but it's fun.
interface IDamagable<T>
{
    event LowDur OnLowDur;

    T Durability { get; set; }

    void TakeDamage(T damageAmount);

}

class Sword : IDamagingItem, IDamagable<int>
{
    public string Name { get; set; }
    public int GoldValue { get; set; }
    public int Durability { get; set; }
    public int Damage { get; }

   

    public Sword(string name, int goldValue, int durability, int damage)
    {
        Name = name;
        GoldValue = goldValue;
        Durability = durability;
        Damage = damage;
    }

    public event LowDur OnLowDur;

    public void Equip()
    {
        Debug.Log(Name + "equipped!");
    }

    public void Sell()
    {
        Debug.Log(Name + " Sold for " + GoldValue + " Rupees!");
    }

    public void TakeDamage(int damageAmount)
    {
        Durability -= damageAmount;
        if (Durability < 0)
            OnLowDur.Invoke();
    }
}

class WoodBox : IDamagable<int>
{
    public int Durability { get; set; }

    public event LowDur OnLowDur;

    public void TakeDamage(int damageAmount)
    {
        Durability -= damageAmount;
        if (Durability < 0)
            OnLowDur.Invoke();
    }
}