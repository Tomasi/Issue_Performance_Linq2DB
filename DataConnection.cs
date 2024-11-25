using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using System.Collections.Generic;
using System.Linq;

namespace Example_Update_Issue
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class MySettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "PostgreSQL";
        public string DefaultDataProvider => "PostgreSQL";        

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "Database",
                        ProviderName = ProviderName.PostgreSQL,
                        ConnectionString = ""
                    };
            }
        }
    }


    public class DB : DataConnection
    {
        public static DB CreateDataConnection(ILinqToDBSettings settings) => new DB(new MySettings());

        public ITable<POCO> Pocos => this.GetTable<POCO>();        

        private DB(MySettings settings) : base("PostgreSQL", "")
        {
            DefaultSettings = settings;
        }        
    }
}
