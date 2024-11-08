using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;          // ��������I�u�W�F�N�g��Prefab
    public Camera mainCamera;                 // �J������ݒ�
    public float spawnDistance = 5.0f;        // �J��������̏�����������
    public float scrollSpeed = 2.0f;          // �z�C�[���X�N���[���̊��x
    public float spawnDistanceMin = 1.0f;     // �����̍ŏ��l�i�C���X�y�N�^�[�Őݒ�\�j
    public float spawnDistanceMax = 20.0f;    // �����̍ő�l�i�C���X�y�N�^�[�Őݒ�\�j
    public Material transparentMaterial;      // �������̃}�e���A��
    private Material originalMaterial;        // ���̃}�e���A����ۑ�
    private GameObject previewObject;         // ���̔������I�u�W�F�N�g
    private bool isPreviewing = false;        // ���̃I�u�W�F�N�g���\�������ǂ���

    void Update()
    {
        HandleMouseScroll(); // �}�E�X�z�C�[���ŋ�������

        if (Input.GetMouseButtonDown(0)) // ���N���b�N���m�F
        {
            if (!isPreviewing)
            {
                // ��x�ڂ̃N���b�N�ŉ��̔������I�u�W�F�N�g�𐶐�
                ShowPreview();
            }
            else
            {
                // ��x�ڂ̃N���b�N�ŃI�u�W�F�N�g���m��
                PlaceObject();
            }
        }

        if (isPreviewing && previewObject != null)
        {
            // ���̃I�u�W�F�N�g���}�E�X�ʒu�ɒǏ]
            UpdatePreviewPosition();
        }
    }

    void HandleMouseScroll()
    {
        // �}�E�X�z�C�[���̃X�N���[���ʂ��擾
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // �X�N���[�����͂ɉ�����spawnDistance�𒲐�
        spawnDistance += scrollInput * scrollSpeed;

        // �C���X�y�N�^�[�Őݒ肵�������̍ŏ��E�ő�l�ɐ���
        spawnDistance = Mathf.Clamp(spawnDistance, spawnDistanceMin, spawnDistanceMax);
    }

    void ShowPreview()
    {
        // ���̔������I�u�W�F�N�g�𐶐�
        previewObject = Instantiate(objectToSpawn);

        // ���̃}�e���A����ۑ����A�������}�e���A����K�p
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;  // ���̃}�e���A����ۑ�
            renderer.material = transparentMaterial; // �������}�e���A����K�p
        }

        isPreviewing = true;
    }

    void UpdatePreviewPosition()
    {
        // �J����������̋����ɂ���}�E�X�ʒu�̃��[���h���W���擾
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = spawnDistance;  // �J��������̋�����ݒ�

        // �J�����̃r���[�|�[�g���Ƀ}�E�X�ʒu�����邩�m�F
        Vector3 viewportPoint = mainCamera.ScreenToViewportPoint(mousePosition);
        if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
        {
            previewObject.SetActive(false); // �r���[�|�[�g�O�Ȃ��\���ɂ���
            return;
        }

        previewObject.SetActive(true); // �r���[�|�[�g���Ȃ�\������

        // �}�E�X�ʒu�Ɋ�Â��A���[���h���W�ŉ��̃I�u�W�F�N�g�̈ʒu��ݒ�
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        previewObject.transform.position = worldPosition;
        previewObject.transform.rotation = Quaternion.identity;
    }

    void PlaceObject()
    {
        // �r���[�|�[�g�O�ŃN���b�N�����ꍇ�͔z�u���Ȃ�
        if (!previewObject.activeSelf) return;

        // �m�肷�邽�߂Ɍ��̃}�e���A����߂��A��Ԃ����Z�b�g
        if (previewObject != null)
        {
            Renderer renderer = previewObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = originalMaterial;  // ���̃}�e���A���ɖ߂�
            }
        }
        previewObject = null;
        isPreviewing = false;
    }
}
