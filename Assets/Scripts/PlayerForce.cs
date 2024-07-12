using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerForce : MonoBehaviour
{

    public Action<int> OnPlayerForceUpdate;
    public int Value => _value;

    private bool counting;

    private int _value = 1;


    private void Start()
    {
        ResetCurrentForce();
    }


    public void ResetCurrentForce()
    {
        ResetPlayerForce();
    }

    public void IncrementPlayerForce(float number)
    {
        _value += (int)number;
        OnPlayerForceUpdate?.Invoke(_value);
    }

    public void MultiplyPlayerForce(float number)
    {
        _value = (int)(_value * number);
        OnPlayerForceUpdate?.Invoke(_value);
    }

    public void DividePlayerForce(float number)
    {
        _value = (int)(_value / number);
        OnPlayerForceUpdate?.Invoke(_value);
    }

    private void ResetPlayerForce()
    {
        _value = 1;
        OnPlayerForceUpdate?.Invoke(_value);
    }

    public void UpdateScoreGameover()
    {
        ResetPlayerForce();
    }
}