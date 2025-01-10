using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffenceEffect : MonoBehaviour
{
    [SerializeField] private float radiusRatio;
    public float ShockwaveRadius;
    // Start is called before the first frame update
    void Start()
    {
        Transform shape = this.gameObject.GetComponent<ParticleSystem>().transform;
        shape.localScale = new Vector3(ShockwaveRadius * radiusRatio, ShockwaveRadius * radiusRatio, ShockwaveRadius * radiusRatio);
        Debug.Log(ShockwaveRadius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
