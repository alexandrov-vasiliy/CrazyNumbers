using System;
using UnityEngine;
using Zenject;

public class Sides: MonoBehaviour
{
    [Inject] private PlayerEvents _playerEvents;
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Remove()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

    }
    
}