using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObjects : MonoBehaviour
{
    public GameObject gameObjectToToggle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        if(gameObjectToToggle)
        {
            gameObjectToToggle.SetActive(!gameObjectToToggle.activeSelf);
        }
    }
}
