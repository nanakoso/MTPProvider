using System;
using System.IO;
using System.Text;
using System.Management.Automation.Provider;
using System.Collections;

namespace PSMTPProvider
{
    public class MTPFileContentWriter : IContentWriter
    {
        private StreamWriter sw;

        public MTPFileContentWriter(string path, Encoding encoding)
        {
            sw = new StreamWriter(path, true, encoding);
        }

        ~MTPFileContentWriter()
        {
            Dispose();
        }

        public void Close()
        {
            if (sw != null)
            {
                sw.Close();
                sw = null;
            }
        }
        public void Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }
        public IList Write(IList content)
        {
            foreach (var c in content)
            {
                sw.WriteLine(c.ToString());
            }
            return null;
        }

        public void Dispose()
        {
            Close();

            GC.SuppressFinalize(this);
        }

        public void ClearContent(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }

    }

}