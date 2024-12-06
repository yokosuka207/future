using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ShockwaveSpawnerDemo : MonoBehaviour
{
    public GameObject shockwavePrefab; // 衝撃波のプレハブ
    public float duration; // 各衝撃波の持続時間リスト
    public float startTime; // 各衝撃波の発生開始時間リスト

    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioMixerGroup audioMixer;
    AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer;
        StartCoroutine(SpawnShockwaveWithDelay(this.transform.position, duration, startTime));
        
        //audioSource.spatialize = true;
        //audioSource.spatializePostEffects = true;
        //audioSource.bypassReverbZones = true;

    }

    private IEnumerator SpawnShockwaveWithDelay(Vector3 position, float duration, float delay)
    {
        // 開始時間まで待機
        yield return new WaitForSeconds(delay);

        // 衝撃波のインスタンスを生成
        GameObject shockwave = Instantiate(shockwavePrefab, position, Quaternion.identity);
        shockwave.transform.parent = this.transform;

        //レーダーに移す用
        GameObject.FindWithTag("Radar").GetComponent<RadarController>().SpownRadarShock(shockwave);

        //サウンドを流す
        audioSource.PlayOneShot(sound1);

        //Invoke(nameof(SoundOff), duration);
        
    }

    public void SoundOff()
    {
        audioSource.Stop();
        // 持続時間後に削除
        Destroy(this.gameObject);
    }

    public void SoundBlock()
    {
        audioSource.Stop();
        // 持続時間後に削除
        Destroy(this.gameObject);
    }
}
