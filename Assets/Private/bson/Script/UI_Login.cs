using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class UI_Login : MonoBehaviour
{
    [SerializeField] TMP_Text infoText;
    [SerializeField] TMP_InputField loginText;
    [SerializeField] TMP_InputField passwordText;
    [SerializeField] Button signUpBtn;
    [SerializeField] Button signInBtn;
    [SerializeField] Button exitBtn;

    //const string tempUrl = "https://localhost:7130/api";

    private void Awake()
    {
        signUpBtn.onClick.AddListener(OnClickSignUp);
        signInBtn.onClick.AddListener(OnClickSignIn);
        exitBtn.onClick.AddListener(OnClickExitBtn);
        infoText.text = "";
    }

    void OnClickSignUp()
    {
        if (string.IsNullOrEmpty(loginText.text) || string.IsNullOrEmpty(passwordText.text))
        {
            Debug.Log("usage error; need fill email or password");
            return;
        }

        infoText.text = "";

        CreateAccountPacketReq packet = new CreateAccountPacketReq()
        {
            AccountName = loginText.text,
            Password = passwordText.text,
        };
        
        WebServerManager.Instance.SignUp(packet, (res) =>
        {
            Debug.Log(res.CreateOk);
            string result = (res.CreateOk) ? "Success" : "Fail";
            infoText.text = $"SignUP: {result}";
            loginText.text = "";
            passwordText.text = "";
        });
    }

    void OnClickExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnClickSignIn()
    {
        if (string.IsNullOrEmpty(loginText.text) || string.IsNullOrEmpty(passwordText.text))
        {
            Debug.Log("usage error; need fill email or password");
            return;
        }

        infoText.text = "Login...";

        LoginAccountPacketReq packet = new LoginAccountPacketReq()
        {
            AccountName = loginText.text,
            Password = passwordText.text,
        };

        WebServerManager.Instance.Login(packet, (res) =>
        {
            Debug.Log(res.LoginOk);
            string result = (res.LoginOk) ? "Success" : "Fail";
            infoText.text = $"LogIn: {result}";
            loginText.text = "";
            passwordText.text = "";

            if (res.LoginOk)
            {
                PhotonNetwork.LoadLevel("MetaArcade");
            }
        });
        
        // StartCoroutine(WebServerManager.Instance.PostSend<LoginAccountPacketRes>("Account/login", packet, (res) =>
        // {
        //     Debug.Log(res.LoginOk);
        //     infoText.text = $"LogIn: {res.LoginOk}";
        //     loginText.text = "";
        //     passwordText.text = "";
        //
        //     if (res.LoginOk)
        //     {
        //         SceneManager.LoadScene("ShootingRange");
        //     }
        // }));
    }

    // IEnumerator PostSend<T>(string url, object obj, Action<T> res)
    // {
    //     string sendUrl = $"{tempUrl}/{url}";
    //
    //     byte[] jsonBytes = null;
    //     if (obj != null)
    //     {
    //         string jsonStr = JsonUtility.ToJson(obj);
    //         jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
    //     }
    //
    //     using (var uwr = new UnityWebRequest(sendUrl, "POST"))
    //     {
    //         uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
    //         uwr.downloadHandler = new DownloadHandlerBuffer();
    //         uwr.SetRequestHeader("Content-Type", "application/json");
    //
    //         yield return uwr.SendWebRequest();
    //
    //         if (uwr.isNetworkError || uwr.isHttpError)
    //         {
    //             Debug.Log(uwr.error);
    //             infoText.text = uwr.error;
    //         }
    //         else
    //         {
    //             //Debug.Log(uwr.downloadHandler.text);
    //             T resObj = JsonUtility.FromJson<T>(uwr.downloadHandler.text);
    //             res.Invoke(resObj);
    //         }
    //     }
    // }
}
