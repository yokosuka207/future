using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShockwaveSettings
{
    public Vector3 position;
    public float duration = 5.0f;
    public float startTime = 1.0f;
    public float growthRate = 0.1f;             // �����g�呬�x
    public float maxGrowthRate = 15.0f;         // �g�呬�x�̏��
    public float growthAcceleration = 0.1f;     // �g������x
    public float growthDeceleration = 0.001f;   // �g�匸���x
    public float maxScale = 5.0f;               // �ő�X�P�[��
}

public class ShockwaveSpawner : MonoBehaviour
{ 
    public GameObject shockwavePrefab; // �Ռ��g�̃v���n�u
    public List<ShockwaveSettings> shockwaveSettings; // �Ռ��g���Ƃ̐ݒ胊�X�g

    void Start()
    {
        foreach (var settings in shockwaveSettings)
        {
            StartCoroutine(SpawnShockwaveWithDelay(settings));
        }
    }
    private IEnumerator SpawnShockwaveWithDelay(ShockwaveSettings settings)
    {
        yield return new WaitForSeconds(settings.startTime);

        GameObject shockwave = Instantiate(shockwavePrefab, settings.position, Quaternion.identity);

        //���[�_�[�ɉf��
        GameObject.FindWithTag("Radar").GetComponent<RadarController>().SpownRadarShock(shockwave);

        ShockwaveCollider collider = shockwave.GetComponent<ShockwaveCollider>();
        if (collider != null)
        {
            collider.Initialize(settings);
        }

        Destroy(shockwave, settings.duration);
    }
}
