using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectHealth : Photon.MonoBehaviour 
{

    //public ParticleSystem partSys;
    ParticleCollisionEvent[] collisionEvents;
    //public GameObject pGameObject;
    public float startingHealth = 100f;
    float currentHealth;
    bool isDead;
	Rect healthRect;

	// Use this for initialization
	void Start ()
    {
        //partSys = GetComponent<ParticleSystem>();
        currentHealth = startingHealth;
        //pGameObject = GetComponent<GameObject>();
        collisionEvents = new ParticleCollisionEvent[16];
		healthRect = new Rect (10, 10, 100, 20);
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("Damage");
        ParticleSystem part = other.GetComponent<ParticleSystem>();
        int safeLength = part.GetSafeCollisionEventSize();

        if (collisionEvents.Length < safeLength)
        {
            collisionEvents = new ParticleCollisionEvent[safeLength];
        }

        int numCollisionEvents = part.GetCollisionEvents(this.gameObject, collisionEvents);
        float damage = other.GetComponent<GunProperties>().damage;
        TakeDamage(numCollisionEvents * damage);
    }

	void OnGUI()
	{
		if (photonView.isMine) 
		{
			GUILayout.Label (currentHealth.ToString ());
		}
	}

	[RPC]
    public void TakeDamage(float damage)
    {
		Debug.Log ("damage taken");
        if(isDead)
        {
            return;
        }

        currentHealth -= damage;

        if(currentHealth <= 0f)
        {
            //Death();
        }
    }

    void Death()
    {
        isDead = true;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
