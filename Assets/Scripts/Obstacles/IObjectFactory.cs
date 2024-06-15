using UnityEngine;

public interface IObjectFactory
{
    GameObject CreateObject(InteractableType type);
}