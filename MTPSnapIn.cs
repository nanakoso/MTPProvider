using System.Management.Automation;
using System.ComponentModel;



namespace PSMTPProvider
{
    [RunInstaller(true)]
    public class MTPSnapIn : PSSnapIn
    {
        public override string Description
        {
            get { return "MTPアクセス用プロバイダです"; }
        }
        public override string Name
        {
            get { return "MTPProvider"; }
        }
        public override string Vendor
        {
            get { return "turner"; }
        }
    }

}
