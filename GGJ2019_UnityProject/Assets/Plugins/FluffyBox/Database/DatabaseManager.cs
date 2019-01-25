using System.Collections;
using System.Collections.Generic;

namespace FluffyBox
{
    public class DatabaseManager : Manager, IDatabaseService
    {
        public static readonly string DataElementsResourcesPath = "DataElements";

        private List<Database> databases = new List<Database>();

        public override void RegisterService()
        {
            Services.AddService<IDatabaseService>(this);
        }

        public override IEnumerator Ignite()
        {
            yield return base.Ignite();
        }

        public override void OnIgnitionCompleted()
        {
            this.RefreshDatabases();
        }

        public IDatabase GetDatabase<T>()
        {
            for (int i = 0; i < this.databases.Count; i++)
            {
                if (databases[i].Type == typeof(T))
                {
                    return this.databases[i] as IDatabase;
                }
            }

            UnityEngine.Debug.LogWarning(string.Format("No Database of type {0} has been found.", typeof(T).ToString()));
            return null;
        }

        public bool TryGetDatabase<T>(out IDatabase database)
        {
            database = this.GetDatabase<T>();

            return database != null;
        }

        private void RefreshDatabases()
        {
            this.databases.Clear();

            // First, we get all Data Elements from resources.
            UnityEngine.Object[] rawDataElements = UnityEngine.Resources.LoadAll<DataElement>(DatabaseManager.DataElementsResourcesPath);

            // For every single Data Element...
            for (int dataElementIndex = 0; dataElementIndex < rawDataElements.Length; dataElementIndex++)
            {                
                // We get its type.
                System.Type dataElementType = rawDataElements[dataElementIndex].GetType();

                // We check if there is already a Database for this type.
                Database database = null;
                for (int databaseIndex = 0; databaseIndex < this.databases.Count; databaseIndex++)
                {
                    if (this.databases[databaseIndex].Type == dataElementType)
                    {
                        database = this.databases[databaseIndex];
                        break;
                    }
                }

                // If not, we create it and add it to the list.
                if (database == null)
                {
                    database = new Database(dataElementType);
                    this.databases.Add(database);
                }

                // Then we add the Data Element to the Database.
                database.AddDataElement(rawDataElements[dataElementIndex] as DataElement);
            }
        }
    }
}