using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    [SerializeField] public string sceneName; // 遷移先のシーン名
    // Start is called before the first frame update
    void Start()
    {
        // シーン遷移
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("シーン名が指定されていません！");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
