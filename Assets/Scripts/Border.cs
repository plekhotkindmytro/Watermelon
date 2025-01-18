using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public ParticleSystem electricShortPrefab;
    public ParticleSystem explosionPrefab;
   
    public AudioClip borderHitSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(Constants.PlayerTag))
        {
            AudioSource.PlayClipAtPoint(borderHitSound, other.transform.position);
            ParticleSystem particle1 = Instantiate(electricShortPrefab, other.contacts[0].point, Quaternion.identity);
        //    ParticleSystem particle2 = Instantiate(explosionPrefab, other.contacts[0].point, Quaternion.identity);
            particle1.Play();
          //  particle2.Play();
        }
    }
}
