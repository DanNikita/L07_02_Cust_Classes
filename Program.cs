using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

public static class Tarifs
{ 
   // Static class and methods cause class do not have specific example.
     public static double Tarif1CallCalc(char callType, int minutes)
     {
        Console.WriteLine("First Rate calc used");
        if (callType == 'Г')
            return minutes * 5;
        else
            if (callType == 'М')
            return minutes * 1;
        return 0;
     }
    public static double Tarif2CallCalc(char callType, int minutes)
    {
        Console.WriteLine("Second Rate calc used");
        if (callType == 'Г')
        {
            if (minutes > 10)
                return minutes * 2.5;
            else
                return minutes * 5;
        }
        else
            if (callType == 'М')
            return minutes * 1;
        return 0;
    }
    public static double Tarif3CallCalc(char callType, int minutes)
    {
        Console.WriteLine("Third Rate calc used");
        if (minutes < 5)
            return Tarif1CallCalc(callType,minutes)/2;
        else
            return Tarif1CallCalc(callType, minutes)*2;
    }
}
class Call
{
    public char callType { get; set; }
    public int minutes { get; set; }
    public Customer cust {  get; set; }

    private static int counter;
    public Call(char callType, int minutes, Customer cust)
    {
        this.cust = cust;
        this.callType = callType;
        this.minutes = minutes;
        counter++;
        RecordCall(callType,minutes);
        Console.WriteLine("Call detected! Call No.:{0}, Customer:{1}, Min:{2}", counter,cust.Name,minutes);
    }

    public void RecordCall(char callType, int minutes)
    {
        
        switch (cust.Rate)
        {
            case "Time Rate": cust.SetBalance(Tarifs.Tarif1CallCalc(callType, minutes)); break;
            case "Half money after 10min Rate": cust.SetBalance(Tarifs.Tarif2CallCalc(callType, minutes)); break;
            case "Less money before 5min Rate": cust.SetBalance(Tarifs.Tarif3CallCalc(callType, minutes)); break;

                //if (callType == 'Г')
                //    Balance -= minutes * 5;
                //else
                //    if (callType == 'М')
                //    Balance -= minutes * 1;
        }
    }
}


class Customer
{
    public string Name { get; set; }
    public double Balance { get; private set; }
    public string Rate { get; set; }

    public static string tar1 = "Time Rate";
    public static string tar2 = "Half money after 10min Rate";
    public static string tar3 = "Less money before 5min Rate";

    public Customer(string name, double balance = 100, int tar = 1) //why cant use just int tar???
    {
        Name = name;
        Balance = balance;
        switch (tar)
        {
            case 1: Rate = tar1; break;
            case 2: Rate = tar2; break;
            case 3: Rate = tar3; break;
            default:
                {
                    Rate = tar1;
                    Console.WriteLine("Invalid rate type, switching to default"); break;
                }
        }

    }
    public double SetBalance(double balance) => Balance = balance;


    public override string ToString()
    {
        return string.Format("Client: {0} with Rate {1} has credits: {2}", Name, Rate, Balance);
    }

    public void RecordPayment(double amountPaid)
    {
        if (amountPaid > 0)
            Balance += amountPaid;
    }
}


    
internal class Program
{
    static void Main(string[] args)
    {
        Customer Ivan = new Customer("Ivan Petrov", 500,1);
        Console.WriteLine(Ivan);
        Customer Elena = new Customer("Elena Ivanova", 100,2);
        Console.WriteLine(Elena);
        Call c1 = new Call('М', 12, Ivan);
        Call c2 = new Call('Г', 3, Elena);
        //Ivan.RecordCall('Г', 12);
        //Elena.RecordCall('М', 25);
        Console.WriteLine(Ivan);
        Console.WriteLine(Elena);

    }
}