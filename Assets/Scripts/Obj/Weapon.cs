using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    /* Dans le player mettre une reference au scrpt weapon et quand le player rammasse une arme update son arme en main
     * 
     */


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
    [SerializeField] GameObject _prefabs;


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
    public GameObject Prefabs
    {
        get { return _prefabs; }
    }

    public void RangeOfWeapon()
    {
        if (_weaponType == TypeOfItem.MELEE)
        {
            _range = 1;
        }else
        {

        }
    }

    public void SpecialEffect()
    {
        if(_effect == ADDITIONALEFFECTS.POISON)
        {
            /* la target perd des hp toute les secondes
             * target.DecreaseHP(target.MaxHp/8);
             */
        }

        if (_effect == ADDITIONALEFFECTS.FREEZE)
        {
            /* la target est dans un bloc de glace et la prochaine atk fais obligatoirement des d�gats per�ants
             * 
             */
        }
        if (_effect == ADDITIONALEFFECTS.PARALYSE)
        {
            /* la target est plus lent 
             * 
             */
        }
        if (_effect == ADDITIONALEFFECTS.BURN)
        {
            /* la target est perd des d�gats d'atk si il a une arme de m�l�e 
             * 
             */
        }
        if (_effect == ADDITIONALEFFECTS.SLEEP)
        {
            /* la target dort (elle fait rien jusqu'a quelle soit attaqu� ou se reveil apr�s un certain temps)
             * 
             */
        }
        if (_effect == ADDITIONALEFFECTS.STUN)
        {
            /* la target est au sol et joueur peut lui fait un coup critique a sa prochaine atk
             * 
             */
        }


    }
        
}
