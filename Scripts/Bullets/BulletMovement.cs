using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour 
{

	public Rigidbody bulletRigidBody;
	public float thrust;
    public float destroyTime;
    float time = 0f;
    bool destroyed = false;

	// Use this for initialization
	void Start () 
	{
		bulletRigidBody.AddForce (transform.rotation * new Vector3(-thrust, 0, 0));
		Debug.Log ((12556).ToString () + transform.rotation.eulerAngles);
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (!destroyed)
        {
            time = time + Time.deltaTime;
            if (time >= destroyTime)
            {
                if (this.gameObject != null)
                {
                    destroyed = true;
                    Destroy(this.gameObject);
                }
            }
        }
	}
}
