using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] DoubleTriggerCheck CheckFromParent;
    private BrickFormation _brickFormation;

    public int _brickReward;


    private void Start()
    {
        _brickFormation = GameEvents.Instance._player.GetComponentInChildren<BrickFormation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckFromParent.isTriggered)
        {
            StartCoroutine(_brickFormation.AddBrick(_brickReward));
            CheckFromParent.SetTriggered();
            _brickFormation.BrickReposition();
        }
    }
}
