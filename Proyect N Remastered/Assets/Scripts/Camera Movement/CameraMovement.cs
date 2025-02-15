﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CameraMovement : MonoBehaviour
{
	[Header("Camera Initialization")]
	[SerializeField] protected Vector3 startingStick;
	[SerializeField] protected Vector3 startingSwivel;

	protected Transform swivel, stick;
    protected Camera m_camera;   

	[Range(0, 1)]
    public float zoom;
	protected float smoothTime = 0.5f;


	private void Awake()
    {
        swivel = transform.GetChild(0);
        stick = swivel.GetChild(0);
        m_camera = stick.GetChild(0).GetComponent<Camera>();		
    }

    protected void Update()
    {
        if (ZoneManager.Instance.isEditingZone || ZoneManager.Instance.isEditingDrops) return;

        if(Input.touchCount > 0)
        {
            if (PersistentData.isGM)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if(Input.GetTouch(i).position.x / Screen.width > 0.66)
                    {
                        return;
                    }
                }
            }

            HandleInput();
        }
    }

    protected abstract void HandleInput();
    public abstract void Initialize();

	public abstract IEnumerator DampStick(Action onCoroutineFinished);


}
