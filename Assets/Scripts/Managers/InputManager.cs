using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public PauseManager pauseManager; 
    public Player player;

    public CameraManager cameraManager;
    public PresetLoader presetLoader;
    MagnetGun gun;

    public static bool acceptInput = true;

    private InputMaster controls = null;
    public Vector2 leftStick;
    public Vector2 rightStick;



    // Start is called before the first frame update
    void Awake()
    {

        controls = new InputMaster();
        controls.Player.ShootL.performed += _ => ShootL();
        controls.Player.ShootR.performed += _ => ShootR();
        controls.Player.Jump.performed += _ => Jump();
        controls.Player.Pickup.performed += _ => Pickup();
        controls.Player.ResetMagnetism.performed += _ => ResetMagnetism();
        controls.Player.ResetGame.performed += _ => ResetGame();

        controls.Player.Pause.performed += _ => PauseGame();

        controls.Player._1.performed += _ => PressNumber(1);
        controls.Player._2.performed += _ => PressNumber(2);
        controls.Player._3.performed += _ => PressNumber(3);
        controls.Player._4.performed += _ => PressNumber(4);
        controls.Player._5.performed += _ => PressNumber(5);
        controls.Player.Tab.performed += _ => Tab();

        gun = player.GetComponent<MagnetGun>();
    }

    private void Update()
    {
        acceptInput = !PauseManager.isPaused;
        if (!acceptInput) return;

        leftStick = controls.Player.Move.ReadValue<Vector2>();
        rightStick = controls.Player.Look.ReadValue<Vector2>();

        player.inputDirection = leftStick;
        cameraManager.camInput = rightStick;
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    public void ShootL()
    {
        if (!acceptInput) return;
        gun.Shoot(false);
    }

    public void ShootR()
    {
        if (!acceptInput) return;
        gun.Shoot(true);
    }

    public void Jump()
    {
        if (!acceptInput) return;
        player.Jump();
    }

    public void ResetMagnetism()
    {
        MagnetismManager.Instance.ResetMagnetism();
    }

    public void Pickup()
    {
        if (!acceptInput) return;
        gun.Pickup();
    }

    void ResetGame()
    {
        LevelManager.Instance.ResetGame();
    }

    void PressNumber(int number) {

        presetLoader.LoadPreset(number);
    }

    void PauseGame() {
        pauseManager.OpenMenu();
    }

    void Tab() {
        gun.ToggleProjectile();
    }
}