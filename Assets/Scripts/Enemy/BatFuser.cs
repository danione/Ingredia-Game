using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFuser : MonoBehaviour
{
    private BatEnemy enemy1;
    private BatEnemy enemy2;

    public void Fuse(BatEnemy enemy)
    {
        if (areBothOccupied()) return;

        if (enemy1 == null)
        {
            enemy1 = enemy;
        }
        else if (enemy2 == null)
        {
            enemy2 = enemy;
        }

        if (areBothOccupied())
            FuseTheBats();

    }

    private void FuseTheBats()
    {
        Vector3 position = enemy1.gameObject.transform.position;

        enemy1.DestroyEnemy();
        enemy2.DestroyEnemy();

        GameEventHandler.Instance.FuseTwoBats(position);

        enemy1 = null;
        enemy2 = null;
    }

    private bool areBothOccupied()
    {
        return enemy1 != null && enemy2 != null;
    }
}
