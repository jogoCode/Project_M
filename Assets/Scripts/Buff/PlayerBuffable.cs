using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffable : Buffable
{

    [Header("Player Buffable")]
    new PlayerController m_buffTarget;

    void AddJump(float newJSpeed)
    {
        m_buffTarget.SetJumpSpeed(newJSpeed);
    }

    void AddSpeed(float newSpeed)
    {
        m_buffTarget.SetBaseSpeed(newSpeed);
    }

}
