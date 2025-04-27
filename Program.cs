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
    public static IAccount? ReadAccount(List<Account> AccountsDB)
    {
      string Owner;
      int pin;
      IAccount? account;
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
  }
  public static class FileSaves
  {
    public static List<IAccount> GetAccountsDB()
    {
      List<IAccount> accounts = new List<IAccount>()
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
    }

    public List<IAccount> accountsDB;

    public static void Transfeer()
    {
      IAccount Sender, Remitent;
      float ammount;
      if (Sender.Decrement(ammount)) Remitent.Increment(ammount);
    }
    public static void Deposit()
    {
      float ammount;
      IAccount account;
      account = 
      account.Increment(ammount);
      Console.WriteLine("Your account has been updated");
    }

    public static bool Extract()
    {
      float ammount;
      IAccount account;
      if (account.Decrement(ammount)) Console.WriteLine("Your account has been updated");
    }
  }
  public static class Program
  {
    public static void Main(String[] args)
    {
      string[] opciones = ["Close", "Deposit", "Extract", "Transfeer"];
      int opcion = -1;
      do
      {
        Console.WriteLine("Welcome to the ATM, please select an action to do:");
        for (int i = 0; i < opciones.Length; i++)
          Console.WriteLine("{0} => {1}", i, opciones[i]);
        opcion = IO.ReadIntInRange(0, opciones.Length - 1);
      } while (opcion != 0);

    }
  }
}


