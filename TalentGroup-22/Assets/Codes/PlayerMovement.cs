using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement objInstance = null;
    readonly string objName = "Player";
    Rigidbody2D playerRigidbody = null;
    Animator playerAnimator = null;
    public float speed = 0f;
    public float runSpeed = 0f;
    public float normalSpeed = 0f;
    bool isRunning = false;
    Vector2 movement = Vector2.zero;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start()
    {
        playerRigidbody = GameObject.Find(objName).GetComponent<Rigidbody2D>();
        playerAnimator = GameObject.Find(objName).GetComponent<Animator>();
    }
    void Update()
    {
        #region Walk
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        #endregion
        #region Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            speed = runSpeed;
        }
        else
        {
            isRunning = false;
            speed = normalSpeed;
        }
        #endregion
        #region Set Anim
        playerAnimator.SetFloat("Horizontal", movement.x);
        playerAnimator.SetFloat("Vertical", movement.y);
        playerAnimator.SetFloat("Speed", movement.sqrMagnitude);
        #endregion
    }
    void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + movement * speed * Time.fixedDeltaTime);
    }
}