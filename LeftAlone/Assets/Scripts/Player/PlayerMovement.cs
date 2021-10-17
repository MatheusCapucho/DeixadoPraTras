using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private bool _isMoving = false;
    private Rigidbody2D _rb;
    private Coroutine cr;

    private Vector2 _lastDir;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (_isMoving)
            return;

       var input = new Vector2( (int) Input.GetAxisRaw("Horizontal"), (int) Input.GetAxisRaw("Vertical"));
        if ((Mathf.Abs(input.x) != Mathf.Abs(input.y)) && input != _lastDir) 
        {
            _lastDir = input;
            cr = StartCoroutine(MovePlayer(input));
        }

    }

    IEnumerator MovePlayer(Vector2 dir)
    {
        _rb.AddForce(dir * _speed, ForceMode2D.Impulse);

        yield return new WaitForEndOfFrame();

        if (_rb.velocity != Vector2.zero)
            _isMoving = true; 
        else 
            _isMoving = false;

        while (_isMoving)
        {
            yield return new WaitForSeconds(.1f);
        }

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
            _isMoving = false;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _isMoving = true;
    }

}
