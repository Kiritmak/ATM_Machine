namespace Program
{
  public class IO
  {
    public static T ReadInput<T>() where T : IParsable<T>
    {
      T variable;
      while(!T.TryParse(Console.ReadLine(), null, out variable))
      {
        Console.WriteLine("Incorrect Format");
      }
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
  }
  public class ATM
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


