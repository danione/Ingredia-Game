using System;
using UnityEngine;

public class LookoutState : IState
{
    
    private float speed = 3.5f;
    private float stoppingDistance = 3.0f;
    private float xBorder = 13.0f;
    private float yBorder = 13.0f;
    private readonly float defaultChannelDuration = 3.0f;
    private float channelDuration;
    public Action FinishedChanneling;

    private Transform ritualistCircle;

    public LookoutState(Transform ritualistCircle) 
    {
        this.ritualistCircle = ritualistCircle;
    }

    public void Enter()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-xBorder, xBorder), UnityEngine.Random.Range(-yBorder, yBorder), ritualistCircle.position.z);
        ritualistCircle.gameObject.SetActive(true);
        ritualistCircle.position = position;
        channelDuration = defaultChannelDuration;
    }

    public void Exit()
    {
        ritualistCircle.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (ritualistCircle == null || PlayerController.Instance == null) return;
        
        Vector3 targetDirection = PlayerController.Instance.transform.position - ritualistCircle.position;
        float distance = targetDirection.magnitude;

        if(distance > stoppingDistance)
        {
            Vector3 desiredVelocity = targetDirection.normalized;

            // Apply the steering force to move the object.
            ritualistCircle.position += desiredVelocity * speed * Time.deltaTime;
        }
        else
        {
            channelDuration -= Time.deltaTime;
        }

        if(channelDuration <= 0)
        {
            FinishedChanneling?.Invoke();
        }
    }
}
