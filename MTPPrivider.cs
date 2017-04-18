using Microsoft.PowerShell.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;

namespace PSMTPProvider
{
    [CmdletProvider("MTPProvider", ProviderCapabilities.ShouldProcess)]
    public class MTPPrivider : NavigationCmdletProvider, IContentCmdletProvider
    {
        protected override bool IsValidPath(string path)
        {
            return true;
        }

        protected override bool ItemExists(string path)
        {
            return true;
        }

        protected override bool IsItemContainer(string path)
        {
            return true;
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            foreach (var f in Directory.GetFiles(path))
            {
                var fi = new FileInfo(f);
                base.WriteItemObject(new MTPFile
                {
                    FileName = fi.Name,
                    Size = fi.Length
                }, f, false);
            }
        }

        protected override void NewItem(string path, string itemTypeName, object newItemValue)
        {
            using (var fs = File.Create(path))
            {
                base.WriteItemObject(new MTPFile
                {
                    FileName = Path.GetFileName(path),
                    Size = fs.Length

                }, path, false);
            }
        }

        protected override bool HasChildItems(string path)
        {
            return false;
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            File.Delete(path);
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            foreach (var f in Directory.GetFiles(path))
            {
                base.WriteItemObject(Path.GetFileName(f), f, false);
            }
        }

        public IContentReader GetContentReader(string path)
        {
            var gcParams = base.DynamicParameters as GetContentParameters;
            var encoding = gcParams != null ? gcParams.EncodingType : Encoding.UTF8;

            return new MTPFileContentReader(path);
        }

        public object GetContentReaderDynamicParameters(string path)
        {
            return new GetContentParameters();
        }

        public IContentWriter GetContentWriter(string path)
        {
            var scParams = base.DynamicParameters as SetContentParameters;
            var encoding = scParams != null ? scParams.EncodingType : Encoding.UTF8;

            return new MTPFileContentWriter(path, encoding);
        }

        public object GetContentWriterDynamicParameters(string path)
        {
            return new SetContentParameters();
        }

        public void ClearContent(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }

        public object ClearContentDynamicParameters(string path)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    [DebuggerStepThrough]
    public class GetContentParameters : FileSystemContentDynamicParametersBase
    {

    }

    [Serializable]
    [DebuggerStepThrough]
    public class SetContentParameters : FileSystemContentWriterDynamicParameters
    {
    }
}
