using UnityEngine;

public class Coin : MonoBehaviour
{
    public ParticleSystem coinParticles; // Reference to the particle system

    private void Start()
    {
        if (coinParticles == null)
        {
            coinParticles = GetComponentInChildren<ParticleSystem>();
            if (coinParticles == null)
            {
                Debug.LogError("ParticleSystem component is missing from the coin.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Play the particle effect
            if (coinParticles != null)
            {
                coinParticles.transform.parent = null; // Detach the particle system from the coin
                coinParticles.Play();
                Destroy(coinParticles.gameObject, coinParticles.main.duration); // Destroy after playing
            }

            // Trigger any other actions (e.g., score update, sound effect)
            //ScoreManager.Instance.ChangeScore(1);

            // Destroy the coin
            Destroy(gameObject);
        }
    }
}