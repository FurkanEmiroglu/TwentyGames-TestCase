using UnityEngine;

public class BridgeCreator : MonoBehaviour
{
    [SerializeField] private BrickFormation _formation;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BridgeBrick"))
        {
            other.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(_formation.PopToBridge());
        }
    }
}
