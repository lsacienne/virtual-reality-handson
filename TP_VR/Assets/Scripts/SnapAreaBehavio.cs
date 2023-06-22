using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapAreaBehavio : MonoBehaviour
{

    Dictionary<string, GameObject> overlappingGrabbables = new Dictionary<string, GameObject>();
    private bool isUsed = false;
    private GameObject snappedGameObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isUsed)
        {
            foreach (KeyValuePair<string, GameObject> kvp in overlappingGrabbables)
            {
                if(!kvp.Value.GetComponent<GrabbableBehavior>().IsHeld())
                {
                    kvp.Value.transform.position = transform.position;
                    kvp.Value.transform.rotation = transform.rotation;
                    snappedGameObject = kvp.Value;
                    isUsed = true;
                    break;
                }
            }
        }
        else
        {
            if(snappedGameObject.GetComponent<GrabbableBehavior>().IsHeld())
            {
                isUsed = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GrabbableBehavior gb = other.GetComponentInParent<GrabbableBehavior>();
        if (gb)
        {
            Debug.Log("Trigger enter");
            overlappingGrabbables.Add(gb.gameObject.name, gb.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        GrabbableBehavior gb = other.GetComponentInParent<GrabbableBehavior>();
        if (gb)
        {
            overlappingGrabbables.Remove(gb.gameObject.name);
        }
    }

}