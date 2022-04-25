using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float _camFollowSpeed;

    [SerializeField]
    private Vector3 _offset;

    private Transform _playerTransform;


    private void Start() 
    {
        _playerTransform = GameEvents.Instance._player.GetComponent<Transform>();
    }

    private void Update() 
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {

        Vector3 desiredPosition = _playerTransform.position + _offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, _camFollowSpeed);

        transform.position = smoothPosition;
    }
}
