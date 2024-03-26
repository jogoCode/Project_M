using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject, IPickable
{
    public enum ADDITIONALEFFECTS
    {
        NONE,
        POISON,
        FREEZE,
        PARALYSE,
        BURN,
        SLEEP,
        STUN
    }

    public enum TypeOfItem
    {
        MELEE,
        RANGE
    }

    public string _name;
    [SerializeField] int _damage;
    [SerializeField] int _atkSpeed;
    [SerializeField] int _armorBreak;
    [SerializeField] float _range;
    [SerializeField] ADDITIONALEFFECTS _effect;
    [SerializeField] float _knockBack;
    [SerializeField] TypeOfItem _weaponType;


    public string Name
    {
        get { return _name; }
    }
    public int Damage
    {
        get { return _damage; }
    }

    public int AtkSpeed
    {
        get { return _atkSpeed; }
    }
    public float Range
    {
        get { return _range; }
    }
    public int ArmorBreak
    {
        get { return _armorBreak; }
    }
    public  ADDITIONALEFFECTS Effect
    {
        get { return _effect; }
    }

    public float KnockBack
    {
        get { return _knockBack; }
    }
    public TypeOfItem TypeItem 
    { 
        get { return _weaponType; }
    }

    public void Pick()
    {
        Debug.Log("j'ai attraper une " + _name);
        
    }
}
