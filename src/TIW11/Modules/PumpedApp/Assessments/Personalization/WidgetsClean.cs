﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace ThisIsWin11.PumpedApp.Assessment.Personalization
{
    internal class WidgetsClean : AssessmentBase
    {
        private static readonly ErrorHelper logger = ErrorHelper.Instance;

        public override string ID()
        {
            return "Uninstall Widgets";
        }

        public override string Info()
        {
            return "The Widgets app runs in the background even with the option turned off.\nThis will uninstall the Widgets app package.";
        }

        public override bool CheckAssessment()
        {
            var appPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Packages", "MicrosoftWindows.Client.WebExperience_cw5n1h2txyewy");

            if (!Directory.Exists(appPath))
            {
                return false;
            }

            return true;
        }

        public override bool DoAssessment()
        {
            try
            {
                using (var client = new WebClient())
                {
                    //patching
                    logger.Log("- Uninstalling Widgets app");
                    WindowsHelper.RunCmd("/c winget uninstall \"windows web experience pack\" ");

                    return true;
                }
            }
            catch (Exception ex)
            { logger.Log("{0}", ex.Message); }

            return false;
        }

        public override bool UndoAssessment()
        {
            try
            {
                using (var client = new WebClient())
                {
                    //patching
                    logger.Log("- We've opened the download link the Microsoft Store.\n\nPlease download the package, called \"Windows Web Experience Pack\". ");
                    Process.Start("ms-windows-store://pdp/?ProductId=9MSSGKG348SP");

                    return true;
                }
            }
            catch (Exception ex)
            { logger.Log("{0}", ex.Message); }

            return false;
        }
    }
}