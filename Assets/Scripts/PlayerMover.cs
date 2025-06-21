using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private ParticleSystem leftTrailParticles;
    [SerializeField] private ParticleSystem rightTrailParticles;

    private Vector2 movement;
    private Rigidbody2D body;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Rotação e partículas
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            spriteTransform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

            Vector3 particleRotation = new Vector3(0f, 0f, angle + 90f);
            leftTrailParticles.transform.rotation = Quaternion.Euler(particleRotation);
            rightTrailParticles.transform.rotation = Quaternion.Euler(particleRotation);

            if (!leftTrailParticles.isPlaying) leftTrailParticles.Play();
            if (!rightTrailParticles.isPlaying) rightTrailParticles.Play();
        }
        else
        {
            if (leftTrailParticles.isPlaying) leftTrailParticles.Stop();
            if (rightTrailParticles.isPlaying) rightTrailParticles.Stop();
        }

        if (body.linearVelocity != Vector2.zero) body.linearVelocity = Vector2.zero;

        // Movimento
        Move();
    }

    private void Move()
    {
        Vector3 delta = MoveSpeed * Time.deltaTime * movement.normalized;
        transform.position += delta;
    }
}
