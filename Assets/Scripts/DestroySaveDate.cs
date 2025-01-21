using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySaveDate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindWithTag("ClearDeta") != null)
        {
            Destroy(GameObject.FindWithTag("ClearDeta"));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
