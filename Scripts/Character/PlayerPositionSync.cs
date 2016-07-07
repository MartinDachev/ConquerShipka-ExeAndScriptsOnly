using UnityEngine;
using System.Collections;

public class PlayerPositionSync : Photon.MonoBehaviour 
{
	Vector3 correctPlayerPos;
	Quaternion correctPlayerRot;
	Transform playerTransform;

	// Use this for initialization
	void Start () {
		playerTransform = transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!photonView.isMine) 
		{
			playerTransform.position = Vector3.Lerp (playerTransform.position, correctPlayerPos, Time.deltaTime * 5);
			playerTransform.rotation = Quaternion.Lerp (playerTransform.rotation, correctPlayerRot, Time.deltaTime * 5);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) 
		{
			// We own whis player: send the others our data
			stream.SendNext (playerTransform.position);
			stream.SendNext (playerTransform.rotation);
		}
		else
		{
			correctPlayerPos = (Vector3)stream.ReceiveNext ();
			correctPlayerRot = (Quaternion)stream.ReceiveNext ();
		}
	}
}
