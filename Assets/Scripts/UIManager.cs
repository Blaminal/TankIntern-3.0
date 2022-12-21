using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.PlayerJoinedGame += PlayerJoinedGame;

        GameManager.instance.PlayerLeftGame += PlayerLeftGame;
    }
    void PlayerJoinedGame(PlayerInput playerInput)
    {

    }
    void PlayerLeftGame(PlayerInput playerInput)
    {

    }
}
