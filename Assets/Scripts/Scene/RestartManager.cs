using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    [SerializeField] public string sceneName; // �J�ڐ�̃V�[����
    // Start is called before the first frame update
    void Start()
    {
        // �V�[���J��
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("�V�[�������w�肳��Ă��܂���I");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
