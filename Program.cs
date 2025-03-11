using PrimaryNumbers;
using System.Diagnostics;
using System.Numerics;



IBigIntegerSqrtFinder bigIntegerSqrtFinder = new SteinerSqrtBigIntegerFinder();
TrialPrimaryNumbersRangeFinder trialPrimaryNumbersRangeFinder = new TrialPrimaryNumbersRangeFinder(bigIntegerSqrtFinder);

SieveOfEratosthenesPrimaryNumbersRangeFinder sieveOfEratosthenesPrimaryNumbersRangeFinder = new();

Stopwatch stopwatch = Stopwatch.StartNew();
BigInteger start = BigInteger.Parse("1000");
BigInteger end = BigInteger.Parse("289000");

var action=(BigInteger i) => { Console.WriteLine("Prime number: " + i.ToString()); };
trialPrimaryNumbersRangeFinder.NextPrimaryNumberWasFound += action;
//sieveOfEratosthenesPrimaryNumbersRangeFinder.NextPrimaryNumberWasFound += action;

//IEnumerable<BigInteger> trialPrimaryNumbers = trialPrimaryNumbersRangeFinder.GetPrimaryNumbersInRange(start, end);
IEnumerable<BigInteger> trialPrimaryNumbers = sieveOfEratosthenesPrimaryNumbersRangeFinder.GetPrimaryNumbersInRange(start, end);

trialPrimaryNumbersRangeFinder.NextPrimaryNumberWasFound-=action;
sieveOfEratosthenesPrimaryNumbersRangeFinder.NextPrimaryNumberWasFound-=action;
Console.WriteLine("*************************************");
Console.WriteLine(trialPrimaryNumbers.Count());
Console.WriteLine("*************************************");
stopwatch.Stop();

Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString()+" ms ");
Console.WriteLine((GC.GetAllocatedBytesForCurrentThread()/1024).ToString()+" kb");