using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class Movement : MonoBehaviour
{
    #region Variable
    private CharacterController controller;
    private new Transform transform;
    private Animator animator;
    private new Camera camera;

    private Plane plane;
    private Ray ray;
    private Vector3 hitPos;

    private PhotonView pv;
    private CinemachineVirtualCamera virtualcamera;

    public float moveSpeed = 2.5f;
    float enter = 0f;
    float h => Input.GetAxis("Horizontal");
    float v => Input.GetAxis("Vertical");
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        transform = this.GetComponent<Transform>();
        animator = this.GetComponent<Animator>();
        pv = this.GetComponent<PhotonView>();
        virtualcamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        camera = Camera.main;
        plane = new Plane(transform.up, this.transform.position);

        if(pv.IsMine)
        {
            virtualcamera.Follow = transform;
            virtualcamera.LookAt = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pv.IsMine)
        {
            Move();
            Turn();
        }
    }
    void Move()
    {
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 moveDir = (cameraForward * v) + (cameraRight * h);
        moveDir.Set(moveDir.x, 0f, moveDir.z);
        controller.SimpleMove(moveDir * moveSpeed);

        float forward = Vector3.Dot(moveDir, transform.forward);
        float strafe = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("Forward", forward);
        animator.SetFloat("Strafe", strafe);
    }
    void Turn()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);

        plane.Raycast(ray, out enter);
        hitPos = ray.GetPoint(enter);

        Vector3 lookDir = hitPos - this.transform.position;
        lookDir.y = 0f;
        transform.localRotation = Quaternion.LookRotation(lookDir);
    }
}
