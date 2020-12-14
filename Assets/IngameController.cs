using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameController : MonoBehaviour
{
    AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {     
        Debug.Log("ingame started. selected mic:"+MicSelectController.micSelected);

        var btn=GameObject.Find("QuitButton");
        var t=btn.GetComponentInChildren<Text>();
        btn.GetComponent<Button>().onClick.AddListener( () => { onQuitButtonClicked(); } );            
   
        clip = Microphone.Start(MicSelectController.micSelected, true, 1, 48000);
        while(Microphone.GetPosition(MicSelectController.micSelected)==0) {
        }           
    }
    void onQuitButtonClicked()
    {
        Debug.Log("quitbutton clicked");
        Microphone.End(MicSelectController.micSelected);
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadScene("MicSelect");
    }                                                         
    void SceneLoaded(Scene nextScene, LoadSceneMode mode) 
    {
        Debug.Log("ingame scene loaded:"+nextScene.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(clip!=null) {
            int pos = Microphone.GetPosition(MicSelectController.micSelected);
            Debug.Log("pos:"+pos);
        }
    }

}
