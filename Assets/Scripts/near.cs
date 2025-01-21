using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class near : MonoBehaviour
{
    private float timeCount;
    [SerializeField] private Image image;
    [SerializeField] private float speed;
    [SerializeField] private float frequency;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        Color color = new Color(1.0f, 1.0f, 1.0f, Mathf.Sin(timeCount * speed) * (0.5f + frequency) + 0.5f + (frequency / 2));
        image.color = color;
    }

    public void SetNear()
    {
        timeCount = 0;
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }
}
