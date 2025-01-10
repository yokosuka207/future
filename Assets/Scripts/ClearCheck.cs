using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearCheck : MonoBehaviour
{
    [SerializeField] private Image[] star = new Image[3];
    [SerializeField] private TextMeshProUGUI quest1;
    [SerializeField] private float alphaRatio;
    [SerializeField] private float[] derayTime = new float[3];
    [SerializeField] private string[] starText = new string[3];
    [SerializeField] private int[] useLimit = new int[3];

    private bool[] starFlug = new bool[3];
    private float[] starAlpha = new float[3];
    private bool[] starActiveFlug = new bool[3];
    private float[] starTime = new float[3];

    private GameObject clearDeta;
    private float[] maxDiffence = new float[4];
    private float[] currentDiffence = new float[4];
    private int life;
    public string name;


    private int useCount;
    // Start is called before the first frame update
    void Start()
    {
        clearDeta = GameObject.FindWithTag("ClearDeta");
        for(int i = 0; i < 4; i++)
        {
            maxDiffence[i] = clearDeta.GetComponent<ClearDeta>().saveData[i].x;
            currentDiffence[i] = clearDeta.GetComponent<ClearDeta>().saveData[i].y;
        }
        life = clearDeta.GetComponent<ClearDeta>().saveLife;
        name = clearDeta.GetComponent<ClearDeta>().name;
        Destroy(clearDeta);

        useCount = 0;
        Check();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 3; i++)
        {
            if(starActiveFlug[i] == false)
            {
                starTime[i] += Time.deltaTime;
                if (starTime[i] >= derayTime[i])
                {
                    starActiveFlug[i] = true;
                }
            }
        }


        for(int n = 0; n < 3; n++)
        {
            if(starActiveFlug[n] == true)
            {
                if (starFlug[n] == true)
                {
                    StarActive(star[n], n);
                }
            }
        }
        

    }

    private void Check()
    {
        if (currentDiffence[3] == 0)
        {
            starFlug[0] = true;
        }
        if (life == 100)
        {
            starFlug[1] = true;
        }
        for (int i = 0; i < 4; i++)
        {
            useCount += (int)currentDiffence[i];
        }

        switch (name)
        {
            case "Level1":
                quest1.text = starText[0];
                if(useCount <= useLimit[0])
                {
                    starFlug[2] = true;
                }
                break;
            case "Level2":
                quest1.text = starText[1];
                if (useCount <= useLimit[1])
                {
                    starFlug[2] = true;
                }
                break;
            case "Level3":
                quest1.text = starText[2];
                if (useCount <= useLimit[2])
                {
                    starFlug[2] = true;
                }
                break;
        }
    }

    private void StarActive(Image star, int i)
    {
        starAlpha[i] += alphaRatio;

        Color color = star.GetComponent<Image>().color;
        color.a = starAlpha[i];
        star.GetComponent<Image>().color = color;
    }
}
