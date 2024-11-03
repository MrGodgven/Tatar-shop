using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
  public static void Main()
  {
    Product[] assrotiment = { new Product("икмэк", 99), new Product("сот", 99), new Product("йомырка", 119),
      new Product("шикэр", 69), new Product("тоз", 59), new Product("казылык", 139),
      new Product("тавык", 139), new Product("догэ", 109), new Product("карабодай", 109),
      new Product("бэрэнге", 109), new Product("кишер",99), new Product("суган", 89),
      new Product("кэбестэ", 109), new Product("помидор", 99), new Product("шартлату жайланмасы", 2000), };
    Shop shop = new Shop(assrotiment, 0);
    shop.Work();
  }

  public class Cart
  {

    private List<Product> cart = new List<Product>();

    public int GetFullPrice()
    {
      int fullprice = 0;
      for(int i=0; i < cart.Count; i++)
      {
        fullprice += cart[i].price;
      }

      return fullprice;
    }

    public void ShowCart()
    {
      for (int i = 0; i < cart.Count; i++)
      {
        if (i < 10) Console.WriteLine($"{i + 1}  - {cart[i].name} очен {cart[i].price}");
        else Console.WriteLine($"{i + 1} - {cart[i].name} очен {cart[i].price}");
      }
    }

    public int getCount()
    {
      return cart.Count;
    }

    public void AddEl(Product product)
    {
      cart.Add(product);
    }

    public void DelEl(int index)
    {
      for(int i = 0; i < cart.Count; i++)
      {
        if (i == index) cart.RemoveAt(index);
      }
    }

    public void Clear()
    {
      cart.Clear();
    }

  }

  public class Shop
  {
    static List<Product> assortiment = new List<Product>();
    private int money = 0;

    public Shop(Product[] Assortiment, int Money)
    {
      foreach (Product product in Assortiment)
      {
        assortiment.Add(product);
      }
      money = Money;
    }

    static void ShowAssortiment()
    {
      for(int i=0; i<assortiment.Count; i++)
      {
        Console.WriteLine(" " + assortiment[i]);
      }
    } 

    public void Work()
    {
      Random rnd = new Random();
      int userMoney = rnd.Next(350, 2500);
      Cart cart = new Cart();
      while (true) // main loop
      {
        Console.Clear();

        Console.WriteLine($"кибетнен {money} акчасы бар\n");

        Console.WriteLine($"сезнену {userMoney} акчагыз бар\n");
        
        ShowAssortiment();

        if (userMoney - cart.GetFullPrice() > 0)
        {
          Console.WriteLine(); Console.WriteLine($"кэрзиннен бэясе {cart.GetFullPrice()}, калды {userMoney - cart.GetFullPrice()}");
        }
        else Console.WriteLine($"кэрзиннен бэясе {cart.GetFullPrice()}");
        Console.WriteLine(); cart.ShowCart(); Console.WriteLine();

        string Num = Console.ReadLine(); int num;
        try
        {
          num = int.Parse(Num);
        }
        catch (Exception)
        {
          Console.Clear();
          Console.WriteLine("кертyдэ хата");
          Thread.Sleep(1000);
          continue;
        }

        if (Num == "01") // restart
        {
          userMoney = rnd.Next(350, 2500);
          cart.Clear();
          continue;
        }
        else if (Num == "00") // buy
        {
          if (cart.GetFullPrice() == 0)
          {
            Console.Clear();
            Console.WriteLine("хата");
            Thread.Sleep(1000);
            continue;
          }
          else if (cart.GetFullPrice() > userMoney)
          {
            Console.WriteLine($"сезгэ тагын {cart.GetFullPrice() - userMoney} акча кирэк");
            Thread.Sleep(1000);
            continue;
          }
          else
          {
            Console.WriteLine("Саубулыгыз");
            money += cart.GetFullPrice();
            userMoney = rnd.Next(350, 2500);
            cart.Clear();
            Thread.Sleep(1000);
            continue;
          }
        }
        else if (Num == "001100") // delet el in cart
        {
          int delnum;
          while (true)
          {
            try
            {
              delnum = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
              Console.Clear();
              Console.WriteLine("кертyдэ хата");
              Thread.Sleep(1000);
              continue;
            }
            if (delnum > cart.getCount())
            {
              Console.Clear();
              Console.WriteLine("кертyдэ хата");
              Thread.Sleep(1000);
              continue;
            }
            break;
          }
          cart.DelEl(delnum - 1);
          continue;
        }
        else if (num > 0) // add el in cart
        {
          cart.AddEl(assortiment[num-1]);
          continue;
        }
        else // error
        {
          Console.Clear();
          Console.WriteLine("хата");
          Thread.Sleep(1000);
          continue;
        }
      }

    }
  }

  public class Product
  {
    public string name { get; private set; }
    public int price { get; private set; }

    public Product(string Name, int Price)
    {
      name = Name;
      price = Price;
    }

  }
}