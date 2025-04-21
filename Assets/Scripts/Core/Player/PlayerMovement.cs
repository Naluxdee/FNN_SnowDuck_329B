using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    /*[Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float turningRate = 30f;


    private Vector2 previousMovementInput;
 
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) { return; }

        inputReader.MoveEvent += HandleMove;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) { return; }

        inputReader.MoveEvent -= HandleMove;
    }

    private void Update()
    {
        if (!IsOwner) { return; }

        float zRotation = previousMovementInput.x * -turningRate * Time.deltaTime;
        bodyTransform.Rotate(0f, 0f, zRotation);
    }

    private void FixedUpdate()
    {

        if (!IsOwner) { return; }

        rb.linearVelocity = (Vector2)bodyTransform.up * previousMovementInput.y * movementSpeed;
    }

    private void HandleMove(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
    }*/
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem dustCloud;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float particleEmissionValue = 10f;
    private ParticleSystem.EmissionModule emissionModule;

    private Vector2 movementInput;
    private Vector3 previousPos;

    private const float ParticleStopThreshold = 0.005f;

    private void Awake()
    {
        emissionModule = dustCloud.emission;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        inputReader.MoveEvent += HandleMove;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        inputReader.MoveEvent -= HandleMove;
    }

    private void Update()
    {
        if (!IsOwner) return;

        // ตรวจสอบว่าผู้เล่นมีการกดปุ่มเคลื่อนที่หรือไม่
        if (movementInput != Vector2.zero)
        {
            // คำนวณมุมการหมุนไปตามทิศทางที่กด WASD
            float targetAngle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // ทำให้การหมุนลื่นขึ้น
            bodyTransform.rotation = Quaternion.Slerp(bodyTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if ((transform.position - previousPos).sqrMagnitude > ParticleStopThreshold)
        {
            emissionModule.rateOverTime = particleEmissionValue;
        }
        else
        {
            emissionModule.rateOverTime = 0;
        }
        previousPos = transform.position;

        if (!IsOwner) return;

        // เคลื่อนที่ไปข้างหน้า ตามที่ตัวละครกำลังหันหน้า
        rb.velocity = bodyTransform.up * movementInput.magnitude * movementSpeed;
    }

    private void HandleMove(Vector2 input)
    {
        movementInput = input; // บันทึกค่าการเคลื่อนที่ของผู้เล่น
    }
}