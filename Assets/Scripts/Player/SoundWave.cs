using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundWave : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // 最大HP
    public int currentHealth;                    // 現在のHP

    [SerializeField] private float speed = 1.0f;  // 移動速度
    [SerializeField] private GameObject target;

    [SerializeField] private GameObject HPbar;

    [SerializeField] private Canvas GameOverUI;

    public CameraShake mainCameraShake;
    public CameraShake subCameraShake;

    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioMixerGroup audioMixer;
    AudioSource audioSource;

    //public CameraShake mainCameraShake;
    //public CameraShake subCameraShake;

    private void Start()
    {
        currentHealth = maxHealth;

        //オーディオソースをいじれるようにする
        audioSource = this.GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer;

        //サウンドを流す
        audioSource.PlayOneShot(sound1);
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 衝撃波との当たり判定
        if (other.gameObject.tag == "ShockWave")
            //TakeDamage();

        // ゴールとの衝突判定
        if(other.CompareTag("Goal"))
            speed = 0;
    }

    // ダメージ計算
    public void TakeDamage(int damage)
    {
        // ダメージ計算
        currentHealth -= damage;

        //HPバーの減少
        HPbar.GetComponent<HPBarController>().HealthDecreese(damage);


        // メインカメラを揺らす
        StartCoroutine(mainCameraShake.Shake());

        // サブカメラを揺らす
        StartCoroutine(subCameraShake.Shake());


        Debug.Log(currentHealth);

        // 死亡処理
        if (currentHealth <= 0)
        {
            //ゲームを中断
            Time.timeScale = 0;

            //音楽を停止
            audioSource.Stop();

            //ゲームオーバーUIを起動
            GameOverUI.gameObject.SetActive(true);

            
        }
    }


}