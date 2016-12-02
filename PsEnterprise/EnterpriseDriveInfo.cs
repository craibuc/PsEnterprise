using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Management.Automation;

using System.Diagnostics;

namespace PsEnterprise
{
    public class EnterpriseDriveInfo : PSDriveInfo
    {

#region "Properties"

        public Object Session
        {
            get { return session; }
            set { session = value; }
        }
        private Object session;

#endregion

        // default constructor
        public EnterpriseDriveInfo(PSDriveInfo driveInfo)
            : base(driveInfo)
        {
            Debug.WriteLine("EnterpriseDriveInfo(PSDriveInfo)");
            this.session = Guid.NewGuid();
        }

    }

}
