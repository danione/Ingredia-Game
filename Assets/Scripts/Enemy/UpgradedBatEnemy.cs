using System.Collections;
using UnityEngine;

public class UpgradedBatEnemy : BatEnemy
{
    [SerializeField] private float rapidShootRateCooldown = 2f;
    [SerializeField] private float fusingStateAttackDuration  = 2f;

    protected override void Shoot()
    {
        if(_state.CurrentState == _state.FusionAttackState)
        {
            spawner._pool.Get().transform.position = gameObject.transform.position;
            StartCoroutine(RapidShootCooldown());
        }
        else if(_state.CurrentState != _state.FusionAttackState)
        {
            base.Shoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided && other.CompareTag("Enemy"))
        {
            hasCollided = true;
            UpgradedBatEnemy uBat = other.GetComponent<UpgradedBatEnemy>();

            if (uBat != null)
            {
                Debug.Log("Other uBat");
                
            }
            else
            {
                BatEnemy bat = other.GetComponent<BatEnemy>();
                
                if (bat == null) return;
                
                Debug.Log("Bat fused");
                bat.DestroyEnemy();

                FusingState();
            }

            hasCollided = false;
        }
    }

    IEnumerator RapidShootCooldown()
    {
        yield return new WaitForSeconds(rapidShootRateCooldown);
        attacked = false;
    }


    public void FusingState()
    {
        if (_state.CurrentState != _state.FusionAttackState)
        {
            _state.TransitiontTo(_state.FusionAttackState);
            StartCoroutine(TransitionBackToNormalState());
        }
    }

    IEnumerator TransitionBackToNormalState()
    {
        if (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(fusingStateAttackDuration);
            _state.TransitiontTo(_state.MoveState);
            attacked = false;
        }
    }
}
