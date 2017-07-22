using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyUI : MonoBehaviour
{
    [SerializeField]
    Image energyBar;

    [SerializeField]
    float fillValue;

    [SerializeField]
    float lerpSpeed;

    public float tempFillValue;

    // Use this for initialization
    void Start ()
    {
        energyBar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleBar();
	}

    void HandleBar()
    {
        fillValue = CalculateValue(Mathf.Clamp(tempFillValue, 0, 100), 0, 100, 0, 1);

        if (fillValue != energyBar.fillAmount)
        {
            energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, fillValue, lerpSpeed);
        }
    }

    float CalculateValue(float value, int minEnergy, int maxEnergy, int minFillAmount, int maxFillAmount)
    {
        return ((value - minEnergy) * (maxFillAmount - minFillAmount) / (maxEnergy - minEnergy) + minFillAmount);
    }
}
