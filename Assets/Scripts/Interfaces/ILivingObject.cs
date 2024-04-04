using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILivingObject
{

    void Attack();

    void Hit();

    void Die(LivingObject killer);

    void SetHp(int hp);

}
