using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLifeBar : BarManager
{
    [Header("LifeBar")]
    [SerializeField] LivingObject m_parentLife;

    [SerializeField] bool _isInWorld; 
    void Start()
    {
        if (!m_parentLife)
        {
            Debug.LogError("Il manque une référence au Living object du parent",this.gameObject);
        }
        m_parentLife.LifeChanged+=UpdatesValues;
        UpdatesValues(m_parentLife.GetHp(),m_parentLife.GetMaxhp());
    }

    public override void UpdatesValues(float newValue, float newMaxValue) // Update les valeurs du slider "value" et "maxValue"
    {
        base.UpdatesValues(newValue, newMaxValue);
        _valueHolder = newValue;
        _maxValueHolder = newMaxValue;
        _maxValueHolder = newMaxValue;
        if (!_textIsActive) return;
        _TextSlider.text = newValue.ToString();
        if (newValue <= newMaxValue / 2)
        {
            _TextSlider.color = Color.red; 
        }
        else{
            _TextSlider.color = Color.white; 
        }
    }

    private void FixedUpdate()
    {
        if (_isInWorld)
        {
            transform.LookAt(transform.position + _camera.transform.forward + _camera.transform.up);
            _Slider.transform.rotation = Camera.main.transform.rotation;
        }
    }

}
