using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
public class PlayfabManager : MonoBehaviour
{   
    public static PlayfabManager instance;    
    public bool isOnline;
    void Start()
    {
        instance = this;
        login();
    }

    void login()
    {   
        /*if(Application.platform == RuntimePlatform.Android)
            var request = new LoginWithAndroidDeviceIDRequest
            {
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithAndroidDeviceID(request, onLogSuccess, onError);
        {
        }
        else
        {
        }*/
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, onLogSuccess, onError);
    }


    public void changeDisplayName()
    {
        string name = UI_Manager.instance.newlyNameInput.text;

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        }, result =>
        {
            UI_Manager.instance.newlyCreatedPanel.SetActive(false);
            Debug.Log("The player's display name is now: " + result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport()));

    }

    void onLogSuccess(LoginResult result)
    {
        Debug.Log("Logged as: " + result.PlayFabId);
        if(result.NewlyCreated)
        {
            UI_Manager.instance.newlyCreatedPanel.SetActive(true);
        }
        else
        {
            UI_Manager.instance.newlyCreatedPanel.SetActive(false);
        }
        isOnline = true;
        var request = new GetAccountInfoRequest
        {
            PlayFabId = result.PlayFabId
        };
        PlayFabClientAPI.GetAccountInfo(request, onGetAccInfo, onError);
    }

    void onGetAccInfo(GetAccountInfoResult result)
    {
        if(string.IsNullOrEmpty(result.AccountInfo.TitleInfo.DisplayName) == false)
        {
            Debug.Log("Display Name: " + result.AccountInfo.TitleInfo.DisplayName);
            GameManager.instance.setName(result.AccountInfo.TitleInfo.DisplayName);
        }
    }
    void onError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        isOnline = false;
    }

    public void sendLeaderboardWin(int score)
    {
        if(isOnline == false) return;
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "time",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, onError);
    }

    public void sendLeaderboardLosses(int score)
    {
        if(isOnline == false) return;
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Losses",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, onError);
    }

    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful Leaderboard Sent");
    }

    public void GetLeaderbaord()
    {
        if(isOnline == false) return;
        var request = new GetLeaderboardRequest
        {
            StatisticName = "time",
            StartPosition = 0,
            MaxResultsCount = 30
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGetWin, onError);

        var requestLosses = new GetLeaderboardRequest
        {
            StatisticName = "Losses",
            StartPosition = 0,
            MaxResultsCount = 30
        };
        PlayFabClientAPI.GetLeaderboard(requestLosses, OnLeaderBoardGetLosses, onError);
    }

    void OnLeaderBoardGetLosses(GetLeaderboardResult result)
    {

        if(UI_Manager.instance.leadeBoardLossesParent.childCount > 0)
        {
            for (int i = 0; i < UI_Manager.instance.leadeBoardLossesParent.childCount; i++)
            {
                Destroy(UI_Manager.instance.leadeBoardLossesParent.GetChild(i).gameObject);
            }
        }      
        
        foreach (var item in result.Leaderboard)
        {
            GameObject boardObj = Instantiate(UI_Manager.instance.leaderBoardPrefab, UI_Manager.instance.leadeBoardLossesParent);
            boardObj.GetComponent<leaderBoardItem>().id.text = item.DisplayName;
            float value = ((float) item.StatValue / 100f);
            TimeSpan ts = TimeSpan.FromSeconds(value);
            String time = ts.ToString("mm\\:ss\\:ff");
            boardObj.GetComponent<leaderBoardItem>().score.text = time;
            boardObj.GetComponent<leaderBoardItem>().position.text = (item.Position + 1).ToString();
        }
    }

    void OnLeaderBoardGetWin(GetLeaderboardResult result)
    {

        if(UI_Manager.instance.leaderBoardParent.childCount > 0)
        {
            for (int i = 0; i < UI_Manager.instance.leaderBoardParent.childCount; i++)
            {
                Destroy(UI_Manager.instance.leaderBoardParent.GetChild(i).gameObject);
            }
        }      
        
        foreach (var item in result.Leaderboard)
        {
            GameObject boardObj = Instantiate(UI_Manager.instance.leaderBoardPrefab, UI_Manager.instance.leaderBoardParent);
            boardObj.GetComponent<leaderBoardItem>().id.text = item.DisplayName;
            float value = ((float) item.StatValue / 100f);
            TimeSpan ts = TimeSpan.FromSeconds(value);
            String time = ts.ToString("mm\\:ss\\:ff");
            boardObj.GetComponent<leaderBoardItem>().score.text = time;
            boardObj.GetComponent<leaderBoardItem>().position.text = (item.Position + 1).ToString();
        }
    }
}
