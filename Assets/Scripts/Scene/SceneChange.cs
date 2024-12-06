using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [SerializeField] public string sceneName; // �J�ڐ�̃V�[����
    [SerializeField] private KeyCode transitionKey = KeyCode.None; // �g���K�[�L�[
    [SerializeField] private float fadeDuration = 1f; // �t�F�[�h�̒���
    [SerializeField] private Image fadeImage; // �t�F�[�h�p��Image

    private bool isFading = false;

    public static SceneChange instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (fadeImage != null)
        {
            // �V�[���J�n���Ƀt�F�[�h�C�������s
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
        // �w�肵���L�[�������ꂽ�ꍇ�ɃV�[���J�ڂ����s
        if (transitionKey != KeyCode.None && Input.GetKeyDown(transitionKey) && !isFading)
        {
            StartCoroutine(ChangeScene());
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
            color.a = 1f - Mathf.Clamp01(elapsedTime / fadeDuration); // ���X�ɓ����ɂ���
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f; // ���S�ɓ�����
        fadeImage.color = color;
        isFading = false;
    }

    public IEnumerator ChangeScene()
    {
        Debug.Log("�V�[���J�ڂ����s���܂�");
        isFading = true;
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // ���X�ɕs�����ɂ���
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f; // ���S�ɕs������
        fadeImage.color = color;

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

}

