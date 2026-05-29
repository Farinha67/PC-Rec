using UnityEngine;
using TMPro;
using System.Collections;

public class NPCPedido : MonoBehaviour
{
    public TMP_Text textoMissao;

    public PickupSystem pickupSystem;

    private bool playerPerto;

    private int nivelMissao = 1;

    private int quantidadeNecessaria;
    private int recompensa;

    private int quantidadeEntregue = 0;

    private bool esperandoNovoPedido = false;

    void Start()
    {
        AtualizarMissao();
    }

    void Update()
    {
        if (playerPerto && Input.GetKeyDown(KeyCode.F))
        {
            EntregarCaixa();
        }
    }

    void AtualizarMissao()
    {
        // MISSÃO 1
        if (nivelMissao == 1)
        {
            quantidadeNecessaria = 2;
            recompensa = 100;
        }

        // MISSÃO 2
        else
        {
            quantidadeNecessaria = 5;
            recompensa = 150;
        }
    }

    void EntregarCaixa()
    {
        // ESPERANDO NOVO PEDIDO
        if (esperandoNovoPedido)
        {
            textoMissao.text =
            "Volte depois!";
            return;
        }

        // VERIFICA SE SEGURA CAIXA
        if (pickupSystem.EstaSegurandoCaixa())
        {
            pickupSystem.DestruirCaixa();

            quantidadeEntregue++;

            // COMPLETOU MISSÃO
            if (quantidadeEntregue >= quantidadeNecessaria)
            {
                Dinheiro.instance.AdicionarDinheiro(recompensa);

                textoMissao.text =
                "Missao completa! +" +
                recompensa +
                "$";

                quantidadeEntregue = 0;

                StartCoroutine(NovoPedido());
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
            textoMissao.text =
            "Pegue uma caixa!";
        }
    }

    IEnumerator NovoPedido()
    {
        esperandoNovoPedido = true;

        textoMissao.text =
        "Volte em 30 segundos!";

        yield return new WaitForSeconds(30f);

        // AUMENTA DIFICULDADE
        nivelMissao++;

        AtualizarMissao();

        textoMissao.text =
        "Novo pedido: " +
        quantidadeNecessaria +
        " caixas!";

        esperandoNovoPedido = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;

            if (!esperandoNovoPedido)
            {
                textoMissao.text =
                "Preciso de " +
                quantidadeNecessaria +
                " caixas!";
            }
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