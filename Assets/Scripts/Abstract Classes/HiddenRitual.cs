using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRitual
{
    public string ritual;
    public bool isEnabled = false;

    public HiddenRitual(string baseRitual)
    {
        ritual = baseRitual;
    }

    public void Disable()
    {
        isEnabled = false;
    }

    public void TemporaryEnable()
    {
        isEnabled = true;
        Unlock();
    }

    public void Unlock()
    {
        GameManager.Instance.GetComponent<RitualManager>().AddRitualToUnlocked(ritual);
    }
}
