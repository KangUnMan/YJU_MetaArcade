using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "v1.0"; // 게임 버전

    public GameObject playerPrefab; // 플레이어 프리팹

   

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        Screen.SetResolution(940, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 연결");
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null); // 방 없으면 방생성 있으면 방에 입장
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장");
        CreatePlayer();
    }

    void CreatePlayer()
    {
        // "woman" 프리팹을 로드하여 플레이어를 생성합니다.

        GameObject playerTemp = PhotonNetwork.Instantiate("woman", new Vector3(Random.Range(2f, -4f), 3, -7), Quaternion.identity); ;
   
    }
}