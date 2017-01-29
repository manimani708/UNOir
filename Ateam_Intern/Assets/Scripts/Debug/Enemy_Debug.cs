using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Enemy_Debug 
{
    public static void BossSpecialAttack()
    {
        var enemies = GameMainUpperManager.instance.enemyList;

        foreach(var elem in enemies)
        {
            elem.SpecialAttack();
        }

    }
}
