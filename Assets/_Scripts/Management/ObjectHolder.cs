using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{

    // This script is for holding and referencing objects
    #region Singleton
    public static ObjectHolder Instance;

    private void Awake() {
        Instance = this;
    }
    #endregion

    public Transform _playerTransform;


}
