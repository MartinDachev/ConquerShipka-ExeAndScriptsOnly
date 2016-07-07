using UnityEngine;
using System.Collections;

public class GunPositionSync : Photon.MonoBehaviour 
{
	Quaternion correctGunRot;
	Transform gunTransform;
	int count = 0;
	int count2 = 0;

	// Use this for initialization
	void Start () {
		gunTransform = transform;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!photonView.isMine) 
		{
			++count;
			//Debug.Log ("Lerp is called" + count);
			gunTransform.rotation = Quaternion.RotateTowards(gunTransform.rotation, correctGunRot, Time.deltaTime);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		++count2;
		//Debug.Log ("Serialized" + count2);
		if (stream.isWriting) 
		{
			// We own whis player: send the others our data
			stream.SendNext (gunTransform.rotation);
		}
		else
		{
			correctGunRot = (Quaternion)stream.ReceiveNext ();
		}
	}
}