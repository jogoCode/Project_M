using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffable : Buffable
{

    [Header("Player Buffable")]
    [SerializeField] PlayerController m_player;

    void Start()
    {
        m_player = GetComponent<PlayerController>();
        AddJump(15);
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
