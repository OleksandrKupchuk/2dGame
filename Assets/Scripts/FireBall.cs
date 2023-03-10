using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private float _speed;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move() {
        _rigidbody2D.velocity = new Vector2(transform.localScale.x * _speed, _rigidbody2D.velocity.y);
    }
}
