using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Text))]
public class CurrentlevelView : MonoBehaviour
{
    [SerializeField] private Text _text;
    private string _template;
    
    [Inject]
    private LevelSwitcher _levelSwitcher;

    private void OnEnable()
    {
        ChangeLevel(_levelSwitcher.CurrentLevelIndex);
    }

    private void Awake() => _levelSwitcher.OnCurrentLevelChange += ChangeLevel;
    private void OnDestroy() => _levelSwitcher.OnCurrentLevelChange -= ChangeLevel;

    private void OnValidate()
    {
        _text ??= GetComponent<Text>();
    }

    private void ChangeLevel(int levelIndex)
    {
        _text.text = (levelIndex+1).ToString();
    }
}
