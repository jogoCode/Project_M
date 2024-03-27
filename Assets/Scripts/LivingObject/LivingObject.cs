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

    public static Action<float,float> IsDying;

    public void Start()
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


    public void Attack()
    {
       
    }

    public void Die(LivingObject killer)
    {
        if (killer.GetComponent<PlayerController>()) // TODO <-- A mettre dans la Class Enemy
        {
            var player = killer.GetComponent<PlayerController>();
            player.m_LevelSystem.AddExp(5);
            IsDying.Invoke(player.m_LevelSystem.GetExp(), player.m_LevelSystem.GetMaxExp());
        }
       if (m_hp <= 0)
       {
            Destroy(gameObject);
       }
    }

    public void Hit()
    {
      
    }

    public void SetHp(int hp)
    {
        m_hp += hp;
    }


    virtual protected void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == this.gameObject.layer) return; // Verifie le layer des deux entité

        SetHp((int)-other.GetComponentInParent<LivingObject>().m_weapon.GetDamage()); // Change les HP en fonction des dégats de l'arme
        if (m_hp <= 0)
        {
            Die(other.GetComponentInParent<PlayerController>());
        }
        if (other.gameObject.layer == 1 << 7) return;  // Verifie Si c'est un joueur ou non pour appliquer les feedsBck
        // CAMERA SHAKE ET FREEZE
        // TODO A METTRE DANS LE SCRIPTS ENEMY
        Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(5f, 0.5f,true,false));
        Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Freeze(0.1f, 0.008f));

        // PARTICLE
        Instantiate(m_hitFx, new Vector3(transform.position.x, other.transform.position.y, transform.position.z), quaternion.identity);    
    }
}
