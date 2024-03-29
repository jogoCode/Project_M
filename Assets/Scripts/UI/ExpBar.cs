using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : BarManager
{
    [Header("ExpBar")]
    [SerializeField] Levelable _parentExpSys;

    private void Start()
    {
        if (!_parentExpSys)
        {
            Debug.LogError("Il manque une reference ",this.gameObject);
        }
        
    }
    protected override void UpdatesValues(float newValue, float newMaxValue) // Update les valeurs du slider value et maxValue et aussi le text
    {
        base.UpdatesValues(newValue, newMaxValue);
        _TextSlider.text = _parentExpSys.GetLevel().ToString();
    }



}
