using UnityEngine;

public class GroundedHandler : MonoBehaviour {
    
    public bool Grounded = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.NameToLayer("Ground") != other.gameObject.layer)
            return;

        Grounded = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (LayerMask.NameToLayer("Ground") != other.gameObject.layer)
            return;

        Grounded = false;
    }
}
