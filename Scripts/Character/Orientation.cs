using UnityEngine;
using System.Collections;

public class Orientation : MonoBehaviour 
{
    Transform target;
    GameObject bullet;
    float bulletSpeed;
	// Use this for initialization
	void Start () 
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bullet = (GameObject)Resources.Load("Sphere");
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.LookAt(target);
        if(Vector3.Distance(transform.position, target.position) <= 10)
        {
            Instantiate(bullet);
            bullet.transform.position = transform.position +Vector3.forward * 10f;
            bullet.transform.LookAt(target.transform);
            bullet.transform.position += Vector3.forward * 10f;
            bullet.SetActive(true);
        }
	}
}
