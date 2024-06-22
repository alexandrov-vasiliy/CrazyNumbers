using UnityEngine;

public class SideLine : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<BaseObstacle>())
		{
			other.gameObject.SetActive(false);
		}
	}
}
