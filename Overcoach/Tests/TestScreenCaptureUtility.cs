using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Overcoach.Utilities;

namespace Overcoach.Tests
{
    [TestFixture]
    public class TestScreenCaptureUtility
    {

        [Test, Timeout(1000)]
        public void TestWorkingInASpeedyFashion()
        {
            Process p = Process.Start("mspaint");
            p.WaitForInputIdle();

            for (int i = 0; i < 10; i++)
            {
                var bmp = ScreenCapture.GetWindowImage(p);
            }

            p.Kill();
        }

    }
}
