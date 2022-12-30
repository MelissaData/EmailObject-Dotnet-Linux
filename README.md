# Melissa Data Email Object Linux Net Sample


## Purpose

This is a sample of the Melissa Data Email Object using C#

The console will ask the user for:

- Email Address

And return 

- Mail Box Name
- Domain Name
- Top-Level Domain Name
- Top-Level Domain Description
- ResultCodes

## Tested Environments

- Linux 64-bit .NET 7.0, .NET 5.0, .NET Core 3.1
- Ubuntu 20.04.05 LTS
- Melissa data files for 2022-12

## Required File(s) and Programs

#### libmdEmail.so

This is the code of the Melissa Data Object.

#### Data File(s)

- mdEmail.cfg
- mdEmail.db3

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

This project is compatible with .NET 7.0, .NET 5.0, and .NET Core 3.1. If you would like to run this project for any other version besides .NET 7.0, proceed with the following procedures but check for and download your desired .NET version.

#### Install the Dotnet Core SDK
Before starting, check to see if you already have the .NET 7.0 SDK already installed by entering this command:

`dotnet --list-sdks`

If the .NET 7.0 SDK is already installed, you should see it in the following list:

![alt text](/screenshots/dotnet_output.PNG)

As long as the above list contains version `7.0.xxx` (underlined in red), then you can skip to the next step. If your list does not contain version 7.0, or you get any kind of error message, then you will need to download and install the .NET 7.0 SDK.

To download, run the following commands to add the Microsoft package signing key to your list of trusted keys and add the package repository.

```
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
```

Next, you can now run this command to install the .NET 7.0 SDK:

```
sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-7.0
```

Once all of this is done, you should be able to verify that the SDK is installed with the `dotnet --list-sdks` command.

----------------------------------------

#### Download this project
```
$ git clone https://github.com/MelissaData/EmailObject-Dotnet-Linux.git
$ cd EmailObject-Dotnet-Linux
```

#### Set up Melissa Updater 
Melissa Updater is a CLI application allowing the user to update their Melissa applications/data. 

- In the root directory of the project, create a folder called `MelissaUpdater` by using the command: 

  `mkdir MelissaUpdater`

- Enter the newly created folder using the command:

  `cd MelissaUpdater`

- Proceed to install the Melissa Updater using the curl command: 

  `curl -L -O https://releases.melissadata.net/Download/Library/LINUX/NET/ANY/latest/MelissaUpdater`

- After the Melissa Updater is installed, you will need to change the Melissa Updater to an executable using the command:

  `chmod +x MelissaUpdater`

- Now that the Melissa Updater is set up, you can now proceed to move back into the project folder by using the command:
  
   `cd ..`
----------------------------------------

#### Different ways to get data file(s)
1.  Using Melissa Updater
	- It will handle all of the data download/path and .so file(s) for you. 
2.  If you already have the latest DQS Release (ZIP), you can find the data file(s) and .so file(s) in there
	- Use the location of where you copied/installed the data and update the "DataPath" variable in the bash script.
	- Copy all the .so file(s) mentioned above into the `MelissaDataEmailObjectLinuxNETSample` project folder.

----------------------------------------

#### Configure Target Framework

Depending on your target .NET framework, you may need to configure the bash script. In order to do this, open up the `MelissaDataEmailObjectLinuxNETSample.sh` for editing, proceed to the bottom of the script where you will find this section of code.

Default set for .NET 7.0
```
dotnet publish -f="net7.0" -c Release -o $BuildPath MelissaDataEmailObjectLinuxNETSample/MelissaDataEmailObjectLinuxNETSample.csproj
#dotnet publish -f="net5.0" -c Release -o $BuildPath MelissaDataEmailObjectLinuxNETSample/MelissaDataEmailObjectLinuxNETSample.csproj
#dotnet publish -f="netcoreapp3.1" -c Release -o $BuildPath MelissaDataEmailObjectLinuxNETSample/MelissaDataEmailObjectLinuxNETSample.csproj
```
The target framework is specified with the -f flag found in the command line. If you wish to use any version besides .NET 7.0, please uncomment the line containing that framework and comment out the line containing the .NET 7.0 framework (# to comment).

#### Change Bash Script Permissions

To be able to run the bash script, you must first make it an executable using the command:

`chmod +x MelissaDataEmailObjectLinuxNETSample.sh`

As an indicator, the filename will change colors once it becomes an executable.

## Run Bash Script

Parameters:
- -e or --email: a test email address
 	
  This is convenient when you want to get results for a specific email address in one run instead of testing multiple email addresses in interactive mode.  

- -l or --license (optional): a license string to test the email object

- -q or --quiet (optional): add to the command if you do not want to get any console output from the Melissa Updater

When you have modified the script to match your data location, let's run the script. There are two modes:
- Interactive 

	The script will prompt the user for an email address, then use the provided email to test Email object. For example:
	```
	$ ./MelissaDataEmailObjectLinuxNETSample.sh
	```
    For quiet mode:
    ```
    $ ./MelissaDataEmailObjectLinuxNETSample.sh --quiet
    ```
- Command Line 

	You can pass an email address in ```--email``` parameter and a license string in ```--license``` parameter to test Email object. For example:
	```
    $ ./MelissaDataEmailObjectLinuxNETSample.sh --email "info@melissa.com"
    $ ./MelissaDataEmailObjectLinuxNETSample.sh --email "info@melissa.com" --license "<your_license_string>"
    ```
	For quiet mode:
    ```
    $ ./MelissaDataEmailObjectLinuxNETSample.sh --email "info@melissa.com" --quiet
    $ ./MelissaDataEmailObjectLinuxNETSample.sh --email "info@melissa.com" --license "<your_license_string>" --quiet
    ```
This is the expected output from a successful setup for interactive mode:

![alt text](/screenshots/output.png)

## Troubleshooting

Troubleshooting for errors found while running your sample program.

### C# Errors:

| Error      | Description |
| ----------- | ----------- |
| ErrorRequiredFileNotFound      | Program is missing a required file. Please check your Data folder and refer to the list of required files above. If you are unable to obtain all required files through the Melissa Updater, please contact technical support below. |
| ErrorDatabaseExpired   | .db file(s) are expired. Please make sure you are downloading and using the latest release version. (If using the Melissa Updater, check the bash script for 'RELEASE_VERSION={version}' and change the release version if you are using an out of date release).     |
| ErrorFoundOldFile   | File(s) are out of date. Please make sure you are downloading and using the latest release version. (If using the Melissa Updater, check the bash script for 'RELEASE_VERSION={version}' and change the release version if you are using an out of date release).    |
| ErrorLicenseExpired   | Expired license string. Please contact technical support below. |

## Contact Us

For free technical support, please call us at 800-MELISSA ext. 4
(800-635-4772 ext. 4) or email us at tech@MelissaData.com.

To purchase this product, contact Melissa Data sales department at
800-MELISSA ext. 3 (800-635-4772 ext. 3).
