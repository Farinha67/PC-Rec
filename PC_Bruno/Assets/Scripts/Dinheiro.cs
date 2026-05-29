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

    public void AdicionarDinheiro(int valor)
    {
        dinheiro += valor;

        textoDinheiro.text = "$ " + dinheiro;
    }
}