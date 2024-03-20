using System.Collections;
using UnityEngine;

public class UpgradedBatEnemy : BatEnemy
{
    [SerializeField] private float rapidShootRateCooldown = 2f;
    [SerializeField] private float fusingStateAttackDuration  = 2f;
    [SerializeField] private float revulsionCooldown = 2f;
    private Vector3 direction = Vector3.zero;
    private bool isRevulted = false;

    public override void Shoot()
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

    public void ChangeDirection(Vector3 direction)
    {
        this.direction = direction;
        isRevulted = true;
        SelfRevultion();
    }

    public Vector3 GetDirection()
    {
        return this.direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        BatEnemy bat = other.GetComponent<BatEnemy>();
        if (bat != null && !hasCollided  && !isRevulted)
        {
            hasCollided = true;
            if (bat.isUpgraded)
            {

                UpgradedBatEnemy uBat = other.GetComponent<UpgradedBatEnemy>();
                
                if (!uBat.isRevulted) uBat.ChangeDirection(Vector3.left);

                ChangeDirection(Vector3.right);
            }
            else 
            {
                GameEventHandler.Instance.DestroyObject(bat.gameObject);
                FusingState();
            }

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
            StartCoroutine(TransitionBackToNormalState(fusingStateAttackDuration));
        }
    }

    IEnumerator TransitionBackToNormalState(float secondsToWait)
    {
        if (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(secondsToWait);
            _state.TransitiontTo(_state.MoveState);
            attacked = false;
            hasCollided = false;
            isRevulted = false;
        }
    }

    public void SelfRevultion()
    {
        _state.TransitiontTo(_state.RevultedState);
        StartCoroutine(TransitionBackToNormalState(revulsionCooldown));
    }
}
