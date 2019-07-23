using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileTileDestroyer : MonoBehaviour
{
    #region Properties
    private Rigidbody2D rigidbodyReference;
    private CircleCollider2D colliderReference;
    [SerializeField]
    private float velocity=0;
    [SerializeField]
    private LayerMask whatIsDestructible;
    private RaycastHit2D hit;
    private Vector3 originalVelocity;
    #endregion
    #region Implementation
    public void ShootTo(Vector2 pDirection)
    {
        rigidbodyReference.velocity = pDirection * velocity;
        originalVelocity = rigidbodyReference.velocity;
    }
    #endregion
    #region Unity callbacks
    public void Awake()
    {
        rigidbodyReference = GetComponent<Rigidbody2D>();
        colliderReference = GetComponent<CircleCollider2D>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}