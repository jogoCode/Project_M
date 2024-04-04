using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelable
{
    int GetLevel();

    void AddExp(float amount);

    public float GetExpForLvl(int lvl);

    public void SetMaxExp();

    public void AddLvl();

    public float GetExp();

    public float GetMaxExp();


}
