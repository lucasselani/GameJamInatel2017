using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour {
	public Canvas messageCanvas;
	public Text text;
	public float yPos=-1f;
	public int talk=0;
	public GameObject background1, background2;
	private bool chained = false;
	private GameObject beggar;
	private GameObject mainChar;
	private bool firstCollision = true;
	private float chopCooldown = 1f;
	private float nextChop = 0;
	private string rightPalm = "";
	private bool ignored = true;
	private int beggarTalk = 0;
	private int bananaqtd = 1;
	 
	// Use this for initialization
	void Start()
	{
		TurnOffMessage ();
	}

	void SetMessage(string s){
		text.text = s;
	}

	private void TurnOnMessage(GameObject gameobj)
	{
		for(int i=0; i< messageCanvas.transform.childCount; i++) {
			GameObject child = messageCanvas.transform.GetChild(i).gameObject;
			if (child != null) {
				child.SetActive (true);
				child.transform.position = gameobj.transform.position + new Vector3 (0, yPos, 0);
			}
		}
		messageCanvas.enabled = true;
		messageCanvas.transform.position = gameobj.transform.position + new Vector3 (0, yPos, 0);
	}

	private void TurnOffMessage()
	{
		for(int i=0; i< messageCanvas.transform.childCount; i++) {
			GameObject child = messageCanvas.transform.GetChild(i).gameObject;
			if (child != null) 	child.SetActive (false);
		}
		messageCanvas.enabled = false;
	}

	/*private void TurnOnImage(GameObject gameobj)
	{
		for(int i=0; i< imageCanvas.transform.childCount; i++) {
			GameObject child = imageCanvas.transform.GetChild(i).gameObject;
			if (child != null) {
				child.SetActive (true);
				child.transform.position = gameobj.transform.position + new Vector3 (1f, yPos, 0);
			}
		}
		imageCanvas.enabled = true;
		imageCanvas.transform.position = gameobj.transform.position + new Vector3 (1f, yPos, 0);
	}

	private void TurnOffImage()
	{
		for(int i=0; i< imageCanvas.transform.childCount; i++) {
			GameObject child = imageCanvas.transform.GetChild(i).gameObject;
			if (child != null) 	child.SetActive (false);
		}
		imageCanvas.enabled = false;
	}*/


	private void ChainInteraction(){
		mainChar = this.transform.GetChild (0).gameObject;
		switch (GameManager.instance.beggarInteraction) {
		case 0:
			if (chained) {
				
				switch (talk) {
				case 0:
					TurnOffMessage ();
					TurnOnMessage (mainChar);
					SetMessage ("*cof cof*");
					talk++;
					Debug.Log (talk);
					break;

				case 1:
					TurnOffMessage ();
					TurnOnMessage (mainChar);
					switch (beggarTalk) {
					case 0:
						TurnOffMessage ();
						TurnOnMessage (mainChar);
						SetMessage ("Garota: Estou perdida... Você sabe que lugar é esse?");
						beggarTalk++;
						break;
					case 1:
						ignored = false;
						TurnOffMessage ();
						TurnOnMessage (mainChar);
						StartCoroutine (SetMessageTime (-1));
						beggarTalk++;
						talk++;
						break;
					}

					Debug.Log (talk);
					break;
				

				case 3:
					TurnOffMessage ();
					GameManager.instance.beggarInteraction = 1;
					GameManager.instance.steps = 2;
					talk = 0;
					break;
				} 
			}
			break;
		case 1:
			switch (talk) {
			case 0:
				//TurnOffMessage ();
				TurnOnMessage (mainChar);
				SetMessage ("Garota: Você está melhor?");
				talk++;
				break;
			
			case 1:
				//TurnOffMessage ();
				TurnOnMessage (mainChar);
				SetMessage ("Homem desconhecido: Sim, obrigado.");
				talk++;
				break;

			case 2:
				//TurnOffMessage ();
				TurnOnMessage (mainChar);
				SetMessage ("Garota: Bom, agora preciso resolver a minha situação.");
				talk++;
				break;

			case 3:
				//TurnOffMessage ();
				TurnOnMessage (mainChar);
				SetMessage ("Homem desconhecido: Você já sabe o que deve fazer.");
				talk++;
				break;

			case 4:
				//TurnOffMessage ();
				TurnOnMessage (mainChar);
				SetMessage ("Garota: ???");
				talk++;
				break;

			case 5:
				TurnOffMessage ();
				talk = 0;
				GameManager.instance.beggarSatisfied = true;
				//TODO Animação de acordar
				break;
			}
			break;
		}
	}

	private void BearInteraction(){
		mainChar = this.transform.GetChild (0).gameObject;
		this.transform.Find ("urso").gameObject.GetComponent<AudioSource> ().Play();
		if (ignored) {
			TurnOnMessage (mainChar);
			SetMessage ("BIRLLLLL");
			return;
		}

		switch (GameManager.instance.bearInteraction) {
			case 0:
				TurnOnMessage (mainChar);
				StartCoroutine (SetMessageTime (GameManager.instance.bearInteraction));
				rightPalm = "palmtree3";
				break;

			case 1:
				TurnOnMessage (mainChar);
				StartCoroutine (SetMessageTime (GameManager.instance.bearInteraction));
				rightPalm = "palmtree2";
				break;

			case 2:
				TurnOnMessage (mainChar);
				StartCoroutine (SetMessageTime (GameManager.instance.bearInteraction));
				rightPalm = "palmtree1";
				GameManager.instance.steps = 4;
				break;
			case 3:
				TurnOffMessage ();
				TurnOnMessage (mainChar);
				SetMessage ("Você completou o desafio... Vá e ajude aquele senhor.");
				GameManager.instance.bearSatisfied = true;
				break;
		}
	}

	IEnumerator SetMessageTime(int i){		
		switch (i) {
		case 0:
			string[] message = {"Eu posso preencher um quarto ou um coração.", "Outros podem me ter, mas nunca posso ser compartilhado."
				,"O que eu sou? Amor, conhecimento ou solidão?"};
			for(int j=0; j<message.Length; j++){
				SetMessage (message[j]);
				yield return new WaitForSeconds (5f);
			}
			break;
		

		case 1:
			string[] message2 = {"Esta é a coisa que tudo devora. Feras, aves, plantas, flora.","Aço e ferro são sua comida, e a dura pedra por ele moída;",
							"Aos reis abate, a cidade arruína, e a alta montanha faz pequenina.", "O que sou? Espadas, tempo ou dragões?"};
			for(int j=0; j<message2.Length; j++){
				SetMessage (message2[j]);
				yield return new WaitForSeconds (5f);
			}
			break;

		case 2:
			string[] message3 = {"O que pode ser quebrado sem ao menos ser segurado?","Promessa, água ou vento?"};
						for(int j=0; j<message3.Length; j++){
				SetMessage (message3[j]);
				yield return new WaitForSeconds (5f);
			}
			break;
		case -1:
			string[] message4 = {"Garota: O senhor não parece estar se sentindo muito bem...",
				"Há algo em que eu possa te ajudar?",
				"Homem desconhecido: Estou faminto, me ajude por favor",
				"Garota: Vou ver o que posso fazer por você",
				"Homem desconhecido: Muito obrigado!"
			};
			for (int j = 0; j < message4.Length; j++) {
				SetMessage (message4 [j]);
				yield return new WaitForSeconds (3f);
			}
			TurnOffMessage ();
			break;
		}


	}


	void OnCollisionEnter2D(Collision2D collision){
		

		 if (collision.gameObject.tag == "bear") {
			BearInteraction ();
		} else if (collision.gameObject.tag == "Finish") {
			SceneManager.LoadScene ("FinalScene", LoadSceneMode.Single);
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		if (collision.gameObject.tag == "bear") {
			TurnOffMessage ();
		}
	}

	private IEnumerator NeckUp(Animator animator){
		animator.SetTrigger ("neckup");
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("man_neckup")) {
			yield return null;
		}
		animator.SetTrigger ("idleup");	
	}

	private IEnumerator NeckDown(Animator animator){
		animator.SetTrigger ("neckdown");
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("man_neckup")) {
			yield return null;
		}
		animator.SetTrigger ("idle");	
	}


		

	void OnTriggerEnter2D(Collider2D collider){
		switch (collider.tag) {
		case "man":
			chained = true;
			Debug.Log ("COLIDIU!!");
			beggar = collider.gameObject;
			ChainInteraction ();
			Animator animator = collider.gameObject.GetComponent<Animator> ();
			StartCoroutine (NeckUp (animator));
			break;
			case "food":
				Destroy (collider.gameObject);
				break;
			case "wood":
				Destroy (collider.gameObject);
				break;

		case "checkpoint2":
			if (Input.GetAxis ("Horizontal") < 0)
				Camera.main.GetComponent<CameraMovement> ().enabled = true;
			break;
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		Debug.Log ("Stay");
		switch (collider.tag) {
		case "palmtree1":
			if (Input.GetKeyDown (KeyCode.Space) && nextChop < Time.time) {
				this.GetComponent<Animator>().SetTrigger ("chop");
				this.transform.Find ("tapa").gameObject.GetComponent<AudioSource> ().Play();
				if (rightPalm.Equals ("palmtree1")) {
					GameManager.instance.bearInteraction++;
					GameObject banana = collider.transform.GetChild (0).gameObject;
					banana.GetComponent<Rigidbody2D> ().gravityScale = 1;
				}
					
			}
			break;
		case "palmtree2":
			if (Input.GetKeyDown (KeyCode.Space) && nextChop < Time.time) {
				this.GetComponent<Animator>().SetTrigger ("chop");
				this.transform.Find ("tapa").gameObject.GetComponent<AudioSource> ().Play();
				if (rightPalm.Equals ("palmtree2"))
					GameManager.instance.bearInteraction++;
					GameObject banana = collider.transform.GetChild (0).gameObject;
					banana.GetComponent<Rigidbody2D> ().gravityScale = 1;
			}
			break;
		case "palmtree3":
			if (Input.GetKeyDown (KeyCode.Space) && nextChop < Time.time) {
				this.GetComponent<Animator>().SetTrigger ("chop");
				this.transform.Find ("tapa").gameObject.GetComponent<AudioSource> ().Play();
				if (rightPalm.Equals ("palmtree3"))
					GameManager.instance.bearInteraction++;
					GameObject banana = collider.transform.GetChild (bananaqtd).gameObject;
					banana.GetComponent<Rigidbody2D> ().gravityScale = 1;
			}
			break;
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		
		if (collider.gameObject.tag == "man") {
			Debug.Log ("SAIU!!");
			chained = false;
			talk = 0;
			TurnOffMessage ();
			Animator animator = collider.gameObject.GetComponent<Animator> ();
			StartCoroutine (NeckDown (animator));
		} 
	
		else if (collider.gameObject.tag == "checkpoint1") {
			if (Input.GetAxis ("Horizontal") > 0) {
				TurnOnMessage (this.transform.GetChild (0).gameObject);
				if (ignored) {
					SetMessage ("Este homem precisa de ajuda...");
				} else {
					SetMessage ("Realmente...");
				}
				StartCoroutine (CloseMessageAfterTime ());
			}

		} else if (collider.gameObject.tag == "checkpoint2") {			
			if (Input.GetAxis ("Horizontal") > 0) {
				Camera.main.GetComponent<CameraMovement> ().enabled = false;
				//background1.transform.position = background2.transform.position + new Vector3 (40.4f, 0, 0);
			} 
				//background1.transform.position = background2.transform.position + new Vector3 (-40.4f, 0, 0);
			else return;
		}
	}

	IEnumerator CloseMessageAfterTime(){
		yield return new WaitForSeconds(3f);
		TurnOffMessage ();
	}

	private IEnumerator WakeUp(Animator animator){
		animator.SetTrigger ("wake");
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("wake")) {
			yield return null;
		}
		animator.SetTrigger ("idle");
		GameManager.instance.final = false;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space) && chained) {
			ChainInteraction ();
		}
		if (GameManager.instance.final) {
			StartCoroutine(WakeUp(this.GetComponent<Animator>()));
		}
	}
}
