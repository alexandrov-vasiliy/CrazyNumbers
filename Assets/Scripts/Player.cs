using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float followSpeed = 2f;

    public float offsetY = 0.8f;
    
    public SpriteRenderer Renderer;


    [SerializeField, Range(0f, 1f)] private float animationDuration = 0.2f;
    [FormerlySerializedAs("scaleMultiplyer")] [SerializeField] private float scaleMultiply = 1.2f;

    private Vector2 destination;

    private Color color;

    private bool follow;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.uIManager.gameState != GameState.PLAYING ||
            !collision.gameObject.CompareTag("Obstacle"))
        {
            return;
        }

        int colidedNumber = collision.gameObject.GetComponent<Obstacle>().NumberForce;

        if (colidedNumber <= ScoreManager.Instance.PlayerForce)
        {
            DOTween.Sequence()
                .Append(transform.DOScale(transform.localScale * scaleMultiply, animationDuration))
                .Append(transform.DOScale(new Vector3(1f, 1f, 1f), animationDuration));
            
            AudioManager.Instance.PlayEffects(AudioManager.Instance.sameColor);
            collision.gameObject.SetActive(false);
            ScoreManager.Instance.IncrementPlayerForce(colidedNumber);
        }
        else
        {
            AudioManager.Instance.PlayEffects(AudioManager.Instance.wrongColor);
            GameManager.Instance.GameOver();
        }
    }

    public void SetColor(Color _color)
    {
        color = _color;
        Renderer.color = color;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            follow = true;
        }

        if (GameManager.Instance.uIManager.gameState == GameState.PLAYING && !GameManager.Instance.uIManager.IsButton())
        {
            if (Input.GetMouseButton(0) && follow)
            {
                destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                destination = new Vector2(destination.x, destination.y + offsetY);
                transform.position = Vector2.Lerp(base.transform.position, destination, followSpeed * Time.deltaTime);
            }

            if (Input.GetMouseButtonUp(0))
            {
                follow = false;
            }
        }
    }
}