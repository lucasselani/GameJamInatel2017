using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSound : MonoBehaviour {
	public float FADE_OUT = 3f;
	AudioSource logoSai, foraCaverna1, transicao1, foraCaverna2, transicao2, perguntas;

	// Use this for initialization
	void Start () {
		logoSai = GetAudioSource ("logoSai");
		foraCaverna1 = GetAudioSource ("foraCaverna1");
		transicao1 = GetAudioSource ("transicao1");
		foraCaverna2 = GetAudioSource ("foraCaverna2");
		transicao2 = GetAudioSource ("transicao2");
		perguntas = GetAudioSource ("perguntas");

		logoSai.Play ();
	}

	IEnumerator fadeOut(AudioSource audio, float stopInterval){
		StartCoroutine (StopMusic (audio, stopInterval));
		while (audio.volume != 0f) {
			audio.volume -= 0.1f;
			yield return new WaitForSeconds (0.5f);
		}
	}

	IEnumerator fadeIn(AudioSource audio, float playInterval, float volume){
		StartCoroutine (StartMusic (audio, playInterval));
		while (audio.volume != 1f) {
			audio.volume += 0.1f;
			yield return new WaitForSeconds (0.5f);
		}
	}

	IEnumerator StartMusic(AudioSource audio, float playInterval){
		audio.Play ();
		yield return new WaitForSeconds (playInterval);
		StartCoroutine (fadeOut (audio, FADE_OUT));
	}

	IEnumerator StopMusic(AudioSource audio, float stopInterval){
		yield return new WaitForSeconds (stopInterval);
		audio.Stop ();
	}

	AudioSource GetAudioSource(string s){
		return this.transform.Find (s).gameObject.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.instance.steps == 0) {
			if (logoSai.time < 10.3) {
				Debug.Log (logoSai.time);
				return;
			}
			else {
				logoSai.Stop ();
				GameManager.instance.steps = 1;
			}
		} else if (GameManager.instance.steps == 1) {
			if(!foraCaverna1.isPlaying) foraCaverna1.Play ();
		} else if (GameManager.instance.steps == 2) {
			if(foraCaverna1.isPlaying) foraCaverna1.Stop ();
			if(!transicao1.isPlaying) transicao1.Play ();
			if (transicao1.time > 3) {
				if(transicao1.isPlaying) transicao1.Stop ();
				GameManager.instance.steps = 3;
			}
		} else if (GameManager.instance.steps == 3) {
			if(!foraCaverna2.isPlaying) foraCaverna2.Play ();
		} else if (GameManager.instance.steps == 4) {
			if(foraCaverna2.isPlaying) foraCaverna2.Stop ();
			if(!transicao2.isPlaying) transicao2.Play ();
			if (transicao2.time > 6) {
				if(transicao2.isPlaying) transicao2.Stop ();
				GameManager.instance.steps = 5;
			}
		} else if (GameManager.instance.steps == 5) {
			if(!perguntas.isPlaying) perguntas.Play ();
		}
	}
}
