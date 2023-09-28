using UnityEngine;

public class ProtectionElixir : IPowerUp
{
    private Transform protectionBarrier = null;
    private float powerupDuration = 4.0f;

    private bool destroyed = false;
    public bool Destroyed { get => destroyed;}

    public void Use()
    {
        Transform gameObject = PlayerController.Instance.transform.GetChild(2);

        if (gameObject != null) 
        {
            protectionBarrier = gameObject.transform;
            protectionBarrier?.gameObject.SetActive(true);

        }
    }

    public void Destroy()
    {
        if (protectionBarrier == null) return;
        protectionBarrier?.gameObject.SetActive(false);
        destroyed = true;
    }

    public void Tick()
    {
        powerupDuration -= Time.deltaTime;
        if(powerupDuration <= 0)
        {
            Destroy();
        }
    }
}
