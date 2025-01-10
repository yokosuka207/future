using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    float HP = 100;
    [SerializeField] private GameObject[] hpBar;
    int damageCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HealthDecreese(int damage)
    {
        HP -= damage;

        for(int i = 0; i < 3; i++)
        {
            if (damageCount >= 10)
            {
                break;
            }
            hpBar[damageCount].SetActive(false);
            damageCount++;

            
        }
    }

}
