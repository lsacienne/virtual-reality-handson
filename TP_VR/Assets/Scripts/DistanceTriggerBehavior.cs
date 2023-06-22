using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DistanceTriggerBehavior : MonoBehaviour
{
    public float maxDistance = 5f;
    private Material nokMaterial;
    public Material okMaterial;
    private bool canTrigger = false;
    private bool pointerVisible = false;
    private LineRenderer lineRenderer;
    private Vector3 destinationPoint;
    private TriggerBehavior targetedTrigger;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        nokMaterial = lineRenderer.material;
        HidePointer();
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerVisible)
        {
            lineRenderer.SetPosition(0, transform.position);
            RaycastHit hit;


            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance))
            {
                if (hit.collider.gameObject.GetComponent<TriggerBehavior>())
                {
                    canTrigger = true;
                    destinationPoint = hit.collider.gameObject.transform.position;
                    lineRenderer.material = okMaterial;
                    lineRenderer.SetPosition(1, transform.position + (destinationPoint - transform.position) * hit.distance);
                    targetedTrigger = hit.collider.gameObject.GetComponent<TriggerBehavior>();
                }
                else
                {
                    canTrigger = false;
                    lineRenderer.material = nokMaterial;
                    lineRenderer.SetPosition(1, transform.position + transform.forward * hit.distance);
                    targetedTrigger = null;
                }

            }
            else
            {
                lineRenderer.SetPosition(1, transform.position + transform.forward * maxDistance);
            }
        }
    }

    void HidePointer()
    {
        if (lineRenderer)
        {
            lineRenderer.enabled = false;
        }
        pointerVisible = false;
    }

    void ShowPointer()
    {
        if (lineRenderer)
        {
            lineRenderer.enabled = true;
        }
        pointerVisible = true;
    }

    public void OnTriggerAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShowPointer();
        }
        if (context.canceled)
        {
            if (canTrigger)
            {
                targetedTrigger.Trigger();
            }
            HidePointer();
        }
    }
}

