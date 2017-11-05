using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Collections.Generic;
using MySocket;
using UnityEngine.EventSystems;


namespace Prototype.NetworkLobby
{
    public class LobbyManager : NetworkLobbyManager 
    {

        static public LobbyManager s_Singleton;
		static public GameObject localPlayer;
		public string localPlayerName;
		public LogPanel logPanel;
		public LoadingNum loadingNum;
		public GameObject idManager;

		public ClientSocket clientSocket;

		private int currentPlayerNum = 0;
		private List<GameObject> gamePlayers = new List<GameObject> ();


        [Header("Unity UI Lobby")]
        [Tooltip("Time in second between all players ready & match start")]
        public float prematchCountdown = 5.0f;

        public LobbyCountdownPanel countdownPanel;
        protected RectTransform currentPanel;

        //Client numPlayers from NetworkManager is always 0, so we count (throught connect/destroy in LobbyPlayer) the number
        //of players, so that even client know how many player there is.
        [HideInInspector]
        public int _playerNumber = 0;

        //used to disconnect a client properly when exiting the matchmaker
        [HideInInspector]
        public bool _isMatchmaking = false;

        protected bool _disconnectServer = false;
        
        protected ulong _currentMatchID;

        protected LobbyHook _lobbyHooks;

        void Start()
        {
            s_Singleton = this;
            _lobbyHooks = GetComponent<Prototype.NetworkLobby.LobbyHook>();

            DontDestroyOnLoad(gameObject);
        }

		public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
		{
			loadingNum.countFromAndTo (60,80);
			base.OnMatchCreate(success, extendedInfo, matchInfo);
            _currentMatchID = (System.UInt64)matchInfo.networkId;
			logPanel.OnMatching ();
		}

		public override void OnMatchJoined (bool success, string extendedInfo, MatchInfo matchInfo){
			loadingNum.countFromAndTo (60,80);
			base.OnMatchJoined (success,extendedInfo,matchInfo);
			Debug.Log ("on match joining");
			Debug.Log ("extendInfo is : " + extendedInfo);
			Debug.Log ("matchinfo to string is : "+matchInfo.ToString ());
//			Debug.Log ("");
			logPanel.OnMatching ();
		}

		public override void OnDestroyMatch(bool success, string extendedInfo)
		{
			base.OnDestroyMatch(success, extendedInfo);
			if (_disconnectServer)
            {
                StopMatchMaker();
                StopHost();
            }
        }

		public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId){
			GameObject lobbyplayer = Instantiate (lobbyPlayerPrefab.gameObject);
			Debug.Log ("create lobby player , curent number is "+currentPlayerNum);
			lobbyplayer.GetComponent<LobbyPlayer> ().ID = currentPlayerNum;
			currentPlayerNum++;
			return lobbyplayer;
		}

		public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn,short playerControllerId){
			GameObject player = Instantiate (gamePlayerPrefab) as GameObject;
			gamePlayers.Add (player);
			return player;
		}

        public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
		{
			base.OnLobbyServerSceneLoadedForPlayer (lobbyPlayer,gamePlayer);
			string name = lobbyPlayer.GetComponent<LobbyPlayer> ().Name;
			Debug.Log ("OnLobbyServerSceneLoadedForPlayer,get lobby player name is "+name);
			gamePlayer.GetComponent<PlayerMessage> ().Name = name;
			gamePlayer.GetComponent <PlayerMessage>().ID = lobbyPlayer.GetComponent<LobbyPlayer>().ID;
            return true;
		}

		public override void OnLobbyClientSceneChanged(NetworkConnection conn)
		{
			base.OnLobbyClientSceneChanged (conn);
			Debug.Log ("scene");
		}


		public override void OnLobbyServerSceneChanged(string sceneName){
			base.OnLobbyServerSceneChanged (sceneName);
			PlanetManager instance = GameObject.Find ("PlanetManager").GetComponent<PlanetManager> ();
			if (instance) {
				Debug.Log ("distribute id ");
				GameObject id = Instantiate (idManager);
				NetworkServer.Spawn (id);
				id.GetComponent<IDManager> ().player0 = 0;
				id.GetComponent<IDManager> ().player1 = 1;
				id.GetComponent<IDManager> ().player2 = 2;
				id.GetComponent<IDManager> ().player3 = 3;
			} 
		}

		public override void OnDropConnection(bool success,string extendedinfo){
			Debug.Log ("override OnDropConnection message==============================================");
			Application.Quit ();
			base.OnDropConnection (success,extendedinfo);
			matchMaker.CreateMatch("game",	(uint)maxPlayers,	true,	"", "", "", 0, 0,OnMatchCreate);
		}
    }
}
