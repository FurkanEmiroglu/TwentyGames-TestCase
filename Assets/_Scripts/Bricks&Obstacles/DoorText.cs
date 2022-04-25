using UnityEngine;
using TMPro;

public class DoorText : MonoBehaviour
{
    [SerializeField] TextMeshPro tmp;
    [SerializeField] DoorTrigger doorTrigger;


    private void Start()
    {
        UpdateText();
    }


    void UpdateText()
    {
        tmp.text = "+ " + doorTrigger._brickReward.ToString();
    }
}
