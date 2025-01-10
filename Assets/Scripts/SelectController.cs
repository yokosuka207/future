using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{

    [SerializeField] private Image level1;
    [SerializeField] private Image level2;
    [SerializeField] private Image level3;
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private GameObject sceneChenge;

    private int borderSituation;//0 ��/1 ��/2 ��
    private int borderSituationOld;
    private float borderPosition;//�{�[�_�[�̃|�W�V����

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�X�N���[���T�C�Y�ɂ���ă{�[�_�[���X�V����
        borderPosition = Screen.height / 3;
        float plusBorder = Screen.height * 20 / 650;

        //�O�t���[���̏�Ԃ�ۑ�
        borderSituationOld = borderSituation;

        //�}�E�X�|�C���^���{�[�_�[���C���𒴂��Ă������Ԃ��C��
        if (Input.mousePosition.y >= borderPosition * 2 - plusBorder && borderSituationOld != 0)
        {
            borderSituation = 0;
        }
        else if (Input.mousePosition.y <= borderPosition * 1 + plusBorder && borderSituationOld != 2)
        {
            borderSituation = 2;
        }
        else if (Input.mousePosition.y > borderPosition * 1 + plusBorder && Input.mousePosition.y < borderPosition * 2  - plusBorder && borderSituationOld != 1)
        {
            borderSituation = 1;
        }

        //�{�[�_�[���C������Ă�����摜�̃A�N�e�B�u��؂�ւ�
        if(borderSituation != borderSituationOld)
        {
            switch (borderSituation)
            {
                case 0:
                    level1.gameObject.SetActive(false);
                    level2.gameObject.SetActive(true);
                    level3.gameObject.SetActive(true);
                    break;
                case 1:
                    level1.gameObject.SetActive(true);
                    level2.gameObject.SetActive(false);
                    level3.gameObject.SetActive(true);
                    break;
                case 2:
                    level1.gameObject.SetActive(true);
                    level2.gameObject.SetActive(true);
                    level3.gameObject.SetActive(false);
                    break;
            }

            audioSource.PlayOneShot(selectSound);
        }

        if(Input.GetMouseButtonDown(0))
        {
            switch (borderSituation)
            {
                case 0:
                    sceneChenge.GetComponent<SceneChange>().PlayChengeScene("Level1");
                    break;
                case 1:
                    sceneChenge.GetComponent<SceneChange>().PlayChengeScene("Level2");
                    break;
                case 2:
                    sceneChenge.GetComponent<SceneChange>().PlayChengeScene("Level3");
                    break;
            }

        }
    }
}
