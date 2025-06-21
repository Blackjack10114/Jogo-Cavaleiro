using UnityEngine;

public class FundoSeguePlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = Vector3.zero;

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            transform.position = new Vector3(
                transform.position.x,
                playerTransform.position.y + offset.y,
                transform.position.z
            );
        }
    }
}
