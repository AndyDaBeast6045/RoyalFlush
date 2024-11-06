using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public abstract string GiveName();

    public virtual void onGet()
    {

    }
    public virtual void onBattleStart()
    {

    }

    public virtual void onBattleEnd()
    {

    }

    public virtual void onEnemyTurn()
    {

    }

    public virtual void onPlayerTurn()
    {

    }
    
    public Item assignItem(Items itemToAssign)
    {
        switch (itemToAssign)
        {
            case Items.GrabbyHand:
                return new GrabbyHand();
            case Items.CashoutTicket:
                return new CashoutTicket();
            case Items.CardCounter:
                return new CardCounter();
            case Items.LuckyCoin: 
                return new LuckyCoin();
            case Items.RouletteShield: 
                return new RouletteShield();
            case Items.FourLeafClover:
                return new FourLeafClover();
            case Items.CreditCard:
                return new CreditCard();
            case Items.Champagne:
                return new Champagne();
            case Items.Shades:
                return new Shades();
            case Items.TailoredSuit:
                return new TailoredSuit();
            case Items.FireDice:
                return new FireDice();
            case Items.VIPCard:
                return new VIPCard();
            case Items.BlackCard:
                return new BlackCard();
            case Items.Vodka:
                return new Vodka();
            case Items.BrokenBottle:
                return new BrokenBottle();
            case Items.BunnyEars:
                return new BunnyEars();
            default:
                return null;
        }
    }
}

public enum Items{
GrabbyHand,
CashoutTicket,
CardCounter,  //rare
LuckyCoin,  //rare
RouletteShield,
FourLeafClover, //rare
CreditCard,
Champagne,
Shades,
TailoredSuit,
FireDice,
VIPCard,
BlackCard,  //rare
Vodka,
BrokenBottle,
BunnyEars  //rare
}

public class GrabbyHand : Item
{
    public override string GiveName()
    {
        return "Grabby Hand";
    }

    public override void onGet()
    {
        //TODO
        //BenchController.baseBenchSize += 1;
    }
}

public class CashoutTicket : Item
{
    public override string GiveName()
    {
        return "Cash-Out Ticket";
    }

    public override void onGet()
    {
        //TODO
        //CurrencyController.setChipsMult(1.2f);
    }
}

public class CardCounter : Item
{
    public override string GiveName()
    {
        return "Card Counter";
    }

    public override void onBattleEnd()
    {
        //TODO: whenever turnSystem is implemented, increase cards recieved after combat by 1 
    }
}

public class LuckyCoin : Item
{
    public override string GiveName()
    {
        return "Lucky Coin";
    }

    public override void onEnemyTurn()
    {
        //TODO
    }
}

public class RouletteShield : Item
{
    public override string GiveName()
    {
        return "Roulette Shield";
    }

    public override void onEnemyTurn()
    {
        //TODO
    }
}

public class FourLeafClover: Item
{
    public override string GiveName()
    {
        return "Four-leaf Clover";
    }

    public override void onBattleEnd()
    {
        //TODO
    }
}

public class CreditCard : Item
{
    public override string GiveName()
    {
        return "Credit Card";
    }

    public override void onGet()
    {
        //TODO
    }
}

public class Champagne : Item
{
    public override string GiveName()
    {
        return "Champagne";
    }

    public override void onBattleEnd()
    {
        //TODO
    }
}

public class Shades : Item
{
    public override string GiveName()
    {
        return "Shades";
    }

    public override void onBattleStart()
    {
        //TODO
    }
}

public class TailoredSuit : Item
{
    public override string GiveName()
    {
        return "Tailored Suit";
    }

    public override void onBattleStart()
    {
        //TODO
    }
}

public class FireDice : Item
{
    public override string GiveName()
    {
        return "Fire Dice";
    }

    public override void onBattleStart()
    {
        //TODO
    }
}

public class VIPCard : Item
{
    public override string GiveName()
    {
        return "VIP Card";
    }

    public override void onGet()
    {
        //TODO
    }
}

public class BlackCard : Item
{
    public override string GiveName() 
    {
        return "Black Card";
    }

    public override void onGet()
    {
        //TODO
    }
}

public class Vodka : Item
{
    public override string GiveName() 
    {
        return "Vodka";
    }

    public override void onBattleStart()
    {
        //TODO
    }
}

public class BrokenBottle : Item
{
    public override string GiveName() 
    {
        return "Broken Bottle";
    }

    public override void onBattleStart()
    {
        //TODO
    }
}

public class BunnyEars : Item
{
    public override string GiveName()
    {
        return "Bunny Ears";
    }

    public override void onGet()
    {
        //TODO
    }
}