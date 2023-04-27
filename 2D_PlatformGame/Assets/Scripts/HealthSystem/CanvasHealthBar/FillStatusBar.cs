using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public HealthStatus healthStatus;
    public Image fillImage;

    public Color fillColor = Color.green;
    public Color lowHealthColor = Color.red;

    private Slider slider;

    private TMP_Text healthAmount;

    private void Awake()
    {
        healthAmount = GameObject.Find("healthAmount").GetComponent<TMP_Text>();
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
            slider.gameObject.gameObject.SetActive(false);
        }

        if (slider.value > slider.minValue && !fillImage.enabled)
            fillImage.enabled = true;
        
        //obliczenie procentu wype³nienia slidera ¿ycia
        float fillValue = (float)healthStatus.health / (float)healthStatus.maxHealth;

        //jeœli ¿ycie spada poni¿ej 1/3 to kolor wype³nienia siê zmienia
        if (fillValue <= slider.maxValue / 3)
            fillImage.color = lowHealthColor;
        else if (fillValue > slider.maxValue / 3)
            fillImage.color = fillColor;

        //ustawienie wartoœci na sliderze
        slider.value = fillValue;
        healthAmount.text = healthStatus.health.ToString();
    }
}
