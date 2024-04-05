using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : BarManager
{
    [Header("LifeBar")]

    [SerializeField] private Slider _lifeSlider = null;
    [SerializeField] private Text _lifeText = null;

    public float _hpHolder;
    public  int _maxHpHolder;




    void Awake()
    {
        //REFERENCES :
        _maxHpHolder = GetComponentInParent<LivingObject>().GetMaxhp();

        _camera = Camera.main; 

        if (_lifeSlider == null)
        {
            _lifeSlider = GetComponent<Slider>(); 
        }
        if (_lifeText == null)
        {
            return;
        }

        //SLIDER VALUE
        SetMaxLifePoints();
    }

   

    //HP VALUE
    private void Update()
    {
        _hpHolder =GetComponentInParent<LivingObject>().GetHp();

        _lifeSlider.value = _hpHolder;

        if(_lifeText != null)
        {
            _lifeText.text = _hpHolder.ToString();
            if(_hpHolder <= _maxHpHolder/2)
            { _lifeText.color = Color.red; }
            else
            { _lifeText.color = Color.white;}
        }
    }

    //CAMERA POSITION
    
    /*private void FixedUpdate()
    {
        if(_isCamActivated) 
        {
            transform.LookAt(transform.position + _camera.transform.forward + _camera.transform.up); 
            _lifeSlider.transform.rotation = Camera.main.transform.rotation;
        }
    }*/
    

    //SLIDER VALUE
    public void SetMaxLifePoints()
    {
        _lifeSlider.minValue = 0;
        _lifeSlider.maxValue = _maxHpHolder;
    }

}