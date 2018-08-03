using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Inteldev.Datos.XML
{
    public class XmlDataSetBuilder
    {
        public DataSet dataSet;

        public XmlDataSetBuilder()
        {
            dataSet = new DataSet();
        }

        public XmlDataSetBuilder(DataSet pDataSet)
        {
            dataSet = pDataSet;
        }

        public void LoadDataReader(IDataReader pDataReader)
        {
            DataTable DT = new System.Data.DataTable();
            DT.Load(pDataReader);
            dataSet.Tables.Add(DT);
        }

        public string GetXml()
        {
            return dataSet.GetXml();
        }
        public string GetSchema()
        {
            return dataSet.GetXmlSchema();
        }
        public byte[] GetXmlComprimido()
        {
            return Compresor.ZipDataset(this.dataSet,false);
        }
    }
}
