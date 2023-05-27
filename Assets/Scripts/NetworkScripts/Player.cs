using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    // Start is called before the first frame update
    public NetworkVariable <int> testString = new NetworkVariable<int>(25);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnNetworkSpawn()
    {
        testString.Value = 3;
        Debug.Log(testString.Value);
        //Debug.Log(5);
    }
    public void OnButtonClick()
    {
        testString.Value = 3;
        Debug.Log(testString.ToString());
        Debug.Log(5);
    }
}
