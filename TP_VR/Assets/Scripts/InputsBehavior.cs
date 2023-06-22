using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsBehavior : MonoBehaviour
{
    public Animator rightAnimator;
    public Animator leftAnimator;
    public GameObject rightThumbstick;
    public GameObject leftThumbstick;
    public void OnAPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("A Pressed");
            if (rightAnimator)
            {
                rightAnimator.SetBool("APressed", true);
            }
        }
        if (context.canceled)
        {
            Debug.Log("A Released");
            if (rightAnimator)
            {
                rightAnimator.SetBool("APressed", false);
            }
        }
    }
    public void OnBPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("B Pressed");
            if (rightAnimator)
            {
                rightAnimator.SetBool("BPressed", true);
            }
        }
        if (context.canceled)
        {
            Debug.Log("B Released");
            if (rightAnimator)
            {
                rightAnimator.SetBool("BPressed", false);
            }
        }
    }

    public void OnXPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("X Pressed");
            if (leftAnimator)
            {
                leftAnimator.SetBool("XPressed", true);
            }
        }
        if (context.canceled)
        {
            Debug.Log("X Released");
            if (leftAnimator)
            {
                leftAnimator.SetBool("XPressed", false);
            }
        }
    }
    public void OnYPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Y Pressed");
            if (leftAnimator)
            {
                leftAnimator.SetBool("YPressed", true);
            }
        }
        if (context.canceled)
        {
            Debug.Log("Y Released");
            if (leftAnimator)
            {
                leftAnimator.SetBool("YPressed", false);
            }
        }
    }
    public void OnTriggerAxisRight(InputAction.CallbackContext context)
    {
        //Debug.Log("Trigger Released");
        if (rightAnimator)
        {
            rightAnimator.SetFloat("RightTrigger", context.ReadValue<float>());
            //Debug.Log("Trigger value: " + context.ReadValue<float>());
        }
    }
    public void OnTriggerAxisLeft(InputAction.CallbackContext context)
    {
        if (leftAnimator)
        {
            leftAnimator.SetFloat("LeftTrigger", context.ReadValue<float>());
        }
    }


    public void OnGripAxisRight(InputAction.CallbackContext context)
    {
        //Debug.Log("Grip Released");
        if (rightAnimator)
        {
            rightAnimator.SetFloat("RightGrip", context.ReadValue<float>());
            //Debug.Log("Grip value: " + context.ReadValue<float>());
        }
    }

    public void OnGripAxisLeft(InputAction.CallbackContext context)
    {
        //Debug.Log("Grip Released");
        if (leftAnimator)
        {
            leftAnimator.SetFloat("LeftGrip", context.ReadValue<float>());
            //Debug.Log("Grip value: " + context.ReadValue<float>());
        }
    }


    public void OnThumbstickAxisRight(InputAction.CallbackContext context)
    {
        if (rightThumbstick)
        {
            Vector2 thumbstickValue = context.ReadValue<Vector2>();
            rightThumbstick.transform.localEulerAngles = new Vector3(thumbstickValue.y, 0, -thumbstickValue.x) * 15f;
        }
    }

    public void OnThumbstickAxisLeft(InputAction.CallbackContext context)
    {
        if (leftThumbstick)
        {
            Vector2 thumbstickValue = context.ReadValue<Vector2>();
            leftThumbstick.transform.localEulerAngles = new Vector3(thumbstickValue.y, 0, -thumbstickValue.x-6.5f) * 15f;
        }
    }

    public void OnTriggerTouchRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (rightAnimator)
            {
                Debug.Log("Trigger Touched");
                rightAnimator.SetBool("Point", false);
            }
        }
        if (context.canceled)
        {
            if (rightAnimator)
            {
                Debug.Log("Trigger Released");
                rightAnimator.SetBool("Point", true);
            }
        }
    }

    public void OnTriggerTouchLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (leftAnimator)
            {
                Debug.Log("Trigger Touched");
                leftAnimator.SetBool("Point", false);
            }
        }
        if (context.canceled)
        {
            if (leftAnimator)
            {
                Debug.Log("Trigger Released");
                leftAnimator.SetBool("Point", true);
            }
        }
    }
}
