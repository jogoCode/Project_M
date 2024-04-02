using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagable : MonoBehaviour
{
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
