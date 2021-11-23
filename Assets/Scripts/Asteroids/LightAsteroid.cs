using UnityEngine;

public class LightAsteroid : BaseAsteroid
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            sound.BrokenLightAsteroid();
            ScoreAdded(score);
            BackToPool();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            BackToPool();
        }

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (knockSound.enabled == true)
                knockSound.Play();
        }
    }
}
