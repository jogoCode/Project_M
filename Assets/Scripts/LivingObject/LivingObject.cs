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
    public Action<float, float> LifeChanged;

    protected virtual void Start()
    {
        IsDying?.Invoke(0,1);
        LifeChanged?.Invoke(m_hp,m_maxhp);
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

    public virtual void Hit()
    {
        LifeChanged?.Invoke(m_hp,m_maxhp);
        Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(5f, 0.5f, true, false));
        Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Freeze(0.08f, 0.008f, false));
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
            int damage = -other.GetComponentInParent<WeaponManager>().GetWeaponData().Damage;
            SetHp(damage); // Change les HP en fonction des dégats de l'arme
            Hit();
            // PARTICLE
            Instantiate(m_hitFx, new Vector3(transform.position.x, other.transform.position.y, transform.position.z), quaternion.identity);
        }
    }
}
