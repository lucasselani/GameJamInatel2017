using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public int beggarInteraction = 0;
	public bool bearSatisfied = false;
	public int bearInteraction = 0;
	public bool beggarSatisfied = false;
	public bool final = false;
	public int steps = 0;
	public string[] talks;

	void Awake()
	{
		if(instance == null) instance = this;
		else if(instance != this) Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}


	// Update is called once per frame
	void Update () {
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name.Equals ("CavernNightScene"))
			beggarInteraction = 1;
		if (bearSatisfied) {
			//TODO: Implementar animação de andar ate o canto da tela
			//e abrir a tela de plataforma
			SceneManager.LoadScene ("CavernNightScene", LoadSceneMode.Single);
			bearSatisfied = false;
		} else if (beggarSatisfied) {
			//TODO: Construir o carrinho e retornar a caverna
			//para ajudar o mandingueiro
			SceneManager.LoadScene ("HomeScene", LoadSceneMode.Single);
			final = true;
			beggarSatisfied = false;
		} 
	}
}
