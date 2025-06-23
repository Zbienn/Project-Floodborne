using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private ParticleSystem leftTrailParticles;
    [SerializeField] private ParticleSystem rightTrailParticles;

    [SerializeField] private AudioMixer audioMixer;
    
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Sprite[] spriteSmalls;

    [SerializeField] private GameObject pause;

    private Vector2 movement;
    private Rigidbody2D body;
    private int boatSprite;
    private float audioVolume;
    private StatsForJSON[] array;

    private bool isPaused = false;
    public bool IsPaused { get => isPaused; set => isPaused = value; }

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        boatSprite = PlayerPrefs.GetInt("BoatSprite", 1);
        SpriteRenderer boat = FindFirstObjectByType<SpriteRenderer>();

        boat.sprite = sprites[boatSprite];

        foreach (SpriteRenderer gameObj in Resources.FindObjectsOfTypeAll(typeof(SpriteRenderer)).Cast<SpriteRenderer>())
            if (gameObj.name.Contains("Boat"))
                gameObj.sprite = spriteSmalls[boatSprite];

        foreach (OrbitBoat gameObj in Resources.FindObjectsOfTypeAll(typeof(OrbitBoat)).Cast<OrbitBoat>())
            if (gameObj.name.Contains("Boat"))
                gameObj.Icon = spriteSmalls[boatSprite];

        audioVolume = PlayerPrefs.GetFloat("AudioVolume", 0f);
        audioMixer.SetFloat("Volume", audioVolume);

        if (PlayerPrefs.HasKey("StatsArray"))
        {
            array = JsonHelper.FromJson<StatsForJSON>(PlayerPrefs.GetString("StatsArray", ""));
            moveSpeed += array[6].level * 0.5f;
        }
    }

    void Update()
    {
        if (pause != null)
            if ((!isPaused && Time.timeScale > 0f) || (isPaused && Time.timeScale <= 0f))
                CheckIfPause();

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
        Vector3 delta = moveSpeed * Time.deltaTime * movement.normalized;
        transform.position += delta;
    }

    private void CheckIfPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                isPaused = true;
                pause.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                isPaused = false;
                pause.SetActive(false);
            }
        }
    }
}
