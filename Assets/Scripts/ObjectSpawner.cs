using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;       // ��������I�u�W�F�N�g�̔z��
    public CameraControll cameraController;              // �J�����؂�ւ��X�N���v�g
    public float spawnDistance = 5.0f;
    public float scrollSpeed = 2.0f;
    public float spawnDistanceMin = 1.0f;
    public float spawnDistanceMax = 20.0f;
    public ObjectPlacementLimit[] placementLimits; // �I�u�W�F�N�g���Ƃ̐ݒ�
    public GameObject diffenceUI;

    private Material originalMaterial;
    private GameObject previewObject;        // ���u���I�u�W�F�N�g
    private bool isPreviewing = false;       // ���u����ԃt���O
    private Vector3 previewWorldPosition;    // ���u���I�u�W�F�N�g�̃��[���h���W
    private int currentObjectIndex = 0;      // ���݂̃I�u�W�F�N�g�C���f�b�N�X
    private Dictionary<GameObject, ObjectPlacementLimit> placementLimitsDict;   // �����ɕϊ����ĊǗ�


    [System.Serializable]
    public class ObjectPlacementLimit
    {
        public GameObject objectType;  // �I�u�W�F�N�g�̎��
        public int maxCount;          // ���̎�ނ̍ő�ݒu��
        public int currentCount;      // ���݂̐ݒu��
    }


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

        // ���u�������� previewObject �� null �łȂ��ꍇ�̂ݍX�V
        if (isPreviewing && previewObject != null)
        {
            UpdatePreviewPosition();
        }
    }


    void Start()
    {
        placementLimitsDict = new Dictionary<GameObject, ObjectPlacementLimit>();

        // �����I�u�W�F�N�g�𐔎��L�[�u1�v�ɑΉ�
        if (objectsToSpawn != null && objectsToSpawn.Length > 0)
        {
            currentObjectIndex = 0; // �z��̍ŏ��̃I�u�W�F�N�g��ݒ�
        }

        // �z�񂩂玫���ɕϊ�
        foreach (var limit in placementLimits)
        {
            if (limit != null && limit.objectType != null)
            {
                placementLimitsDict[limit.objectType] = limit;
                limit.currentCount = 0; // ������
            }
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
        // ���ݑI�𒆂̃I�u�W�F�N�g���擾
        GameObject currentObject = objectsToSpawn[currentObjectIndex];

        // �ݒu���������m�F
        if (placementLimitsDict.TryGetValue(currentObject, out ObjectPlacementLimit limit))
        {
            if (limit.currentCount >= limit.maxCount)
            {
                isPreviewing = false; // ���u����Ԃ�����
                return; // ���u���I�u�W�F�N�g�𐶐����Ȃ�
            }
        }

        // ���u���I�u�W�F�N�g�𐶐�
        previewObject = Instantiate(currentObject);

        // �������}�e���A����ݒ�
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;

            // �����������邽�߂̃}�e���A�����쐬
            Material transparentMat = new Material(originalMaterial);
            Color color = transparentMat.color;
            color.a = 0.5f; // �A���t�@�l��ύX���Ĕ�������
            transparentMat.color = color;
            renderer.material = transparentMat;
        }

        isPreviewing = true; // ���u����Ԃ�ݒ�
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
        GameObject currentObject = objectsToSpawn[currentObjectIndex];

        if (placementLimitsDict.TryGetValue(currentObject, out ObjectPlacementLimit limit))
        {
            // �ő吔�𒴂��Ă���ꍇ�͐ݒu�𖳌���
            if (limit.currentCount >= limit.maxCount)
            {
                return;
            }

            // �J�E���g���X�V
            placementLimits[currentObjectIndex].currentCount++;

        }

        if (!previewObject.activeSelf) return;

        // �m��z�u���Ƀ}�e���A�������ɖ߂�
        Renderer renderer = previewObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = originalMaterial;
        }

        //UI�̐��l���X�V
        diffenceUI.GetComponent<UIDiffenceCount>().CheckDiffenceNum(currentObjectIndex);

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

    public void ResetPlacementCounts()
    {
        foreach (var limit in placementLimits)
        {
            limit.currentCount = 0;
        }
    }

}
