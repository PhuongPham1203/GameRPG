using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LangDropDown : MonoBehaviour
{
	[SerializeField] string[] myLangs;
	//Dropdown drp;
	int index;

	void Awake ()
	{
		/*
		drp = this.GetComponent <Dropdown> ();
		int v = PlayerPrefs.GetInt ("_language_index", 0);
		drp.value = v;

		drp.onValueChanged.AddListener (delegate {
			index = drp.value;
			PlayerPrefs.SetInt ("_language_index", index);
			PlayerPrefs.SetString ("_language", myLangs [index]);
			Debug.Log ("language changed to " + myLangs [index]);
			//apply changes
			ApplyLanguageChanges ();
		});
		*/
	}

	public void ChangeLanguage(int numberLanguage)
    {
		index = numberLanguage;
		PlayerPrefs.SetInt("_language_index", index);
		PlayerPrefs.SetString("_language", myLangs[index]);
		Debug.Log("language changed to " + myLangs[index]);
		//apply changes
		ApplyLanguageChanges();
	}

	void ApplyLanguageChanges ()
	{
		//SceneManager.LoadScene (0);
		//GetComponent<Text>().text = GameMultiLang.GetTraduction(key);
		TextTranslator[] all = GameObject.FindObjectsOfType<TextTranslator>();

		foreach (TextTranslator textTranslator in all)
        {
			textTranslator.GetComponent<Text>().text = GameMultiLang.GetTraduction(textTranslator.key);
		}
	}

	void OnDestroy ()
	{
		//drp.onValueChanged.RemoveAllListeners ();
	}
}
