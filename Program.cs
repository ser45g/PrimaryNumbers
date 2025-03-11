using PrimaryNumbers;
using System.Diagnostics;
using System.Numerics;

IPrimaryNumbersRangeFinder primaryNumbersRangeFinder;

var action = (BigInteger i) => { Console.WriteLine("Prime number: " + i.ToString()); };

while (ConsoleKey.Enter!=Console.ReadKey().Key)
{
    Console.WriteLine("Enter the start value:");
    string? startStr=Console.ReadLine();

    Console.WriteLine("Enter the end value:");
    string? endStr=Console.ReadLine();

    Console.WriteLine("Enter the algorithm you want to use (s for theh sieve of eraosthenes, t for the trial based one):");
    char algorithmType=Console.ReadKey().KeyChar;

    if(algorithmType=='s' || algorithmType=='S')
    {
         primaryNumbersRangeFinder = new SieveOfEratosthenesPrimaryNumbersRangeFinder();
        

    }
    else
    {
        IBigIntegerSqrtFinder bigIntegerSqrtFinder = new SteinerSqrtBigIntegerFinder();
        primaryNumbersRangeFinder = new TrialPrimaryNumbersRangeFinder(bigIntegerSqrtFinder);
    }
    primaryNumbersRangeFinder.NextPrimaryNumberWasFound += action;
    BigInteger start = BigInteger.Parse(startStr);
    BigInteger end = BigInteger.Parse(endStr);

    Stopwatch stopwatch = Stopwatch.StartNew();
    var primaryNumbers=primaryNumbersRangeFinder.GetPrimaryNumbersInRange(start, end);
    stopwatch.Stop();

    Console.WriteLine("*************************************");
    Console.WriteLine(primaryNumbers.Count());
    Console.WriteLine("*************************************");

    Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString() + " ms ");
    Console.WriteLine((GC.GetAllocatedBytesForCurrentThread() / 1024).ToString() + " kb");

    primaryNumbersRangeFinder.NextPrimaryNumberWasFound -= action;

}

