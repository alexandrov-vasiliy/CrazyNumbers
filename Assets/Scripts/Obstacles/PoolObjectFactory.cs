using UnityEngine;

public class PoolObjectFactory : IObjectFactory
{
    public GameObject CreateObject(InteractableType type)
    {
        // В этом методе логика получения объекта из пула в зависимости от типа
        // Например, мы можем иметь разные пулы для разных типов объектов
        // или один большой универсальный пул, где используется информация о типе для поиска нужного объекта
        switch (type)
        {
            case InteractableType.Obstacle:
                return ObstaclePool.Get.GetRandomObject(); 
            case InteractableType.Multiply:
                return MultiplyPool.Get.GetRandomObject();
            case InteractableType.Divider:
                return DividerPool.Get.GetRandomObject();
            case InteractableType.Boss:
                return BossPool.Get.GetRandomObject();
            default:
                return null;
        }
    }
}