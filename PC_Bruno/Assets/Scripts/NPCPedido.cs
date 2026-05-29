using UnityEngine;
using TMPro;

public class NPCPedido : MonoBehaviour
{
    public int quantidadeNecessaria = 2;

    private int quantidadeEntregue = 0;

    public int recompensa = 100;

    private bool playerPerto;

    public TMP_Text textoMissao;

    public PickupSystem pickupSystem;

    void Update()
    {
        if (playerPerto && Input.GetKeyDown(KeyCode.F))
        {
            EntregarCaixa();
        }
    }

    void EntregarCaixa()
    {
        // VERIFICA SE O PLAYER ESTÁ SEGURANDO CAIXA
        if (pickupSystem.EstaSegurandoCaixa())
        {
            // DESTROI A CAIXA
            pickupSystem.DestruirCaixa();

            quantidadeEntregue++;

            // MISSÃO COMPLETA
            if (quantidadeEntregue >= quantidadeNecessaria)
            {
                Dinheiro.instance.AdicionarDinheiro(recompensa);

                textoMissao.text = "Entrega concluida!";
            }
            else
            {
                textoMissao.text =
                "Entregue: " +
                quantidadeEntregue +
                "/" +
                quantidadeNecessaria;
            }
        }
        else
        {
            textoMissao.text = "Pegue uma caixa!";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;

            textoMissao.text =
            "Preciso de " +
            quantidadeNecessaria +
            " caixas!";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;

            textoMissao.text = "";
        }
    }
}