using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class PhotonManager : MonoBehaviour
{
    string roomName = "main";
    RoomOptions roomOptions = new RoomOptions();

    void OnJoinedLobby()
    {
        PhotonNetwork.playerName = PhotonNetwork.countOfPlayers.ToString();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }
}
