using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryNumbers
{
    public class TrialPrimaryNumbersRangeFinder : IPrimaryNumbersRangeFinder
    {
        private IBigIntegerSqrtFinder _bigIntegerSqrtFinder;
        public event Action<BigInteger> NextPrimaryNumberWasFound=null;

        public TrialPrimaryNumbersRangeFinder(IBigIntegerSqrtFinder bigIntegerSqrtFinder)
        {
           // _nextPrimaryNumberWasFound += nextPrimaryNumberFoundWasFoundHandler;
            _bigIntegerSqrtFinder = bigIntegerSqrtFinder;
        }

     
        public IEnumerable<BigInteger> GetPrimaryNumbersInRange(BigInteger start, BigInteger end)
        {
            if (start <= 1)
                throw new ArgumentException("The start of the range must be greater than 1");
            List<BigInteger> result = new List<BigInteger>();
            for (var i =start; i <= end; ++i)
            {
                bool isPrime = true; // Move initialization to here
                for (var j = BigInteger.Parse("2"); j < i; j++) // you actually only need to check up to sqrt(i)
                {
                    if (i % j == 0) // you don't need the first condition
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    //Console.WriteLine("Prime:" + i);
                    if(NextPrimaryNumberWasFound != null)
                    {
                        NextPrimaryNumberWasFound.Invoke(i);
                    }
                    
                    result.Add(i);
                }
                
            }
            return result;
        }

      
    }
}
