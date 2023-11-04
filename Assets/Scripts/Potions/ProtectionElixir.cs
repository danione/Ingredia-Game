using UnityEngine;

public class ProtectionElixir : IPotion
{
    private Transform protectionBarrier = null;
    private const float powerupDurationDefault = 4.0f;
    private float powerupDuration;

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

        powerupDuration = powerupDurationDefault;
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

    public void Reset()
    {
        destroyed = false;
    }
}
