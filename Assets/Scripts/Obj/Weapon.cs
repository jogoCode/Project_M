using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    /* Dans le player mettre une reference au scrpt weapon et quand le player rammasse une arme update son arme en main
     * 
     */

    //-----------------------------------ENUM---------------------
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


    // test //
    private float enemyHealth = 100f; 
    private float damageInterval = 3f; 
    private float damageAmount = 0.125f;

    //-----------------------------------GET---------------------
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

 //-----------------------------------FONCTIONS---------------------
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
        if(_effect == ADDITIONALEFFECTS.NONE)
        {
        }
        if(_effect == ADDITIONALEFFECTS.POISON)
        {
            /* la target perd des hp toute les secondes
             * target.DecreaseHP(target.MaxHp/8);
             */
            //StartCoroutine(InflictDamage());
        }

        if (_effect == ADDITIONALEFFECTS.FREEZE)
        {
            /* la target est dans un bloc de glace et la prochaine atk fais obligatoirement des dégats perçants
             * 
             */
            //StartCoroutine(StunEffect());
        }
        if (_effect == ADDITIONALEFFECTS.PARALYSE)
        {
            /* la target est plus lent 
             * 
             */
            //StartCoroutine(StunEffect());
        }
        if (_effect == ADDITIONALEFFECTS.BURN)
        {
            /* la target est perd des dégats d'atk si il a une arme de mélée 
             * effet de flamme
             * damageAmount = 0.25;
             * StartCoroutine(InflictDamage());
             */
        }
        if (_effect == ADDITIONALEFFECTS.SLEEP)
        {
            /* la target dort (elle fait rien jusqu'a quelle soit attaqué ou se reveil après un certain temps)
             * 
             */
            //StartCoroutine(StunEffect());
        }
        if (_effect == ADDITIONALEFFECTS.STUN)
        {
            /* la target est au sol et joueur peut lui fait un coup critique a sa prochaine atk
             * 
             */
            //StartCoroutine(StunEffect());

        }


    }

    private void ApplyDamage()
    {
        // Calculer les dégâts en fonction de la fraction spécifiée
        float damage = enemyHealth * damageAmount;

        // Retirer les points de vie
        enemyHealth -= damage;

        // Vérifier si l'ennemi est toujours en vie
        if (enemyHealth <= 0f)
        {
            EnemyDefeated();
        }
    }
    private void EnemyDefeated()
    {
        Debug.Log("L'ennemi a été vaincu !");
        //anim de mort
    }

    //-----------------------------------ENUMERATOR---------------------
    private IEnumerator InflictDamage()
    {  
            while (true)
            {
                yield return new WaitForSeconds(damageInterval);
                ApplyDamage();
            }
    }
    private IEnumerator StunEffect()
    {
        int stunchance = 30;
        int random = UnityEngine.Random.Range(0, 100);
        /*
         * a une chance d'empecher de bouger
         *  mettre la speed a 0 ?
         *  enemy.speed = 0;
        */
        if (random <= stunchance)
        {
            //enemySpeed = 0;
            yield return new WaitForSeconds(2f);
        }
        //retour de la speed normal de l'enemy
        
        
    }

}
