using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;       // ��������I�u�W�F�N�g�̔z��
    public CameraControll cameraController;              // �J�����؂�ւ��X�N���v�g
    public float spawnDistance = 5.0f;
    public float scrollSpeed = 2.0f;
    public float spawnDistanceMin = 1.0f;
    public float spawnDistanceMax = 20.0f;

    private Material originalMaterial;
    private GameObject previewObject;        // ���u���I�u�W�F�N�g
    private bool isPreviewing = false;       // ���u����ԃt���O
    private Vector3 previewWorldPosition;    // ���u���I�u�W�F�N�g�̃��[���h���W
    private int currentObjectIndex = 0;      // ���݂̃I�u�W�F�N�g�C���f�b�N�X

    void Update()
    {
        HandleObjectSwitch(); // �e���L�[���͂ŃI�u�W�F�N�g��؂�ւ�
        HandleMouseScroll();

        if (Input.GetMouseButtonDown(0))
        {
            if (!isPreviewing)
            {
                ShowPreview();
            }
            else
            {
                PlaceObject();
            }
        }

        if (isPreviewing && previewObject != null)
        {
            UpdatePreviewPosition();
        }
    }

    void Start()
    {
        // �����I�u�W�F�N�g�𐔎��L�[�u1�v�ɑΉ�
        if (objectsToSpawn != null && objectsToSpawn.Length > 0)
        {
            currentObjectIndex = 0; // �z��̍ŏ��̃I�u�W�F�N�g��ݒ�
        }
    }

    void HandleObjectSwitch()
    {
        // �z�񂪋�̏ꍇ�͏������Ȃ�
        if (objectsToSpawn == null || objectsToSpawn.Length == 0)
        {
            return;
        }

        // �����L�[�Ő؂�ւ�
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            // �����L�[1���C���f�b�N�X0�ɑΉ�
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentObjectIndex = i;

                // ���u���I�u�W�F�N�g�����݂��Ă���ꍇ�͍Đ���
                if (isPreviewing)
                {
                    Destroy(previewObject);
                    ShowPreview();
                }
            }
        }
    }



    void HandleMouseScroll()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        spawnDistance += scrollInput * scrollSpeed;
        spawnDistance = Mathf.Clamp(spawnDistance, spawnDistanceMin, spawnDistanceMax);
    }

    void ShowPreview()
    {
        // ���݂̃I�u�W�F�N�g�����u���Ƃ��Đ���
        previewObject = Instantiate(objectsToSpawn[currentObjectIndex]);

        // �v���n�u�̌��̃}�e���A�����擾���ĐF��ύX
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material; // ���̃}�e���A����ۑ�

            // �V�����}�e���A�����쐬���Č��̐F�𔼓����ɕύX
            Material previewMaterial = new Material(originalMaterial);
            Color originalColor = originalMaterial.color; // ���̐F���擾
            originalColor.a = 0.5f; // �������ɐݒ�
            previewMaterial.color = originalColor;

            renderer.material = previewMaterial; // ���u���I�u�W�F�N�g�ɐݒ�
        }

        isPreviewing = true;
    }


    void UpdatePreviewPosition()
    {
        // ���݂̃A�N�e�B�u�ȃJ�������擾
        Camera activeCamera = cameraController.ActiveCamera;

        // �}�E�X�ʒu���X�N���[�����W�Ƃ��Ď擾
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = spawnDistance; // �J��������̋�����ݒ�

        // �X�N���[�����W�����[���h���W�ɕϊ�
        previewWorldPosition = activeCamera.ScreenToWorldPoint(mousePosition);

        // ���u���I�u�W�F�N�g�̈ʒu���X�V
        previewObject.transform.position = previewWorldPosition;
        previewObject.transform.rotation = Quaternion.identity;
    }

    void PlaceObject()
    {
        if (!previewObject.activeSelf) return;

        // �m��z�u���Ƀ}�e���A�������ɖ߂�
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = originalMaterial;
        }

        previewObject = null;
        isPreviewing = false;
    }

    public void RefreshPreviewObject()
    {
        if (isPreviewing && previewObject != null)
        {
            UpdatePreviewPosition();
        }
    }
}
