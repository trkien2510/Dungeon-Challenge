using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public GameObject textHealth;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        if (textHealth != null)
        {
            textMeshPro = textHealth.GetComponent<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        textMeshPro.SetText(slider.value.ToString());
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
