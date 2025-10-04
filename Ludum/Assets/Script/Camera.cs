using UnityEngine;

public class Camera : MonoBehaviour
{
   public Transform player;
public Vector3 offset = new Vector3(0, 0, -10);

void Update()
{
    transform.position = player.position + offset;
}
}
