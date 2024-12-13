using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCollider : MonoBehaviour
{
    private float growthRate;
    private float maxGrowthRate;
    private float growthAcceleration;
    private float growthDeceleration;
    private float maxScale;

    [SerializeField] private int damage = 10; // �_���[�W��
    [SerializeField] private float minGrowthRate = 0.0001f; // �ŏ��g�呬�x
    [SerializeField] private float shrinkRate = 100.0f; // �k�����x
    [SerializeField] private float minScale = 1.0f; // �ŏ��X�P�[��
    [SerializeField] private float accelerationBoost = 100.0f; // ���������ňꎞ�I�ɑ�������g�呬�x

    private SphereCollider sphereCollider;
    private float currentGrowthRate; // ���݂̊g�呬�x
    private bool isShrinking = false; // �k�������ǂ����̃t���O

    // �_���[�W�ʂ�Ԃ��v���p�e�B
    public int Damage()
    {
        return damage;
    }

    private void SyncColliderToScale()
    {
        if (sphereCollider != null)
        {
            sphereCollider.radius = Mathf.Round(transform.localScale.x / 1.0f * 1000f) / 1000f; // �����_�ȉ�3���Ŋۂ߂�
        }
    }

    private void Start()
    {
        // SphereCollider���擾
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            Debug.LogError("ShockwaveCollider requires a SphereCollider.");
        }
    }

    private void Update()
    {
        if (isShrinking)
        {
            Shrink();
        }
        else
        {
            Expand();
        }
    }

    private void Expand()
    {
        // �g�呬�x�������܂��͌����i����Ɖ����̊ԂŐ���j
        if (currentGrowthRate < maxGrowthRate)
        {
            currentGrowthRate += growthAcceleration * Time.deltaTime;
        }
        else if (currentGrowthRate > 0)
        {
            currentGrowthRate -= growthDeceleration * Time.deltaTime;
        }

        // Shockwave�̃X�P�[�������X�Ɋg��
        if (transform.localScale.x < maxScale)
        {
            float scaleIncrement = currentGrowthRate * Time.deltaTime;
            transform.localScale += new Vector3(scaleIncrement, scaleIncrement, scaleIncrement);
        }
        else
        {
            isShrinking = true;
        }
    }

    internal void Initialize(ShockwaveSettings settings)
    {
        this.growthRate = settings.growthRate;
        this.maxGrowthRate = settings.maxGrowthRate;
        this.growthAcceleration = settings.growthAcceleration;
        this.growthDeceleration = settings.growthDeceleration;
        this.maxScale = settings.maxScale;

        this.currentGrowthRate = this.growthRate;
    }

    private void Shrink()
    {
        if (transform.localScale.x > minScale)
        {
            float scaleDecrement = shrinkRate * Time.deltaTime;
            transform.localScale -= new Vector3(scaleDecrement, scaleDecrement, scaleDecrement);

            SyncColliderToScale(); // �X�P�[���ƃR���C�_�[�𓯊�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Player"�^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ
        if (other.CompareTag("Player"))
        {
            Debug.Log("Shockwave hit the Player!");
            
        }

        // "Diffence"�^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ
        if (other.CompareTag("Diffence"))
        {
            Debug.Log("Shockwave hit a Diffence and will be destroyed.");
            Destroy(gameObject);
        }

        // "Acceleration"�^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ�ɃX�P�[����������
        if (other.CompareTag("Acceleration"))
        {
            AccelerateScale();
        }

        // "Deceleration"�^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ�ɃX�P�[����������
        if (other.CompareTag("Deceleration"))
        {
            DecelerateScale();
        }

        // "ShrinkTrigger"�^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ�ɏk���J�n
        if (other.CompareTag("ShrinkTrigger"))
        {
            Debug.Log("Shockwave collided with a ShrinkTrigger and will start shrinking.");
            isShrinking = true; // �k�����[�h��L���ɂ���
        }
    }

    // �X�P�[����������
    private void AccelerateScale()
    {
        currentGrowthRate += accelerationBoost;

        // ����𒴂��Ȃ��悤�ɒ���
        if (currentGrowthRate > maxGrowthRate)
        {
            currentGrowthRate = maxGrowthRate;
        }

        Debug.Log($"Scale growth rate temporarily boosted: {currentGrowthRate}");
    }

    // �X�P�[����������
    private void DecelerateScale()
    {
        currentGrowthRate -= accelerationBoost;

        // �����������Ȃ��悤�ɒ���
        if (currentGrowthRate < minGrowthRate)
        {
            currentGrowthRate = minGrowthRate;
        }

        Debug.Log($"Scale growth rate temporarily reduced: {currentGrowthRate}");
    }

}