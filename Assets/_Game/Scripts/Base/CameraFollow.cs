using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Character player;

    [SerializeField] private Transform target;
    [SerializeField] public Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private Vector3 _offset;

    void LateUpdate()
    {
        if (player == null) return;
        target = player.transform;
        if (target != null)
        {
            AdjustOffsetBasedOnScale();
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }
    }

    private void AdjustOffsetBasedOnScale()
    {
        if (player != null)
        {
            _offset = new Vector3(offset.x * player.transform.localScale.x, offset.y * player.transform.localScale.y, offset.z * player.transform.localScale.z);
        }
    }

    public void SetPlayer(Character newPlayer)
    {
        player = newPlayer;
        if (player != null)
        {
            target = player.transform;
            offset = transform.position - target.position;
            _offset = offset;
        }
    }
}
