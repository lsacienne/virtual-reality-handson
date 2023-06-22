using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class GrabbableBehavior : MonoBehaviour
{
    private Rigidbody rigidBody;
    private GameObject grabber;
    private bool wasKinematic;
    private bool isHeld = false;
    
    public enum GrabType { None, Free, Snap };
    public GrabType grabType = GrabType.Free;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        wasKinematic = rigidBody.isKinematic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryGrab(GameObject grabber)
    {
        switch(grabType)
        {
            case GrabType.None:
                break;
            case GrabType.Free:
                rigidBody.isKinematic = true;
                transform.parent = grabber.transform;
                this.grabber = grabber;
                isHeld = true;
                break;
            case GrabType.Snap:
                rigidBody.isKinematic = true;
                transform.parent = grabber.transform;
                this.grabber = grabber;
                isHeld = true;
                transform.position = grabber.transform.position;
                transform.rotation = grabber.transform.rotation;
                break;

        }
        
    }

    public void TryRelease(GameObject grabber)
    {
        if (grabber.Equals(this.grabber) && isHeld)
        {
            transform.parent = null;
            rigidBody.isKinematic = wasKinematic;
            isHeld = false;
        }
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}
