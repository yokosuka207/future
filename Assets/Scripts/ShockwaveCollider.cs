using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCollider : MonoBehaviour
{
    [SerializeField] private int damege;

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("“–‚½‚Á‚½");
        }
    }

    public int Damege()
    {
        return damege;
    }
}
