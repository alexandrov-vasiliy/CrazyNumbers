using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Image))]
public class Painter : MonoBehaviour
{
    [Inject] private LevelSwitcher _levelSwitcher;

    [Header("Painters")] [SerializeField] private Sprite[] _painters;
    [SerializeField] private Image _image;
    [SerializeField] private Camera mainCamera;
    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] private int _currentPainter;

    [Header("Randomizer")] [SerializeField]
    private Vector2 _minMaxRotation;

    [FormerlySerializedAs("points")] [SerializeField] private Transform[] _points;

    private int _lastPainterIndex = -1; 

    private void OnEnable()
    {
        _levelSwitcher.OnCurrentLevelChange += RandomizeImage;
        mainCamera ??= Camera.main;
    }

    private void OnDisable()
    {
        _levelSwitcher.OnCurrentLevelChange -= RandomizeImage;
    }

    private void OnValidate()
    {
        _image ??= GetComponent<Image>();

        if (_currentPainter < 0)
        {
            _currentPainter = 0;
        }
        else if (_currentPainter >= _painters.Length)
        {
            _currentPainter = _painters.Length - 1;
        }

        SetImage();
    }

    private void SetImage()
    {
        if (_painters.Length > 0)
            _image.sprite = _painters[_currentPainter];
        else
            throw new ArgumentException("Need Fill Painters");
    }

    [Button]
    private void TestRandomize()
    {
        RandomizeImage(0);
    }

    private void RandomizeImage(int level)
    {
        int newPainterIndex;
        do
        {
            newPainterIndex = Random.Range(0, _painters.Length);
        } while (newPainterIndex == _lastPainterIndex);
    
        _lastPainterIndex = newPainterIndex;  // Обновляем последний выбранный индекс
        _currentPainter = newPainterIndex;
    
        int randomPoint = Random.Range(0, _points.Length);
        _image.rectTransform.position = _points[randomPoint].position;
        float randomRotation = Random.Range(_minMaxRotation.x, _minMaxRotation.y);
        _image.rectTransform.rotation = Quaternion.Euler(0, 0, randomRotation);
        SetImage();
    }
}