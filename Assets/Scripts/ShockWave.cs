using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public float startDelay = 3.0f;           // �J�n�܂ł̒x�����ԁi�b�j
    public float initialRadius = 1.0f;        // �����̔��a
    public float maxRadius = 5.0f;            // �ő�̔��a
    public float expansionSpeed = 1.0f;       // ���a�̊g�呬�x
    public int durability = 100;              // �ϋv�l
    public int decayRate = 1;                 // ���Ԍo�߂ɂ�錸���l

    private SphereCollider outerCollider;     // �O�������̃R���C�_�[
    private float currentRadius;              // ���݂̔��a
    private float timeElapsed;                // �o�ߎ���
    private bool isExpanding = false;         // �g��J�n�t���O

    private void Start()
    {
        // ������
        currentRadius = initialRadius;
        timeElapsed = 0f;

        // �R���C�_�[��ݒ�
        outerCollider = gameObject.AddComponent<SphereCollider>();
        outerCollider.isTrigger = true;
        outerCollider.radius = initialRadius;

        // �����ڂ̃X�P�[�����������a�ɍ��킹��
        transform.localScale = Vector3.one * initialRadius * 2;

        // �g��J�n�̒x���^�C�}�[��ݒ�
        Invoke(nameof(StartExpansion), startDelay);
    }

    private void StartExpansion()
    {
        isExpanding = true; // �g��J�n�t���O�𗧂Ă�
    }

    private void Update()
    {
        // �g�傪�J�n����Ă��Ȃ��ꍇ�͏������~
        if (!isExpanding)
            return;
        // ���Ԍo�߂őϋv�l������
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 1.0f)
        {
            durability -= decayRate;
            timeElapsed = 0f;
        }

        // ���̊g��
        if (currentRadius < maxRadius)
        {
            currentRadius += expansionSpeed * Time.deltaTime;
            outerCollider.radius = currentRadius;

            // �I�u�W�F�N�g�̃X�P�[�����X�V
            transform.localScale = Vector3.one * currentRadius * 2;
        }

        // �ϋv�l���[���ɂȂ��������
        if (durability <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)//�ڐG���̏���
    {

    }
}
