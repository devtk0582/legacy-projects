using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLib.NetworkFolder
{
    public class NetworkFolderAccessor
    {
        public IEnumerable<FileInfo> DisplayFiles(string path, string username, string password)
        {
            string error = PinvokeWindowsNetworking.connectToRemote(path, username, password);

            if (!Directory.Exists(path))
            {
                return null;
            }

            DirectoryInfo di = new DirectoryInfo(path);
            var fileList = di.GetFiles()?.ToList();

            PinvokeWindowsNetworking.disconnectRemote(path);

            return fileList;
        }

        public bool CopyFile(string localPath, string remotePath, string fileName, string username, string password)
        {
            string error = PinvokeWindowsNetworking.connectToRemote(remotePath, username, password);

            if (!Directory.Exists(localPath) || !Directory.Exists(remotePath))
            {
                return false;
            }

            File.Copy(Path.Combine(localPath, fileName), Path.Combine(remotePath, fileName));

            PinvokeWindowsNetworking.disconnectRemote(remotePath);

            return true;
        }

        public bool CopyFileByUser(string localPath, string remotePath, string fileName, string username, string password, string impersonatedUsername, string impersonatedDomain, string impersonatedPassword)
        {
            UserImpersonation impersonator = new UserImpersonation();

            bool result = impersonator.impersonateUser(impersonatedUsername, impersonatedDomain, impersonatedPassword);

            if (!result)
            {
                return false;
            }

            string error = PinvokeWindowsNetworking.connectToRemote(remotePath, username, password);

            if (!Directory.Exists(localPath) || !Directory.Exists(remotePath))
            {
                return false;
            }

            File.Copy(Path.Combine(localPath, fileName), Path.Combine(remotePath, fileName));

            PinvokeWindowsNetworking.disconnectRemote(remotePath);

            impersonator.undoimpersonateUser();

            return true;
        }
    }
}
