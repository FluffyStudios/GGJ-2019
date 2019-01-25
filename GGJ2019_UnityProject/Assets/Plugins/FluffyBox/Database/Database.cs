using System.Collections.Generic;

namespace FluffyBox
{
    public class Database
    {
        public Database(System.Type type)
        {
            this.DataElements = new List<DataElement>();
            this.Type = type;
        }

        public List<DataElement> DataElements
        {
            get;
            set;
        }

        public System.Type Type
        {
            get;
            set;
        }

        public void AddDataElement(DataElement dataElement)
        {
            if (this.DataElements == null)
            {
                throw new System.NullReferenceException("The Database has not been correctly initialized");
            }

            if (dataElement == null)
            {
                throw new System.NullReferenceException("The given Data Element is null");
            }

            this.DataElements.Add(dataElement);
        }
    }
}
