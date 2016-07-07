using UnityEngine;
using System.Collections;

public class CollisionHandling : MonoBehaviour 
{

    public ParticleSystem partSys;
    public ParticleCollisionEvent[] collisionEvents;

	// Use this for initialization
	void Start ()
    {
        partSys = GetComponent<ParticleSystem>();
        collisionEvents = new ParticleCollisionEvent[16];
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnParticleCollision(GameObject other)
    {
        int safeLength = partSys.GetSafeCollisionEventSize();

        if (collisionEvents.Length < safeLength)
        {
            collisionEvents = new ParticleCollisionEvent[safeLength];
        }

        int numCollisionEvents = partSys.GetCollisionEvents(other, collisionEvents);
        Rigidbody rb = other.GetComponent<Rigidbody>();
        for (int i = 0; i < numCollisionEvents; i++)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                rb.AddForce(force);
            }
        }
    }
}
