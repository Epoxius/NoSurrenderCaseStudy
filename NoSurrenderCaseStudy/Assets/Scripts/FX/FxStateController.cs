using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxStateController : MonoBehaviour
{
    public void OnParticleSystemStopped()
    {
        GameManager.Instance.particlePool.SetPooledObject(this);
    }
}
