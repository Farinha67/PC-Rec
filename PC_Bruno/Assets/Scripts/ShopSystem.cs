using UnityEngine;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    public GameObject boxPrefab;
    public GameObject torchPrefab;

    public Transform spawnPoint;

    public TMP_Text textoLoja;

    private bool playerPerto;

    void Update()
    {
        if (playerPerto)
        {
            // COMPRAR CAIXA
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ComprarCaixa();
            }

            // COMPRAR TOCHA
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ComprarTocha();
            }
        }
    }

    void ComprarCaixa()
    {
        if (Dinheiro.instance.dinheiro >= 20)
        {
            Dinheiro.instance.dinheiro -= 20;

            Instantiate(
            boxPrefab,
            spawnPoint.position,
            Quaternion.identity);

            Dinheiro.instance.textoDinheiro.text =
            "$ " + Dinheiro.instance.dinheiro;

            textoLoja.text = "Caixa comprada!";
        }
        else
        {
            textoLoja.text = "Sem dinheiro!";
        }
    }

    void ComprarTocha()
    {
        if (Dinheiro.instance.dinheiro >= 60)
        {
            Dinheiro.instance.dinheiro -= 60;

            Instantiate(
            torchPrefab,
            spawnPoint.position,
            Quaternion.identity);

            Dinheiro.instance.textoDinheiro.text =
            "$ " + Dinheiro.instance.dinheiro;

            textoLoja.text = "Tocha comprada!";
        }
        else
        {
            textoLoja.text = "Sem dinheiro!";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;

            textoLoja.text =
            "1 = Caixa ($20)\n2 = Tocha ($60)";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;

            textoLoja.text = "";
        }
    }
}