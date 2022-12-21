using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private PlayerControls controls;

    private Camera main;

    private CharacterController playerController;
    

    
    private void Awake()
    {
        controls = new PlayerControls();
        playerController = gameObject.GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    private void Update()
    {
        Vector3 movementInput = controls.Player.Move.ReadValue<Vector2>();        
        playerController.Move(movementInput * Time.deltaTime);
    }


}
