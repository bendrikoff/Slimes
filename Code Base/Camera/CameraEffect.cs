using UnityEngine;

public class CameraEffect : MonoBehaviour
{
   [SerializeField] public Acceleration Acceleration;
   [SerializeField] public ParticleSystem SpeedParticle;
   [SerializeField] public TrailRenderer Trail;

   private void OnEnable()
   {
      Acceleration.AccelerationStarted += PlaySpeedEffects;
      Acceleration.AccelerationCanceled += StopSpeedEffects;   
   }

   private void OnDisable()
   {
      Acceleration.AccelerationStarted -= PlaySpeedEffects;
      Acceleration.AccelerationCanceled -= StopSpeedEffects;  
   }

   public void PlaySpeedEffects()
   {
      SpeedParticle.Play();
      Trail.enabled = true;
   }

   public void StopSpeedEffects()
   {
      SpeedParticle.Stop();
      Trail.enabled = false;
   }
}
