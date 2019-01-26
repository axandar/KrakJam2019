using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code{
	public class MainMenuController : MonoBehaviour{
		[SerializeField] private Button playButton;
		[SerializeField] private Button exitButton;

		private void Start(){
			playButton.onClick.AddListener(Play);
			exitButton.onClick.AddListener(Exit);
		}

		private static void Play(){
			SceneManager.LoadScene(1);
		}

		private static void Exit(){
			Application.Quit();
			Debug.Log("Wyjszlo z apki");
		}
	}
}