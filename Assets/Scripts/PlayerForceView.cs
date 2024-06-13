using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class PlayerForceView : MonoBehaviour
{
    private TextMeshPro _playerForceText;

    private void Awake()
    {
        _playerForceText = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        ScoreManager.Instance.OnPlayerForceUpdate += UpdatePlayerForce;
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnPlayerForceUpdate -= UpdatePlayerForce;
    }

    private void UpdatePlayerForce(int playerForce)
    {
        _playerForceText.text = playerForce.ToString();
    }
}