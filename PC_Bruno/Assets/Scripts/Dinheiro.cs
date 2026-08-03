using UnityEngine;
using TMPro;

public class Dinheiro : MonoBehaviour
{
    public static Dinheiro instance;

    public int dinheiro;

    public TMP_Text textoDinheiro;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        AtualizarTexto();
    }

    public void AdicionarDinheiro(int valor)
    {
        dinheiro += valor;
        AtualizarTexto();
    }

    public bool RemoverDinheiro(int valor)
    {
        if (dinheiro < valor)
            return false;

        dinheiro -= valor;
        AtualizarTexto();

        return true;
    }

    public bool TemDinheiro(int valor)
    {
        return dinheiro >= valor;
    }

    void AtualizarTexto()
    {
        textoDinheiro.text = "$ " + dinheiro;
    }
}