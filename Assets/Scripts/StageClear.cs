using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StageClear : MonoBehaviour
{
    private float currentTime;
    private float textx;
    private bool stopFlug;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
         
        currentTime += Time.deltaTime;
        if(stopFlug == false)
        {
            if (currentTime > 1)
            {
                if (transform.position.x >= Screen.width / 2)
                {
                    transform.position = new Vector3(Screen.width / 2, transform.position.y, 0);
                    stopFlug = true;
                }

                transform.position = new Vector3(transform.position.x + speed, transform.position.y, 0);
            }
        }
        
    }
}
