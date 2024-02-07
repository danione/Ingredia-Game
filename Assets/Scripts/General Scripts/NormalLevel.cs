using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEventHandler.Instance.SetsNormalMode();
        GameEventHandler.Instance.UpdateUI();
    }
}
