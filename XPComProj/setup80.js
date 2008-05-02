// Setup program for the XPComProj Wizard for VC++ 8.0 (Whidbey)

main();

function main()
{
	var bDebug = false;
	var Args = WScript.Arguments;
	if(Args.length > 0 && Args(0) == "/debug")
		bDebug = true;

	// Create shell object
	var WSShell = WScript.CreateObject("WScript.Shell");
	// Create file system object
	var FileSys = WScript.CreateObject("Scripting.FileSystemObject");

	var strValue = FileSys.GetAbsolutePathName(".");
	if(strValue == null || strValue == "")
		strValue = ".";

	var strSourceFolder = FileSys.BuildPath(strValue, "XPComProj");
	if(bDebug)
		WScript.Echo("Source: " + strSourceFolder);

	if(!FileSys.FolderExists(strSourceFolder))
	{
		WScript.Echo("ERROR: Cannot find Wizard folder (should be: " + strSourceFolder + ")");
		return;
	}

	try
	{
		var strVC8Key = "HKLM\\Software\\Microsoft\\VisualStudio\\8.0\\Setup\\VC\\ProductDir";
		strValue = WSShell.RegRead(strVC8Key);
	}
	catch(e)
	{
		try
		{
			var strVC8Key_x64 = "HKLM\\Software\\Wow6432Node\\Microsoft\\VisualStudio\\8.0\\Setup\\VC\\ProductDir";
			strValue = WSShell.RegRead(strVC8Key_x64);
		}
		catch(e)
		{
			WScript.Echo("ERROR: Cannot find where Visual Studio 8.0 is installed.");
			return;
		}
	}

	var strDestFolder = FileSys.BuildPath(strValue, "vcprojects");
	if(bDebug)
		WScript.Echo("Destination: " + strDestFolder);
	if(!FileSys.FolderExists(strDestFolder))
	{
		WScript.Echo("ERROR: Cannot find destination folder (should be: " + strDestFolder + ")");
		return;
	}

	// Copy files
	try
	{
		var strSrc = FileSys.BuildPath(strSourceFolder, "XPComProj.ico");
		var strDest = FileSys.BuildPath(strDestFolder, "XPComProj.ico");
		FileSys.CopyFile(strSrc, strDest);

		strSrc = FileSys.BuildPath(strSourceFolder, "XPComProj.vsdir");
		strDest = FileSys.BuildPath(strDestFolder, "XPComProj.vsdir");
		FileSys.CopyFile(strSrc, strDest);
	}
	catch(e)
	{
		var strError = "no info";
		if(e.description.length != 0)
			strError = e.description;
		WScript.Echo("ERROR: Cannot copy file (" + strError + ")");
		return;
	}

	// Read and write WTLAppWiz.vsz, add engine version and replace path when found
	try
	{
		var strSrc = FileSys.BuildPath(strSourceFolder, "XPComProj.vsz");
		var strDest = FileSys.BuildPath(strDestFolder, "XPComProj.vsz");

		var ForReading = 1;
		var fileSrc = FileSys.OpenTextFile(strSrc, ForReading);
		if(fileSrc == null)
		{
			WScript.Echo("ERROR: Cannot open source file " + strSrc);
			return;
		}

		var ForWriting = 2;
		var fileDest = FileSys.OpenTextFile(strDest, ForWriting, true);
		if(fileDest == null)
		{
			WScript.Echo("ERROR: Cannot open destination file" + strDest);
			return;
		}

		while(!fileSrc.AtEndOfStream)
		{
			var strLine = fileSrc.ReadLine();
			if(strLine.indexOf("Wizard=VsWizard.VsWizardEngine") != -1)
				strLine += ".8.0";
			else if(strLine.indexOf("WIZARD_VERSION") != -1)
				strLine = "Param=\"WIZARD_VERSION = 8.0\"";
			else if(strLine.indexOf("ABSOLUTE_PATH") != -1)
				strLine = "Param=\"ABSOLUTE_PATH = " + strSourceFolder + "\"";
			fileDest.WriteLine(strLine);
		}

		fileSrc.Close();
		fileDest.Close();
	}
	catch(e)
	{
		var strError = "no info";
		if(e.description.length != 0)
			strError = e.description;
		WScript.Echo("ERROR: Cannot read and write XPComProj.vsz (" + strError + ")");
		return;
	}

	// Create WTL folder
	var strDestWTLFolder = "";
	try
	{
		strDestWTLFolder = FileSys.BuildPath(strDestFolder, "Mozilla");
		if(!FileSys.FolderExists(strDestWTLFolder))
			FileSys.CreateFolder(strDestWTLFolder);
		if(bDebug)
			WScript.Echo("WTL Folder: " + strDestWTLFolder);
	}
	catch(e)
	{
		var strError = "no info";
		if(e.description.length != 0)
			strError = e.description;
		WScript.Echo("ERROR: Cannot create WTL folder (" + strError + ")");
		return;
	}

	// Read and write additional WTLAppWiz.vsdir, add path to the wizard location
	try
	{
		var strSrc = FileSys.BuildPath(strSourceFolder, "XPComProj.vsdir");
		var strDest = FileSys.BuildPath(strDestWTLFolder, "XPComProj.vsdir");

		var ForReading = 1;
		var fileSrc = FileSys.OpenTextFile(strSrc, ForReading);
		if(fileSrc == null)
		{
			WScript.Echo("ERROR: Cannot open source file " + strSrc);
			return;
		}

		var ForWriting = 2;
		var fileDest = FileSys.OpenTextFile(strDest, ForWriting, true);
		if(fileDest == null)
		{
			WScript.Echo("ERROR: Cannot open destination file" + strDest);
			return;
		}

		while(!fileSrc.AtEndOfStream)
		{
			var strLine = fileSrc.ReadLine();
			if(strLine.indexOf("XPComProj.vsz|") != -1)
				strLine = "..\\" + strLine;
			fileDest.WriteLine(strLine);
		}

		fileSrc.Close();
		fileDest.Close();
	}
	catch(e)
	{
		var strError = "no info";
		if(e.description.length != 0)
			strError = e.description;
		WScript.Echo("ERROR: Cannot read and write Mozilla\\XPComProj.vsdir (" + strError + ")");
		return;
	}

	WScript.Echo("XPComProj Wizard successfully installed!");
}
