using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private bool _isMoving = false;
    private Rigidbody2D _rb;
    private Coroutine cr;
    private Animator anim;
    private SpriteRenderer sr;

    private Vector2 _lastDir;

    private bool isFacingRight = true;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void LateUpdate()
    {
        if (_isMoving)
            return;

       var input = new Vector2( (int) Input.GetAxisRaw("Horizontal"), (int) Input.GetAxisRaw("Vertical"));
        if ((Mathf.Abs(input.x) != Mathf.Abs(input.y)) && input != _lastDir) 
        {
            _lastDir = input;
            transform.rotation = Quaternion.identity;
            Flip();
            if (cr == null)
                cr = StartCoroutine(MovePlayer(input));
        }

    }
    private void Flip()
    {
        var scale = transform.localScale;
        if (_lastDir.x < 0)
        {
            scale.x = -1;
            scale.y = 1;
            transform.localScale = scale;
            isFacingRight = false;
            anim.SetTrigger("Horizontal");
        } 
        else 
        if (_lastDir.x > 0)
        {
            scale.x = 1;
            scale.y = 1;
            transform.localScale = scale;
            isFacingRight = true;
            anim.SetTrigger("Horizontal");
        }
        else 
        if (_lastDir.y > 0)
        {
            scale.y = 1;
            scale.x = 1;
            transform.localScale = scale;
            anim.SetTrigger("Vertical");
        } 
        else 
        if (_lastDir.y < 0)
        {
            scale.y = -1;
            scale.x = 1;
            transform.localScale = scale;
            anim.SetTrigger("Vertical");
        }
  
    }

    private void Rotation(Vector2 dir)
    {
        var scale = transform.localScale;
        if (dir.x > 0)
        {
            scale.x = -1;
            transform.localScale = scale;
        } 
        else 
        if (dir.x < 0)
        {
            scale.x = 1;
            transform.localScale = scale;
        }
        else
        if (dir.y < 0)
        {
            scale.x = 1;
            scale.y = 1;
            transform.localScale = scale;
            transform.rotation = Quaternion.Euler(0,0,90);
        }
        else
        if (dir.y > 0)
        {
            scale.y = 1;
            scale.x = 1;
            transform.localScale = scale;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    IEnumerator MovePlayer(Vector2 dir)
    {
        _rb.AddForce(dir * _speed, ForceMode2D.Impulse);

        if (_rb.velocity != Vector2.zero)
            _isMoving = true; 
        else 
            _isMoving = false;
        int aux = 0;
        while (_isMoving)
        {
            yield return new WaitForSeconds(.1f);
            aux++;
            if (aux > 15)
                _isMoving = false;
        }

        Rotation(dir);
        anim.SetTrigger("Idle");

        cr = null;
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
