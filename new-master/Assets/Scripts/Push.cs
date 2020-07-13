using UnityEngine;

public class Push : MonoBehaviour
{
	[SerializeField]
	float speed, ropeSpeed, jumpSpeed, maxJumpSpeed;
	Vector3 hitOffset, ropeOldPos, ropeNewPos;
	public bool isSwinging;
	public Transform player, rope, ropeTip;
	[SerializeField]
	CharacterController CharacterController;
	[SerializeField]
	Movement move;
	[SerializeField]
	GameObject letGo;
	public KeyCode interact = KeyCode.E;

    void OnControllerColliderHit(ControllerColliderHit namelessParam)
    {
		if (namelessParam.collider.CompareTag("Pushable"))
		{
	    	Rigidbody rb = namelessParam.gameObject.GetComponent<Rigidbody>();
			Vector3 dir = rb.transform.position - transform.position;
			Vector3 pushDir = new Vector3(dir.x, 0f, dir.x);
    		rb.AddForce(pushDir * speed);
    	}
    	if (Input.GetKey(interact))
	    	if (namelessParam.collider.CompareTag("Rope"))
	    	{
	    		if (!isSwinging)
	    		{
	    			Rigidbody rb = namelessParam.transform.parent.gameObject.GetComponent<Rigidbody>();
		    		Vector3 dir = rb.transform.position - transform.position;
		    		float fl = System.Math.Max(dir.x, dir.z);
	    			Vector3 pushDir = new Vector3(dir.x / System.Math.Abs(fl), 0f, dir.z / System.Math.Abs(fl));
	    			rb.AddForce(pushDir * ropeSpeed);
		    		rope = rb.transform;
		    		hitOffset = namelessParam.point - rope.position;
		    		rope.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
		    		isSwinging = true;
		    	}
	    	}
    }

    void Update()
    {
    	if (jumpSpeed > 0f)
    	{
	    	jumpSpeed -= Time.deltaTime * 2.5f;
	    }

	    if (jumpSpeed < 0f)
	    {
	    	jumpSpeed = 0f;
	    }

    	if (!isSwinging)
    	{
	    	CharacterController.Move(ropeNewPos * jumpSpeed * Time.deltaTime);
    	}
    	else if (Input.GetKeyDown(KeyCode.Space))
    	{
    		if (Input.GetKey(interact))
    			letGo.SetActive(true);
    		else
    		{
    			Detach();
    			letGo.SetActive(false);
    		}
    	}
	    if (move.isGrounded)
	    	jumpSpeed = 0f;
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
    	if (isSwinging)
    	{
    		Swing();
    	}
    	if (isSwinging)
	    	move.velocity = new Vector3 (0f, -2f, 0f);
    }

    void Detach()
    {
    	jumpSpeed = maxJumpSpeed;
		rope.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
    	isSwinging = false;
    }

    void Swing() => player.transform.position = rope.GetChild(0).GetChild(0).position;
}