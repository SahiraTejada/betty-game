using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
     [Header("Particle Effect")]
    public GameObject Destroy_Particle;
    [Tooltip("Scale multiplier for the destruction particle")]
    [Range(0.1f, 2f)]
    public float particleScale = 0.5f;

    [Header("Bullet Settings")]
    [Tooltip("Delay before collision detection is enabled to avoid hitting the shooter")]
    public float collisionDelay = 0.05f;

    [Tooltip("Time before the bullet auto-destroys if it hasn't hit anything")]
    public float autoDestroyTime = 3f;

    [Tooltip("Time before the destruction particle is destroyed")]
    public float particleLifetime = 2f;

    private bool canCollide = false;

    private void Start()
    {
        // Enable collision detection after a short delay to avoid hitting shooter
        Invoke(nameof(EnableCollision), collisionDelay);

        // Auto-destroy after specified time if it hasn't hit anything
        Destroy(gameObject, autoDestroyTime);
    }

    private void EnableCollision()
    {
        canCollide = true;
    }

   
   private  void OnTriggerEnter2D(Collider2D other)
    {
            // Don't detect collisions until enabled
        if (!canCollide) return;

        // Ignore collisions with Player
        if (other.CompareTag("Player")) return;

        // Spawn destruction particle effect
        SpawnDestructionParticle();

        // Destroy the bullet
        Destroy(gameObject);
    }
  

    private void SpawnDestructionParticle()
    {
        if (Destroy_Particle == null) return;

        GameObject particle = Instantiate(Destroy_Particle, transform.position, Quaternion.identity);

        // Apply scale to the particle
        particle.transform.localScale = Vector3.one * particleScale;

        // Force all particle systems to play immediately
        ParticleSystem[] particleSystems = particle.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }

        Destroy(particle, particleLifetime);
    }
}
