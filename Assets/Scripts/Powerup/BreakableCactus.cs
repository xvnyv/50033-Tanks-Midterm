using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCactus : MonoBehaviour
{
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    [HideInInspector] public GameObject powerup;
    [HideInInspector] public bool hasPowerup;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Shell"))
        {
            // get cactus position
            Vector3 pos = GetComponent<Transform>().position;
            pos.y = 1f;

            // break cactus
            gameObject.SetActive(false);

            if (hasPowerup)
            {
                // spawn powerup
                powerup.transform.position = pos;
                powerup.SetActive(true);
            }
        }
    }
}
