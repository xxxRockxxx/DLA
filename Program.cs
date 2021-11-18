using System;
using System.Threading;

namespace DLA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!\nPlease, write the number of living cells:");
            int numberOfivingCells = int.Parse(Console.ReadLine());
            WorkingWithMatrix start = new WorkingWithMatrix();
            start.GenerationMatrix(numberOfivingCells);
            Output show = new Output();
            bool work = show.Work;
            while (work)
            {
                start.SpawnParticle();
                show.DrawMatrix();
                start.CheckNeighbors();
                int numberParticle = start.CheckNumberParticle;
                show.DrawMatrix();
                work = show.Work;
                while (numberParticle!=0)
                {
                    start.ParticleMovement();
                    show.DrawMatrix();
                    start.CheckNeighbors();
                    show.DrawMatrix();
                    numberParticle = start.CheckNumberParticle;
                }
                work = show.Work;
            }
        }
    }
}
