using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public bool jogoIniciou;
    public bool vezPlayer1;
    public bool jogoEncerrou;

    public int countTurnos;

    // Objetos

    public QuadController[] quads;

    public GameObject player1;

    public GameObject player2;
    // UI
    public Button jogarButton;
    public Button reiniciarButton;

    public string vencedor;

    public TextMeshProUGUI vencedorLabelText;  
    public TextMeshProUGUI vencedorPlayerText; 
    public TextMeshProUGUI turnoPlayerText;
    public TextMeshProUGUI turnoLabelText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countTurnos >= 9 ){
            GameObject vencedor = VerificaVencedor();
            if(vencedor != null){
                if(vencedor.tag == player1.tag){
                    //Debug.Log("jogador 1 venceu!");
                    vencedorPlayerText.text = "Jogador 1";
                    vencedorPlayerText.color = new Color(127, 0, 0);
                } else {
                    //Debug.Log("Jogador 2 venceu!");
                    vencedorPlayerText.text = "Jogador 2";
                    vencedorPlayerText.color = new Color(0, 127, 0);
                } 
                jogoEncerrou = true;
                vencedorLabelText.gameObject.SetActive(true);
                vencedorPlayerText.gameObject.SetActive(true);
                reiniciarButton.gameObject.SetActive(true);
                turnoPlayerText.gameObject.SetActive(false);
                turnoLabelText.gameObject.SetActive(false);
            } else {
                DarVelha();
            }
            
        }
    }
    public string ObterQuad(int i){
        if(i == 1){
            return "Superior Esquerdo";
        } else if( i == 2) {
            return "Superior Centro";
        } else if(i == 3){
            return "Superior Direito";
        } else if(i == 4){
            return "Central Esquerdo";
        } else if( i == 5) {
            return "Central";
        } else if(i == 6){
            return "Central Direito";
        } else if(i == 7){
            return "Inferior Esquerdo";
        } else if( i == 8) {
            return "Inferior Central";
        } else if(i == 9){
            return "Inferior Direito";
        } else {
            return "";
        }
    }

    public void IniciarJogo(){
        CriaQuads();
        vezPlayer1 = true;
        jogarButton.gameObject.SetActive(false);
        jogoIniciou = true;
        vencedor = "";
        countTurnos = 0;
        turnoPlayerText.text = "Jogador 1";
        turnoPlayerText.color = new Color (127, 0, 0);
        turnoPlayerText.gameObject.SetActive(true);
        turnoLabelText.gameObject.SetActive(true);
    }
    public void ReiniciarJogo(){
        foreach(QuadController q in quads){
            q.playerIcon = null;
        }
        
        GameObject[] iconsPlayer1 = GameObject.FindGameObjectsWithTag("Player1");
        GameObject[] iconsPlayer2 = GameObject.FindGameObjectsWithTag("Player2");
        
        foreach(GameObject icon in iconsPlayer1){
            Destroy(icon);
        }
        foreach(GameObject icon in iconsPlayer2){
            Destroy(icon);
        }
        jogoEncerrou = false;
        vencedorLabelText.text = "Vencedor:";
        vencedorLabelText.gameObject.SetActive(false);
        vencedorPlayerText.gameObject.SetActive(false);
        reiniciarButton.gameObject.SetActive(false);
        vezPlayer1 = true;
        jogoIniciou = true;
        vencedor = "";
        countTurnos = 0;
        turnoPlayerText.gameObject.SetActive(true);
        turnoLabelText.gameObject.SetActive(true);
    }

    private void InicializaVariaveis(){
        jogarButton.gameObject.SetActive(true);
    }

    public void CriaQuads(){
        foreach(QuadController q in quads){
            Instantiate(q);
        }
    }

    public GameObject ObtemJogador(){
        if(vezPlayer1){
            return player1;
        } else {
            return player2;
        }
    }

    public void AlternaJogador(){
        vezPlayer1 = !vezPlayer1;
        countTurnos++;
        if(vezPlayer1){
            turnoPlayerText.text = "Jogador 1";
            turnoPlayerText.color = new Color (127, 0, 0);
        } else {
            turnoPlayerText.text = "Jogador 2";
            turnoPlayerText.color = new Color (0, 127, 0);
        }
        
    }

    public string ObtemJogadorNome(){
        if(vezPlayer1){
            return "Jogador 1";
        } else {
            return "jogador 2";
        }
    }

    public GameObject VerificaVencedor(){

        if(VerificaVencedorJogador1(player1)){
            return player1;
        } else if(VerificaVencedorJogador1(player2)){
            return player2;
        } else {
            return null;
        }

    }

    public bool VerificaVencedorJogador1(GameObject jogador){
        // 
        if( VerificaLinha(1,2,3, jogador)){
            return true;
        } else if( VerificaLinha(4,5,6, jogador)){
            return true;
        } else if( VerificaLinha(7,8,9, jogador)){
            return true;
        } else if( VerificaLinha(1,4,7, jogador)){
            return true;
        } else if( VerificaLinha(2,5,8, jogador)){
            return true;
        } else if( VerificaLinha(3,6,9, jogador)){
            return true;
        } else if( VerificaLinha(1,5,9, jogador)){
            return true;
        } else if( VerificaLinha(3,5,7, jogador)){
            return true;
        } else { 
            return false;
        }
        
    }

    public bool VerificaLinha(int linha1, int linha2, int linha3, GameObject jogador){
        linha1--;
        linha2--;
        linha3--;
        if(quads[linha1].playerIcon != null &&  quads[linha2].playerIcon != null && quads[linha3].playerIcon != null){
            if(quads[linha1].playerIcon.tag == jogador.tag  && 
                quads[linha2].playerIcon.tag == jogador.tag && 
                quads[linha3].playerIcon.tag == jogador.tag){
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
        
    }

    public void EncerraJogo(){
        GameObject vencedor = VerificaVencedor();
        if(vencedor != null){
            if(vencedor.tag == player1.tag){
                //Debug.Log("jogador 1 venceu!");
                vencedorPlayerText.text = "Jogador 1";
                vencedorPlayerText.color = new Color(127, 0, 0);
            } else {
                //Debug.Log("Jogador 2 venceu!");
                vencedorPlayerText.text = "Jogador 2";
                vencedorPlayerText.color = new Color(0, 127, 0);
            } 
            jogoEncerrou = true;
            vencedorLabelText.gameObject.SetActive(true);
            vencedorPlayerText.gameObject.SetActive(true);
            reiniciarButton.gameObject.SetActive(true);
            turnoPlayerText.gameObject.SetActive(false);
            turnoLabelText.gameObject.SetActive(false);
        } 
    }
    
    public void SalvaIconQuad(int quadNumber, GameObject icon){
        quads[quadNumber].playerIcon = icon;
    }

    public void DarVelha(){
        jogoEncerrou = true;
        vencedorLabelText.text = "Deu Velha!";
        vencedorLabelText.gameObject.SetActive(true);
        reiniciarButton.gameObject.SetActive(true);
        turnoPlayerText.gameObject.SetActive(false);
        turnoLabelText.gameObject.SetActive(false);
        
    }

}
