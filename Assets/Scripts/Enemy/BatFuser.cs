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
        else if (enemy2 == null && enemy != enemy1)
        {
            enemy2 = enemy;
        }

        if (areBothOccupied())
            FuseTheBats();

    }

    private void FuseTheBats()
    {
        Vector3 position = enemy1.gameObject.transform.position;


        GameEventHandler.Instance.DestroyObject(enemy1.gameObject);
        GameEventHandler.Instance.DestroyObject(enemy2.gameObject);
        GameEventHandler.Instance.FuseTwoBats(position);

        enemy1 = null;
        enemy2 = null;
    }

    private bool areBothOccupied()
    {
        return enemy1 != null && enemy2 != null;
    }
}
