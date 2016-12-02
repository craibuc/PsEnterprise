using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Management.Automation.Provider;
using System.Management.Automation;

using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace PsEnterprise
{
    [CmdletProvider("Enterprise", ProviderCapabilities.None)]
    public class EnterpriseProvider : NavigationCmdletProvider
    {
        // default constructor
        //public EnterpriseProvider()
        //{
        //    Debug.WriteLine("EnterpriseProvider()");
        //}

        protected override bool IsValidPath(string path)
        {
            
            // the root folder itself
            // SERVER:\InfoObjects\Root Folder
            
            // all items in the root folder
            // SERVER:\InfoObjects\Root Folder

            Debug.WriteLine(String.Format("IsValidPath({0})", path));

            string pattern = @"([a-zA-Z]+:)?(\\[a-zA-Z0-9_.-: :]+)*\\?";
            var regex = new Regex(pattern);
            return regex.IsMatch(path);

        }

        protected override System.Management.Automation.PSDriveInfo NewDrive(System.Management.Automation.PSDriveInfo drive)
        {
            Debug.WriteLine("NewDrive()");

            // Check if the drive object is null.
            if (drive == null)
            {
                WriteError(new ErrorRecord(
                           new ArgumentNullException("drive"),
                           "NullDrive",
                           ErrorCategory.InvalidArgument,
                           null));

                return null;
            }

            EnterpriseDriveInfo driveInfo = new EnterpriseDriveInfo(drive);

            //driveInfo.Session.Logon();

            return driveInfo;

        }

        protected override System.Management.Automation.PSDriveInfo RemoveDrive(System.Management.Automation.PSDriveInfo drive)
        {
            // Check if drive object is null.
            if (drive == null)
            {
                WriteError(new ErrorRecord(
                           new ArgumentNullException("drive"),
                           "NullDrive",
                           ErrorCategory.InvalidArgument,
                           drive));

                return null;
            }

            // Close the ODBC connection to the drive.
            EnterpriseDriveInfo driveInfo = drive as EnterpriseDriveInfo;

            if (driveInfo == null)
            {
                return null;
            }

            //driveInfo.Session.Logoff();
            driveInfo.Session = null;

            return driveInfo;

        }

        //protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        //{
        //    PSDriveInfo drive = new PSDriveInfo("MyDrive", this.ProviderInfo, "", "", null);
        //    Collection<PSDriveInfo> drives = new Collection<PSDriveInfo>() { drive };
        //    return drives;
        //}

        protected override bool ItemExists(string path)
        {
            Debug.WriteLine(String.Format("ItemExists({0})",path));
            return true;
        }

        protected override void SetItem(string path, object value)
        {
            Debug.WriteLine(String.Format("SetItem({0},{1})",path,value));
            base.SetItem(path, value);
        }

        protected override bool IsItemContainer(string path)
        {
            Debug.WriteLine(String.Format("IsItemContainer({0})", path));

            var regex = new Regex(@"([a-zA-Z]:)?(\\\\[a-zA-Z0-9_.-]+)+\\\\?");
            return regex.IsMatch(path);

        }

        //protected override void GetChildItems(string path, bool recurse)
        //{
        //    WriteItemObject("Hello", "Hello", true);
        //}

    } // class

} // namespace
