using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    private Transform t_this;
    
    [SerializeField] private Transform t_followTarget;
    [SerializeField] private float f_lerpAmount = 0.1f;

    private void Start()
    {
        t_this = transform;
    }

    void Update()
    {
        if (!t_followTarget) return;

        t_this.position = Vector3.Lerp(t_this.position, t_followTarget.position, f_lerpAmount);
    }
}
