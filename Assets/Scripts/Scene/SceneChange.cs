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
            sceneChangeFlug = true;
        }

        //���S�V�[���̏ꍇ�o�ߎ��Ԍ�V�[���`�F���W
        if (viewScene == true)
        {
            currentTime += Time.deltaTime;

            if(currentTime >= elapsedTime)
            {
                sceneChangeFlug = true;
            }
        }

        //�^�C�g���V�[���̏ꍇ�{�^�����͂���������V�[���`�F���W
        if(titleScene == true)
        {
            if (Input.anyKeyDown && !isFading)
            {
                sceneChangeFlug = true;
            }
        }

        //�V�[���`�F���W�t���O�����ƃV�[���`�F���W
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
            color.a = 1f - Mathf.Clamp01(elapsedTime / fadeDuration); // ���X�ɓ����ɂ���
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f; // ���S�ɓ�����
        fadeImage.color = color;
        isFading = false;
    }

    public IEnumerator ChangeScene(string chengeSceneName)
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
        if (!string.IsNullOrEmpty(chengeSceneName))
        {
            
            SceneManager.LoadScene(chengeSceneName);
        }
        else
        {
            Debug.LogError("�V�[�������w�肳��Ă��܂���I");
        }
    }

    //���X�N���v�g����V�[���`�F���W���\��
    public void PlayChengeScene(string selectSceneName)
    {
        StartCoroutine(ChangeScene(selectSceneName));
    }

}

