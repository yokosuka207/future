using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearDeta : MonoBehaviour
{
    public Vector2[] saveData = new Vector2[4];
    public int saveLife;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveClearDeta(Vector2[] diffence, int life)
    {
        // シーンをまたいでオブジェクトを保持する
        DontDestroyOnLoad(gameObject);

        for(int i = 0; i < 4; i++)
        {
            saveData[i].x = diffence[i].x;
            saveData[i].y = diffence[i].y;

        }
        saveLife = life;
        name = SceneManager.GetActiveScene().name;
    }
}
