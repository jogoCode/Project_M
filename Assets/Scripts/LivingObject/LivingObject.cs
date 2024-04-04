using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LivingObject : MonoBehaviour , ILivingObject
{

    [Header("LivingObject")]
    //Life
    [SerializeField] protected int m_hp;
    [SerializeField] protected int m_maxHp;
    [SerializeField] protected int m_armor;

    [SerializeField] protected ParticleSystem m_hitFx;
    [SerializeField] protected WeaponManager m_weapon;

    public static Action<float,float> IsDying; //TODO METTRE DANS LE ENNEMY
    public Action<float, float> LifeChanged;

    int m_dmgBuff = 0;
    protected virtual void Start()
    {
        IsDying?.Invoke(0,1);
        LifeChanged?.Invoke(m_hp,m_maxHp);
        if (!m_weapon)
        {
            m_weapon = GetComponent<WeaponManager>();
            if (!m_weapon)
            {
                Debug.LogError("Il manque le script WeaponManager sur "+this.gameObject.name );
            }
        }
    }
    public int GetArmor()
    {
        return m_armor;
    }
    public int GetMaxhp() 
    {
        return m_maxHp;    
    }
    public int GetHp()
    {
        return m_hp;
    }
    public WeaponManager GetWeapon()
    {
        return m_weapon;
    }

    public int GetBuffDamage()
    {
        return m_dmgBuff;
    }
    public void Attack()
    {
       
    }

    public void Die(LivingObject killer)
    {


    }

    public virtual void Hit()
    {
        LifeChanged?.Invoke(m_hp,m_maxHp);
        Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(5f, 0.5f, true, false));
        Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Freeze(0.08f, 0.008f, false));
    }

    public void SetHp(int hp)
    {
        m_hp += hp;
        m_hp = Mathf.Clamp(m_hp,0,m_maxHp);
        LifeChanged?.Invoke(m_hp, m_maxHp);
    }

    public void SetMaxHp(int newMhp)
    {
       
        if(m_hp == m_maxHp)
        {
            m_maxHp += newMhp;
            m_hp = m_maxHp;
        }
        else
        {
            m_maxHp += newMhp;
        }
        LifeChanged?.Invoke(m_hp, m_maxHp);
    }

    public void SetDmgBuff(int dmg){
        m_dmgBuff += dmg;
    }


    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<LivingObject>())
        {
            if (other.gameObject.layer == this.gameObject.layer || other.GetComponentInParent<WeaponManager>().GetWeaponData() == null) return; // Verifie le layer des deux entité
            int damage = -(other.GetComponentInParent<WeaponManager>().GetWeaponData().Damage + other.GetComponentInParent<LivingObject>().GetBuffDamage());
            //DAMAGES BY ARMOR
            var playerinparent = other.GetComponentInParent<PlayerController>();
            if (playerinparent)
            {
                if (m_armor >= playerinparent.GetArmor())
                {
                    if (playerinparent)
                    {
                        SetHp(damage); // Change les HP en fonction des dégats de l'arme
                    }
                }
                else
                {
                    SetHp(damage / 2);
                }
            }
            else
            {
            }
                SetHp(damage);   // Change les HP en fonction des dégats de l'arme
            Hit();
            // PARTICLE
            Instantiate(m_hitFx, new Vector3(transform.position.x, other.transform.position.y, transform.position.z), quaternion.identity);
        }
    }
}
