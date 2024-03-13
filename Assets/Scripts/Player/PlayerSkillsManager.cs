using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSkillsManager : MonoBehaviour
{
    private GhostSkill ghost;
    [SerializeField] private OverloadSkill overload;
    [SerializeField] private Transform shieldObject;
    private bool isShielding = false;
    private float shieldDuration = 0;

    private void Start()
    {
        ghost = new GhostSkill();
        overload = new OverloadSkill();

        GameEventHandler.Instance.ActivatedGhost += OnActivateGhost;
        GameEventHandler.Instance.ActivatedLaser += OnActivateLaser;
        GameEventHandler.Instance.ActivatedBarrier += OnActivateBarrier;
        PlayerEventHandler.Instance.PlayerDied += OnPlayerDeath;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.ActivatedGhost -= OnActivateGhost;
        GameEventHandler.Instance.ActivatedLaser -= OnActivateLaser;
        GameEventHandler.Instance.ActivatedBarrier -= OnActivateBarrier;
        PlayerEventHandler.Instance.PlayerDied -= OnPlayerDeath;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene a, LoadSceneMode b)
    {
        OnPlayerDeath();
    }

    private void OnPlayerDeath()
    {
        isShielding = false;
        shieldObject.gameObject.SetActive(false);
    }

    private void Update()
    {
        ghost.Tick();
        overload.Tick();
        ShieldActive();
    }

    private void ShieldActive()
    {
        if (!isShielding) return;

        shieldDuration -= Time.deltaTime;

        if(shieldDuration < 0)
        {
            isShielding = false;
            shieldObject.gameObject.SetActive(false);
            GameEventHandler.Instance.ShieldDisable();
            return;

        }

        GameEventHandler.Instance.SendShieldStats(shieldDuration);
    }

    private void OnActivateGhost(GhostPotionData data)
    {
        ghost.CreateOrReset(data);
    }

    private void OnActivateLaser(OverloadElixirData data)
    {
        overload.CreateOrRefresh(data);
    }

    private void OnActivateBarrier(float duration)
    {
        if (isShielding)
        {
            shieldDuration += duration;
            GameEventHandler.Instance.SendShieldStats(shieldDuration);
            return;
        }
        shieldDuration = duration;
        isShielding = true;
        shieldObject.gameObject.SetActive(true);
        GameEventHandler.Instance.ShieldEnable();
    }
}
