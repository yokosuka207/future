using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // ��������^�[�Q�b�g�i�v���C���[�j
    public float distance = 10f; // �^�[�Q�b�g����̋���
    public float rotationSpeed = 50f; // ��]���x

    public Camera mainCamera; // ���C���J����
    public Camera subCamera; // ���C���J����
    public Camera orbitCamera; // �T�u�J�����i���̃X�N���v�g���A�^�b�`����Ă���J�����j

    private float currentAngle = 0f; // ���݂̉�]�p�x
    private bool hasSwitched = false; // �J�����؂�ւ����s��ꂽ��

    void Start()
    {
        // �T�u�J������L�������A���C���J�����𖳌����i�����ݒ�j
        if (mainCamera != null)
        {
            mainCamera.enabled = false;
        }
        if (subCamera != null)
        {
            subCamera.enabled = false;
        }
        if (orbitCamera != null)
        {
            orbitCamera.enabled = true;
        }
    }

    void Update()
    {
        // �����A�J�����؂�ւ�
        if (currentAngle >= 360f && !hasSwitched)
        {
            SwitchToMainCamera();
            return; // �����I��
        }

        // ���Ԃɉ����ăJ��������]
        currentAngle += rotationSpeed * Time.deltaTime;

        // ����i360�x�j�𒴂�����A�l�����Z�b�g�i�ߏ�ȑ�����h���j
        if (currentAngle >= 360f)
        {
            currentAngle = 360f;
        }

        float radians = currentAngle * Mathf.Deg2Rad;

        // �V�����J�����̈ʒu���v�Z
        Vector3 newPosition = new Vector3(
            target.position.x + Mathf.Sin(radians) * distance,
            target.position.y,
            target.position.z + Mathf.Cos(radians) * distance
        );

        // �J������V�����ʒu�Ɉړ����A�^�[�Q�b�g������
        transform.position = newPosition;
        transform.LookAt(target);
    }

    void SwitchToMainCamera()
    {
        // ���C���J������L�������A�T�u�J�����𖳌���
        if (mainCamera != null)
        {
            mainCamera.enabled = true;
        }
        if (subCamera != null)
        {
            subCamera.enabled = true;
        }
        if (orbitCamera != null)
        {
            orbitCamera.enabled = false;
        }

        hasSwitched = true; // �؂�ւ��ς݃t���O��ݒ�
    }
}
