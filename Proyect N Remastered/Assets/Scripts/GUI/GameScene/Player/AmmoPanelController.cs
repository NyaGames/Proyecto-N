﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPanelController : MonoBehaviour
{
    private AmmoInfo myPlayerAmmoinfo;
    public TMPro.TextMeshProUGUI ammoText;

	[SerializeField] private Button photoButton;

    void Start()
    {
        myPlayerAmmoinfo = GameManager.Instance.myPlayer.GetComponent<AmmoInfo>();
    }

   
    void Update()
    {
		if (myPlayerAmmoinfo.currentAmmo <= 0 || !GameManager.Instance.gameStarted)
        {
			photoButton.interactable = false;           
        }
        else
        {
			photoButton.interactable = true;
        }

		if (FindObjectOfType<LanguageControl>().GetSelectedLanguage() == 0)
		{
			ammoText.text = myPlayerAmmoinfo.currentAmmo.ToString() + "\nSnaps";
		}
		else
		{
			ammoText.text = myPlayerAmmoinfo.currentAmmo.ToString() + "\nSnaps";
		}
		
	}
}
