using System;
using System.Threading;
/// <summary>
///  reset 将事件状态设置为非终止状态，导致线程阻止。 
/// set  将事件状态设置为终止状态，允许一个或多个等待线程继续。
/// 
/// 
/// </summary>
class CalculateTest
{
    static void Main()
    {
        
        Calculate calc = new Calculate();
        Console.WriteLine("Result = {0}.",
            calc.Result(234).ToString());
        Console.WriteLine("Result = {0}.",
            calc.Result(55).ToString());
    }
}

class Calculate
{
    double baseNumber, firstTerm, secondTerm, thirdTerm;
    AutoResetEvent[] autoEvents;
    ManualResetEvent manualEvent;

    // Generate random numbers to simulate the actual calculations.
    Random randomGenerator;

    public Calculate()
    {
        autoEvents = new AutoResetEvent[]
        {
            new AutoResetEvent(false),
            new AutoResetEvent(false),
            new AutoResetEvent(false)
        };

        manualEvent = new ManualResetEvent(false);
    }

    void CalculateBase(object stateInfo)
    {
        baseNumber = randomGenerator.NextDouble();

        // Signal that baseNumber is ready.
        manualEvent.Set();
    }

    // The following CalculateX methods all perform the same
    // series of steps as commented in CalculateFirstTerm.

    void CalculateFirstTerm(object stateInfo)
    {
        // Perform a precalculation.
        double preCalc = randomGenerator.NextDouble();

        // Wait for baseNumber to be calculated.
        manualEvent.WaitOne();

        // Calculate the first term from preCalc and baseNumber.
        firstTerm = preCalc * baseNumber *
            randomGenerator.NextDouble();

        // Signal that the calculation is finished.
        autoEvents[0].Set();
    }

    void CalculateSecondTerm(object stateInfo)
    {
        double preCalc = randomGenerator.NextDouble();
        manualEvent.WaitOne();
        secondTerm = preCalc * baseNumber *
            randomGenerator.NextDouble();
        autoEvents[1].Set();
    }

    void CalculateThirdTerm(object stateInfo)
    {
        double preCalc = randomGenerator.NextDouble();
        manualEvent.WaitOne();
        thirdTerm = preCalc * baseNumber *
            randomGenerator.NextDouble();
        autoEvents[2].Set();
    }

    public double Result(int seed)
    {
        randomGenerator = new Random(seed);

        // Simultaneously calculate the terms.
        ThreadPool.QueueUserWorkItem(
            new WaitCallback(CalculateBase));
        ThreadPool.QueueUserWorkItem(
            new WaitCallback(CalculateFirstTerm));
        ThreadPool.QueueUserWorkItem(
            new WaitCallback(CalculateSecondTerm));
        ThreadPool.QueueUserWorkItem(
            new WaitCallback(CalculateThirdTerm));

        // Wait for all of the terms to be calculated.
        WaitHandle.WaitAll(autoEvents);

        // Reset the wait handle for the next calculation.
        manualEvent.Reset();

        return firstTerm + secondTerm + thirdTerm;
    }
}
