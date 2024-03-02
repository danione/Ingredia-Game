using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUIPopup : MonoBehaviour
{
    [SerializeField] private float speedGoingDown;
    [SerializeField] private float speedDisappearing;

    private TextMeshProUGUI textValue;
    // Start is called before the first frame update
    void Awake()
    {
        textValue = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * speedGoingDown;
        textValue.alpha -= Time.deltaTime * speedDisappearing;
        if(textValue.color.a < 0f)
        {
            GameEventHandler.Instance.DestroyedObject(gameObject);
        }
    }

    public void DisplayDamage(float value)
    {
        textValue.text = "-" + Mathf.Ceil(value).ToString();
        textValue.alpha = 1;
    }
}
