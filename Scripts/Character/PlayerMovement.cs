using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	private float temp;

    GameObject mainCamera;
    public Rigidbody playerRigidBody;
    float horizontalMovement;
    float verticalMovement;
    float horizontalRotation;
    float verticalRotation;
    public Vector3 minXRotation;
    public Vector3 maxXRotation;
    public Vector3 standPosition;
    public Vector3 crouchRelativePos;
    Vector3 tcrouchCameraPos;
    Vector3 cameraRotation;
    float moveSpeed;
    public float standMoveSpeed;
    public float crouchMoveSpeed;
	public float jumpThrust;
    public float crouchAnimSpeed;
    float crouchAnim;
    bool isJumping = false;
    bool isCrouching = false;
    bool isStanding = false;
    bool isCrouched = false;
    Vector3 playerTranslation;
    Vector3 headRotation;
    Vector3 playerRotation;

	// Use this for initialization
	void Start () 
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	} 
	
	// Update is called once per frame
	void Update () 
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalRotation = Input.GetAxis("Mouse Y");
        verticalRotation = Input.GetAxis("Mouse X");
        //Debug.Log(horizontalMovement);
        RotatePlayer(-horizontalRotation, verticalRotation);
        
        if(Input.GetButton("Jump"))
        {
            Jump();
        }
        
        Crouch();
        MovePlayer(horizontalMovement, verticalMovement);
	}

    void MovePlayer(float x, float z)
    {
        playerTranslation.Set(x, 0, z);
        //playerTranslation.Normalize();
		//transform.Translate(playerTranslation.normalized * moveSpeed * Time.deltaTime);
        playerTranslation = (playerRigidBody.rotation * playerTranslation).normalized * standMoveSpeed;
        playerRigidBody.velocity = new Vector3(playerTranslation.x, playerRigidBody.velocity.y, playerTranslation.z);
        //Debug.Log(playerRigidBody.velocity);
    }

    void Jump()
    {
        if(!isJumping)
        {
            playerRigidBody.AddForce(transform.up * jumpThrust);
            isJumping = true;
        }
    }

    void Crouch()
    {
        if(!isCrouching && !isStanding && Input.GetKey(KeyCode.C))
        {
            if (isCrouched)
            {
                isStanding = true;
            }
            else
            {
                isCrouching = true;
            }
        }

        if(isCrouching)
        {
            crouchAnim = Mathf.Lerp(crouchAnim, 1, crouchAnimSpeed * Time.deltaTime);
            if(crouchAnim >= 0.99)
            {
                isCrouching = false;
                isCrouched = true;
            }
        }

        if(isStanding)
        {
            crouchAnim = Mathf.Lerp(crouchAnim, 0, crouchAnimSpeed * Time.deltaTime);
            if (crouchAnim <= 0.01)
            {
                isStanding = false;
                isCrouched = false;
            }
        }

        //Debug.Log(crouchAnim);
        tcrouchCameraPos = crouchRelativePos * crouchAnim;
        mainCamera.transform.localPosition= standPosition + tcrouchCameraPos;

    }

    void RotatePlayer(float x, float y)
    {
        x = Mathf.Clamp(headRotation.x + x, minXRotation.x, maxXRotation.x);
        
        headRotation.Set(x, headRotation.y, headRotation.z);
        mainCamera.transform.localRotation = Quaternion.Euler(headRotation);

        playerRotation.Set(0, y, 0);
        transform.Rotate(playerRotation);
        playerRigidBody.rotation = Quaternion.Euler(transform.localRotation.eulerAngles + playerRotation);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Terrain" && isJumping)
        {
            isJumping = false;
        }
    }
}
