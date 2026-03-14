using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonParticleEffect : MonoBehaviour, IPointerClickHandler
{
    [Header("Particle Settings")]
    public GameObject particlePrefab;
    
    // ✅ New: Track if particles are currently active
    private bool isSpawning = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        SpawnParticles();
    }

    void SpawnParticles()
    {
        // ✅ If particles are already on screen, do nothing
        if (isSpawning) 
        {
            return; 
        }

        if (particlePrefab != null)
        {
            // Set flag to true
            isSpawning = true;

            // Spawn particles at Z = -1
            Vector3 spawnPos = transform.position;
            spawnPos.z = -1f; 

            Instantiate(particlePrefab, spawnPos, Quaternion.identity);

            // ✅ Reset the flag after 1 second (match this to your particle duration)
            Invoke(nameof(ResetSpawn), 1f); 
        }
    }

    // ✅ Called automatically after 1 second to allow spawning again
    void ResetSpawn()
    {
        isSpawning = false;
    }
}