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
    [SerializeField] protected Weapon m_weapon;

    public static Action IsHit;

    protected virtual void Start()
    {
        IsHit?.Invoke();
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


    public void Attack()
    {
       
    }

    public void Die()
    {
       if (m_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        IsHit();
    }

    public void SetHp(int hp)
    {
        m_hp += hp;
        if (m_hp <= 0)
        {
            Die();
        } 
    }


    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == this.gameObject.layer) return; // Verifie le layer des deux entit�

        SetHp(-other.GetComponentInParent<LivingObject>().m_weapon.Damage); // Change les HP en fonction des d�gats de l'arme

        if (other.gameObject.layer == 1 << 7) return;  // Verifie Si c'est un joueur ou non pour appliquer les feedsBck
        // CAMERA SHAKE ET FREEZE
        // TODO A METTRE DANS LE SCRIPTS ENEMY
        Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(4f, 0.5f,true,false));
        Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Freeze(0.05f, 0.008f));

        // PARTICLE
        Instantiate(m_hitFx, transform.position, quaternion.identity);
        
       
    }
}
