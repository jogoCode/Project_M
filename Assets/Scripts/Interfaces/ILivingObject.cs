using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILivingObject
{

    void Attack();

    void Hit();

    void Die();

    void SetHp(int hp);

}
