using UnityEngine;

public class PoolObjectFactory : IObjectFactory
{
    public GameObject CreateObject(ObstacleType type)
    {
        // В этом методе логика получения объекта из пула в зависимости от типа
        // Например, мы можем иметь разные пулы для разных типов объектов
        // или один большой универсальный пул, где используется информация о типе для поиска нужного объекта
        switch (type)
        {
            case ObstacleType.Obstacle:
                return ObstaclePool.Get.GetRandomObject(); 
            case ObstacleType.Multiply:
                return MultiplyPool.Get.GetRandomObject();
            case ObstacleType.Divider:
                return DividerPool.Get.GetRandomObject();
            case ObstacleType.Boss:
                return BossPool.Get.GetRandomObject();
            default:
                return null;
        }
    }
}