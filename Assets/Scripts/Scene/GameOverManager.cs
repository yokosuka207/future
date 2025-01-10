using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private KeyCode ActivateKey = KeyCode.None; // �����{�^��
    [SerializeField] private KeyCode upKey = KeyCode.UpArrow; // ��ړ��L�[
    [SerializeField] private KeyCode downKey = KeyCode.DownArrow; // ���ړ��L�[
    [SerializeField] private KeyCode selectKey = KeyCode.Return; // �I���L�[
    [SerializeField] private Image[] menuItems; // ���ډ摜�i�c�ɕ��ׂ�j
    [SerializeField] private Vector2[] menuItemPositions; // �e���ڂ�XY���W
    [SerializeField] private Image selectorFrame; // �I��g
    [SerializeField] private string[] scenes; // �e���ڂőJ�ڂ���V�[����
    [SerializeField] private float selectorOffset = 10f; // �g�̃}�[�W��

    private int currentIndex = 0; // ���݂̑I���C���f�b�N�X
    private SceneChange sceneChange; // �t�F�[�h�t���V�[���J��

    public int HP = 100; // �v���C���[��HP
    public GameObject gameOverUI; // �Q�[���I�[�o�[UI
    public Image blackScreen; // �����w�i�iImage�j
    public Slider gauge; // �Q�[�W (Slider)
    public int time = 0; // ���ԕϐ� (0�`100)

    private bool isGameOver = false;
    private bool isFading = false;

    private void Start()
    {

        // �e���ڂ̍��W��ݒ�
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (menuItems[i] != null)
            {
                menuItems[i].rectTransform.anchoredPosition = menuItemPositions[i];
            }
            else
            {
                Debug.LogWarning($"menuItems[{i}] ���ݒ肳��Ă��܂���");
            }
        }

        // �����ʒu�ɑI��g��ݒ�
        if (selectorFrame != null)
        {
            UpdateSelectorPosition();
        }
        else
        {
            Debug.LogWarning("selectorFrame ���ݒ肳��Ă��܂���");
        }


    }
    void Update()
    {
        // HP��0�ȉ��ŃQ�[���I�[�o�[����x�������s
        if (HP <= 0 && !isGameOver)
        {
            GameOver();
        }

        if (isGameOver)
        {
            HandleGameOverInput();
            // SceneChange�R���|�[�l���g���擾
            sceneChange = GetComponent<SceneChange>();
        }
        else if (!isGameOver)
        {
            // �Q�[�W���i�s���� (���Ԃ𑝉�)
            time = Mathf.Clamp(time + 1, 0, 60000);
            //gauge.value = time / 100f;
            
        }

        //�������u�@HP�Ǘ��n�����I�������������
        if (ActivateKey != KeyCode.None && Input.GetKeyDown(ActivateKey) && !isFading)
        {
            HP = 0;
        }

    }

    void GameOver()
    {
        if (blackScreen == null || gameOverUI == null || gauge == null)
        {
            Debug.LogError("�K�v�ȃI�u�W�F�N�g���ݒ肳��Ă��܂���");
            return;
        }
        isGameOver = true;

        // �Q�[����ʂ��ꎞ��~
        Time.timeScale = 0;

        // ������ʂ��t�F�[�h�C��
        StartCoroutine(FadeInBlackScreen());

        // �Q�[�W���Œ肵�A�Q�[���I�[�o�[UI��L����
        gameOverUI.SetActive(true);
        gauge.value = time / 100f;
       
    }

    // ������ʂ��t�F�[�h�C��
    IEnumerator FadeInBlackScreen()
    {
        float alpha = 0;
        Color color = blackScreen.color;

        while (alpha < 0.65f)
        {
            alpha += Time.unscaledDeltaTime / 1; // 2�b�Ńt�F�[�h�C��
            color.a = alpha;
            blackScreen.color = color;
            yield return null;
        }

        // �ŏI�I�ɃA���t�@�l��0.5�ɐݒ�
        color.a = 0.65f;
        blackScreen.color = color;

    }

    private void UpdateSelectorPosition()
    {
        if (selectorFrame != null && menuItems[currentIndex] != null)
        {
            RectTransform menuItemRect = menuItems[currentIndex].rectTransform;
            RectTransform selectorRect = selectorFrame.rectTransform;

            // �I��g�����ډ摜�ɍ��킹�Ĉړ�
            selectorRect.anchoredPosition = menuItemRect.anchoredPosition;

            // �I��g�̃T�C�Y�����ډ摜�ɍ��킹�Ē���
            selectorRect.sizeDelta = menuItemRect.sizeDelta + new Vector2(selectorOffset, selectorOffset);
        }
    }

    private void HandleGameOverInput()
    {
        // �㉺�L�[�őI�����ڂ�ύX
        if (Input.GetKeyDown(upKey))
        {
            Debug.Log("���ڂ̑I������Ɉړ����܂�");
            currentIndex = (currentIndex - 1 + menuItems.Length) % menuItems.Length; // �z����
            UpdateSelectorPosition();
        }
        else if (Input.GetKeyDown(downKey))
        {
            Debug.Log("���ڂ̑I�������Ɉړ����܂�");
            currentIndex = (currentIndex + 1) % menuItems.Length; // �z����
            UpdateSelectorPosition();
        }

        // Enter�L�[�őI�����ڂɑΉ�����V�[�������[�h
        if (Input.GetKeyDown(selectKey))
        {
            Debug.Log("���ڂ�I�����܂���");
            Time.timeScale = 1;
            if (sceneChange != null && !string.IsNullOrEmpty(scenes[currentIndex]))
            {
                sceneChange.SetSceneName(scenes[currentIndex]); // �J�ڐ�V�[����ݒ�
                sceneChange.ChangeScene(scenes[currentIndex]); // �t�F�[�h�t���V�[���J�ڂ����s
            }
        }
    }
}
