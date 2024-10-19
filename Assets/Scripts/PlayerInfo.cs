using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    // public InputField inputName;
    public Text playerMoney;
    public Text playerItem;
    public int money;
    int ItemPrice;
    public int number;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            money = 100000;
            PlayerPrefs.SetInt("Money", money); //게임 첫 시작시 돈 0으로 시작
        }
        number = 0;
        ItemPrice = 100;
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        playerMoney.text = "money : " + money.ToString();
        playerItem.text = "myItem : " + number.ToString();
    }

    public void buy()
    {
        if (PlayerPrefs.GetInt("Money") >= ItemPrice) //아이템 가격보다 돈이 많을 때
        {
            number++;
            money -= ItemPrice;
            PlayerPrefs.SetInt("Money", money);
        }
    }
}
