using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ShockwaveCollider : MonoBehaviour
{
    private float growthRate;
    private float maxGrowthRate;
    private float growthAcceleration;
    private float growthDeceleration;
    private float maxScale;

    [SerializeField] private int damage = 20; // �_���[�W��
    [SerializeField] private float minGrowthRate = 0.0001f; // �ŏ��g�呬�x
    [SerializeField] private float shrinkRate = 100.0f; // �k�����x
    [SerializeField] private float minScale = 1.0f; // �ŏ��X�P�[��
    [SerializeField] private float accelerationBoost = 100.0f; // ���������ňꎞ�I�ɑ�������g�呬�x
    [SerializeField] private GameObject effect;

    private SphereCollider sphereCollider;
    private float currentGrowthRate; // ���݂̊g�呬�x
    private bool isShrinking = false; // �k�������ǂ����̃t���O

    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioMixerGroup audioMixer;
    AudioSource audioSource;
    private bool stopFlag;
    private float saveGrowthRate;

    // �_���[�W�ʂ�Ԃ��v���p�e�B
    public int Damage()
    {
        return damage;
    }

    private void SyncColliderToScale()
    {
        if (sphereCollider != null)
        {
            sphereCollider.radius = 0.5f;
            //sphereCollider.radius = transform.localScale.x / 2; // �����_�ȉ�3���Ŋۂ߂�
            //Debug.Log(new Vector2(sphereCollider.radius,transform.localScale.x));
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

        //�I�[�f�B�I�\�[�X���������悤�ɂ���
        audioSource = this.GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer;

        //�T�E���h�𗬂�
        audioSource.PlayOneShot(sound1);
    }

    private void Update()
    {
        SyncColliderToScale();
        if (isShrinking)
        {
            Shrink();
        }
        else
        {
            Expand();
        }

        if(Time.timeScale == 0 && stopFlag == false)
        {
            SoundStop();
            stopFlag = true;
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
            EffectSpown();
            NearFind();
            Destroy(this.gameObject);
        }
        SyncColliderToScale(); // �X�P�[���ƃR���C�_�[�𓯊�
    }

    internal void Initialize(ShockwaveSettings settings)
    {
        this.growthRate = settings.growthRate;
        this.maxGrowthRate = settings.maxGrowthRate;
        this.growthAcceleration = settings.growthAcceleration;
        this.growthDeceleration = settings.growthDeceleration;
        this.maxScale = settings.maxScale;

        this.currentGrowthRate = this.growthRate;
        saveGrowthRate = this.growthRate;
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
            //Destroy(gameObject);
            isShrinking = false;
            this.currentGrowthRate = saveGrowthRate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Player"�^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ
        if (other.CompareTag("Player"))
        {
            Debug.Log("Shockwave hit the Player!");
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            obj.GetComponent<SoundWave>().TakeDamage(damage);
            audioSource.Stop();
            NearFind();
            Destroy(gameObject);
        }

        // "Diffence"�^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ
        if (other.CompareTag("Diffence"))
        {
            Debug.Log("Shockwave hit a Diffence and will be destroyed.");
            audioSource.Stop();
            EffectSpown();
            NearFind();
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
            //other.GetComponentInParent<ShrinkDestroy>().DestroyShrink();
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

    private void SoundStop()
    {
        audioSource.Stop();
    }

    private void EffectSpown()
    {
        GameObject effectP = Instantiate(effect, this.gameObject.transform.position, Quaternion.identity);
        effectP.GetComponent<DiffenceEffect>().ShockwaveRadius = transform.localScale.x;
    }

    private void NearFind()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject near = player.transform.Find("disCheck").gameObject;
        near.GetComponent<disCheck>().vanishEnemy();
    }
}