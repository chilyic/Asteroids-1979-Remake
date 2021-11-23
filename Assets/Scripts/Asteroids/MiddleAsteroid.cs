using UnityEngine;

public class MiddleAsteroid : BaseAsteroid
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            sound.BrokenMiddleAsteroid();
            Broke();
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
