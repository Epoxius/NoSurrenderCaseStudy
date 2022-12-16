using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParticleController : MonoBehaviour
{
    public ParticleSystem spawnFx;

    public void SpawnFx()
    {
        spawnFx.Play();
    }
}
