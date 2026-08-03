using UnityEngine;
using TMPro;

public class MensagemUI : MonoBehaviour
{
    public static MensagemUI instance;

    public TMP_Text texto;


    void Awake()
    {
        instance = this;
    }


    public void Mostrar(string mensagem)
    {
        texto.text = mensagem;

        CancelInvoke();

        Invoke(nameof(Limpar), 3f);
    }


    void Limpar()
    {
        texto.text = "";
    }
}