using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juicy : MonoBehaviour
{
    float _vel;
    float _displacement;
    [SerializeField][Range(0.0f, 900f)] float _spring = 900;
    [SerializeField][Range(0.0f, 900f)] float _damp = 5;

    void Start()
    {
        _vel = 0;
    }

    // Update is called once per frame
    void Update()
    {
         ApplyJuicy();   
    }

    void ApplyJuicy()
    {
        var force = -_spring * _displacement - _damp * _vel;
        _vel += force * Time.deltaTime;
        _displacement += _vel * Time.deltaTime;
        //transform.localScale = new Vector3(5,5,5) + new Vector3(_displacement,_displacement,_displacement) *1;
        transform.localScale = new Vector3(1, 1, 1) + new Vector3(_displacement, _displacement, _displacement);



    }

    public void Hit()
    {
        _vel = 20;
    }
}
