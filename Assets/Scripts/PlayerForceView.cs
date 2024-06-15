using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshPro))]
public class PlayerForceView : MonoBehaviour
{
    private TextMeshPro _playerForceText;

    [Inject] private PlayerForce _playerForce;

    private void Awake()
    {
        _playerForceText = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        _playerForce.OnPlayerForceUpdate += UpdatePlayerForce;
    }

    private void OnDisable()
    {
        _playerForce.OnPlayerForceUpdate -= UpdatePlayerForce;
    }

    private void UpdatePlayerForce(int playerForce)
    {
        _playerForceText.text = playerForce.ToString();
    }
}