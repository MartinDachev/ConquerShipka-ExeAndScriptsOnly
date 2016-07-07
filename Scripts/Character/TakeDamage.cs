using UnityEngine;
using System.Collections;

public class TakeDamage : MonoBehaviour 
{
	public float health;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Damage(float damage)
	{
		health = health - damage;
        //Debug.Log(health.ToString());
		if (health <= 0) 
		{
			Destroy (this.gameObject);
		}
	}
}
