using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    private string sceneName; // �J�ڐ�̃V�[������Inspector�Ŏw��

    [SerializeField]
    private KeyCode transitionKey = KeyCode.None; // �V�[���J�ڂ��g���K�[����L�[��Inspector�Ŏw��

    public static SceneChange instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("�V�[�������w�肳��Ă��܂���I");
        }
    }

    private void Update()
    {
        // �w�肵���L�[�������ꂽ�ꍇ�ɃV�[���J�ڂ����s
        if (transitionKey != KeyCode.None && Input.GetKeyDown(transitionKey))
        {
            ChangeScene();
        }
    }
}