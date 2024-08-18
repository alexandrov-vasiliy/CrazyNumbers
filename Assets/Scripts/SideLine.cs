using UnityEngine;
using UnityEngine.Serialization;

public class SideLine : MonoBehaviour
{
    [SerializeField] private BorderLine borderLine;
    [SerializeField] private float offset;

    [FormerlySerializedAs("camera")] [SerializeField] private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        SetLinePosition();
    }
    

    private void SetLinePosition()
    {
        Vector3 vector = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        Vector3 sidePosition = Vector3.zero;
        if (borderLine == BorderLine.LEFT)
        {
            sidePosition = new Vector3(0f - vector.x - offset, 0f, 0f);
        }
        else if (borderLine == BorderLine.RIGHT)
        {
            sidePosition  = new Vector3(vector.x + offset, 0f, 0f);
        }
        else if (borderLine == BorderLine.BOTTOM)
        {
            sidePosition = new Vector3(0f, offset - vector.y, 0f);
        }
        
        transform.position = sidePosition;
    }
}