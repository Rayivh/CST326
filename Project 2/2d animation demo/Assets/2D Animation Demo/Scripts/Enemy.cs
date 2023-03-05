using UnityEngine;

public class EnemyComplete : MonoBehaviour
{
    //-----------------------------------------------------------------------------
    void OnCollisionEnter2D(Collision2D collision)
    {
        // todo - trigger death animation
        Destroy(collision.gameObject); // destroy bullet
        Debug.Log("Ouch!");
    }
}
