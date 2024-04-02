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
    [SerializeField] protected int m_maxhp;
    [SerializeField] protected int m_armor;

    [SerializeField] protected ParticleSystem m_hitFx;
    [SerializeField] protected WeaponManager m_weapon;

    public static Action<float,float> IsDying; //TODO METTRE DANS LE ENNEMY


    protected virtual void Start()
    {
        IsDying?.Invoke(0,0);
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
        return m_maxhp;    
    }
    public int GetHp()
    {
        return m_hp;
    }
    public WeaponManager GetWeapon()
    {
        return m_weapon;
    }

    public void Attack()
    {
       
    }

    public void Die(LivingObject killer)
    {


    }

    public void Hit()
    {
      
    }

    public void SetHp(int hp)
    {
        m_hp += hp;
        m_hp = Mathf.Clamp(m_hp,0,m_maxhp);
    }


    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<LivingObject>())
        {
            if (other.gameObject.layer == this.gameObject.layer || other.GetComponentInParent<WeaponManager>().GetWeaponData() == null) return; // Verifie le layer des deux entité
            if (other.GetComponent<PlayerController>())
            {
                var player = other.GetComponent<PlayerController>();
                if (player.GetActualState() != PlayerController.States.ATTACK) return;
            }
            int damage = -other.GetComponentInParent<WeaponManager>().GetWeaponData().Damage;
            SetHp(damage); // Change les HP en fonction des dégats de l'arme

            // PARTICLE
            Instantiate(m_hitFx, new Vector3(transform.position.x, other.transform.position.y, transform.position.z), quaternion.identity);
        }
    }
}
