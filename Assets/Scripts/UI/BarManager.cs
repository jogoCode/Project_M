using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    [Header("Bar")]

    [SerializeField] protected Slider _Slider = null;
    [SerializeField] protected Text _TextSlider = null;


    [SerializeField] protected float _valueHolder;
    [SerializeField] protected float _maxValueHolder;

    protected Camera _camera = null;


    [SerializeField] protected bool _isCamActivated;




    void Awake()
    {
        _camera = Camera.main;
        if (!_Slider)
        {
            _Slider = GetComponent<Slider>();
        }
        if (_TextSlider == null)
        {
            _TextSlider = GetComponentInChildren<Text>();
        }
        LivingObject.IsDying += UpdatesValues;
    }



    protected void UpdatesValues(float newValue, float newMaxValue)
    {
        _Slider.value = newValue;
        _Slider.maxValue = newMaxValue;
    }


}


