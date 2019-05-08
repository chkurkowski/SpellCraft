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
	public float SCRAWLTIME = 26.5f;

	void Start()
	{
		holdTimer = 0f;
		scrawlTimer = 0f;
		buttonPrompt = false;
		Time.timeScale = 1f;
		storyScrawl = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		print("Hit");
		transform.Translate(Vector2.up * (scrawlSpeed * Time.deltaTime));

		if(!buttonPrompt)
			SkipScene();

		DoneScrawling();
	}

	private void DoneScrawling()
	{
		scrawlTimer += Time.deltaTime;
		print(scrawlTimer);
		if(scrawlTimer > SCRAWLTIME && !buttonPrompt)
		{
			print("Hit prompt");
			buttonPrompt = true;
			skipText.SetActive(false);
			InvokeRepeating("ButtonPromptFlicker", 0f, 1f);
		}

		if(pressAnyButton.activeSelf && Input.anyKeyDown)
		{
			CancelInvoke();
			if(SceneManager.GetActiveScene().name == "TitleScrawl")
				SceneManager.LoadScene("ProtoNovusBoss");
			else
				SceneManager.LoadScene("Main Menu");
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
			print("Hit space");
			holdTimer += Time.deltaTime;

			circle.fillAmount = holdTimer / 2;

			if(holdTimer >= HOLDMAXTIME)
			{
				CancelInvoke();
				if(SceneManager.GetActiveScene().name == "TitleScrawl")
					SceneManager.LoadScene("ProtoNovusBoss");
				else
					SceneManager.LoadScene("Main Menu");
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
