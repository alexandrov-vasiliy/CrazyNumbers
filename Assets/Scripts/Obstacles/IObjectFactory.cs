using UnityEngine;

public interface IObjectFactory
{
    GameObject CreateObject(ObstacleType type);
}