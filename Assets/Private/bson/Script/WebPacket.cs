using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

[System.Serializable]
public class CreateAccountPacketReq
{
    public string AccountName { get; set; }
    public string Password { get; set; }
}

[System.Serializable]
public class CreateAccountPacketRes
{
    
    public bool CreateOk { get; set; }
}

[System.Serializable]
public class LoginAccountPacketReq
{
    public string AccountName { get; set; }
    public string Password { get; set; }
}

public class ServerInfo
{
    public string Name { get; set; }
    public string Ip { get; set; }
    public int CrowdedLevel { get; set; }
}

[System.Serializable]
public class LoginAccountPacketRes
{
    public bool LoginOk { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
    public DateTime ExpiredTime { get; set; }
}

[System.Serializable]
public class SetRankingPacketReq
{
    public string AccountName { get; set; }
    public int Score { get; set; }
}

[System.Serializable]
public class SetRankingPacketRes
{
    public bool SetRankOk { get; set; }
}

[System.Serializable]
public class RankingPacketRes
{
    public string AccountName { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }
}
