using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonParticleEffect : MonoBehaviour, IPointerClickHandler
{
    [Header("Particle Settings")]
    public GameObject particlePrefab; // Drag your particle system here
    public Transform spawnPoint; // Where particles appear (usually the button itself)

    public void OnPointerClick(PointerEventData eventData)
    {
        SpawnParticles();
    }

    void SpawnParticles()
    {
        if (particlePrefab != null)
        {
            // Spawn particles at the button's position
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
            
            GameObject particles = Instantiate(particlePrefab, spawnPos, Quaternion.identity);
            
            // Auto-destruct after particles finish playing
            ParticleSystem ps = particles.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(particles, ps.main.duration + ps.main.startLifetime.constant);
            }
            else
            {
                Destroy(particles, 1f);
            }
        }
    }
}