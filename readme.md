# aqs-owner-check-in
AQS Pet Owner Check In Portal

Setup
-------------

#### Restoring Packages

After cloning this project locally, you will need to download the packages that the project depends on. Use Visual Studio's NuGet Package manager to download the dependencies:

> **Restore Packages in Visual Studio**
> 
> 1. Enable package restore and automatic checking (Tools / Options / NuGet Package Manager / General)
> 2. Manage NuGet Packages For Solution
> 3. Check the 'Allow NuGet to download missing packages' and 'Automatically check for missing packages during build in Visual Studio' checkboxes.
> 4. Build Project

> **Restore Packages in package.json**
> 
> 1. Type 'npm install' in terminal of choice (command prompt, git bash). Node must be installed in order for this to work. 

> **Add Client Proxies to Reference paths**
>
> 1. After downloading and extracting ClientProxies files, right click project in Solution Explorer and click Properties.
> 2. Go to Reference Paths
> 3. Browse to folder containing ClientProxies files and then click Add Folder button to add path.

> **Project publishing**
>
> 1. Create new folder to contain published project executables
> 2. Click Build > Publish
> 3. *Profile* - Create new publish profile by clicking dropdown then <New Profile...>
> 4. *Connection* - Select 'File System' for Publish method, and set 'Target location' to folder created in step 1.
> 5. *Settings* - Select Configuration file based on what's available (Local-Test is provided).
> 6. Click Publish.

> **IIS Setup**
>
> 1. Click 'Add Application' in IIS Server > Sites > Default Web Site
> 2. Set Alias to AQSOwnerCheckIn.
> 3. Set 'Physical path' to folder created in step 1 of prior steps.
> 4. Navigate to localhost/AQSOwnerCheckIn/ in browser to test site.
```
