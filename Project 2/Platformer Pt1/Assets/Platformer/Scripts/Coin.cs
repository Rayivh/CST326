using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody _rb = GetComponent<Rigidbody>();
        _rb.AddForce(Vector3.up * 7.5f, ForceMode.Impulse);
        StartCoroutine(DelayedDelete());
    }
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 300, 0) * Time.deltaTime);
    }
    IEnumerator DelayedDelete()
    {
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }
}
