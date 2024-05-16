using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsProcessor : MonoBehaviour
{
    [SerializeField] private ShotFactory bulletFactory;
    private PlayerInput input;
    private PlayerActionSettings settings;
    [SerializeField]
    private Rigidbody2D playerBody;

    [SerializeField]
    private float speed;

    private void Start()
    {
        settings = new PlayerActionSettings();
        settings.Player.Enable();
        settings.Player.Shoot.performed += Shoot_performed;
        settings.Player.Jump.performed += Jump_performed;
        settings.Player.Movement.performed += Movement_performed;

    }

    private void Update()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        Vector2 directionToMouse = mouseScreenPosition - transform.position;
        this.transform.up = directionToMouse;
    }

    private void FixedUpdate()
    {
        ApplyMovement(settings.Player.Movement.ReadValue<Vector2>());
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        ApplyMovement(moveVector);
    }

    private void ApplyMovement(Vector2 move)
    {
        playerBody.AddForce(move * speed, ForceMode2D.Force);
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        bulletFactory.CreateShot();
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump");
    }
}
