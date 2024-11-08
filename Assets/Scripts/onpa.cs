using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSample : MonoBehaviour
{


    Vector3 direction = new Vector3(10f, 0f, 0f);

    [SerializeField] private float speed = 1.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, direction, step);

    }
}