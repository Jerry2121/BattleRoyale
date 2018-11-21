using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    RectTransform thrusterFuelFill;

    [SerializeField]
    RectTransform healthBarFill;

    [SerializeField]
    Text ammoText;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    GameObject scoreboard;
    [SerializeField]
    Compass compass;
    [SerializeField]
    MiniMapFollow miniMapFollow;

    public Player player { get; protected set; }
    private PlayerController controller;
    private WeaponManager weaponManager;

    void Start()
    {
        PauseMenu.isOn = false;
        compass.Player = player.transform;
        miniMapFollow.player = player.gameObject;
    }

    void Update()
    {
        SetFuelAmount(controller.thrusterFuelAmount);
        SetHealthAmount(player.GetHealthPercentage());
        SetAmmoAmount(weaponManager.GetCurrentWeapon().currentAmmo);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            TogglePauseMenu();
            pauseMenu.GetComponent<PauseMenu>().ShowInventory();
        }

    }

    void SetFuelAmount(float _amount)
    {
        thrusterFuelFill.localScale = new Vector3(_amount, 1f, 1f);
    }

    void SetHealthAmount(float _amount)
    {
        healthBarFill.localScale = new Vector3(_amount, 1f, 1f);
    }

    void SetAmmoAmount(int _amount)
    {
        ammoText.text = _amount.ToString();
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
        controller = player.GetComponent<PlayerController>();
        weaponManager = player.GetComponent<WeaponManager>();
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.isOn = pauseMenu.activeSelf;
    }

}
