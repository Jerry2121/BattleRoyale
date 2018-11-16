using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class HealthBar : MonoBehaviour {
    public Slider healthslider;
    public Slider StaminaSlider;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI StaminaText;
    public int Health = 100;
    public float Stamina = 100;
    public bool run;
    private bool walking;
    private KeyCode RunKey = KeyCode.LeftShift;
    private KeyCode WalkForward = KeyCode.W;
    private KeyCode WalkBackward = KeyCode.S;
    private KeyCode WalkLeft = KeyCode.A;
    private KeyCode WalkRight = KeyCode.D;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(WalkForward) || Input.GetKey(WalkBackward) || Input.GetKey(WalkLeft) || Input.GetKey(WalkRight))
        {
            walking = true;
        }
        else
        {
            walking = false;
        }
        if (Health >= 100)
        {
            Health = 100;
        }
        else if (Health <= 0)
        {
            Health = 0;
        }
        if (Stamina >= 100)
        {
            Stamina = 100;
        }
        else if (Stamina <= 0)
        {
            run = false;
            Stamina = 0;
        }
        if (Input.GetKey(RunKey) && Stamina > 0 && walking) 
        {
            run = true;
            Stamina -= Time.deltaTime * 3;
        }
        else
        {
            Stamina += Time.deltaTime / 0.75f;
        }
        healthslider.value = Health;
        StaminaSlider.value = Stamina;
        HealthText.GetComponent<TextMeshProUGUI>().text = "+ " + healthslider.value;
        StaminaText.GetComponent<TextMeshProUGUI>().text = "" + StaminaSlider.value;
    }
}
