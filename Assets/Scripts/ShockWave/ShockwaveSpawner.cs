using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSpawner : MonoBehaviour
{
    public GameObject shockwavePrefab; // �Ռ��g�̃v���n�u
    public List<Vector3> spawnPositions; // �e�Ռ��g�̐����ʒu���X�g
    public List<float> durations; // �e�Ռ��g�̎������ԃ��X�g
    public List<float> startTimes; // �e�Ռ��g�̔����J�n���ԃ��X�g

    void Start()
    {
        // �e�ʒu�ɑ΂��ăR���[�`�����J�n���A�����J�n���Ԃ�K�p
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            float startTime = (i < startTimes.Count) ? startTimes[i] : 0f; // �J�n���Ԃ����X�g�ɂȂ��ꍇ��0�b
            float duration = (i < durations.Count) ? durations[i] : 5.0f; // �������Ԃ����X�g�ɂȂ��ꍇ��5�b
            Vector3 position = spawnPositions[i];

            StartCoroutine(SpawnShockwaveWithDelay(position, duration, startTime));
        }
    }

    private IEnumerator SpawnShockwaveWithDelay(Vector3 position, float duration, float delay)
    {
        // �J�n���Ԃ܂őҋ@
        yield return new WaitForSeconds(delay);

        // �Ռ��g�̃C���X�^���X�𐶐�
        GameObject shockwave = Instantiate(shockwavePrefab, position, Quaternion.identity);

        // �������Ԍ�ɍ폜
        Destroy(shockwave, duration);
    }
}
