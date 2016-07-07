using UnityEngine;
using System.Collections;

public class GunMovement : MonoBehaviour 
{
	float aimAnim = 1.0f;
	float taimAnim = 1.0f;
	float aimAnimSpeed = 20f;
	float aimAnimVel = 0.0f;
	float aimAnimVel2 = 0.0f;
	public bool isAimed;
	public Vector3 holdRelativeGunPos;
	public Vector3 aimedGunPos;
	Vector3 tholdGunPos;
	public Vector3 holdRelativeRotation;
	public Vector3 aimedRotation;
	Vector3 tholdRotation;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton (1)) 
		{
			taimAnim = Mathf.Lerp (taimAnim, 0f, aimAnimSpeed * Time.deltaTime);
			tholdGunPos.Set(holdRelativeGunPos.x * taimAnim, holdRelativeGunPos.y * taimAnim, holdRelativeGunPos.z * taimAnim);
			tholdRotation = holdRelativeRotation * taimAnim;
		}
		else
		{
			taimAnim = Mathf.Lerp (taimAnim, 1f, aimAnimSpeed * Time.deltaTime);
			tholdGunPos.Set(holdRelativeGunPos.x * taimAnim, holdRelativeGunPos.y * taimAnim, holdRelativeGunPos.z * taimAnim);
			tholdRotation = holdRelativeRotation * taimAnim;
		}
		//Debug.Log (taimAnim.ToString ());
		transform.localPosition = aimedGunPos + tholdGunPos;
		transform.localRotation = Quaternion.Euler (aimedRotation + tholdRotation);
	}
}
