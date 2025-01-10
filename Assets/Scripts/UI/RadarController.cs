using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarController : MonoBehaviour
{
    private GameObject player; //音波
    [SerializeField] private Canvas canvas;     //UIのキャンバス
    [SerializeField] private Image enemy;       //レーダーに移す衝撃波の位置画像
    private GameObject[] shockWave = new GameObject[10];
    private Image[] shockWaveRadar = new Image[10];
    private bool[] use = new bool[10];
    [SerializeField] private float distanceCheck;
    [SerializeField] private float ratioCheck;

    [SerializeField] private Vector2 radarCheck;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        for (int n = 0; n < 10; n++)
        {
            if(use[n] == false)
            {
                continue;
            }

            if(shockWave[n] == null)
            {
                use[n] = false;
                shockWaveRadar[n].enabled = false;
                continue;
            }
            Vector3 shockPos = shockWave[n].transform.position;
            float dis = Vector3.Distance(playerPos, shockPos);
            //Debug.Log(dis);
            if (dis < distanceCheck)
            {
                if(shockWaveRadar[n].enabled == false)
                {
                    shockWaveRadar[n].enabled = true;
                } 
            }
            shockWaveRadar[n].transform.position = new Vector3(((playerPos.z - shockPos.z) * (gameObject.transform.position.x / ratioCheck)) + (gameObject.transform.position.x * radarCheck.x), ((shockPos.x - playerPos.x) * (gameObject.transform.position.x / ratioCheck)) + (gameObject.transform.position.y * radarCheck.y), 0);
            //Debug.Log(gameObject.transform.position);
        } 
    }

    public void SpownRadarShock(GameObject shockPos)
    {
        
        for(int i = 0; i < 10; i++)
        {

            if(use[i] == true)
            {
                continue;

            }

            shockWave[i] = shockPos;
            shockWaveRadar[i] = Instantiate(enemy);
            use[i] = true;
            shockWaveRadar[i].transform.SetParent(canvas.transform, false);
            shockWaveRadar[i].enabled = false;
            //Debug.Log(shockWave[i].transform.position);
            break;
        }
    }
}
