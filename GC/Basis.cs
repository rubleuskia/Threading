namespace GC
{
    public class Basis
    {
        class Country
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public static void Run()
        {
            Country country = new Country();
            country.X = 10;
            country.Y = 15;
        }

        public static void GcMethods()
        {
            Country country = new Country();

            System.GC.AddMemoryPressure(10000000);
            System.GC.RemoveMemoryPressure(10000000);
            System.GC.Collect();
            System.GC.Collect(generation: 2);
            var result = System.GC.GetGeneration(country);
            var totalMemory = System.GC.GetTotalMemory(forceFullCollection: false);
            System.GC.WaitForPendingFinalizers();
        }
    }
}
