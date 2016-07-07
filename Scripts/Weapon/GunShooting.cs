using UnityEngine;
using System.Collections;

public class GunShooting : MonoBehaviour 
{
    public BulletManager bulletManager;
    public GunProperties gunProperties;
    public Transform gunTransform;
    public Rigidbody gunRigidbody;
	public PhotonView photonView;
    float waitingTime;
    float timeBetweenShots;
	public ParticleSystem bulletsParticleSys;
	ParticleSystem.EmissionModule bulletsPartSysEM;
	bool shooting = false;

	// Use this for initialization
	void Start ()
	{
		timeBetweenShots = 60 / gunProperties.rateOfFire;
		Debug.Log (gunProperties.rateOfFire);
		waitingTime = timeBetweenShots;
		bulletsPartSysEM = bulletsParticleSys.emission;
	}
	// Update is called once per frame
	void Update () 
    {
		if (!photonView.isMine) {
			bulletsPartSysEM.enabled = shooting;
		} else {
			timeBetweenShots = 60 / gunProperties.rateOfFire;
			if (Input.GetButton ("Fire1")) {
				if (!shooting) {
					shooting = true;
					//RPCStartShooting ();
				}
				if (waitingTime >= timeBetweenShots) {
					waitingTime = 0.0f;
					bulletManager.SpawnBullet (gunTransform.position, gunTransform.rotation);
				}
				waitingTime = waitingTime + Time.deltaTime;
			} else {
				shooting = false;
				//RPCStopShooting ();
			}
		}
	}

	/*public void RPCStartShooting()
	{
		Debug.Log ("Method is called");
		photonView.RPC ("StartShootingEffect", PhotonTargets.Others);
	}

	public void RPCStopShooting()
	{
		photonView.RPC ("StopShootingEffect", PhotonTargets.Others);
	}*/

	/*[RPC]
	public void StartShootingEffect()
	{
		//bulletsParticleSys.gameObject.SetActive (true);
		bulletsPartSysEM.enabled = true;
		//Debug.Log ("Rpc called");
	}

	[RPC]
	public void StopShootingEffect()
	{
		bulletsPartSysEM.enabled = false;
		//Debug.Log ("Rpc called 2");
	}*/

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext (shooting);
		}
		else
		{
			// Network player, receive data
			shooting = (bool)stream.ReceiveNext();
		}
	}
}