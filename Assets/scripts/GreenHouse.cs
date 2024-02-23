using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHouse : MonoBehaviour
{

    private BoxCollider2D m_BoxCollider;

    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("something: " + collision.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.LoseGame();
        }
    }

}
