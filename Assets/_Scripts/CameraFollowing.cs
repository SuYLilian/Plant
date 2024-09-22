using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    private void Start()
    {
        offset = new Vector3(0, transform.position.y - player.transform.position.y, transform.position.z-player.transform.position.z);
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(0,offset.y+player.transform.position.y, offset.z + player.transform.position.z);
    }
}
