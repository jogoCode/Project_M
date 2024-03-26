using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]

// Classe abstraitre attrapable implémante notre interface
public abstract class Pickable : MonoBehaviour, IPickable
{
    //protected float m_poids;
    public void Pick()
    {
        
    }
    public static Action OnPickedUp;


    void Start()
    {
        OnPickedUp?.Invoke();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Debug.Log("j'ai attrapé quelque chose");
            Destroy(gameObject);
            // ajouter l'item dans la main du joueur (inventaire)
        }
    }
}
