using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnPoint;

    public static GameManager instance = null;

    public List<PlayerInput> playerList = new List<PlayerInput>();
    [SerializeField]
    private InputAction joinAction;
    [SerializeField]
    private InputAction leaveAction;

    public event System.Action<PlayerInput> PlayerJoinedGame;
    public event System.Action<PlayerInput> PlayerLeftGame;

    [SerializeField]
    private List<GameObject> playerHasntJoinedUI;
    [SerializeField]
    private List<GameObject> playerHasJoinedUI;
    [SerializeField]
    private List<Text> scoreText;

    private List<int> currentPlayerScores;

    // Dictionary < PlayerInput, Tuple<GameObject, GameObject>> dictionary
    // dictionary[playerInput] --> Tuple<GameObject, GameObject> // O(1) 
    // https://developerinsider.co/big-o-notation-explained-with-examples/

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");

        joinAction.Enable();
        joinAction.performed += context => JoinAction(context);

        leaveAction.Enable();
        leaveAction.performed += context => LeaveAction(context);

        currentPlayerScores = new List<int>{0,0,0,0};
        Debug.Log(currentPlayerScores.Count);

    }

    public void ChangeScoreOfPlayer(PlayerInput playerInput, int amountToChange) {
        int index = playerList.IndexOf(playerInput);
        Debug.Log("Index: " + index);
        Debug.Log(currentPlayerScores.Count);
        currentPlayerScores[index] += amountToChange;
        scoreText[index].text = currentPlayerScores[index].ToString();
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        playerList.Add(playerInput);
        playerInput.gameObject.GetComponentInChildren<TankMover>().gameManager = this;

        Debug.Log("On Player Joined Getting Called");
        playerHasntJoinedUI[playerList.Count - 1].SetActive(true);
        playerHasJoinedUI[playerList.Count - 1].SetActive(false);
        PlayerJoinedGame?.Invoke(playerInput);
        // if (PlayerJoinedGame != null)
        // {
        //     PlayerJoinedGame(playerInput);
        // }
    }

    void OnPlayerLeft(PlayerInput playerInput) // TODO - check if gets called when the object is destroyed
    {
        int index = playerList.IndexOf(playerInput);
        playerHasntJoinedUI[index].SetActive(false);
        playerHasJoinedUI[index].SetActive(true);
    }

    void JoinAction(InputAction.CallbackContext context)
    {
        PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);
    }

    void LeaveAction(InputAction.CallbackContext context)
    {
        if(playerList.Count > 1)
        {
            foreach (var player in playerList)
            {
                foreach(var device in player.devices)
                {
                    if(device != null && context.control.device == device)
                    {
                        UnregisterPlayer(player);
                        return;
                    }
                }
            }
        }
    }

    public void UnregisterPlayer(PlayerInput playerInput)
    {
        playerList.Remove(playerInput);

        if (PlayerLeftGame != null)
        {
            PlayerLeftGame(playerInput);
        }

        Destroy(playerInput.gameObject);
    }
}
