using UnityEngine;

public class BansheeEnemy : Enemy
{
    private Vector3 direction;
    private bool setDirection = false;
    public BansheeStateMachine _state;

    private void Start()
    {
        _state = new BansheeStateMachine(this.transform);
        _state.Initialise(_state.PickDirectionState);
    }

    private void Update()
    {
       if(setDirection)
       {
            setDirection = false;
            _state.TransitiontTo(_state.IdleState);
       }
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        setDirection = true;
    }
}
