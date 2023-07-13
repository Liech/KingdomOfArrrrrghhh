using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer instance;
    public List<string> soundNames;
    public List<GameObject> soundInstance;
    Dictionary<string, GameObject> soundDict;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        soundDict = new Dictionary<string, GameObject>();
        for(int i = 0;i < soundNames.Count; i++) {
            soundDict[soundNames[i]] = soundInstance[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(string soundName) {
        if (soundDict.ContainsKey(soundName)) {
            var obj = Instantiate(soundDict[soundName], gameObject.transform, false);
            obj.transform.position = transform.position;
        }
    }
}
