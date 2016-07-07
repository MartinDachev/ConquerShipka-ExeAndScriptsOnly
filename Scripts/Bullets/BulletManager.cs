using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour 
{
    List<BulletsMovement> bulletScripts;
    BulletsMovement tmpScript;
    public ParticleSystem bulletsParticleSystem;
    ParticleSystem.Particle sampleParticle;
    public Transform gunTransform;
    public GameObject bulletPrefab;
    public Vector3 storePosition;
    public Vector3 storeRotation;
    public float bulletClipRadius;
	public GunProperties gunProperties;
    ParticleSystem.Particle[] particles;
    ParticleSystem.Particle[] psp = new ParticleSystem.Particle[10000];
    int allBulletsCount;
    int activeBulletsCount;
    private float sqrDist;
    private float xd;
    private float yd;
    private float zd;
    private Vector3 bPos;

	// Use this for initialization
	void Start () 
    {
        bulletScripts = new List<BulletsMovement>(1000);
        particles = new ParticleSystem.Particle[1000];
        for(int i=0; i<150; i++)
        {
            //GameObject go = (GameObject) Instantiate(bulletPrefab, transform.position, transform.rotation);
            bulletScripts.Add((BulletsMovement) ((GameObject) Instantiate(bulletPrefab, transform.position, transform.rotation)).GetComponent(typeof(BulletsMovement)));
            bulletScripts[i].gameObject.SetActive(false);
			bulletScripts[i].gunProperties = gunProperties;
			Debug.Log ((bulletScripts [i].gunProperties != null) + "+++++++++");
        }
        allBulletsCount = 0;
        activeBulletsCount = 0;
        
        bulletsParticleSystem.GetParticles(psp);
        sampleParticle = psp[0];
        Debug.Log("color = " + sampleParticle.color);
        //psp[0].position = bulletScripts[0].bulletRigidbody.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        Debug.Log("AB = " + allBulletsCount);
        int i;
        for (i = 0; i < particles.Length && i < allBulletsCount; ++i)
        {
            //Debug.Log("enabled = " + bulletScripts[i].enabled + ", bulletcount = " + allBulletsCount);
            if (!bulletScripts[i].gameObject.activeSelf)
            {
                //Debug.Log("zafawf");
                int lastIndex = allBulletsCount - 1;
                while (lastIndex > i && !bulletScripts[lastIndex].enabled)
                {
                    lastIndex--;
                    allBulletsCount--;
                }

                allBulletsCount--;

                if (lastIndex != i)
                {
                    tmpScript = bulletScripts[i];
                    bulletScripts[i] = bulletScripts[lastIndex];
                    bulletScripts[lastIndex] = tmpScript;
                }
                else
                {
                    break;
                }
            }
            //sampleParticle.position = bulletScripts[i].bulletRigidbody.position;
            bPos = bulletScripts[i].bulletRigidbody.position;
            particles[i].position = bPos;
            particles[i].size = 0.1f;
            particles[i].velocity = bulletScripts[i].bulletRigidbody.rotation * new Vector3(0,0,1);
            
            xd = gunTransform.position.x - bPos.x;
            yd = gunTransform.position.y - bPos.y;
            zd = gunTransform.position.z - bPos.z;

            float sqrDist = xd*xd + yd*yd + zd*zd;
            //Debug.Log("sqrd = " + sqrDist);
            if (bulletClipRadius * bulletClipRadius >= sqrDist)
            {
                particles[i].color = new Color(0, 0, 0, 0);
            }
            else
            {
                //particles[i].color = new Color(0, 0, 0, 0);
                particles[i].color = new Color(255, 255, 255, 255);
            }
            //sampleParticle.position = bulletScripts[i].bulletRigidbody.position;
        }

        //bulletsParticleSystem.Emit(allBulletsCount);
        //bulletsParticleSystem.GetParticles(psp);
        //Debug.Log(psp[0].color);
        bulletsParticleSystem.SetParticles(particles, allBulletsCount);
        if (particles.Length >= 2)
        {
           // Debug.Log(particles[particles.Length - 2].position);
        }
    }

    public void SpawnBullet(Vector3 position, Quaternion rotation)
    {
		Debug.Log ("SpawnBullet called");
        //int count = bulletScripts.Count;
        //bool spawnedBullet = false;
        //for(int i=0; i < count; i++) 
        //{
        //    if(!bulletScripts[i].isActiveAndEnabled)
        //    {
        //        bulletScripts[i].SetActiveBullet(true);
        //        bulletScripts[i].StartBullet(position, rotation);
        //        spawnedBullet = true;
        //        break;
        //    }
        //}
        if(allBulletsCount >= bulletScripts.Count)
		{
            bulletScripts.Add((BulletsMovement)((GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation)).GetComponent(typeof(BulletsMovement)));
			bulletScripts [allBulletsCount].gunProperties = gunProperties;
			Debug.Log (gunProperties.bulletSpeed);
			Debug.Log (bulletScripts [allBulletsCount].gunProperties.bulletSpeed + "bs2");
        }
        else
        {
            //bulletScripts[allBulletsCount].bulletGameObject.SetActive(true);
            //Debug.Log(bulletScripts[allBulletsCount].gameObject);
            //bulletScripts[allBulletsCount].gameObject.SetActive(true);
            //bulletScripts[allBulletsCount].gameObject.SetActive(true);
            
           bulletScripts[allBulletsCount].SetActiveBullet(true);
        }
		Quaternion rot = new Quaternion();
		Vector3 transformEuler = transform.rotation.eulerAngles;
		float x = Random.Range(-40f, 40f);
		float y = Random.Range(-0.05f, 0.05f);
		rot.eulerAngles.Set(transformEuler.x + 40, transformEuler.y + 40, 0);
        bulletScripts[allBulletsCount].StartBullet(position, rotation);
        //activeBulletsCount++;
        allBulletsCount++;
        //Debug.Log("bullet spawned");
        //Debug.Log(", all count = " + allBulletsCount);
    }
}
