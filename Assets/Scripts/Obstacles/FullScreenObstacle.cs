using UnityEngine;
using UnityEngine.Serialization;

public class FullScreenObstacle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _boxCollider;

   public  void OnEnable()
    {
        AdjustSizeToFitScreen();
    }

    private void AdjustSizeToFitScreen()
    {
        Camera camera = Camera.main;
        if(camera == null)
        {
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
        _boxCollider.size = new Vector2(screenWidthInWorldUnits / transform.localScale.x, screenWidthInWorldUnits / transform.localScale.x);
    }

}