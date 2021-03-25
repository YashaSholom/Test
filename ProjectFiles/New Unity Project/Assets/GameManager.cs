using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public List<GameObject> OpenWindwos
    {
        get { return openWindows; }
    }

    private List<GameObject> openWindows = new List<GameObject>();


    private void Awake()
    {
        if(GameManager.Instance == null)
        {
            Instance = this;
        }
    }

    //Temp
    public void AddUIWindow()
    {
        openWindows.Add(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
