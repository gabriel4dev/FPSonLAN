using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public MatchSettings matchSettings;

    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Debug.Log("Error more than one GameManager in this scene.");
        }
        else
        {
            GameManager.instance = this;
        }
        
    }

    #region Player tracking

    private const string PLAYER_ID_PREFIX = "Player";
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();
    
    internal static void RegisterPlayer(string pNetId, Player pPlayer)
    {
        string vPlayerID = GameManager.PLAYER_ID_PREFIX + pNetId;

        GameManager.players.Add(vPlayerID, pPlayer);
        pPlayer.transform.name = vPlayerID;
    }

    internal static void UnRegisterPlayer(string pPlayerId)
    {
        GameManager.players.Remove(pPlayerId);
    }

    public static Player GetPlayer(string pPlayerId)
    {
        return GameManager.players[pPlayerId];
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));

    //    GUILayout.BeginVertical();

    //    foreach (string vPlayer in players.Keys)
    //    {
    //        GUILayout.Label(vPlayer + " - " + GameManager.players[vPlayer].transform.name);
    //    }

    //    GUILayout.EndVertical();

    //    GUILayout.EndArea();
    //}

    #endregion
}
