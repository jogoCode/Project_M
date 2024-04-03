using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILivingObject
{

    void Attack();

    public void Hit();

    void Die(LivingObject killer);

    void SetHp(int hp);

    public int GetArmor();

    public int GetMaxhp();

    public int GetHp();

    public WeaponManager GetWeapon();

}
