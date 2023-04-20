using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreReqText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string getText(string level_name){
        if(level_name=="level1"){
            return "Pre Req: Tuotrial 1, 2, 3";
        }
        return "No Pre Req";
    }
}
