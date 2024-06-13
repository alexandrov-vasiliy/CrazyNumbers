using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerForceView : MonoBehaviour
{
    private Text _playerForceText;

    private void Awake()
    {
        _playerForceText = GetComponent<Text>();
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