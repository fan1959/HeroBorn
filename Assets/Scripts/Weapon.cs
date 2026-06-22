using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Weapon
{
    public string name;
    public int damage;
    public Weapon(string name, int damage)
    {
        this.name = name;
        this.damage = damage;
    }
    public void PrintWeaponStats()
    {
        Debug.LogFormat($"Weapon:{this.name}-{this.damage}DMG");
    }
}
public class WeaponShop {
    public List<Weapon> inventory;
}

