using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [FormerlySerializedAs("bullet")] public GameObject bulletPrefab;
    [FormerlySerializedAs("shottingOffset")] public Transform shootOffsetTransform;

    private Animator playerAnimator;
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private Rigidbody2D body;

    //-----------------------------------------------------------------------------
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        float horizontalExtent = Camera.main.orthographicSize * Camera.main.aspect;
        if (Mathf.Abs(body.position.x) > horizontalExtent)
        {
            body.velocity = new Vector2(-body.velocity.x, body.velocity.y);
        }
        float axis = Input.GetAxis("Horizontal");
        body.AddForce(Vector2.right * axis, ForceMode2D.Force);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // todo - trigger a "shoot" on the animator
            playerAnimator.SetTrigger(Shoot);
            GameObject bullet = Instantiate(bulletPrefab, shootOffsetTransform.position, Quaternion.identity);
            bullet.tag = "friendly";
            Debug.Log("Bang!");
            Destroy(bullet, 6f);
        }
        
        
    }
}
