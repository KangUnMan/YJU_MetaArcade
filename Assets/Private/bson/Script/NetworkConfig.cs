using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Network", menuName = "Network/NetworkConfig")]
public class NetworkConfig : ScriptableObject
{
    [Header("웹 서버 연결 정보")]
    public string UrlForWeb;
    
    //혹시나 로컬 웹서버를 써야하는 상황
    [Header("로컬 서버 연결 정보")] 
    public string UrlForLocal;



}
