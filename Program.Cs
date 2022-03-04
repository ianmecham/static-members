using System; 

public class Program 
{
  public static void Main(string[] args) 
  {
    //Count1000();
    //PrintStars();
    //NumStars();
    GuessingGame();
    //UserStars();
  }
  public static void UserStars()
  {
    int number;
    int guess = Convert.ToInt32(Console.ReadLine());
    string stars = "*";
    number = 1;
    while (number <= guess)
    {
      stars = stars + "*";
      if (guess != 0)
      {
        number = number + 1;
      }
      if (number == guess)
      {
        Console.WriteLine(stars);
      }
    }
  }
  public static void GuessingGame()
  {
    int number;
    number = 1;
    while (number <= 10)
    {
      Console.WriteLine("Guess a number between 1 and 10");
      int guess = Convert.ToInt32(Console.ReadLine());
      Random generator = new Random();
      int randomNumber = generator.Next(1, 10);
      //Console.WriteLine(randomNumber);
      if (guess == randomNumber)
      {
        Console.WriteLine("Correct");
        number = number + 10;
      }
      if (guess != randomNumber)
      {
        Console.WriteLine("Wrong, Try again");
        Console.WriteLine();
        Console.WriteLine();
      }
    }
  }
  public static void NumStars()
  {
    int number;
    string stars;
    stars = "*";
    number = 1;
    while (number <= 10)
    {
      Console.WriteLine(number);
      number = number + 1;
      Console.WriteLine(stars);
      stars = stars + "*";
    }
  }
  public static void Count1000()
  {
    int number;
    number = 1;
    while (number <= 1000)
    {
      Console.WriteLine(number);
      number = number + 1;
    }
  }
  public static void PrintStars()
  {
    int number;
    number = 1;
    while (number <=5)
    {
      Console.WriteLine("*");
      number = number + 1;
    }
  }
}