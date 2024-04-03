using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffable : Buffable
{

    [Header("Player Buffable")]
    [SerializeField] PlayerController m_player;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        AddAtk(5);
    }

    void AddJump(float newJSpeed)
    {
        m_player.SetJumpSpeed(newJSpeed);
    }

    void AddSpeed(float newSpeed)
    {
        m_player.SetBaseSpeed(newSpeed);
    }

}
