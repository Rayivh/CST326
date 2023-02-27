using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public string state;
    
    public float acceleration = 100f;
    public float maxSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpBoost = 5f;
    
    public bool isGrounded;
    
    public float longClickDuration = 1f;
    float _clickLength;
    bool _click;
    private Rigidbody _rbody;
    private Collider _col;

    // Update is called once per frame
    private void Start()
    {
        state = "Normal";
        _col = GetComponent<Collider>();
        _rbody = GetComponent<Rigidbody>();
        _rbody.isKinematic = false;
    }

    void Update()
    {
        switch(state)
        {
            case "Static":
                break;
            
            case "Normal": 
                Invoke(nameof(NormalMovementUpdate), 0f);
                break;
            
            case "Dead": 
                Invoke(nameof(DeathProcess), 0f);
                state = "Static";
                break;
            
            case "Cleared": 
                Invoke(nameof(LevelComplete), 0f);
                state = "Static";
                break;
        }
    }

    void NormalMovementUpdate()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        _rbody.velocity += Vector3.right * (horizontalAxis * Time.deltaTime * acceleration);

        float halfHeight = _col.bounds.extents.y;

        isGrounded = Physics.Raycast( new Vector3(transform.position.x, transform.position.y + halfHeight, transform.position.z), Vector3.down, halfHeight);

        _rbody.velocity = new Vector3(Mathf.Clamp(_rbody.velocity.x, -maxSpeed, maxSpeed), _rbody.velocity.y, _rbody.velocity.z);
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                _click = true;
                _clickLength = 0f;
                _rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        if (_click && Input.GetKey(KeyCode.Space))
        {
            _clickLength += Time.deltaTime;

            if (_clickLength < longClickDuration)
            {
                _rbody.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
            }
            else
            {
                _click = false;
            }
        }

        if (_click && Input.GetKeyUp(KeyCode.Space))
        {
            _click = false;
        }

        Color lineColor = isGrounded ? Color.green : Color.red;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * halfHeight, lineColor, 0f, false);
    }

    void DeathProcess()
    {
        transform.Rotate(0, 90, 0);
        _rbody.velocity = Vector3.zero;
        _rbody.AddForce(new Vector3(0f, 1.5f, -1f) * 5f, ForceMode.Impulse);
    }
    
    void LevelComplete()
    {
        _rbody.velocity = Vector3.zero;
    }
}
