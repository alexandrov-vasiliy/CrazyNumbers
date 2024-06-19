using UnityEngine;

public class FullScreenObstacle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CircleCollider2D _circleCollider;

   public  void OnEnable()
    {
        AdjustSizeToFitScreen();
    }

    private void AdjustSizeToFitScreen()
    {
        Camera camera = Camera.main;
        if(camera == null)
        {
            Debug.LogError("Main camera not found.");
            return;
        }

        // Расчет ширины экрана в мировых единицах
        float screenHeightInWorldUnits = camera.orthographicSize * 2;
        float screenWidthInWorldUnits = screenHeightInWorldUnits * Screen.width / Screen.height;

        // Установка масштаба спрайта, чтобы круглый спрайт был вписан по ширине
        float spriteWidth = _spriteRenderer.sprite.bounds.size.x;
        Vector3 scale = new Vector3(screenWidthInWorldUnits / spriteWidth, screenWidthInWorldUnits / spriteWidth, 1f);
        _spriteRenderer.transform.localScale = scale;
   
        // Корректировка радиуса коллайдера в соответствии с масштабированным спрайтом
        // Считаем диаметр спрайта в мировых координатах и делим его пополам, чтобы получить радиус
        _circleCollider.radius = (screenWidthInWorldUnits / 2) / transform.localScale.x;
    }

}