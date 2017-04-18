using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation.Provider;
using System.Text;

namespace PSMTPProvider
{

    public class MTPFileContentReader : IContentReader
    {
        private StreamReader sr;

        public MTPFileContentReader(string path)
        {
            sr = File.OpenText(path);
        }

        public MTPFileContentReader(string path, Encoding encoding)
        {
            sr = new StreamReader(path, encoding);
        }

        ~MTPFileContentReader()
        {
            Dispose();
        }

        public void Close()
        {
            if (sr != null)
            {
                sr.Close();
                sr = null;
            }
        }
        public IList Read(long readCount)
        {
            var lines = new List<string>();

            for (var i = 0; i < readCount; i++)
            {
                var line = sr.ReadLine();

                if (line == null) break;

                lines.Add(line);
            }
            return lines;
        }
        public void Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Close();

            GC.SuppressFinalize(this);
        }
    }
}
