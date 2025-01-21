using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class disCheck : MonoBehaviour
{
    [SerializeField] private Image image;
    private bool enter;
    private bool stay;
    private static GameObject hit;

    private bool vanish;
    private float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(vanish)
        {
            timeCount += Time.deltaTime;
            if(timeCount >= 0.5f)
            {
                image.gameObject.SetActive(false);
                enter = false;
                vanish = false;
            }
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ShockWave"))
        {
            if(enter == false)
            {
                Debug.Log("Enter!");
                image.gameObject.SetActive(true);
                image.GetComponent<near>().SetNear();
                enter = true;
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ShockWave"))
        {
            if (enter == false)
            {
                image.gameObject.SetActive(true);
                enter = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enter == true)
        {
            if (other.CompareTag("ShockWave"))
            {
                Debug.Log("exit!");
                image.gameObject.SetActive(false);
                enter = false;

            }
        }
    }

    public void vanishEnemy()
    {
        vanish = true;
    }
}
