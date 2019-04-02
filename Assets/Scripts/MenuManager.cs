using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	private static MenuManager _instance = null;
	public static MenuManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType (typeof (MenuManager)) as MenuManager;
				if (_instance == null)
				{
					GameObject go = new GameObject ();
					_instance = go.AddComponent<MenuManager> ();
					go.name = "MenuManager";
				}
			}

			return _instance;
		}
	}

	[SerializeField]
	private GameObject mainMenuPanel;
	[SerializeField]
	private GameObject gamePanel;
	[SerializeField]
	private GameObject gameoverPanel;

	[Header ("SCORE")]
	[SerializeField]
	private Text[] scoreTexts;

	// Use this for initialization
	void Awake ()
	{
		AudioManager.Instance.PlayBGM ("MainMenu");
	}

	public void StartGame ()
	{
		mainMenuPanel.SetActive (false);

		if (!scoreTexts[0].gameObject.activeSelf)
		{
			scoreTexts[0].gameObject.SetActive (true);
		}

		gamePanel.SetActive (true);
		GameManager.Instance.StartGame ();
	}

	public void Gameover ()
	{
		scoreTexts[0].gameObject.SetActive (false);
		AudioManager.Instance.PlayUISound ("GameOver");
		AudioManager.Instance.StopBGM ();
		gameoverPanel.SetActive (true);
	}

	public void Retry ()
	{
		GameManager.Instance.Replay ();
		gameoverPanel.SetActive (false);
		StartGame ();
	}

	public void ScoreTextChange (int score)
	{
		foreach (Text scoreText in scoreTexts)
		{
			scoreText.text = score.ToString ();
		}
	}
}