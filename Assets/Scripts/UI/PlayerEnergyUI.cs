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

    public float CurrentVal
    {
        get
        {
            return fillValue;
        }
        set
        {
            fillValue = CalculateValue(Mathf.Clamp(value,0,100), 0, 100, 0, 1);
        }
    }

    // Use this for initialization
    void Start ()
    {
        energyBar = GetComponent<Image>();
        energyBar.type = Image.Type.Filled;
        energyBar.fillMethod = Image.FillMethod.Horizontal;
        energyBar.fillOrigin = (int)Image.OriginHorizontal.Left;
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleBar();
	}

    void HandleBar()
    {
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
