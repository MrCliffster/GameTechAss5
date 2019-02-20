using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class PlayerController : MonoBehaviour {

	public Transform rightGunBone;
	public Transform leftGunBone;
	public Arsenal[] arsenal;
    public int moveSpeed;
    public int rotationSpeed;
    public Canvas UI;

	private Animator animator;
    private CharacterController cc;
    private CameraManager cm;
    private Actions actions;
    public bool inGame = false;
    private int numCollected = 0;

    void Awake() {
		animator = GetComponent<Animator> ();
        actions = GetComponent<Actions>();
        cc = GetComponent<CharacterController>();
        cm = FindObjectOfType<CameraManager>();
		if (arsenal.Length > 0)
			SetArsenal (arsenal[0].name);
		}

	public void SetArsenal(string name) {
		foreach (Arsenal hand in arsenal) {
			if (hand.name == name) {
				if (rightGunBone.childCount > 0)
					Destroy(rightGunBone.GetChild(0).gameObject);
				if (leftGunBone.childCount > 0)
					Destroy(leftGunBone.GetChild(0).gameObject);
				if (hand.rightGun != null) {
					GameObject newRightGun = (GameObject) Instantiate(hand.rightGun);
					newRightGun.transform.parent = rightGunBone;
					newRightGun.transform.localPosition = Vector3.zero;
					newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
					}
				if (hand.leftGun != null) {
					GameObject newLeftGun = (GameObject) Instantiate(hand.leftGun);
					newLeftGun.transform.parent = leftGunBone;
					newLeftGun.transform.localPosition = Vector3.zero;
					newLeftGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
				}
				animator.runtimeAnimatorController = hand.controller;
				return;
				}
		}
	}

	[System.Serializable]
	public struct Arsenal {
		public string name;
		public GameObject rightGun;
		public GameObject leftGun;
		public RuntimeAnimatorController controller;
	}

    public void Update()
    {
        if (inGame)
        {
            if (numCollected == 2)
            {
                UI.GetComponent<UIController>().EndGame();
            }
            float fwd = Input.GetAxis("Vertical");
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0));
            if (System.Math.Abs(fwd) > 0)
            {
                cc.Move(transform.TransformDirection(Vector3.forward * Time.deltaTime * moveSpeed * fwd));
                actions.Run();
            }
            else
            {
                actions.Stay();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered a collider!");
        if (other.CompareTag("Pickup"))
        {
            Debug.Log("picked-up");
            other.gameObject.SetActive(false);
            ParticleSystem ps = FindObjectOfType<ParticleSystem>();
            ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
            emitOverride.startLifetime = 10f;
            ps.Emit(emitOverride, 20);
            cm.hasPickup = true;
            this.numCollected++;
        }
        if (other.name.Contains("Room1"))
        {
            Debug.Log("Entered room 1");
            cm.EnterRoom1();
        }
        else if (other.name.Contains("Room2"))
        {
            Debug.Log("Entered room 2");
            cm.EnterRoom2();
        }
        else if (other.name.Contains("Room3"))
        {
            Debug.Log("Entered room 3");
            cm.EnterRoom3();
        }
    }
}
