using UnityEngine;

public class Bau : MonoBehaviour
{
    public float distanciaInteracao = 3f;

    private Transform jogador;
    private PickupSystem pickup;

    private int ticketsNoBau = 0;


    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        pickup = jogador.GetComponent<PickupSystem>();
    }


    void Update()
    {
        float distancia = Vector3.Distance(
            transform.position,
            jogador.position
        );


        if (distancia <= distanciaInteracao)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                EntregarTicket();
            }
        }
    }


    void EntregarTicket()
    {
        if (ticketsNoBau >= 6)
        {
            MensagemUI.instance.Mostrar(
                "O desafio já foi concluído!"
            );

            return;
        }


        if (!pickup.EstaSegurandoTicket())
        {
            MensagemUI.instance.Mostrar(
                "Você precisa de um Ticket!"
            );

            return;
        }


        // Remove o Ticket da mão
        pickup.DestruirObjeto();


        ticketsNoBau++;


        MensagemUI.instance.Mostrar(
            "Ticket entregue! " + ticketsNoBau + "/6"
        );


        if (ticketsNoBau == 6)
        {
            Vitoria();
        }
    }


    void Vitoria()
    {
        MensagemUI.instance.Mostrar(
            "Parabéns! Você concluiu o desafio!"
        );

        Debug.Log("FIM DO JOGO");
    }
}