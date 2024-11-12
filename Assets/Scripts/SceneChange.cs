using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    private string sceneName; // 遷移先のシーン名をInspectorで指定

    [SerializeField]
    private KeyCode transitionKey = KeyCode.None; // シーン遷移をトリガーするキーをInspectorで指定

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
            Debug.LogError("シーン名が指定されていません！");
        }
    }

    private void Update()
    {
        // 指定したキーが押された場合にシーン遷移を実行
        if (transitionKey != KeyCode.None && Input.GetKeyDown(transitionKey))
        {
            ChangeScene();
        }
    }
}