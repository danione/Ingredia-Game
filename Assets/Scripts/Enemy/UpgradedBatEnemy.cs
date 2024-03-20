using System.Collections;
using UnityEngine;

public class UpgradedBatEnemy : BatEnemy
{
    [SerializeField] private float rapidShootRateCooldown = 2f;
    [SerializeField] private float fusingStateAttackDuration  = 2f;
    [SerializeField] private float revulsionCooldown = 2f;
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

    private void OnTriggerEnter(Collider other)
    {
        BatEnemy bat = other.GetComponent<BatEnemy>();
        if (bat != null && !hasCollided  && !isRevulted)
        {
            hasCollided = true;
            UpgradedBatEnemy uBat = other.GetComponent<UpgradedBatEnemy>();

            if (uBat != null && !uBat.hasCollided)
            {
                uBat.SelfRevultion(isGoingLeft: false);
                SelfRevultion(isGoingLeft: true);
                isRevulted = true;
            }
            else 
            {
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

    public void SelfRevultion(bool isGoingLeft)
    {
        _state.TransitiontTo(_state.RevultedState);
        StartCoroutine(Revulsion(isGoingLeft));
        StartCoroutine(RevertToNormalState());

    }

    IEnumerator RevertToNormalState()
    {
        yield return new WaitForSeconds(revulsionCooldown);
        _state.TransitiontTo(_state.MoveState);
        attacked = false;
        isRevulted = false;
    }

    IEnumerator Revulsion(bool isGoingLeft)
    {
        Vector3 direction = isGoingLeft ? Vector3.left : Vector3.right;
        while (_state.CurrentState == _state.RevultedState)
        {
            transform.Translate(enemyData.movementSpeed * 2 * Time.deltaTime * direction);
            yield return new WaitForEndOfFrame();
        }
    }
}
