using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : BarManager
{
    [Header("ExpBar")]
    public Levelable _parentExpSys;

    private void Start()
    {
        if (!_parentExpSys)
        {
            Debug.LogError("Il manque une reference pour le level system ",this.gameObject);
        }
        LivingObject.IsDying += UpdatesValues;
        UpdatesValues(_parentExpSys.GetExp(), _parentExpSys.GetMaxExp());
;
    }
    public override void UpdatesValues(float newValue, float newMaxValue) // Update les valeurs du slider value et maxValue et aussi le text
    {
        base.UpdatesValues(newValue, newMaxValue);
        if (!_TextSlider) return;
        _TextSlider.text = _parentExpSys.GetLevel().ToString();
      }



}
