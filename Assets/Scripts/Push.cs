using static System.Math;
using System.Collections;
using UnityEngine;

public class Push : MonoBehaviour
{
	public static Push instance;
	[SerializeField]
	float speed, ropeSpeed, maxJumpSpeed;
	[HideInInspector]
	public float jumpSpeed;
	Vector3 hitOffset, ropeOldPos;
	[HideInInspector]
	public Vector3 ropeNewPos;
	public bool isSwinging;
	public Transform player, rope, ropeTip;
	[SerializeField]
	CharacterController CharacterController;
	[SerializeField]
	Movement move;
	[SerializeField]
	GameObject letGo;
	public KeyCode interact = KeyCode.E;
	[SerializeField]
	float zipLineSpeed;
	[SerializeField]
	public bool isZipLining;
	Transform zipLineCarrier;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
		if (hit.collider.CompareTag("Pushable"))
		{
			Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
			Vector3 dir = rb.transform.position - transform.position;
			Vector3 pushDir = new Vector3(dir.x, 0f, dir.x);
    		rb.AddForce(pushDir * speed);
		}
		if (Input.GetKey(interact))
		{
			if (hit.collider.CompareTag("Rope"))
				if (!isSwinging)
				{
					Rigidbody rb = hit.transform.parent.gameObject.GetComponent<Rigidbody>();
					Vector3 dir = rb.transform.position - transform.position;
					float fl = Max(dir.x, dir.z);
					Vector3 pushDir = new Vector3(dir.x / Abs(fl), 0f, dir.z / Abs(fl));
	    			rb.AddForce(pushDir * ropeSpeed);
					rope = rb.transform;
					hitOffset = hit.point - rope.position;
					rope.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
					isSwinging = true;
				}
			if (hit.collider.CompareTag("Zipline"))
			{
				Zipline zipline = hit.transform.parent.gameObject.GetComponent<Zipline>();
				zipLineCarrier = hit.transform;
				zipline?.Move(zipLineSpeed, hit.point);
				isZipLining = true;
				instance = this;
			}
		}
    }

    void Update()
    {
    	if (jumpSpeed > 0f) jumpSpeed -= Time.deltaTime * 2.5f;

		if (jumpSpeed < 0f) jumpSpeed = 0f;

		if (isSwinging && Input.GetKeyDown(KeyCode.Space))
		{
			if (Input.GetKey(interact)) letGo.SetActive(true);
			else
			{
				Detach();
				letGo.SetActive(false);
			}
		}
		if (move.isGrounded) jumpSpeed = 0f;

		if (isZipLining && Input.GetKeyDown(KeyCode.Space)) isZipLining = false;

		if (isZipLining && isSwinging)
		{
			Detach();
			isZipLining = false;
		}
    }

    void FixedUpdate()
    {
		if (isSwinging)
		{
			ropeNewPos = rope.GetChild(0).position - ropeOldPos;
			ropeOldPos = rope.GetChild(0).position;
		}
    }

    void LateUpdate()
    {
		if (isSwinging) Swing();
		if (isZipLining) ZipLine();

		if (isSwinging || isZipLining) move.velocity = new Vector3(0f, -2f, 0f);
    }

    void Detach()
    {
		jumpSpeed = maxJumpSpeed;
		rope.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
		isSwinging = false;
    }

    void Swing() => player.transform.position = rope.GetChild(0).GetChild(0).position;

    void ZipLine() => player.transform.position = zipLineCarrier.position;

    void OnTriggerEnter()
	{
		if (jumpSpeed <= maxJumpSpeed -5f) jumpSpeed = 0f;
	}

	public IEnumerator DetachZipline()
	{
		while (true)
		{
			isZipLining = false;
			yield return new WaitForSeconds(1f);
			StopCoroutine(DetachZipline());
		}
	}
}