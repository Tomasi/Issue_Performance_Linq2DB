using System.Linq;
using LinqToDB;

namespace Example_Update_Issue
{
    public class Program
    {
        public static void Main() => DefaultVersion.Initialize();        

        private class DefaultVersion
        {
            public static void Initialize()
            {            
                var dataConnection = DB.CreateDataConnection(new MySettings());
                dataConnection.Options.LinqOptions.WithDisableQueryCache(true);

                var exists = dataConnection.Pocos?.Any();

                if (!exists.GetValueOrDefault())
                {
                    for (int i = 1; i <= 5000; i++)
                    {
                        var table = new POCO() { Field1 = i };
                        dataConnection.Insert(table);
                    }
                }
                
                foreach (var item in dataConnection.Pocos.ToList()) 
                    dataConnection.Update(item); // Performance Issue
            }
        }
    }
}