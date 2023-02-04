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
    [SerializeField] private bool b_isFoot;
    [SerializeField] private PlayerKnight knight;
    
    [SerializeField] private Transform t_footAnchor;
    private Rigidbody rb_this;
    [SerializeField] private float f_anchorForce = 1000;
    private bool b_isAnchored = false;

    public bool IsAnchored
    {
        get { return b_isAnchored; }
        set
        {
            //Debug.Log(name + " is anchored? " + value.ToString());
            b_isAnchored = value;
        }
    }
    
    private void Awake()
    {
        t_this = transform;
        q_startTargetRot = t_this.localRotation;
    }

    void Start()
    {
        cj_joint = GetComponent<ConfigurableJoint>();
        rb_this = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!cj_joint) return;
        if (!t_targetBone) return;

        cj_joint.targetRotation = Quaternion.Inverse(t_targetBone.localRotation) * q_startTargetRot;

        if (b_isFoot && t_footAnchor && b_isAnchored)
        {
            //rb_this.Move(t_footAnchor.position, t_footAnchor.rotation);
            rb_this.AddForce((t_footAnchor.position - t_this.position).normalized * f_anchorForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!b_isFoot) return;
        
        knight.OnLanded();
        
        t_footAnchor.SetPositionAndRotation(t_this.position, t_this.rotation);
        IsAnchored = true;
    }
}
