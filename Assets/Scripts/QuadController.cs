using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadController : MonoBehaviour
{
    GameManager gameManager;
    public GameObject playerIcon;
    public int quadNumber;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown() {
        if(playerIcon == null && !gameManager.jogoEncerrou){
            GameObject novoIcon = Instantiate(gameManager.ObtemJogador(), transform.position, transform.rotation);
            playerIcon = novoIcon;
            gameManager.AlternaJogador(); 
            gameManager.SalvaIconQuad(quadNumber - 1, playerIcon );
            //Debug.Log( gameManager.ObtemJogadorNome() +  " clicou no " + gameManager.ObterQuad(quadNumber));
        } 
       gameManager.EncerraJogo();
    }
}
