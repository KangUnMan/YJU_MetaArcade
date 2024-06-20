using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using WebSocketSharp;
using Newtonsoft.Json;
using System.Security.Cryptography;

public class WebServerManager: MonoBehaviour
{
    public static WebServerManager Instance { get; set; }
    
    public string UserName { get; set; }
    
    [SerializeField] private string _token;

    [SerializeField] private NetworkConfig config;

    private string _connectableUrl;

    public bool IsJWT => !_token.IsNullOrEmpty();

    private void Awake()
    {
        if (WebServerManager.Instance == null)
        {
            WebServerManager.Instance = this;
            DontDestroyOnLoad(gameObject);

            init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void init()
    {
        _connectableUrl = (config.UrlForWeb.IsNullOrEmpty()) ? config.UrlForLocal : config.UrlForWeb;
    }

    public void Login(object obj, Action<LoginAccountPacketRes> res)
    {
        StartCoroutine(PostSend<LoginAccountPacketRes>("Account/login", obj, (packet) =>
        {
            _token = (packet.LoginOk) ? packet.Token : null;
            UserName = (packet.LoginOk) ? packet.UserName : string.Empty;

            res?.Invoke(packet);
        }));
    }

    public void SignUp(object obj, Action<CreateAccountPacketRes> res)
    {
        StartCoroutine(PostSend<CreateAccountPacketRes>("Account/create", obj, (packet) =>
        {
            res?.Invoke(packet);
        }));
    }

    public bool SetScore(int score)
    {
        if (!IsJWT)
        {
            return false;
        }

        SetRankingPacketReq obj = new SetRankingPacketReq()
        {
            AccountName = UserName,
            Score = score,
        };

        StartCoroutine(PostSend<CreateAccountPacketRes>("Ranking/create", obj, null));

        return true;
    }

    public void UpdateRanking(Action<List<RankingPacketRes>> res)
    {
        StartCoroutine(PostSend<List<RankingPacketRes>>("Ranking/get", null, (packet) =>
        {
            res?.Invoke(packet);
        }));
    }
    
    private IEnumerator PostSend<T>(string url, object obj, Action<T> res)
    {
        string sendUrl = $"{_connectableUrl}/{url}";

        byte[] jsonBytes = null;
        if (obj != null)
        {
            //string jsonStr = JsonUtility.ToJson(obj);
            string jsonStr = JsonConvert.SerializeObject(obj);
            jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
        }

        using (var uwr = new UnityWebRequest(sendUrl, "POST"))
        {
            if (IsJWT == true)
            {
                uwr.SetRequestHeader("Authorization", "Bearer " + _token);
            }
            
            uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");

            
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.LogError(uwr.error);
            }
            else
            {
                //Debug.Log(uwr.downloadHandler.text);
                T resObj = JsonConvert.DeserializeObject<T>(uwr.downloadHandler.text);
                res?.Invoke(resObj);
            }
        }
    }
    
    
}
