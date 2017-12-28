using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Logic;
using SdaWpfLib.General;

namespace StresserBudget
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var lCulture = CultureInfo.GetCultureInfo("de-CH");
            Thread.CurrentThread.CurrentCulture = lCulture;
            Thread.CurrentThread.CurrentUICulture = lCulture;

            SelectAllTextBox.EnableSelectAll();

            try
            {
                this.ConnectDatabase();
            }
            catch (Exception lEx)
            {
                MessageBox.Show("Es konnte nicht auf die Datenbank zugegriffen werden: " + lEx.Message);
                return;
            }

            base.OnStartup(e);
        }

        private string GetAbsolutePath(string aPathToCheck)
        {
            if (Path.IsPathRooted(aPathToCheck))
            {
                return aPathToCheck;
            }

            var lPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, aPathToCheck);
            return Path.GetFullPath(lPath);
        }

        private void ConnectDatabase()
        {
            string lConStringKey = "StresserBudgetDb";
            string lConString = ConfigurationManager.ConnectionStrings[lConStringKey].ConnectionString;
            string lDbFileName = Path.Combine(
                    this.GetAbsolutePath(ConfigurationManager.AppSettings["DataDirectory"]),
                    ConfigurationManager.AppSettings["DatabaseFilename"]);

            if (!File.Exists(lDbFileName))
            {
                throw new Exception(string.Format("Datenbank-Datei '{0}' existiert nicht oder ist nicht zugreifbar.", lDbFileName));
            }

            string lDbBackupDir = this.GetAbsolutePath(ConfigurationManager.AppSettings["DatabaseBackupDir"]);
            if (!string.IsNullOrEmpty(lDbBackupDir))
            {
                int lLebensdauer = int.Parse(ConfigurationManager.AppSettings["DatabaseBackupLebenszeit"]);
                this.HandleBackups(lDbFileName, lDbBackupDir, lLebensdauer);
            }

            if (!lConString.EndsWith(";"))
            {
                lConString += ";";
            }

            lConString += "Data Source=" + lDbFileName;

            DataManager.Initialize(lConString);
        }

        private void HandleBackups(string aDbFilePath, string aBackupDirectory, int aLebensdauer)
        {
            // Do Backup now
            string lDbFileName = Path.GetFileName(aDbFilePath);
            string lBakFolderName = DateTime.Today.ToString("yyyyMMdd");
            string lBakFolderPath = Path.Combine(aBackupDirectory, lBakFolderName);

            if (!Directory.Exists(lBakFolderPath))
            {
                Directory.CreateDirectory(lBakFolderPath);
            }

            string lBakFilePath = Path.Combine(lBakFolderPath, lDbFileName);
            int i = 1;
            while (File.Exists(lBakFilePath))
            {
                lBakFilePath = Path.Combine(lBakFolderPath, Path.GetFileNameWithoutExtension(lDbFileName) + "_" + i.ToString() + Path.GetExtension(lDbFileName));
                i++;
            }

            File.Copy(aDbFilePath, lBakFilePath);

            // Kill old backups
            BackgroundWorker lWorker = new BackgroundWorker();

            lWorker.DoWork += delegate (object aS, DoWorkEventArgs aArgs)
            {
                foreach (var lOneSubDir in Directory.GetDirectories(aBackupDirectory))
                {
                    try
                    {
                        var lTimestamp = DateTime.ParseExact(Path.GetFileName(lOneSubDir), "yyyyMMdd", CultureInfo.InvariantCulture);

                        if (lTimestamp.AddDays(aLebensdauer) < DateTime.Today)
                        {
                            Directory.Delete(lOneSubDir, true);
                        }
                    }
                    catch
                    {
                        // Ignore
                    }
                }
            };

            lWorker.RunWorkerAsync();
        }
    }
}
