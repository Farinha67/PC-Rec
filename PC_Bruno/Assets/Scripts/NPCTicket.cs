using UnityEngine;

public class NPCTicket : MonoBehaviour
{
    public GameObject ticketPrefab;

    public float distanciaInteracao = 3f;

    private Transform jogador;
    private PickupSystem pickup;

    private int ticketsComprados = 0;

    private int[] precos =
    {
        50,
        60,
        70,
        80,
        90,
        120
    };

    private float cooldownTempo = 30f;
    private bool podeComprar = true;

    private bool estavaPerto = false;


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


        bool pertoAgora = distancia <= distanciaInteracao;


        // Entrou perto do NPC
        if (pertoAgora && !estavaPerto)
        {
            MostrarPreco();
        }


        estavaPerto = pertoAgora;


        if (pertoAgora)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ComprarTicket();
            }
        }
    }


    void ComprarTicket()
    {
        // Já comprou todos
        if (ticketsComprados >= 6)
        {
            MensagemUI.instance.Mostrar(
                "Todos os Tickets já foram comprados!"
            );

            return;
        }


        // Cooldown
        if (!podeComprar)
        {
            MensagemUI.instance.Mostrar(
                "Aguarde o cooldown!"
            );

            return;
        }


        // Mão ocupada
        if (pickup.GetObjetoNaMao() != null)
        {
            MensagemUI.instance.Mostrar(
                "Sua mão está ocupada!"
            );

            return;
        }


        int precoAtual = precos[ticketsComprados];


        // Dinheiro insuficiente
        if (!Dinheiro.instance.TemDinheiro(precoAtual))
        {
            MensagemUI.instance.Mostrar(
                "Dinheiro insuficiente!"
            );

            return;
        }


        // Remove dinheiro
        Dinheiro.instance.RemoverDinheiro(precoAtual);


        // Coloca Ticket na mão
        pickup.PegarObjeto(ticketPrefab);


        ticketsComprados++;

        podeComprar = false;


        Invoke(
            nameof(LiberarCompra),
            cooldownTempo
        );


        MensagemUI.instance.Mostrar(
            "Ticket comprado por R$" + precoAtual
        );
    }


    void LiberarCompra()
    {
        podeComprar = true;


        MensagemUI.instance.Mostrar(
            "Novo Ticket disponível!"
        );
    }


    void MostrarPreco()
    {
        if (ticketsComprados >= 6)
        {
            MensagemUI.instance.Mostrar(
                "Todos os Tickets foram comprados!"
            );

            return;
        }


        int precoAtual = precos[ticketsComprados];


        MensagemUI.instance.Mostrar(
            "Ticket disponível por R$" + precoAtual +
            "\nAperte F para comprar"
        );
    }
}