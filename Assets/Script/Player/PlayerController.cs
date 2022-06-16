using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class PlayerController : MonoBehaviourPunCallbacks , IDamageable
{
	const float health = 100f;
	float currentHealth = health;

	public Image healthBar;
	

	[SerializeField] GameObject cameraHolder;

	[SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

	public PlayerManager playerManager;
	



	float verticalLookRotation;
	public bool grounded;
	Vector3 smoothMoveVelocity;
	Vector3 moveAmount;

	Rigidbody rb;

	PhotonView PV;

	public GameObject gun;
	public GameObject canvas;

	bool active = false;
	

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		PV = GetComponent<PhotonView>();

		playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
	}

	void Start()
	{		
		if (!PV.IsMine)
		{
			Destroy(GetComponentInChildren<Camera>().gameObject);
			Destroy(rb);
			Destroy(canvas);
		}

		healthBar.fillAmount = currentHealth / health;

		
	}

	void Update()
	{
		if (!PV.IsMine)
			return;

		Look();
		Move();
		Jump();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			gun.SetActive(!active);
			active = !active;

            Hashtable hash = new Hashtable();
            hash.Add("Gun", active);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F))
        {
			print("Fire!!!!");
			gun.GetComponent<Gun>().Fire();
        }

		if (transform.position.y < -10f)
			Destroy(this.gameObject);
	}

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            gun.SetActive((bool)changedProps["Gun"]);
        }
    }
    void Look()
	{
		transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

		verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

		cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
	}

	void Move()
	{
		Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

		moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
	}

	void Jump()
	{
		if (Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			print("Jump");
			rb.AddForce(transform.up * jumpForce);
		}
	}

	public void SetGroundedState(bool _grounded)
	{
		grounded = _grounded;
	}

	void FixedUpdate()
	{
		if (!PV.IsMine)
			return;

		rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
	}

    public void Damage(float damage, int actorNumber)
    {
		PV.RPC(nameof(RPC_TakeDamage), RpcTarget.All, damage, actorNumber);
    }

    [PunRPC]
	void RPC_TakeDamage(float damage, int actorNumber)
    {
		if (!PV.IsMine)
			return;

		currentHealth -= damage;
		healthBar.fillAmount = currentHealth / health;
		if (currentHealth <= 0)
        {
			Die(actorNumber);
        }
    }

	void Die(int actorNumber)
    {
		playerManager.Die(actorNumber);
    }
}