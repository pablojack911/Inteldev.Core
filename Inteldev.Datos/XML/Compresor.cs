using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.IO.Compression;

namespace Inteldev.Datos.XML
{
    class Compresor
    {
        public static byte[] ZipDataset(DataSet ds, bool bIgnoreSchema)
        {
            byte[] bb = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress))
                {
                    ds.WriteXml(zip, bIgnoreSchema ? System.Data.XmlWriteMode.IgnoreSchema : XmlWriteMode.WriteSchema);
                    zip.Close();
                }
                bb = ms.GetBuffer();
                ms.Close();
            }

            return bb;
        }
        public static DataSet UnzipDataset(byte[] b)
        {
            DataSet dataSet = new DataSet();
            using (MemoryStream ms = new MemoryStream(b))
            {
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    dataSet.ReadXml(zip, System.Data.XmlReadMode.Auto);
                    zip.Close();
                }
                ms.Close();
            }

            return dataSet;
        }
    }
}
