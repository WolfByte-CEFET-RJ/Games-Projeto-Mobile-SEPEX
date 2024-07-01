using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAttack : MonoBehaviour
{
    private Animator anim;
    public IAttackStrategy attackSelected;//Aqui vao uma das 3 opcoes de ataque: Sonic, Bomba patch e Bears Garden, ambos
                                          //Implementando essa interface

    [SerializeField] private float frequenciaAtaque;
    private float cronometer;

    [Header("BearGardenSettings")]
    [SerializeField] private GameObject[] batatas;

    
    [Header("BombaPatchSettings")]
    [SerializeField] private GameObject ball;

    [Header("SonicSettings")]
    [SerializeField] private GameObject spinDashObj;

    private bool canAttack = true;
    public void SetCanAttack(bool b) { canAttack = b; }
    void Start()
    {
        anim = GetComponent<Animator>();
        choiseAttack();
        cronometer = -8;//Comeca negativo assim para que possa dar tempo da cutscene ser executada antes do primeiro ataque
    }
    private void Update()
    {
        if(canAttack)
        {
            cronometer += Time.deltaTime;
            if (cronometer >= frequenciaAtaque && attackSelected)
            {
                attackSelected.chargeAttack();//Essa funcao ira carregar e executar o ataque selecionado
                cronometer = 0;
            }
        }      
    }

    private void choiseAttack()
    {
        int attackChoise = Random.Range(0, 3);
        switch (attackChoise)
        {
            case 0: attackSelected = gameObject.AddComponent<SanicStrategy>();//Alternativa da unity pra new SanicStrategy().
                break;
            case 1:
                attackSelected = gameObject.AddComponent<BearGardenStrategy>();
                break;
            case 2:
                attackSelected = gameObject.AddComponent<BombaPatchStrategy>();
                break;
        }
        anim.SetInteger("jogoAtaque", attackChoise);
        Debug.Log("Ataque " + attackChoise);
    }

    #region Getters
    public GameObject[] getBatatas() { return batatas; }
    public GameObject getBall() { return ball; }

    public GameObject getSpinDash() { return spinDashObj; }

    public void setAttackSelectedToNull() { attackSelected = null; }//Metodo para parar qualquer ataque apos a morte do fliperama

    #endregion

}
