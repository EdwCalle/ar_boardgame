using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGLTF;

public class borrar : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        //GetComponent<GLTFComponent>().PlayAnimationOnLoad = true;
        await GetComponent<GLTFComponent>().Load();

        transform.GetChild(0).GetComponent<Animator>().Play(0);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
