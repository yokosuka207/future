using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    float HP = 100;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = HP / 10;
    }

    public void HealthDecreese(int damage)
    {
        HP -= damage;
    }

}
