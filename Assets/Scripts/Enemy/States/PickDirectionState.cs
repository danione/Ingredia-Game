using UnityEngine;

public class PickDirectionState : IState
{
    private float channelTime;
    [SerializeField] private readonly float defaultChannelTime;
    [SerializeField] private float noise;
    private Transform thisObject;
    BansheeEnemy component;
    public PickDirectionState(Transform thisObject)
    {
        this.thisObject = thisObject;
        component = thisObject.gameObject.GetComponent<BansheeEnemy>();
    }
    public void Enter()
    {
        channelTime = defaultChannelTime;
        // Nothing Happens
    }

    public void Exit()
    {
        // Nothing Happens
    }

    public void Update()
    {
        channelTime -= Time.deltaTime;
        if(channelTime <= 0)
        {
            Vector3 originalDirection = (PlayerController.Instance.gameObject.transform.position - thisObject.position).normalized;
            Vector3 noisyDirection = (originalDirection + Random.insideUnitSphere * noise);
            if(component != null)
            {
                component.SetDirection(noisyDirection.normalized);
            }
            
        }
    }
}
