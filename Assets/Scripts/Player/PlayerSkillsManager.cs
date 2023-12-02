using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsManager : MonoBehaviour
{
    private GhostSkill ghost;
    private OverloadSkill overload;
    [SerializeField] private Transform shieldObject;
    private bool isShielding = false;

    private void Start()
    {
        ghost = new GhostSkill();
        overload = new OverloadSkill();

        GameEventHandler.Instance.ActivatedGhost = OnActivateGhost;
        GameEventHandler.Instance.ActivatedLaser = OnActivateLaser;
        GameEventHandler.Instance.ActivatedBarrier = OnActivateBarrier;
    }

    private void Update()
    {
        ghost.Tick();
        overload.Tick();
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
        if (isShielding) return;

        isShielding = true;
        StartCoroutine(ShieldActivation(duration));
    }

    private IEnumerator ShieldActivation(float duration)
    {
        shieldObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        shieldObject.gameObject.SetActive(false);
        isShielding = false;
    }
}
