using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MicSelectController : MonoBehaviour
{
    //AudioClip clip;
    static public string micSelected;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Application.targetFrameRate = 60; 

        Debug.Log("Start");

        // マイクを選ぶ部分はアプリケーション側で実装する。MMVV側でデフォルトマイクを検索する機能があってもよいかも
        // TODO:　マイクが複数ある場合、選択できるようにする
        // TODO: androidの場合 camcoderを選択する。
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);

        if (Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            Debug.Log("Microphone authorized");
        }
        else
        {
            Debug.Log("Microphone not authorized");
        }

        var devs=Microphone.devices;
        Debug.Log("mic device num:"+devs.Length);        
        for(int i=0;i<devs.Length;i++) {
            Debug.Log("Mic device "+i+" : "+devs[i]);
            string name="Button"+i;
            var btn=GameObject.Find(name);
            var t=btn.GetComponentInChildren<Text>();
            t.text=devs[i];
            btn.GetComponent<Button>().onClick.AddListener( () => { onButtonClicked(name); } );                                                                      
        }
    }
    void onButtonClicked(string name)                                                         
    {                                                   
        var devname=GameObject.Find(name).GetComponentInChildren<Text>().text;
        Debug.Log("button down name:"+name + " dev:"+devname);
        micSelected=devname;
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadScene("Ingame");        
    }                                                                                           

    // Update is called once per frame
    void Update()
    {
        
    }
    void SceneLoaded(Scene nextScene, LoadSceneMode mode) 
    {
        Debug.Log("micsel: sceneloaded"+nextScene.name);
    }
}
