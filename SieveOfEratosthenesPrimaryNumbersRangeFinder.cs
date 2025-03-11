using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace PrimaryNumbers
{
    public class SieveOfEratosthenesPrimaryNumbersRangeFinder : IPrimaryNumbersRangeFinder
    {
        public event Action<BigInteger> NextPrimaryNumberWasFound;

        public IEnumerable<BigInteger> GetPrimaryNumbersInRange(BigInteger start, BigInteger end)
        {
            string path= Directory.GetCurrentDirectory();
            string fileName = "SieveOfErathosthenesCache.txt";
            string fullPath= Path.Combine(path, fileName);
            // Get the boolean array representing prime numbers
            // up to n


            Dictionary<BigInteger, bool> isPrime;
            if (!File.Exists(fullPath)) { 
                File.Create(fullPath);
            }
            string serializedObj;
            using (StreamReader file = File.OpenText(fullPath))
            {
                file.ReadLine();
                serializedObj=file.ReadToEnd();
               
            }
            if (string.IsNullOrWhiteSpace(serializedObj))
            {
                isPrime = sieveOfEratosthenes(end);
            }
            else
            {
                isPrime=JsonConvert.DeserializeObject<Dictionary<BigInteger,bool>>(serializedObj);
            }
                    
            //sieveOfEratosthenes(end);
            List<BigInteger> result = new List<BigInteger>();

            // Count the number of primes in the range [m, n]
            int count = 0;
            foreach (var pair in isPrime)
            {
                if (pair.Value && pair.Key >= start) {
                    if (NextPrimaryNumberWasFound != null)
                    {
                        NextPrimaryNumberWasFound.Invoke(pair.Key);
                    }
                    result.Add(pair.Key);
                }
            }
            
           
            int cachedCountOfPrimaryNumbers = 0;
            try
            {
                cachedCountOfPrimaryNumbers=int.Parse(File.ReadLines(fullPath).First());
            }
            catch (Exception ex)
            {
                
            }

            if(cachedCountOfPrimaryNumbers < isPrime.Count())
            {
                using (StreamWriter file = File.CreateText(fullPath)){
                
                    string content = JsonConvert.SerializeObject(isPrime);
                    file.WriteLine(isPrime.Count);
                    file.WriteLine(content);
                }
            }
           
            return result;
        }

        private Dictionary<BigInteger,bool> sieveOfEratosthenes(BigInteger n)
        {

            // Create a boolean array "prime[0..n]"
            // and initialize all entries as true.
            // A value in prime[i] will be false
            // if 'i' is not prime, otherwise true.
            Dictionary<BigInteger,bool> prime = new();
            for (BigInteger i = 0; i <= n; ++i)
                prime[i]= true;

            // Mark 0 and 1 as non-prime
            prime[0] = false;
            prime[1] = false;

            // Loop through numbers from 2 to sqrt(n)
            // to mark their multiples as non-prime
            for (BigInteger p = 2; p * p <= n; p++)
            {

                // If prime[p] is still true, it means 'p' is
                // prime
                if (prime[p])
                {

                    // Mark all multiples of p greater
                    // than or equal to p^2 as non-prime
                    // Numbers less than p^2 would
                    // have already been marked as non-prime
                    for (BigInteger i = p * p; i <= n; i += p)
                        prime[i] = false;
                    
                }
            }

            return prime;
        }
    }
}
