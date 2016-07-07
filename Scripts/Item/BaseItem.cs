using UnityEngine;
using System.Collections;

public class BaseItem : MonoBehaviour {

	private float Speed = 5f;


	void Update()
	{
		this.transform.Rotate (new Vector3 (5, 5, 0) * Speed * Time.deltaTime);
	}

}