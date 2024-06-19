using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Network", menuName = "Network/NetworkConfig")]
public class NetworkConfig : ScriptableObject
{
    [Header("�� ���� ���� ����")]
    public string UrlForWeb;
    
    //Ȥ�ó� ���� �������� ����ϴ� ��Ȳ
    [Header("���� ���� ���� ����")] 
    public string UrlForLocal;



}
