using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagable : MonoBehaviour
{

    [SerializeField] LivingObject m_owner;
    public enum States
    {
        IDLE,
        MOVE,
        ATTACK,
        HIT,
        DIE
    }

    [SerializeField] States m_actualState;
    [SerializeField] States m_lastState;
    private void Start()
    {
        m_owner = GetComponent<LivingObject>();
    }

    public States GetState()
    {
        return m_actualState;
    }

    public void SetState(States newState)
    {
        m_lastState = m_actualState;
        m_actualState = newState;
    }
}
