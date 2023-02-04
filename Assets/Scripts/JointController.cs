using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{
    private Transform t_this;
    private Quaternion q_startTargetRot;
    [SerializeField] private Transform t_targetBone;

    private ConfigurableJoint cj_joint;
    [SerializeField] private bool b_inverted = false;

    private void Awake()
    {
        t_this = transform;
        q_startTargetRot = t_this.localRotation;
    }

    void Start()
    {
        
        cj_joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!cj_joint) return;
        if (!t_targetBone) return;

        //t_this.localPosition = t_targetBone.localPosition;
        //cj_joint.targetRotation = Quaternion.Euler(t_targetBone.localEulerAngles);

        // if (b_inverted)
        // {
        //     cj_joint.targetRotation = Quaternion.Inverse(t_targetBone.localRotation * q_startTargetRot);
        // }
        // else
        // {
        //     cj_joint.targetRotation = t_targetBone.localRotation * q_startTargetRot;
        // }

        cj_joint.targetRotation = Quaternion.Inverse(t_targetBone.localRotation) * q_startTargetRot;
    }
}
