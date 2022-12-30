using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using MelissaData;

namespace MelissaDataEmailObjectLinuxNETSample
{
  class Program
  {
    static void Main(string[] args)
    {
      // Variables
      string license = "";
      string testEmail = "";
      string dataPath = "";

      ParseArguments(ref license, ref testEmail, ref dataPath, args);
      RunAsConsole(license, testEmail, dataPath);
    }

    static void ParseArguments(ref string license, ref string testEmail, ref string dataPath, string[] args)
    {
      for (int i = 0; i < args.Length; i++)
      {
        if (args[i].Equals("--license") || args[i].Equals("-l"))
        {
          if (args[i + 1] != null)
          {
            license = args[i + 1];
          }
        }
        if (args[i].Equals("--email") || args[i].Equals("-e"))
        {
          if (args[i + 1] != null)
          {
            testEmail = args[i + 1];
          }
        }
        if (args[i].Equals("--dataPath") || args[i].Equals("-d"))
        {
          if (args[i + 1] != null)
          {
            dataPath = args[i + 1];
          }
        }
      }
    }

    static void RunAsConsole(string license, string testEmail, string dataPath)
    {
      Console.WriteLine("\n\n========== WELCOME TO MELISSA DATA EMAIL LINUX NET SAMPLE ==========\n");

      EmailObject emailObject = new EmailObject(license, dataPath);

      bool shouldContinueRunning = true;

      if (emailObject.mdEmailObj.GetInitializeErrorString() != "No error.")
      {
        shouldContinueRunning = false;
      }

      while (shouldContinueRunning)
      {
        DataContainer dataContainer = new DataContainer();

        if (string.IsNullOrEmpty(testEmail))
        {
          Console.WriteLine("\nFill in each value to see the Email Object results");
          Console.WriteLine("Email:");

          Console.CursorTop -= 1;
          Console.CursorLeft = 7;
          dataContainer.Email = Console.ReadLine();
        }
        else
        {
          dataContainer.Email = testEmail;
        }

        // Print user input
        Console.WriteLine("\n============================== INPUTS ==============================\n");
        Console.WriteLine($"\t               Email: {dataContainer.Email}");

        // Execute Email Object
        emailObject.ExecuteObjectAndResultCodes(ref dataContainer);

        // Print output
        Console.WriteLine("\n============================== OUTPUT ==============================\n");
        Console.WriteLine("\n\tEmail Object Information:");

        Console.WriteLine($"\t                    Email: {dataContainer.Email}");
        Console.WriteLine($"\t              MailBoxName: {emailObject.mdEmailObj.GetMailBoxName()}");
        Console.WriteLine($"\t               DomainName: {emailObject.mdEmailObj.GetDomainName()}");
        Console.WriteLine($"\t           TopLevelDomain: {emailObject.mdEmailObj.GetTopLevelDomain()}");
        Console.WriteLine($"\tTopLevelDomainDescription: {emailObject.mdEmailObj.GetTopLevelDomainDescription()}");
        Console.WriteLine($"\t             Result Codes: {dataContainer.ResultCodes}");

        String[] rs = dataContainer.ResultCodes.Split(',');
        foreach (String r in rs)
          Console.WriteLine($"        {r}: {emailObject.mdEmailObj.GetResultCodeDescription(r, mdEmail.ResultCdDescOpt.ResultCodeDescriptionLong)}");

        bool isValid = false;
        if (!string.IsNullOrEmpty(testEmail))
        {
          isValid = true;
          shouldContinueRunning = false;
        }
        while (!isValid)
        {
          Console.WriteLine("\nTest another email? (Y/N)");
          string testAnotherResponse = Console.ReadLine();

          if (!string.IsNullOrEmpty(testAnotherResponse))
          {
            testAnotherResponse = testAnotherResponse.ToLower();
            if (testAnotherResponse == "y")
            {
              isValid = true;
            }
            else if (testAnotherResponse == "n")
            {
              isValid = true;
              shouldContinueRunning = false;
            }
            else
            {
              Console.Write("Invalid Response, please respond 'Y' or 'N'");
            }
          }
        }
      }
      Console.WriteLine("\n============ THANK YOU FOR USING MELISSA DATA NET OBJECT ===========\n");
    }
  }

  class EmailObject
  {
    // Path to email object data files (.dat, etc)
    string dataFilePath;
    //private readonly string dataFilePath = @"C:\Program Files\Melissa DATA\DQT\Data";

    // Create instance of Melissa Data Email Object
    public mdEmail mdEmailObj = new mdEmail();

    public EmailObject(string license, string dataPath)
    {
      // Set license string and set path to data files (.dat, etc)
      mdEmailObj.SetLicenseString(license);
      dataFilePath = dataPath;

      // If you see a different date than expected, check your license string and either download the new data files or use the Melissa Updater program to update your data files.  
      mdEmailObj.SetPathToEmailFiles(dataFilePath);
      mdEmail.ProgramStatus pStatus = mdEmailObj.InitializeDataFiles();

      if (pStatus != mdEmail.ProgramStatus.ErrorNone)
      {
        Console.WriteLine("Failed to Initialize Object.");
        Console.WriteLine($"Program Status: {pStatus}");
        return;
      }
      
      Console.WriteLine($"                DataBase Date: {mdEmailObj.GetDatabaseDate()}");
      Console.WriteLine($"              Expiration Date: {mdEmailObj.GetLicenseStringExpirationDate()}");

      /**
       * This number should match with file properties of the Melissa Data Object binary file.
       * If TEST appears with the build number, there may be a license key issue.
       */
      Console.WriteLine($"               Object Version: {mdEmailObj.GetBuildNumber()}\n");
    }

    // This will call the lookup function to process the input email as well as generate the result codes
    public void ExecuteObjectAndResultCodes(ref DataContainer data)
    {
      // These are the configuarble pieces of the email object. We are setting what kind of information we want to be looked up
      mdEmailObj.SetCacheUse(1);
      mdEmailObj.SetCorrectSyntax(true);
      mdEmailObj.SetDatabaseLookup(true);
      mdEmailObj.SetFuzzyLookup(true);
      mdEmailObj.SetMXLookup(true);
      mdEmailObj.SetStandardizeCasing(true);
      mdEmailObj.SetWSLookup(false);

      mdEmailObj.VerifyEmail(data.Email);
      data.ResultCodes = mdEmailObj.GetResults();

      // ResultsCodes explain any issues email object has with the object.
      // List of result codes for Email object
      // https://wiki.melissadata.com/?title=Result_Code_Details#Email_Object
    }
  }

  public class DataContainer
  {
    public string RecID { get; set; }
    public string Email { get; set; }
    public string ResultCodes { get; set; } = "";
  }
}
