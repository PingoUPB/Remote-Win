using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinRemote.App.Models
{
    class VersionInfo
    {
        public double VersionNumber { get; set; }

        public VersionInfo(double versionNumber)
        {
            VersionNumber = versionNumber;
        }
    }
}
