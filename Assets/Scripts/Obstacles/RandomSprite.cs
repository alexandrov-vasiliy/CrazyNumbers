using System.Collections.Generic;
using Obstacles;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSprite : MonoBehaviour
{
    [SerializeField] private SpritesCollection _spritesCollection;
    [SerializeField]  private SpriteRenderer _spriteRenderer;
    private void OnValidate()
    {
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if(_spritesCollection.Sprites.Count == 0) throw new System.IndexOutOfRangeException("Невозможно выбрать элемент из пустого списка");
        
        int randomIndex = Random.Range(0, _spritesCollection.Sprites.Count);
        _spriteRenderer.sprite = _spritesCollection.Sprites[randomIndex];
    }
    
}