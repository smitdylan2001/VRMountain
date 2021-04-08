using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour
{
	[System.Serializable]
	public struct AudioList
	{
		public AudioClip[] audioclips;
	}

	[SerializeField] AudioList[] audios;
	//[SerializeField] AudioClip[] shortPart1;
	//[SerializeField] AudioClip[] shortPart2;
	//[SerializeField] AudioClip[] MainPart2;

    [SerializeField] AudioSource[] audioSources;

	float startSize;
	Quaternion startRotation;

	int counter = 0;

	private void Start()
	{
		startSize = gameObject.transform.localScale.y;
		startRotation = gameObject.transform.rotation;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("AudioSwitch"))
		{
			StartCoroutine(SwitchAudio(other.gameObject));
		}
	}

	IEnumerator SwitchAudio(GameObject go)
	{
		while(gameObject.transform.localScale.x < 100)
		{
			gameObject.transform.localScale *= (1 + Time.deltaTime) * 1.1f;
			//TODO: Rotate
			gameObject.transform.Rotate(new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime));
			yield return null;
		}

		while (audioSources[0].volume > 0.02f)
		{
			foreach(AudioSource aus in audioSources)
			{
				aus.volume -= Time.deltaTime*2	;
			}
			yield return null;
		}
		foreach (AudioSource aus in audioSources)
		{
			aus.volume =0;
		}

		
		for (int i=0; i<audioSources.Length; i++)
		{
			audioSources[i].Stop();
			audioSources[i].clip = audios[counter].audioclips[i%audios[counter].audioclips.Length];
			audioSources[i].Play();
		}

		gameObject.transform.rotation = startRotation;

		while (audioSources[0].volume < 0.98f)
		{
			foreach (AudioSource aus in audioSources)
			{
				aus.volume += Time.deltaTime;
			}
			yield return null;
		}

		while (gameObject.transform.localScale.x > startSize + 0.5)
		{
			gameObject.transform.localScale /= (1 + Time.deltaTime) * 1.1f;
			yield return null;
		}
		gameObject.transform.localScale = new Vector3(startSize, startSize, startSize);
		go.SetActive(false);
		counter++;
	}
}
