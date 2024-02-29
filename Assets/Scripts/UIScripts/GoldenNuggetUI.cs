using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldenNuggetUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;
    [SerializeField] private float speedGoingDown;
    [SerializeField] private float speedDisappearing;

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * speedGoingDown;
        m_TextMeshProUGUI.alpha -= Time.deltaTime * speedDisappearing;
        if (m_TextMeshProUGUI.color.a < 0f)
        {
            GameEventHandler.Instance.DestroyedObject(gameObject);
        }
    }

    public void DisplayGoldValue(int amount)
    {
        m_TextMeshProUGUI.text = "+" + amount.ToString() + " Gold";
        m_TextMeshProUGUI.alpha = 1;
    }
}
