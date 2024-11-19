using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Image[] menuItems; // ���ډ摜�i�c�ɕ��ׂ�j
    [SerializeField] private Vector2[] menuItemPositions; // �e���ڂ�XY���W
    [SerializeField] private Image selectorFrame; // �I��g
    [SerializeField] private string[] scenes; // �e���ڂőJ�ڂ���V�[����
    [SerializeField] private float selectorOffset = 10f; // �g�̃}�[�W��
    [SerializeField] private KeyCode upKey = KeyCode.UpArrow; // ��ړ��L�[
    [SerializeField] private KeyCode downKey = KeyCode.DownArrow; // ���ړ��L�[
    [SerializeField] private KeyCode selectKey = KeyCode.Return; // �I���L�[

    private int currentIndex = 0; // ���݂̑I���C���f�b�N�X
    private SceneChange sceneChange; // �t�F�[�h�t���V�[���J��

    private void Start()
    {
        if (menuItems.Length != scenes.Length || menuItems.Length != menuItemPositions.Length)
        {
            Debug.LogError("���ډ摜�A�V�[�����A�܂��͍��W�̐�����v���Ă��܂���I");
            return;
        }

        // �e���ڂ̍��W��ݒ�
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (menuItems[i] != null)
            {
                menuItems[i].rectTransform.anchoredPosition = menuItemPositions[i];
            }
        }

        // �����ʒu�ɑI��g��ݒ�
        UpdateSelectorPosition();

        // SceneChange�R���|�[�l���g���擾
        sceneChange = GetComponent<SceneChange>();
    }

    private void Update()
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
            if (sceneChange != null && !string.IsNullOrEmpty(scenes[currentIndex]))
            {
                sceneChange.SetSceneName(scenes[currentIndex]); // �J�ڐ�V�[����ݒ�
                //sceneChange.ChangeScene(); // �t�F�[�h�t���V�[���J��
            }
        }
    }

    // �I��g�̈ʒu���X�V����
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
}