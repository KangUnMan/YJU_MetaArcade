using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

public class Photon_Manager : MonoBehaviourPunCallbacks
{
    public List<GameObject> list_Photon_Prefabs;
    public Transform tf_Respawn_Point;
    public UserData m_LocalPlayer_Data;
    public GameObject obj_LocalPlayer;

    private void Start()
    {
        //여기서 포톤에서 생성하는 오브젝트를 등록
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        pool.ResourceCache.Clear();
        if (pool != null && list_Photon_Prefabs != null)
        {
            foreach (GameObject prefab in list_Photon_Prefabs)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }
        }
        InitPhotonServer();
    }

    //포톤 마스터 서버 접속 및 로비 입장
    private void InitPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Photon : ConnectUsingSettings");
        }
    }

    //방 입장
    private void JoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom("YD_METAVERSE", roomOptions, TypedLobby.Default);
    }

    private void CreateCharacter(UserData m_data)
    {
        if (m_data.gender == 0)
        {
            Debug.Log("남자 캐릭터 생성");
            obj_LocalPlayer = PhotonNetwork.Instantiate(list_Photon_Prefabs[0].name, tf_Respawn_Point.position, Quaternion.identity);
        }
        else if (m_data.gender == 1)
        {
            obj_LocalPlayer = PhotonNetwork.Instantiate(list_Photon_Prefabs[1].name, tf_Respawn_Point.position, Quaternion.identity);
            Debug.Log("여자 캐릭터 생성");
        }
        else
        {
            Debug.Log("성별이 없으니 생성하지 않습니다.");
        }
    }

    ///////////////////포톤마스터서버 관련 콜백 시작
    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon : OnConnectedToMaster");
    }

    ///////////////////포톤마스터서버 관련 콜백 끝

    ///////////////////로비 관련 콜백 시작
    public override void OnJoinedLobby()
    {
        Debug.Log("Photon : OnJoinedLobby");
        JoinRoom();
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Photon : OnLeftLobby");
    }

    ///////////////////로비 관련 콜백 끝

    ///////////////////룸 관련 콜백 시작

    public override void OnCreatedRoom()
    {
        Debug.Log("Photon : OnCreatedRoom");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Photon : OnCreateRoomFailed returnCode : " + returnCode + ", message : " + message);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Photon : OnJoinedRoom");
        CreateCharacter(m_LocalPlayer_Data);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Photon : OnJoinRoomFailed returnCode : " + returnCode + ", message : " + message);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Photon : OnLeftRoom");
    }

    ///////////////////룸 관련 콜백 끝

    ///////////////////플레이어 관련 콜백 시작
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Photon : OnPlayerEnteredRoom newPlayer : " + newPlayer.UserId);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Photon : OnPlayerLeftRoom otherPlayer : " + otherPlayer.UserId);
    }

    ///////////////////플레이어 관련 콜백 끝
}

[System.Serializable]
public class UserData
{
    /// <summary>
    /// 0 : 남, 1 : 여
    /// </summary>
    public int gender;

    public string name;

    public UserData()
    {
        gender = 0;
        name = "";
    }
}