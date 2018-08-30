using UnityEngine;

public class Pause : MonoBehaviour {

    public GameObject PauseMenu; 

	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }	
	}

    public void PauseMobile()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }
}
