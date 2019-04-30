using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryScrawl : MonoBehaviour {

	public Text storyScrawl;

	public GameObject pressAnyButton;

	public float scrawlSpeed = 10f;

	public Image circle;

	public GameObject skipText;

	private bool buttonPrompt = false;

	private float holdTimer = 0f;
	private const float HOLDMAXTIME = 2f;

	private float scrawlTimer = 0f;
	private const float SCRAWLTIME = 26.5f;

	void Start()
	{
		storyScrawl = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector2.up * (scrawlSpeed * Time.deltaTime));

		if(!buttonPrompt)
			SkipScene();

		DoneScrawling();
	}

	private void DoneScrawling()
	{
		scrawlTimer += Time.deltaTime;

		if(scrawlTimer > SCRAWLTIME && !buttonPrompt)
		{
			buttonPrompt = true;
			skipText.SetActive(false);
			InvokeRepeating("ButtonPromptFlicker", 0f, 1f);
		}

		if(pressAnyButton.activeSelf && Input.anyKeyDown)
		{
			SceneManager.LoadScene("ProtoNovusBoss");
		}

	}

	private void ButtonPromptFlicker()
	{
		pressAnyButton.SetActive(!pressAnyButton.activeSelf);
	}

	private void SkipScene()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			holdTimer += Time.deltaTime;

			circle.fillAmount = holdTimer / 2;

			if(holdTimer >= HOLDMAXTIME)
			{
				SceneManager.LoadScene("ProtoNovusBoss");
			}
		}
		else 
		{
			if(holdTimer > 0)
				holdTimer -= Time.deltaTime;

			circle.fillAmount = holdTimer / 2;
		}
	}
}
