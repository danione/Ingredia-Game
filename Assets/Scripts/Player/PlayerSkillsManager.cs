using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsManager : MonoBehaviour
{
    private GhostSkill ghost;
    private OverloadSkill overload;

    private void Start()
    {
        ghost = new GhostSkill();
        overload = new OverloadSkill();

        GameEventHandler.Instance.ActivatedGhost = OnActivateGhost;
        GameEventHandler.Instance.ActivatedLaser = OnActivateLaser;
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
}
