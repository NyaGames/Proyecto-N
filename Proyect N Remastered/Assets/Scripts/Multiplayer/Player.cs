﻿using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool isGameMaster;
    public int id { get; set; }
    [HideInInspector] public string nickName;
    [HideInInspector]public PhotonView photonViewTransform;

    private BoxCollider playerCollider;

    private void Awake()
    {
        photonViewTransform = GetComponent<PhotonView>();
        //Debug.Log("AHORA SE CREA EL PLAYER");
        if (photonViewTransform.IsMine)
        {
            nickName = PhotonNetwork.NickName;
            isGameMaster = PersistentData.isGM;
            id = PhotonNetwork.LocalPlayer.ActorNumber;
           
        }
    }

    void Start()
    {
        playerCollider = GetComponent<BoxCollider>();
        //Variables
        if (!photonViewTransform.IsMine) //Si no soy yo, cojo los datos del servidor
        {
            Photon.Realtime.Player playerReference = getPlayerReference(photonViewTransform.OwnerActorNr); 
            id = (int)playerReference.CustomProperties["id"];
            isGameMaster = (bool)playerReference.CustomProperties["isGameMaster"];
            nickName = gameObject.GetPhotonView().Owner.NickName;

        }
        else //Si soy yo,cojo los datos de mi escena
        {
            InitCamera();
        }
		           
    }

    public Photon.Realtime.Player getPlayerReference(int actorNumber){
        for(int i = 0;i  < PhotonNetwork.PlayerList.Length; i++)
        {
            if(PhotonNetwork.PlayerList[i].ActorNumber == actorNumber)
            {
                return PhotonNetwork.PlayerList[i];
            }
        }
        return null;
    }

    void Update()
    {
      
       
       /* if (!isGameMaster)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector3 position = this.transform.position;
                position.x--;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector3 position = this.transform.position;
                position.x++;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector3 position = this.transform.position;
                position.z++;
                this.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Vector3 position = this.transform.position;
                position.z--;
                this.transform.position = position;
            }
        }
        else
        {

            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                this.transform.position = new Vector3( hit.point.x,0,hit.point.z);
        
            }
        }*/

    }

    public void EnterGameMasterMode(){
        isGameMaster = GameObject.Find("GameMasterToggle").GetComponent<Toggle>().isOn;
        Debug.Log("Gamemaster = " + isGameMaster);
    }

	private void InitCamera()
	{
		if (isGameMaster)
		{
			Camera.main.GetComponentInParent<CameraController>().UnlockCamera();
			Camera.main.GetComponentInParent<FreeCameraMovement>().gameMaster = transform;
		}
		else
		{
			Camera.main.GetComponentInParent<CameraController>().LockCameraToPlayer(transform);
		}
	}

    [PunRPC]
    public void OnKillReceived(string playerName)
    {
		if (FindObjectOfType<LanguageControl>().GetSelectedLanguage() == 0)
		{
			GameSceneGUIController.Instance.playerMessages.AddMessage(new PlayerMessage(playerName + " has been eliminated!", 3, 5f));
		}
		else
		{
			GameSceneGUIController.Instance.playerMessages.AddMessage(new PlayerMessage(playerName + " ha sido eliminado!", 3, 5f));
		}
		
    }

}
