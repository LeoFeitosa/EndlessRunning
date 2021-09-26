using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 0.03f;
    [SerializeField] private float forwardSpeed = 0.06f;
    [SerializeField] private float laneDistanceX = 3;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ = 5;
    [SerializeField] private float jumpHeightY = 2;

    Vector3 initialPosition;
    float targetPositionX;

    public bool IsJumping { get; private set; }

    public float JumpDuration => jumpDistanceZ / forwardSpeed;

    float jumpStartZ;

    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;

    void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        ProcessInput();

        Vector3 position = transform.position;
        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessForwardMovement();

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
        if (Input.GetKeyDown(KeyCode.W) && !IsJumping)
        {
            IsJumping = true;
            jumpStartZ = transform.position.z;
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
                IsJumping = false;
            }
        }
        return initialPosition.y + deltaY;
    }

    public void Die()
    {
        enabled = false;
    }
}
