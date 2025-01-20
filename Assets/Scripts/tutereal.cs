using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutereal : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    [SerializeField] private Image black;
    [SerializeField] private Image tute1;
    [SerializeField] private Image tute2;
    [SerializeField] private Image tute3;
    [SerializeField] private Image tute4;
    [SerializeField] private Image tute5;

    [SerializeField] private float time1;
    [SerializeField] private float time2;
    [SerializeField] private float time3;
    [SerializeField] private float time4;
    [SerializeField] private float time5;



    private float timeCount;
    private bool stop1;
    private bool stop2;
    private bool stop3;
    private bool stop4;
    private bool stop5;
    private float mouseCount1;
    private float mouseCount2;
    private float mouseCount3;
    private float mouseCount4;
    private float mouseCount5;





    // Start is called before the first frame update
    void Start()
    {
        black.gameObject.SetActive(false);
        tute1.gameObject.SetActive(false);
        tute2.gameObject.SetActive(false);
        tute3.gameObject.SetActive(false);
        tute4.gameObject.SetActive(false);
        tute5.gameObject.SetActive(false);

        timeCount = 0;
        stop1 = false;
        stop2 = false;
        stop3 = false;
        stop4 = false;
        stop5 = false;

        mouseCount1 = 0;
        mouseCount2 = 0;
        mouseCount3 = 0;
        mouseCount4 = 0;
        mouseCount5 = 0;

    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if(timeCount > time1 && stop1 == false)
        {
            Time.timeScale = 0;
            black.gameObject.SetActive(true);
            tute1.gameObject.SetActive(true);
            gameManager.GetComponent<ObjectSpawner>().TutorialSet(true);

            if(Input.GetMouseButton(0))
            {
                mouseCount1 += 0.02f;

                if(mouseCount1 > 1.0f)
                {
                    Time.timeScale = 1;
                    black.gameObject.SetActive(false);
                    tute1.gameObject.SetActive(false);
                    stop1 = true;
                    gameManager.GetComponent<ObjectSpawner>().TutorialSet(false);
                }
            }
        }
        else if (timeCount > time2 && stop2 == false)
        {
            Time.timeScale = 0;
            black.gameObject.SetActive(true);
            tute2.gameObject.SetActive(true);
            gameManager.GetComponent<ObjectSpawner>().TutorialSet(true);

            if (Input.GetMouseButton(0))
            {
                mouseCount2 +=  0.02f;

                if (mouseCount2 > 1.0f)
                {
                    Time.timeScale = 1;
                    black.gameObject.SetActive(false);
                    tute2.gameObject.SetActive(false);
                    stop2 = true;
                    gameManager.GetComponent<ObjectSpawner>().TutorialSet(false);
                }
            }
        }
        else if (timeCount > time3 && stop3 == false)
        {
            Time.timeScale = 0;
            black.gameObject.SetActive(true);
            tute3.gameObject.SetActive(true);
            gameManager.GetComponent<ObjectSpawner>().TutorialSet(true);

            if (Input.GetMouseButton(0))
            {
                mouseCount3 += 0.02f;

                if (mouseCount3 > 1.0f)
                {
                    Time.timeScale = 1;
                    black.gameObject.SetActive(false);
                    tute3.gameObject.SetActive(false);
                    stop3 = true;
                    gameManager.GetComponent<ObjectSpawner>().TutorialSet(false);
                }
            }
        }
        else if (timeCount > time4 && stop4 == false)
        {
            Time.timeScale = 0;
            black.gameObject.SetActive(true);
            tute4.gameObject.SetActive(true);
            gameManager.GetComponent<ObjectSpawner>().TutorialSet(true);

            if (Input.GetMouseButton(0))
            {
                mouseCount4 += 0.02f;

                if (mouseCount4 > 1.0f)
                {
                    Time.timeScale = 1;
                    black.gameObject.SetActive(false);
                    tute4.gameObject.SetActive(false);
                    stop4 = true;
                    gameManager.GetComponent<ObjectSpawner>().TutorialSet(false);
                }
            }
        }
        else if (timeCount > time5 && stop5 == false)
        {
            Time.timeScale = 0;
            black.gameObject.SetActive(true);
            tute5.gameObject.SetActive(true);
            gameManager.GetComponent<ObjectSpawner>().TutorialSet(true);

            if (Input.GetMouseButton(0))
            {
                mouseCount5 += 0.02f;

                if (mouseCount5 > 1.0f)
                {
                    Time.timeScale = 1;
                    black.gameObject.SetActive(false);
                    tute5.gameObject.SetActive(false);
                    stop5 = true;
                    gameManager.GetComponent<ObjectSpawner>().TutorialSet(false);
                }
            }
        }
    }
}
