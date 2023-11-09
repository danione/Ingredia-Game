using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRitual : IHidden
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
        Lock();
    }

    public void TemporaryEnable()
    {
        isEnabled = true;
        Unlock();
    }

    public void Unlock()
    {
        GameManager.Instance.GetComponent<RitualManager>().AddRitual(ritual);
    }

    private void Lock()
    {
        GameManager.Instance.GetComponent<RitualManager>().RemoveRitual(ritual);
    }
}
