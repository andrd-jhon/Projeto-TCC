using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songcontroller : MonoBehaviour
{

    public AudioSource audioSourceMusicaDeFundo;
    public AudioClip[] musicasDeFundo;
    // Start is called before the first frame update
    void Start()
    {
       AudioClip primeiraFaseSong = musicasDeFundo[0];
       audioSourceMusicaDeFundo.clip = primeiraFaseSong;
       audioSourceMusicaDeFundo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
