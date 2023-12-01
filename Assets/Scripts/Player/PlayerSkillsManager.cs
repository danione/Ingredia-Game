using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsManager : MonoBehaviour
{
    private GhostSkill ghost;

    private void Start()
    {
        ghost = new GhostSkill();
        GameEventHandler.Instance.ActivatedGhost = OnActivateGhost;
    }

    private void Update()
    {
        ghost.Tick();
    }

    private void OnActivateGhost(GhostPotionData data)
    {
        ghost.CreateOrReset(data);
    }


}
