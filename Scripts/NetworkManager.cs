using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
    public Camera standbyCamera;
    GameObject myPlayerGO;
    void Start()
    {
        Connect();
    }

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("0.0.0.1");
    }

    void OnGUI()
    {
		//GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
    }

    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonJoinFailed");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        myPlayerGO = PhotonNetwork.Instantiate("PlayerController", new Vector3(10,10,10), Quaternion.identity, 0);
        standbyCamera.enabled = false;
		myPlayerGO.SetActive (true);
        ((MonoBehaviour)myPlayerGO.GetComponent(typeof(PlayerMovement))).enabled = true;
        Camera cam = ((Camera)myPlayerGO.GetComponentInChildren(typeof(Camera)));
        cam.enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponentInChildren(typeof(GunMovement))).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponentInChildren(typeof(GunShooting))).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponentInChildren(typeof(GunProperties))).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponentInChildren(typeof(BulletManager))).enabled = true;
		Transform partSysLocal = (Transform)myPlayerGO.transform.Find ("Main Camera/BulletsPartSystemLocal");
		if (partSysLocal == null) {
			Debug.Log ("partSysLocal is null");
		}
		partSysLocal.gameObject.SetActive (true);
		Transform partSysAll = (Transform)myPlayerGO.transform.Find ("Main Camera/BulletsPartSystemAll");
		if (partSysAll == null) {
			Debug.Log ("partSysAll is null");
		}
		partSysAll.gameObject.SetActive (false);
    }
}
