using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    RectTransform thrusterFuelFill;

    [SerializeField]
    RectTransform healthBarFill;

    [SerializeField]
    TextMeshProUGUI ammoText;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    GameObject scoreboard;
    [SerializeField]
    Compass compass;
    [SerializeField]
    MiniMapFollow miniMapFollow;
    [SerializeField]
    GameObject outsideOfZoneImage;
    [SerializeField]
    InventoryScriptLITE inventoryScriptLITE;
    [SerializeField]
    TextMeshProUGUI killsText;
    [SerializeField]
    TextMeshProUGUI aliveText;
    [SerializeField]
    GameObject winCanvas;
    [SerializeField]
    TextMeshProUGUI GameStartingText;
    [SerializeField]
    GameObject GameStartButtonText;
    [SerializeField]
    GameObject MapCanvas;
    [SerializeField]
    GameObject HUD;
    [SerializeField]
    GameObject OutOfAmmoText;
    [SerializeField]
    Slider InputSlider;
    [SerializeField]
    TextMeshProUGUI InputFieldText;
    [SerializeField]
    Animator InventoryCanvas;
    [SerializeField]
    GameObject InventoryDropCanvas;
    [SerializeField]
    Slider DropCanvasSlider;
    [SerializeField]
    GameObject HeavyAmmo;
    [SerializeField]
    GameObject MediumAmmo;
    [SerializeField]
    GameObject LightAmmo;
    [SerializeField]
    GameObject Bandages;
    [SerializeField]
    GameObject HeavyDropButton;
    [SerializeField]
    GameObject MediumDropButton;
    [SerializeField]
    GameObject LightDropButton;
    [SerializeField]
    GameObject BandagesDropButton;
    [SerializeField]
    TextMeshProUGUI ItemText;
    [SerializeField]
    GameObject HeavyButton;
    [SerializeField]
    GameObject MediumButton;
    [SerializeField]
    GameObject LightButton;
    [SerializeField]
    GameObject BandagesButton;



    [Header("Inventory Items And Components")]
    //manages all the inventory based interactions
    public KeyCode inventoryKey = KeyCode.I;
    public KeyCode useKey = KeyCode.E;
    public KeyCode healKey = KeyCode.Q;

    //this is for UI stuff and tracking how much ammo you have.
    public int lightAmmoAmount;
    public int mediumAmmoAmount;
    public int heavyAmmoAmount;
    public int healingItemsAmount;

    //Manages all the UI updates
    [SerializeField]
    TextMeshProUGUI HeavyAmmoText;
    [SerializeField]
    TextMeshProUGUI RifleAmmoText;
    [SerializeField]
    TextMeshProUGUI PistolAmmoText;
    [SerializeField]
    TextMeshProUGUI BandagesText;
    [SerializeField]
    TextMeshProUGUI healingItemsAmountText;

    public GameObject itemPrompt;

    public float rayLength = 2;
    private Transform lookingAt;
    [Tooltip("The layermask used when it comes to raycasting for items.")]
    public LayerMask layerMask;
    private float timer;


    public static bool InInventory = false;

    public bool isMapOpen = false;
    public bool started;
    private float GameStartTimer;
    public InventoryScriptLITE invScript { get { return inventoryScriptLITE; } }

    public Player player { get; protected set; }
    private PlayerController controller;
    private WeaponManager weaponManager;

    void Start()
    {
        MapCanvas = GameObject.FindGameObjectWithTag("Map");
        isMapOpen = false;
        started = false;
        PauseMenu.isOn = false;
        compass.Player = player.transform;
        miniMapFollow.player = player.gameObject;
        GetComponent<WeaponSwitchingUI>().weaponManager = player.GetComponent<WeaponManager>();
        player.outsideOfZoneImage = outsideOfZoneImage;
        inventoryScriptLITE.player = player.transform;
        if (pauseMenu.activeSelf == true)
            TogglePauseMenu();
        
    }

    void Update()
    {
        killsText.text = "Kills: " + player.kills;
        aliveText.text = "Alive: " + GameManager.GetAllPlayers().Length;
        GameStartTimer = GameManager.Instance.gameTimer;
        SetFuelAmount(controller.thrusterFuelAmount);
        SetHealthAmount(player.GetHealthPercentage());
        if (weaponManager.GetCurrentWeapon() != null)
            SetAmmoAmount(weaponManager.GetCurrentWeapon().currentAmmo);
        else
            SetAmmoAmount(0);

        if (Input.GetKeyDown(healKey))
        {
                if (player.GetComponent<Player>().GetHealthPercentage() < 1.0f)
                {
                    player.GetComponent<Player>().CmdHeal(20);
                    healingItemsAmount--;
                    healingItemsAmountText.text = "(" + healingItemsAmount + ")";
                }
        }
        RaycastHit hit;
        Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);


        if (Physics.Raycast(player.transform.position, fwd, out hit, rayLength, layerMask) && !PauseMenu.isOn)
        {
            Debug.Log(hit.collider.gameObject.name);
            itemPrompt.SetActive(true);
            if (hit.transform != lookingAt)
            {
                if (lookingAt != null)
                {
                    lookingAt.SendMessage("LookAway", SendMessageOptions.DontRequireReceiver);
                    lookingAt = null;
                }
                hit.transform.SendMessage("LookAt", SendMessageOptions.DontRequireReceiver);
                lookingAt = hit.transform;
            }
            if (Input.GetKeyUp(useKey))
            {
                
                 PickableItem temp = hit.transform.GetComponent<PickableItem>();


                if (temp.itemType == "Healing")
                {
                    if (temp.amount > 1)
                        healingItemsAmount += temp.amount;
                    else
                        healingItemsAmount++;
                    healingItemsAmountText.text = "(" + healingItemsAmount + ")";
                }

                if (temp.itemType == "LightAmmo")
                    if (temp.amount > 1)
                        lightAmmoAmount += temp.amount;
                    else
                        lightAmmoAmount++;
                else if (temp.itemType == "MediumAmmo")
                    if (temp.amount > 1)
                        mediumAmmoAmount += temp.amount;
                    else
                        mediumAmmoAmount++;
                else if (temp.itemType == "HeavyAmmo")
                    if (temp.amount > 1)
                        heavyAmmoAmount += temp.amount;
                    else
                        heavyAmmoAmount++;


                //}
                //hit.transform.SendMessage ("Interacted", transform, SendMessageOptions.DontRequireReceiver);
                //hit.transform.SendMessage ("Execute", SendMessageOptions.DontRequireReceiver);
                Destroy(hit.transform.gameObject);
                itemPrompt.SetActive(false);
            }
        }
        else
        {
            itemPrompt.SetActive(false);
        }

        //HEAVY AMMO CHECKING IF NEGATIVE OR NOT THEN SETTING TO 0 IF ITS NEGATIVE ALONG WITH TURNING OFF THE BUTTON IF THE VALUE IS 0 OTHERWISE TURNING IT ON.
        if (heavyAmmoAmount < 0)
        {
            heavyAmmoAmount = 0;
        }
        else if (heavyAmmoAmount == 0)
        {
            HeavyDropButton.SetActive(false);
        }
        else
        {
            HeavyDropButton.SetActive(true);
        }

        //MEDIUM AMMO CHECKING IF NEGATIVE OR NOT THEN SETTING TO 0 IF ITS NEGATIVE ALONG WITH TURNING OFF THE BUTTON IF THE VALUE IS 0 OTHERWISE TURNING IT ON.
        if (mediumAmmoAmount < 0)
        {
            mediumAmmoAmount = 0;
        }
        else if (mediumAmmoAmount == 0)
        {
            MediumDropButton.SetActive(false);
        }
        else
        {
            MediumDropButton.SetActive(true);
        }

        //LIGHT AMMO CHECKING IF NEGATIVE OR NOT THEN SETTING TO 0 IF ITS NEGATIVE ALONG WITH TURNING OFF THE BUTTON IF THE VALUE IS 0 OTHERWISE TURNING IT ON.
        if (lightAmmoAmount < 0)
        {
            lightAmmoAmount = 0;
            LightDropButton.SetActive(false);
        }
        else if (lightAmmoAmount == 0)
        {
            LightDropButton.SetActive(false);
        }
        else
        {
            LightDropButton.SetActive(true);
        }

        //BANDAGES CHECKING IF NEGATIVE OR NOT THEN SETTING TO 0 IF ITS NEGATIVE ALONG WITH TURNING OFF THE BUTTON IF THE VALUE IS 0 OTHERWISE TURNING IT ON.
        if (healingItemsAmount < 0)
        {
            healingItemsAmount = 0;
            BandagesDropButton.SetActive(false);
        }
        else if (healingItemsAmount == 0)
        {
            BandagesDropButton.SetActive(false);
        }
        else
        {
            BandagesDropButton.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.I) && !InInventory)
        {
            InInventory = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I) && InInventory)
        {
            InInventory = false;
        }
        if (InInventory)
        {
            InventoryCanvas.SetBool("Inventory", true);
            HeavyAmmoText.text = "(" + heavyAmmoAmount + ")";
            RifleAmmoText.text = "(" + mediumAmmoAmount + ")";
            PistolAmmoText.text = "(" + lightAmmoAmount + ")";
            BandagesText.text = "" + healingItemsAmountText.text;
        }
        else if (!InInventory && PauseMenu.isOn == false)
        {
            InventoryCanvas.SetBool("Inventory", false);
            InventoryDropCanvas.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
            pauseMenu.GetComponent<PauseMenu>().ShowResume();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
        if (GameStartTimer <= 0 && GameManager.GetAllPlayers().Length > 1)
        {
            GameStartingText.text = "Game Starting In: " + Mathf.RoundToInt(GameStartTimer) * -1;
        }
        else if (GameStartTimer >= 0 && GameManager.GetAllPlayers().Length > 1)
        {
            GameStartingText.text = "";
            started = true;
        }
        if (GameManager.GetAllPlayers().Length <= 1 && GameStartTimer == -120 && GameManager.Instance.inStartPeriod)
        {
            GameStartingText.text = "Not Enough Players. Need 1 More Player.";
        }
        if (NetworkManager.singleton.GetComponent<NetworkDiscoveryScript>().isServer && !started && GameManager.GetAllPlayers().Length > 1)
        {
            GameStartButtonText.SetActive(true);
        }
        else
        {
            GameStartButtonText.SetActive(false);
        }
        if (weaponManager.GetCurrentWeapon() == null)
        {
            ammoText.text = "N/A";
        }
        if (weaponManager.GetCurrentWeapon() != null && weaponManager.GetCurrentWeapon().currentAmmo == 0)
        {
            if (weaponManager.GetCurrentWeapon().weaponType == WeaponType.Light && lightAmmoAmount == 0)
            {
                OutOfAmmoText.SetActive(true);
            }
            if (weaponManager.GetCurrentWeapon().weaponType == WeaponType.Medium && mediumAmmoAmount == 0)
            {
                OutOfAmmoText.SetActive(true);
            }
            if (weaponManager.GetCurrentWeapon().weaponType == WeaponType.Heavy && heavyAmmoAmount == 0)
            {
                OutOfAmmoText.SetActive(true);
            }
        }
        else
        {
            OutOfAmmoText.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MapCanvas.transform.GetComponentInChildren<Canvas>().enabled = !MapCanvas.transform.GetComponentInChildren<Canvas>().enabled;
            HUD.SetActive(!HUD.activeSelf);
        }
        /*if (MapCanvas.transform.GetComponentInChildren<Canvas>().enabled == true)
        {
            isMapOpen = true;
        }*/
        else
        {
            isMapOpen = false;
        }
        if (Input.GetKeyDown(KeyCode.P) && GameManager.Instance.inStartPeriod && GameManager.GetAllPlayers().Length > 1 && NetworkManager.singleton.GetComponent<NetworkDiscoveryScript>().isServer && !started)
        {
            started = true;
            GameStartButtonText.SetActive(false);
            GameManager.Instance.gameTimer = -5;
        }

        if (GameManager.IsGameOver())
        {
            winCanvas.SetActive(true);
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
        if (weaponManager.GetCurrentWeapon() != null && weaponManager.GetCurrentWeapon().weaponType == WeaponType.Light)
        {
            ammoText.text = (_amount.ToString() + " / " + lightAmmoAmount);
        }
        if (weaponManager.GetCurrentWeapon() != null && weaponManager.GetCurrentWeapon().weaponType == WeaponType.Medium)
        {
            ammoText.text = (_amount.ToString() + " / " + mediumAmmoAmount);
        }
        if (weaponManager.GetCurrentWeapon() != null && weaponManager.GetCurrentWeapon().weaponType == WeaponType.Heavy)
        {
            ammoText.text = (_amount.ToString() + " / " + heavyAmmoAmount);
        }
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
        controller = player.GetComponent<PlayerController>();
        weaponManager = player.GetComponent<WeaponManager>();
    }

    public void TogglePauseMenu()
    {
        PauseMenu pauseMenuScript = pauseMenu.GetComponent<PauseMenu>();
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.isOn = pauseMenu.activeSelf;
    }

    public void DropItemSliderUpdate()
    {
        InputFieldText.text = "" + InputSlider.value;
    }
    public void InventoryHeavyDropButton()
    {
        InventoryDropCanvas.SetActive(true);
        HeavyButton.SetActive(true);
        MediumButton.SetActive(false);
        LightButton.SetActive(false);
        BandagesButton.SetActive(false);
        ItemText.text = "Heavy Ammo";
        float total = heavyAmmoAmount;
        DropCanvasSlider.maxValue = total;
        DropCanvasSlider.value = 1.0f;
    }
    public void InventoryMediumDropButton()
    {
        InventoryDropCanvas.SetActive(true);
        HeavyButton.SetActive(false);
        MediumButton.SetActive(true);
        LightButton.SetActive(false);
        BandagesButton.SetActive(false);
        ItemText.text = "Medium Ammo";
        float total = mediumAmmoAmount;
        DropCanvasSlider.maxValue = total;
        DropCanvasSlider.value = 1.0f;
    }
    public void InventoryLightDropButton()
    {
        InventoryDropCanvas.SetActive(true);
        HeavyButton.SetActive(false);
        MediumButton.SetActive(false);
        LightButton.SetActive(true);
        BandagesButton.SetActive(false);
        ItemText.text = "Light Ammo";
        float total = lightAmmoAmount;
        DropCanvasSlider.maxValue = total;
        DropCanvasSlider.value = 1.0f;
    }
    public void InventoryBandagesDropButton()
    {
        InventoryDropCanvas.SetActive(true);
        HeavyButton.SetActive(false);
        MediumButton.SetActive(false);
        LightButton.SetActive(false);
        BandagesButton.SetActive(true);
        ItemText.text = "Bandages";
        float total = healingItemsAmount;
        DropCanvasSlider.value = 1.0f;
    }
    public void InventoryDropButtonExit()
    {
        InventoryDropCanvas.SetActive(false);
    }
    public void InventoryHeavyDropCanvasDropButton()
    {
        int amountToDrop;
        amountToDrop = Mathf.RoundToInt(DropCanvasSlider.value);
        float TempDropAmount = 0;
        int DropAmount = (int)TempDropAmount;
        GameObject ammo = Utility.InstantiateOverNetwork(HeavyAmmo, player.transform.position, player.transform.rotation);
        if (heavyAmmoAmount - amountToDrop < 0)
        {
            int DropEverything = heavyAmmoAmount;
            ammo.GetComponent<PickableItem>().amount = DropEverything;
            return;
        }
        else if (heavyAmmoAmount - amountToDrop >= 0)
        {
            DropAmount = heavyAmmoAmount - amountToDrop;
        }
        int TotalDropAmount = DropAmount;
        ammo.GetComponent<PickableItem>().amount = DropAmount;
        InventoryDropCanvas.SetActive(false);
    }
    public void InventoryMediumDropCanvasDropButton()
    {
        int amountToDrop;
        amountToDrop = Mathf.RoundToInt(DropCanvasSlider.value);
        float TempDropAmount = 0;
        int DropAmount = (int) TempDropAmount;
        GameObject ammo = Utility.InstantiateOverNetwork(MediumAmmo, player.transform.position, player.transform.rotation);
        if (mediumAmmoAmount - amountToDrop < 0)
        {
            int DropEverything = mediumAmmoAmount;
            ammo.GetComponent<PickableItem>().amount = DropEverything;
            return;
        }
        else if (mediumAmmoAmount - amountToDrop >= 0)
        {
            DropAmount = mediumAmmoAmount - amountToDrop;
        }
        int TotalDropAmount = DropAmount;
        ammo.GetComponent<PickableItem>().amount = DropAmount;
        InventoryDropCanvas.SetActive(false);
    }
    public void InventoryLightDropCanvasDropButton()
    {
        int amountToDrop;
        amountToDrop = Mathf.RoundToInt(DropCanvasSlider.value);
        float TempDropAmount = 0;
        int DropAmount = (int)TempDropAmount;
        GameObject ammo = Utility.InstantiateOverNetwork(LightAmmo, player.transform.position, player.transform.rotation);
        if (lightAmmoAmount - amountToDrop < 0)
        {
            int DropEverything = lightAmmoAmount;
            ammo.GetComponent<PickableItem>().amount = DropEverything;
            return;
        }
        else if (lightAmmoAmount - amountToDrop >= 0)
        {
            DropAmount = lightAmmoAmount - amountToDrop;
        }
        int TotalDropAmount = DropAmount;
        ammo.GetComponent<PickableItem>().amount = DropAmount;
        InventoryDropCanvas.SetActive(false);
    }
    public void InventoryBandagesDropCanvasDropButton()
    {
        int amountToDrop;
        amountToDrop = Mathf.RoundToInt(DropCanvasSlider.value);
        float TempDropAmount = 0;
        int DropAmount = (int)TempDropAmount;
        GameObject ammo = Utility.InstantiateOverNetwork(Bandages, player.transform.position, player.transform.rotation);
        if (healingItemsAmount - amountToDrop < 0)
        {
            int DropEverything = healingItemsAmount;
            ammo.GetComponent<PickableItem>().amount = DropEverything;
            return;
        }
        else if (healingItemsAmount - amountToDrop >= 0)
        {
            DropAmount = healingItemsAmount - amountToDrop;
        }
        int TotalDropAmount = DropAmount;
        ammo.GetComponent<PickableItem>().amount = DropAmount;
        InventoryDropCanvas.SetActive(false);
    }
}
