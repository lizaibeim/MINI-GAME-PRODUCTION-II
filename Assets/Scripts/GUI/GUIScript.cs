using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    int lastHP;
    public PlayerStatus playerStatus;

    public Slider hpSlider;

    public Image img;
    public float overlaySpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();
        playerStatus.takeDamageEvent += StartOverlay;
    }

    private void OnDisable()
    {
        playerStatus.takeDamageEvent -= StartOverlay;
    }


    // Update is called once per frame
    void Update()
    {
        if (lastHP != playerStatus.health)
        {
            lastHP = playerStatus.health;
            hpSlider.value = playerStatus.health;
            hpText.text = "" + lastHP;
        }

        if (img.color.a > 0)
        {
            Color c = img.color;
            c.a -= overlaySpeed;
            img.color = c;
        }
    }

    public void StartOverlay() {
        Color c = img.color;
        c.a = 0.5F;
        img.color = c;
    }
}
