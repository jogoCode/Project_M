using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : BarManager
{
    [SerializeField] Levelable _parentExpSys;

    private void Start()
    {
        if (!_parentExpSys)
        {
            Debug.LogError("Il manque une reference ",this.gameObject);
        }
    }

    void Update()
    {
        _valueHolder = _parentExpSys.GetExp();
        _maxValueHolder = _parentExpSys.GetMaxExp();
        _TextSlider.text = _parentExpSys.GetLevel().ToString();
        UpdatesValues(_valueHolder,_maxValueHolder);

    }



}
