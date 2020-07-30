using PlayFab;
using PlayFab.AuthenticationModels;
using PlayFab.ClientModels;
using PlayFab.PfEditor.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayFabController : MonoBehaviour
{
    [SerializeField]
    private InputField InputFieldemail;


    [SerializeField]
    private InputField InputFieldepass;

    [SerializeField]
    private InputField InputFieldeusername;

    public GameObject menuCanvas;

    public GameObject connectCanvas;

    public GameObject friendspanel;
    public GameObject shopanel;
    public GameObject lobbypanel;
    public GameObject lederboardpanel;



    private string userEmail
    {
        get
        {
            if (!PlayerPrefs.HasKey("EMAIL"))
            {
                return InputFieldemail.text;
            }
            else
                return PlayerPrefs.GetString("EMAIL");
        }
    }

    private string UserPassword
    {
        get
        {
            if (!PlayerPrefs.HasKey("PASSWORD"))
            {
                return InputFieldepass.text;
            }
            else
                return PlayerPrefs.GetString("PASSWORD");
        }

    }
    public string username
    {
        get
        {
            //if (!PlayerPrefs.HasKey("USERNAME"))
           // {
                return InputFieldeusername.text;
            //}
            //else
                //return PlayerPrefs.GetString("USERNAME");
        }
    }
    private string myID;

    public GameObject panelColor;

    public static PlayFabController PFC;
    public GameObject LoginPanel;

    public GameObject registerpanel;

    public GameObject lobby;
    void DisplayPlayFabError(PlayFabError error) => Debug.Log(error.GenerateErrorReport());

    private void OnEnable()
    {
        if(PlayFabController.PFC == null) PlayFabController.PFC = this;
        else
            if(PlayFabController.PFC != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId)) PlayFabSettings.TitleId = "475FE"; // Please change this value to your own titleId from PlayFab Game Manager

        if (PlayerPrefs.HasKey("EMAIL"))
        {
            //userEmail = PlayerPrefs.GetString("EMAIL");
            //UserPassword = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = UserPassword };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
            LoginPanel.SetActive(false);
            registerpanel.SetActive(false);
        }
        Debug.Log(PlayerPrefs.GetString("EMAIL"));
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

	#region login
	private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", UserPassword);
        GetStats();
        myID = result.PlayFabId;
        GetPlayerData();
        menuCanvas.SetActive(true);
        connectCanvas.SetActive(false);
    }

    private void OnRegisterSucsess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", UserPassword);
        PlayerPrefs.SetString("USERNAME", username);
        PlayerPrefs.Save();
        LoginPanel.SetActive(false);
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = username }, Ondisplaylogin, OnLoginFailure);
        GetStats();
        GetPlayerData();
        GetStats();
        //myID = result.PlayFabId;
        GetPlayerData();
        menuCanvas.SetActive(true);
        connectCanvas.SetActive(false);
    }

    public void Ondisplaylogin(UpdateUserTitleDisplayNameResult result) => Debug.Log(result.DisplayName + "is your new displayer name");

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        //var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = UserPassword,Username = username };
        //PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSucsess, OnRegistereFailure);
    }
    public void OnclickRegisterButton()
    {
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = UserPassword,Username = username };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSucsess, OnRegistereFailure);
    }

    private void OnRegistereFailure(PlayFabError error) => Debug.LogError(error.GenerateErrorReport());

    //public void GetUserEmail(string emailIn) => userEmail = emailIn;

    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = UserPassword};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    #endregion

    public int playerpoints;

    public int kills;

    public int playerhighScore;

    public int playerhealth;

	#region PlayerStats
    public void setStats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
        new StatisticUpdate { StatisticName = "PlayerPoints", Value = playerpoints },
        new StatisticUpdate { StatisticName = "Kills", Value = kills },
        new StatisticUpdate { StatisticName = "PlayerHealth", Value = playerhealth },
        new StatisticUpdate { StatisticName = "playerHighScore", Value = playerhighScore },

    }
        },
result => { Debug.Log("User statistics updated"); },
error => { Debug.LogError(error.GenerateErrorReport()); });
    }

    void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                case "PlayerPoints":
                    playerpoints = eachStat.Value;
                    break;
                case "Kills":
                    kills = eachStat.Value;
                    break;
                case "playerhealth":
                    playerhealth = eachStat.Value;
                    break;
                case "playerHighScore":
                    playerhighScore = eachStat.Value;
                    break;

            }
        }
    }

    public void StartCloudUpdatePlayerstats()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { Points = playerpoints, PlayerKills = kills, health = playerhealth, playerHighScore = playerhighScore }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudUpdateStats, OnErrorShared);
    }
    private static void OnCloudUpdateStats(ExecuteCloudScriptResult result)
    {
        // CloudScript returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);
    }

    private static void OnErrorShared(PlayFabError error) => Debug.Log(error.GenerateErrorReport());

    // OnCloudHelloWorld defined in the next code block
    #endregion playerstats

    #region Leaderboard

    public GameObject leaderboardpanel;
    public GameObject listingprefab;
    public Transform listingcontainer;
    public void GetLeaderboard()
    {
        var requestlederboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "PlayerPoints", MaxResultsCount = 100 };
        PlayFabClientAPI.GetLeaderboard(requestlederboard, OngetLederboard, onErorrlederboard);
    }

    public void OngetLederboard(GetLeaderboardResult result)
    {
        leaderboardpanel.SetActive(true);
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            GameObject templisting = Instantiate(listingprefab, listingcontainer);
            Lederboarllisting LL = templisting.GetComponent<Lederboarllisting>();
            LL.playernametext.text = player.DisplayName;
            LL.playerScore.text = player.StatValue.ToString();
            Debug.Log(player.DisplayName + ": " + player.StatValue);
        }
    }

    public void Closeleaderboardpanel()
    {
        leaderboardpanel.SetActive(false);
        for (int i = listingcontainer.childCount - 1; i >= 0; i --) Destroy(listingcontainer.GetChild(i).gameObject);
    }
    public void onErorrlederboard(PlayFabError error) => Debug.LogError(error.GenerateErrorReport());

    #endregion Leaderboard
    #region playerdata
    public void GetPlayerData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myID,
            Keys = null
        }, UserDataSuccess, onErorrlederboard);
    }

    void UserDataSuccess(GetUserDataResult result)
    {
        if (result.Data == null || !result.Data.ContainsKey("Skins")) Debug.Log("Skins not set");
        else Prescictentdtata.PD.SkinsStringtodata(result.Data["Skins"].Value);
    }

    public void SetUserData(string SkinData)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"Skins", SkinData}
            }
        }, SetDataSuccess, onErorrlederboard);
    }

    void SetDataSuccess(UpdateUserDataResult result) => Debug.Log(result.DataVersion);

    #endregion

    #region friends
    [SerializeField]
    Transform friendscrollview;
    void DisplayFriends(List<FriendInfo> friendsCache)
    {
        foreach (FriendInfo f in friendsCache)
        {
            bool isfound = false;
            if(myfriends != null)
            {
                foreach (FriendInfo g in myfriends)
                    if (f.FriendPlayFabId == g.FriendPlayFabId) isfound = true;
            }
            if(isfound == false)
            {
                GameObject listing = Instantiate(listingprefab, friendscrollview);
                Lederboarllisting tempListing = listing.GetComponent<Lederboarllisting>();
                tempListing.playernametext.text = f.TitleDisplayName;
                myfriends = friendsCache;
            }
        }
    }

    public IEnumerator WaitForFriend()
    {
        yield return new WaitForSeconds(2);
        GetFriends();
    }

    public void RunWaitFuction() => StartCoroutine(WaitForFriend());

    List<FriendInfo> _friends = null;

    public void GetFriends()
    {
        PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest
        {
            IncludeSteamFriends = false,
            IncludeFacebookFriends = false,
            XboxToken = null
        }, result => {
            _friends = result.Friends;
            DisplayFriends(_friends); // triggers your UI
        }, DisplayPlayFabError);
    }
    //show your friends



    enum FriendIdType { PlayFabId, Username, Email, DisplayName };

    void AddFriend(FriendIdType idType, string friendId)
    {
        var request = new AddFriendRequest();
        switch (idType)
        {
            case FriendIdType.PlayFabId:
                request.FriendPlayFabId = friendId;
                break;
            case FriendIdType.Username:
                request.FriendUsername = friendId;
                break;
            case FriendIdType.Email:
                request.FriendEmail = friendId;
                break;
            case FriendIdType.DisplayName:
                request.FriendTitleDisplayName = friendId;
                break;
        }
        // Execute request and update friends when we are done
        PlayFabClientAPI.AddFriend(request, result => {
            Debug.Log("Friend added successfully!");
        }, DisplayPlayFabError);
}
    string friendsearch;
    [SerializeField]
    GameObject friendpanel;

    List<FriendInfo> myfriends;

    public void InputFriendID(string idIn) => friendsearch = idIn;

    public void SubmitFrienRequest() => AddFriend(FriendIdType.PlayFabId, friendsearch);

    public void OpenCloseFriends() => friendpanel.SetActive(!friendpanel.activeInHierarchy);
    #endregion

    public void LogOut()
    {
        PlayerPrefs.DeleteKey("EMAIL");
        PlayerPrefs.DeleteKey("USERNAME");

        connectCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    #region JoinParty
    #endregion


    public void OnClickFriendsButton()
    {
        friendpanel.SetActive(true);
        leaderboardpanel.SetActive(false);
        lobbypanel.SetActive(false);
        shopanel.SetActive(false);
    }

    public void OnClickShopButton()
    {
        friendpanel.SetActive(false);
        leaderboardpanel.SetActive(false);
        lobbypanel.SetActive(false);
        shopanel.SetActive(true);
    }

    public void OnClicklobbyButton()
    {
        friendpanel.SetActive(false);
        leaderboardpanel.SetActive(false);
        lobbypanel.SetActive(true);
        shopanel.SetActive(false);
    }

    public void OnClicklederboardButton()
    {
        friendpanel.SetActive(true);
        leaderboardpanel.SetActive(false);
        lobbypanel.SetActive(false);
        shopanel.SetActive(false);
    }
}