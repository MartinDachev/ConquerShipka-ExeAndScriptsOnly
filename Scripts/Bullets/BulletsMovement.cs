using UnityEngine;
using System.Collections;

public class BulletsMovement : MonoBehaviour 
{

    Ray shootRay;
    public Rigidbody bulletRigidbody;
    public Transform bulletTransform;
    public GameObject bulletGameObject;
	public GunProperties gunProperties;
    public BulletsMovement ScriptRef;
    RaycastHit[] hits;
    ObjectHealth enemyHealth;
	PhotonView photonViewEnemy;
    Vector3 lastPosition;
    float range;
    float speed;
    float damage;
    
	// Use this for initialization
	void Start () 
    {
		speed = gunProperties.bulletSpeed;
		//Debug.Log (gunProperties != null);
		damage = 13f;
        //speed = 373f;
        //damage = 13f;
	}
	
	// Update is called once per frame
    void Update()
    {
        shootRay.origin = bulletRigidbody.position;
        shootRay.direction =  lastPosition - bulletRigidbody.position;
        Debug.DrawRay(shootRay.origin, lastPosition - bulletRigidbody.position);
        //Debug.Log("bulletsss");

        hits = Physics.RaycastAll(shootRay, Vector3.Distance(bulletRigidbody.position, lastPosition));
        foreach(RaycastHit hit in hits)
        {
            enemyHealth = (ObjectHealth)hit.collider.GetComponent(typeof(ObjectHealth));
            if(enemyHealth != null)
            {
				PhotonView enemyHealthPV = enemyHealth.GetComponent<PhotonView> ();
				if (enemyHealthPV != null)
				{
					Debug.Log ("RPC DAMAGE");
					enemyHealthPV.RPC ("TakeDamage", PhotonPlayer.Find(enemyHealthPV.ownerId), damage);  
				}
				else 
				{
					Debug.Log ("View is empty for rpc damage to be called");
				}
			}
			
            //ScriptRef.enabled = false;
			//if (hit != null) 
			//{
				/*photonViewEnemy = (PhotonView)hit.collider.GetComponent (typeof(PhotonView));
				if (photonViewEnemy != null) 
				{
					Debug.Log ("Rpc will be called");
					photonViewEnemy.RPC ("TakeDamage", PhotonTargets.MasterClient, damage);
				}*/
			//}
            this.bulletRigidbody.velocity = Vector3.zero;
            this.bulletGameObject.SetActive(false);
        }
        lastPosition = bulletRigidbody.position;
    }

    public void SetActiveBullet(bool value)
    {
        this.gameObject.SetActive(value);
        this.enabled = value;
        //Debug.Log("activated = " + value);
    }

    public void StartBullet(Vector3 position, Quaternion rotation)
    {
        this.bulletRigidbody.MovePosition(position);
        this.bulletRigidbody.MoveRotation(rotation);
        this.bulletTransform.position = position;
        this.bulletTransform.rotation = rotation;
       
        this.lastPosition = this.bulletTransform.position;

        float startForce = (bulletRigidbody.mass * gunProperties.bulletSpeed) / Time.fixedDeltaTime;
        //Debug.Log("force = " + (speed));
        this.bulletRigidbody.AddForce(bulletTransform.forward * startForce);
        SetActiveBullet(true);
    }
}
