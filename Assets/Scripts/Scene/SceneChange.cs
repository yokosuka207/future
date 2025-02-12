using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [SerializeField] public string sceneName; // 遷移先のシーン名
    [SerializeField] private KeyCode transitionKey = KeyCode.None; // トリガーキー
    [SerializeField] private float fadeDuration = 1f; // フェードの長さ
    [SerializeField] private Image fadeImage; // フェード用のImage
    [SerializeField] private bool viewScene;
    [SerializeField] private float elapsedTime;
    [SerializeField] private bool titleScene;

    private bool isFading = false;
    private float currentTime;

    public static SceneChange instance;
    public bool sceneChangeFlug;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            // シーン開始時にフェードインを実行
            fadeImage.color = new Color(0f, 0f, 0f, 1f);
            StartCoroutine(FadeIn());
        }

        
    }

    public void SetSceneName(string newSceneName)
    {
        sceneName = newSceneName;
    }

    private void Update()
    {
        // 指定したキーが押された場合にシーン遷移を実行
        if (transitionKey != KeyCode.None && Input.GetKeyDown(transitionKey) && !isFading)
        {
            sceneChangeFlug = true;
        }

        //ロゴシーンの場合経過時間後シーンチェンジ
        if (viewScene == true)
        {
            currentTime += Time.deltaTime;

            if(currentTime >= elapsedTime)
            {
                sceneChangeFlug = true;
            }
        }

        //タイトルシーンの場合ボタン入力があったらシーンチェンジ
        if(titleScene == true)
        {
            if (Input.anyKeyDown && !isFading)
            {
                sceneChangeFlug = true;
            }
        }

        //シーンチェンジフラグが立つとシーンチェンジ
        if (sceneChangeFlug == true)
        {
            StartCoroutine(ChangeScene(sceneName));
        }

    }


    private IEnumerator FadeIn()
    {
        isFading = true;
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsedTime / fadeDuration); // 徐々に透明にする
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f; // 完全に透明に
        fadeImage.color = color;
        isFading = false;
    }

    public IEnumerator ChangeScene(string chengeSceneName)
    {
        Debug.Log("シーン遷移を実行します");
        isFading = true;
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // 徐々に不透明にする
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f; // 完全に不透明に
        fadeImage.color = color;

        // シーン遷移
        if (!string.IsNullOrEmpty(chengeSceneName))
        {
            
            SceneManager.LoadScene(chengeSceneName);
        }
        else
        {
            Debug.LogError("シーン名が指定されていません！");
        }
    }

    //他スクリプトからシーンチェンジを可能に
    public void PlayChengeScene(string selectSceneName)
    {
        StartCoroutine(ChangeScene(selectSceneName));
    }

}

