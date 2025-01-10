using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private Image button1;
    [SerializeField] private Image button2;
    [SerializeField] private Image button3;
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private GameObject sceneChenge;
    [SerializeField] private float buttonMove;

    private int borderSituation;//0 ��/1 ��/2 �E
    private int borderSituationOld;
    private float borderPosition;//�{�[�_�[�̃|�W�V����

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        borderSituation = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //�X�N���[���T�C�Y�ɂ���ă{�[�_�[���X�V����
        borderPosition = Screen.width / 3;
        float plusBorder = Screen.width * 10 / 650;

        //�O�t���[���̏�Ԃ�ۑ�
        borderSituationOld = borderSituation;



        //�}�E�X�|�C���^���{�[�_�[���C���𒴂��Ă������Ԃ��C��
        if (Input.mousePosition.y <= Screen.height / 5 * 2 + buttonMove && Input.mousePosition.y > Screen.height / 5 * 1 + buttonMove)
        {
            if (Input.mousePosition.x >= borderPosition * 2 - plusBorder && borderSituationOld != 2)
            {
                borderSituation = 2;
            }
            else if (Input.mousePosition.x <= borderPosition * 1 + plusBorder && borderSituationOld != 0)
            {
                borderSituation = 0;
            }
            else if (Input.mousePosition.x > borderPosition * 1 + plusBorder && Input.mousePosition.x < borderPosition * 2 - plusBorder && borderSituationOld != 1)
            {
                borderSituation = 1;
            }
        }


        //�{�[�_�[���C������Ă�����摜�̃A�N�e�B�u��؂�ւ�
        if (borderSituation != borderSituationOld)
        {
            switch (borderSituation)
            {
                case 0:
                    button1.gameObject.SetActive(false);
                    button2.gameObject.SetActive(true);
                    button3.gameObject.SetActive(true);
                    break;
                case 1:
                    button1.gameObject.SetActive(true);
                    button2.gameObject.SetActive(false);
                    button3.gameObject.SetActive(true);
                    break;
                case 2:
                    button1.gameObject.SetActive(true);
                    button2.gameObject.SetActive(true);
                    button3.gameObject.SetActive(false);
                    break;
            }

            audioSource.PlayOneShot(selectSound);
        }

        if (Input.mousePosition.y <= Screen.height / 5 * 2 + buttonMove && Input.mousePosition.y > Screen.height / 5 * 1 + buttonMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
                switch (borderSituation)
                {
                    case 0:
                        sceneChenge.GetComponent<SceneChange>().PlayChengeScene(SceneManager.GetActiveScene().name);
                        break;
                    case 1:
                        sceneChenge.GetComponent<SceneChange>().PlayChengeScene("LevelSelect");
                        break;
                    case 2:
                        sceneChenge.GetComponent<SceneChange>().PlayChengeScene("Title");
                        break;
                }
                
            }
        }
        else
        {
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);
        }
    }
}
