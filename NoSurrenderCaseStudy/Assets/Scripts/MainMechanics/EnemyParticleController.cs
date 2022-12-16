using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleController : MonoBehaviour
{
   public ParticleSystem explosionFx;

   public void OnDeath()
   {
      explosionFx.Play();
   }
}
