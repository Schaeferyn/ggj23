using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKnight : MonoBehaviour
{
    private PlayerInput pInput;
    private bool b_isInitialized = false;

    [SerializeField] Rigidbody rb_knight;
    
    [SerializeField] private Animator anim_target;
    private bool b_isGrounded = false;
    [SerializeField] private float f_jumpForce = 1000;

    private Vector2 v_moveInput;
    private Vector2 v_lookInput;
    [SerializeField] private ConfigurableJoint[] cj_feet;
    [SerializeField] private JointController[] jc_feet;
    
    void Initialize()
    {
        pInput = GetComponent<PlayerInput>();
        b_isInitialized = true;
    }

    private void OnEnable()
    {
        if (!b_isInitialized) Initialize();

        pInput.onActionTriggered += OnInputActionTriggered;
    }

    private void OnDisable()
    {
        pInput.onActionTriggered -= OnInputActionTriggered;
    }

    void OnInputActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == "Jump")
        {
            if (context.performed)
            {
                OnJumpPressed();
            }
            else if (context.canceled)
            {
                OnJumpReleased();
            }
        }
        else if (context.action.name == "Move")
        {
            v_moveInput = context.ReadValue<Vector2>();
        }
        else if (context.action.name == "Look")
        {
            v_lookInput = context.ReadValue<Vector2>();
        }
    }

    void ProcessMove()
    {
        
    }

    void ProcessLook()
    {
        
    }

    void OnJumpPressed()
    {
        //Debug.Log("JUMP PRESSED");
        
    }

    void OnJumpReleased()
    {
        if (!b_isGrounded) return;
        Debug.Log("JUMP RELEASED");
        
        // foreach (ConfigurableJoint cj in cj_feet)
        // {
        //     cj.xMotion = ConfigurableJointMotion.Free;
        //     cj.yMotion = ConfigurableJointMotion.Free;
        //     cj.zMotion = ConfigurableJointMotion.Free;
        // }

        foreach (JointController jc in jc_feet)
        {
            jc.IsAnchored = false;
        }
        
        anim_target.SetTrigger("Jump");
        rb_knight.AddForce(Vector3.up * f_jumpForce);
        
        b_isGrounded = false;
    }

    public void OnLanded()
    {
        if (b_isGrounded) return;
        Debug.Log("Knight landed");

        b_isGrounded = true;
        anim_target.SetTrigger("Land");
        
        // foreach (ConfigurableJoint cj in cj_feet)
        // {
        //     cj.xMotion = ConfigurableJointMotion.Locked;
        //     cj.yMotion = ConfigurableJointMotion.Locked;
        //     cj.zMotion = ConfigurableJointMotion.Locked;
        // }
    }
}
