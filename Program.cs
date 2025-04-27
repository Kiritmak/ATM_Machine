using System.Reflection;
using System.Security.Principal;

namespace Program
{
  public interface IAccount
  {
    bool Decrement(float ammount);
    void Increment(float ammount);
  }
  public class Account : IAccount
  {
    public Account(int id, string owner, int pin, float initialAmmount)
    {
      Id = id;
      Owner = owner;
      PIN = pin;
      Money = initialAmmount;
    }

    public string Owner { get; private set; }
    public int Id { get; private set; }
    public int PIN { get; private set; }
    public float Money { get; private set; }

    public bool Decrement(float ammount)
    {
      if (Money >= ammount)
      {
        Money -= ammount;
        return true;
      }
      Console.WriteLine("Can't do this action");
      return false;
    }
    public void Increment(float ammount) => Money += ammount;
    public override string ToString()
    {
      return $"Owner: {Owner}\nCurrent Balance: {Money}\nId: {Id}";
    }

  }

  public static class IO
  {
    public static T ReadInput<T>() where T : IParsable<T>
    {
      T variable;
      while(!T.TryParse(Console.ReadLine(), null, out variable))
        Console.WriteLine("Incorrect Format");
      return variable;
    }
    public static int ReadIntInRange(int min=int.MinValue, int mx = int.MaxValue)
    {
      int variable = IO.ReadInput<int>();
      while(variable < min || variable > mx)
      {
        Console.WriteLine("Incorrect Format");
        variable = IO.ReadInput<int>();
      }
      return variable;
    }
    public static Account? ReadAccountOwner(List<Account> AccountsDB)
    {
      string Owner;
      int pin;
      Account? account;
      Console.WriteLine("Please enter the username of the account");
      Owner = ReadInput<string>();
      if(!AccountsDB.Any(x => x.Owner == Owner))
      {
        Console.WriteLine($"There is no accoun with username {Owner}");
        return null;
      }
      Console.WriteLine("Please enter the pin of the account");
      pin = ReadInput<int>();
      account = AccountsDB.Find(a => a.Owner == Owner && a.PIN == pin);
      if (account == null) Console.WriteLine("Incorrect PIN");
      return account;
    }
    public static Account? ReadAccountRemitent(List<Account> AccountsDB)
    {
      string Owner;
      Account? account;
      Console.WriteLine("Please enter the username of the account");
      Owner = ReadInput<string>();
      if (!AccountsDB.Any(x => x.Owner == Owner))
      {
        Console.WriteLine($"There is no accoun with username {Owner}");
        return null;
      }
      account = AccountsDB.Find(a => a.Owner == Owner);
      return account;
    }
    public static void FormatDecorator(Action f)
    {
      Console.WriteLine("---------------------------------");
      f();
      Console.WriteLine("---------------------------------");
    }
  }
  public static class FileSaves
  {
    public static List<Account> GetAccountsDB()
    {
      List<Account> accounts = new List<Account>()
      {
        new Account(0, "Zoe", 1234, 1000),
        new Account(1, "Mark", 3333, 700),
        new Account(2, "RichMan", 7777, 9999),
        new Account(3, "Kirit", 4321, 5000)
      };
      return accounts;
    }
  }
  public class ATM
  {
    public ATM()
    {
      accountsDB = FileSaves.GetAccountsDB();
      Count = accountsDB.Aggregate(0, (a, b) => int.Max(a, b.Id)) + 1;
      Console.WriteLine(Count);
    }

    public List<Account> accountsDB;
    public int Count { get; private set; }

    public void Transfeer() 
    {
      Account? Sender, Remitent;
      float ammount;

      Console.WriteLine("Please input your account");
      Sender = IO.ReadAccountOwner(accountsDB);
      if (Sender==null) return;
      Console.WriteLine("Please input the ammount to transfeer");
      ammount = IO.ReadInput<float>();
      Console.WriteLine("Please input the remitent account");
      Remitent = IO.ReadAccountRemitent(accountsDB);
      if (Remitent == null) return;

      if (Sender.Decrement(ammount)) Remitent.Increment(ammount);
      Console.WriteLine($"You have transfeer {ammount}$ to {Remitent.Owner} succesfully");
    }
    public void Deposit()
    {
      float ammount;
      Account? account;

      Console.WriteLine("Please input your account");
      account = IO.ReadAccountOwner(accountsDB);
      if (account == null) return;
      Console.WriteLine("Please input the ammount to deposit");
      ammount = IO.ReadInput<float>();

      account.Increment(ammount);
      Console.WriteLine("Your account has been updated");
    }
    public void Extract()
    {
      float ammount;
      Account? account;

      Console.WriteLine("Please input your account");
      account = IO.ReadAccountOwner(accountsDB);
      if (account == null) return;
      Console.WriteLine("Please input the ammount to extract");
      ammount = IO.ReadInput<float>();

      
      if(!account.Decrement(ammount)) return;
      Console.WriteLine("Your account has been updated");
    }
    public void AccountStatus()
    {
      Account? account;

      Console.WriteLine("Please input your account");
      account = IO.ReadAccountOwner(accountsDB);
      if(account == null) return;

      Console.WriteLine("---------------------------------");
      Console.WriteLine(account);
      Console.WriteLine("---------------------------------");
    }
    public void CreateNewAccount()
    {
      string? username;
      int pin = new Random().Next(1000, 9999);
      float initialAmmount;

      Console.WriteLine("Please input an username");
      username = IO.ReadInput<string>();
      if (accountsDB.Any(a => a.Owner == username))
      {
        Console.WriteLine("This username is currently in use");
        return;
      }
      Console.WriteLine("Please deposit your initial ammount");
      initialAmmount = IO.ReadInput<float>();
      accountsDB.Add(new Account(Count++, username, pin, initialAmmount));

      Console.WriteLine($"This is your pin: {pin}\nYour Account has been created succesfully");
      Console.WriteLine("---------------------------------");
      Console.WriteLine(accountsDB.Find(a => a.Owner == username));
      Console.WriteLine("---------------------------------");

    }
  }
  public static class Program
  {
    public static void Main(String[] args)
    {
      ATM aTM = new ATM();

      string[] opciones = ["Close", "Deposit", "Extract", "Transfeer", "Account Status", "Create New Account"];
      Action[] Functions = [() => { }, aTM.Deposit, aTM.Extract, aTM.Transfeer, aTM.AccountStatus, aTM.CreateNewAccount];
      int opcion = -1;
      do
      {
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Welcome to the ATM, please select an action to do:");
        for (int i = 0; i < opciones.Length; i++)
          Console.WriteLine("{0} => {1}", i, opciones[i]);
        Console.WriteLine("---------------------------------");
        opcion = IO.ReadIntInRange(0, opciones.Length - 1);
        Functions[opcion].Invoke();
      } while (opcion != 0);

    }
  }
}


