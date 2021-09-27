using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 0.03f;
    [SerializeField] private float forwardSpeed = 0.06f;
    [SerializeField] private float laneDistanceX = 3;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ = 5;
    [SerializeField] private float jumpHeightY = 2;
    [SerializeField] private float jumpLerpSpeed = 8;

    [Header("Roll")]
    [SerializeField] private float rollDistanceZ = 5;

    [Header("Colliders")]
    [SerializeField] Collider regularCollider;
    [SerializeField] Collider rollCollider;

    Vector3 initialPosition;
    float targetPositionX;

    public bool IsJumping { get; private set; }

    public bool IsRolling { get; private set; }

    public float JumpDuration => jumpDistanceZ / forwardSpeed;
    public float RollDuration => rollDistanceZ / forwardSpeed;

    private bool CanJump => !IsJumping;
    private bool CanRoll => !IsRolling;

    float jumpStartZ;

    float rollStartZ;

    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;

    void Awake()
    {
        initialPosition = transform.position;
        StopRoll();
    }

    void Update()
    {
        ProcessInput();

        Vector3 position = transform.position;
        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessForwardMovement();
        ProcessRoll();

        transform.position = position;
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPositionX -= laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetPositionX += laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.W) && CanJump)
        {
            StartJump();
        }
        if (Input.GetKeyDown(KeyCode.S) && CanRoll)
        {
            StartRoll();
        }

        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }

    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeed);
    }

    private float ProcessForwardMovement()
    {
        return transform.position.z + forwardSpeed * Time.deltaTime;
    }

    private float ProcessJump()
    {
        float deltaY = 0;
        if (IsJumping)
        {
            //posicao Z atual menos a posicao Z que estava quando foi precionado botao de pulo
            float jumpCurrentProgress = transform.position.z - jumpStartZ;

            //posicao Z atual menos a posicao Z que estava quando foi precionado botao de pulo dividida pelo distancia do pulo setada
            float jumpPercent = jumpCurrentProgress / jumpDistanceZ;

            // se jumpPercent for 1 significa que a distancia setada em jumpDistanceZ foi totalmente percorrida
            if (jumpPercent <= 1)
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeightY;
            }
            else
            {
                StopJump();
            }
        }
        float targetPositionY = initialPosition.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * jumpLerpSpeed);
    }

    private void StartJump()
    {
        IsJumping = true;
        jumpStartZ = transform.position.z;
        StopRoll();
    }

    private void StopJump()
    {
        IsJumping = false;
    }

    private void ProcessRoll()
    {
        if (IsRolling)
        {
            //posicao Z atual menos a posicao Z que estava quando foi precionado botao de pulo
            float rollCurrentProgress = transform.position.z - rollStartZ;

            //posicao Z atual menos a posicao Z que estava quando foi precionado botao de pulo dividida pelo distancia do pulo setada
            float rollPercent = rollCurrentProgress / rollDistanceZ;

            // se jumpPercent for 1 significa que a distancia setada em jumpDistanceZ foi totalmente percorrida
            if (rollPercent >= 1)
            {
                StopRoll();
            }
        }
    }

    private void StartRoll()
    {
        rollStartZ = transform.position.z;
        IsRolling = true;
        regularCollider.enabled = false;
        rollCollider.enabled = true;
        StopJump();
    }

    private void StopRoll()
    {
        IsRolling = false;
        regularCollider.enabled = true;
        rollCollider.enabled = false;
    }

    public void Die()
    {
        forwardSpeed = 0;
        StopJump();
        StopRoll();
    }
}
