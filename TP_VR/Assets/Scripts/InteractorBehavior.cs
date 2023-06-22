using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractorBehavior : MonoBehaviour
{
    Dictionary<string, GameObject> overlappingGrabbables = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> overlappingTriggers = new Dictionary<string, GameObject>();

    bool TriggerButtonPressed = false, GripButtonPressed = false, isGrabbing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Grip Button pressed
    public void OnGripAxis(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        if(value>0.95)
        {
            GripButtonPressed = true;
        }
        else
        {
            GripButtonPressed = false;
        }
        ManageGrab();
    }

    //Trigger button pressed
    public void OnGrabAction(InputAction.CallbackContext context)
    { 
        
        if (context.started)
        {
            TriggerButtonPressed = true;
            ManageGrab();
        }
        if (context.canceled)
        {
            TriggerButtonPressed = false;
            ManageGrab();
        }
        
    }

    // A or X pressed
    public void OnTriggerAction(InputAction.CallbackContext context)
    {
        GameObject nearestTrigger = GetNearestTrigger();
        if (nearestTrigger)
        {
            if (context.started)
            {
                nearestTrigger.GetComponent<TriggerBehavior>().Trigger();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GrabbableBehavior gb = other.GetComponentInParent<GrabbableBehavior>();
        if (gb)
        {
            overlappingGrabbables.Add(gb.gameObject.name, gb.gameObject);
        }
        TriggerBehavior tb = other.GetComponentInParent<TriggerBehavior>();
        if (tb)
        {
            overlappingTriggers.Add(tb.gameObject.name, tb.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GrabbableBehavior gb = other.GetComponentInParent<GrabbableBehavior>();
        if (gb)
        {
            overlappingGrabbables.Remove(gb.gameObject.name);
        }
        TriggerBehavior tb = other.GetComponentInParent<TriggerBehavior>();
        if (tb)
        {
            overlappingTriggers.Remove(tb.gameObject.name);
        }
    }


    private GameObject GetNearesGrabbable()
    {
        GameObject nearestGrabbable = null;
        float minDistance = Mathf.Infinity;

        //Debug.Log("List length =" + overlappingGrabbables.Count);
        foreach (KeyValuePair<string, GameObject> kvp in overlappingGrabbables)
        {
            if (kvp.Value.GetComponent<GrabbableBehavior>())
            {
                float distance = Vector3.Distance(transform.position, kvp.Value.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestGrabbable = kvp.Value;
                }
            }
        }
        return nearestGrabbable;
    }

    private GameObject GetNearestTrigger()
    {
        GameObject nearestTrigger = null;
        float minDistance = Mathf.Infinity;
        foreach (KeyValuePair<string, GameObject> kvp in overlappingTriggers)
        {
            float distance = Vector3.Distance(transform.position, kvp.Value.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTrigger = kvp.Value;
            }
        }
        return nearestTrigger;
    }

    private void ManageGrab()
    {
        GameObject nearestGrabbable;
        if (!isGrabbing)
        {
            if(GripButtonPressed || TriggerButtonPressed)
            {
                if(nearestGrabbable = GetNearesGrabbable())
                {
                    nearestGrabbable.GetComponent<GrabbableBehavior>().TryGrab(gameObject);
                    Debug.Log("Try grab");
                    isGrabbing = true;
                }
                
            }
        }
        else
        {
            if(!GripButtonPressed && !TriggerButtonPressed)
            {
                if (nearestGrabbable = GetNearesGrabbable())
                {
                    nearestGrabbable.GetComponent<GrabbableBehavior>().TryRelease(gameObject);
                    Debug.Log("Try release");
                    isGrabbing = false;
                }
                    
            }
        }
    }
}
