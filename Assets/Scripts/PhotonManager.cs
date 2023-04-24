using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string version = "1.0f";
    private string userId = "Tony";
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.NickName = userId;

        Debug.Log(PhotonNetwork.SendRate);

        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        Debug.Log($"PhotonNetwork.InLobby) = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby) = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Filled {returnCode}");

        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        ro.IsOpen = true;
        ro.IsVisible = true;

        PhotonNetwork.CreateRoom("Javis", ro);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        //룸에 접속한 사용자 정보 확인
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"Player Name = {player.Value.NickName}, {player.Value.ActorNumber}");
        }

        Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(0, points.Length);
        PhotonNetwork.Instantiate("Player", points[idx].position, points[idx].rotation, 0);
    }
}
