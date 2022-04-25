using System.Collections;
using UnityEngine;

public class BrickTrigger : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    private Vector3 scale;

    private void Start()
    {
        // . . for remaining same scale after changing parents
        scale = transform.localScale;
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.transform.CompareTag("Obstacle")) 
        {
            GameEvents.Instance.ObstacleBrickCollision(gameObject);
            EnableRagdoll();                                        // maybe this can be added to event system as action<int id>
            StartCoroutine(ReturnPoolAfterDelay());
        }
    }

    private void EnableRagdoll() 
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.useGravity = true;
        gameObject.layer = LayerMask.NameToLayer("BrickFlying");
    }

    private void DisableRagdoll()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        gameObject.layer = LayerMask.NameToLayer("Bricks");
    }

    private IEnumerator ReturnPoolAfterDelay() 
    {
        yield return new WaitForSecondsRealtime(2f);
        DisableRagdoll();
        ObjectPooler.Instance.ReturnToPool("Brick", this.gameObject);
        transform.localScale = scale;
    }
}
