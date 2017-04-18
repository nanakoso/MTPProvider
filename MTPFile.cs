using System;
using System.Diagnostics;

namespace PSMTPProvider
{
    [Serializable]
    [DebuggerStepThrough]
    public class MTPFile
    {
        public string FileName
        {
            get;
            set;
        }
        public long Size
        {
            get;
            set;
        }
    }
}