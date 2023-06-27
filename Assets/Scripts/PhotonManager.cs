using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : MonoBehaviour
{
    string roomName = "main";
    [SerializeField]
    GameObject PlayerManagerPrefab;

    void Start() {
        PhotonNetwork.ConnectUsingSettings("game");
    }

    void OnConnectedToServer() {
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedLobby() {
        RoomOptions roomOptions = new RoomOptions();
        PhotonNetwork.playerName = PhotonNetwork.countOfPlayers.ToString();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        Debug.Log("lobby joined");
    }

    void OnJoinedRoom() {
        Debug.Log("joined the room");
        PhotonNetwork.Instantiate(PlayerManagerPrefab.name, Vector3.zero, Quaternion.identity, 0);
    }
}
