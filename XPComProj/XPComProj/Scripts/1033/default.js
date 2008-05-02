// Copyright (c) Microsoft Corporation. All rights reserved.

function OnFinish(selProj, selObj)
{
	try
	{
		var strProjectPath = wizard.FindSymbol("PROJECT_PATH");
		var strProjectName = wizard.FindSymbol("PROJECT_NAME");
		var strSDKPath =  wizard.FindSymbol("SDK_PATH");
		
        wizard.AddSymbol("PROJECT_NAME_UPPER", strProjectName.toUpperCase() );
        
		selProj = CreateProject(strProjectName, strProjectPath);
		selProj.Object.Keyword = "Win32Proj";

		AddCommonConfig(selProj, strProjectName);
		AddSpecificConfig(selProj, strProjectName, strSDKPath);

		SetupFilters(selProj);

		if (IsDBCSCharSet(wizard))
			wizard.AddSymbol("ABOUTBOX_FONT_SIZE", "9"); //VS-supported codedepages that require FontSize 9
		else
			wizard.AddSymbol("ABOUTBOX_FONT_SIZE", "8"); //all the rest and unsupported codepages: Fontsize 8 
			
		AddFilesToProjectWithInfFile(selProj, strProjectName);
		
		// 创建Filter
		var oGeneratedFiles = selProj.Object.AddFilter("生成的文件");
		oGeneratedFiles.SourceControlFiles = false;
		oGeneratedFiles.AddFile(strProjectName + ".h");
		oGeneratedFiles.AddFile(strProjectName + ".xpt");

		SetCommonPchSettings(selProj);	

		//////////////////////////////////////////////////////////////////
        var strIdlName = GetTargetName("root.idl", strProjectName, "", "");
        var oIdlFile = selProj.Object.Files( strIdlName )
        
        oIdlFile.FileConfigurations("Debug").Tool = selProj.Object.Configurations("Debug").Tools("VCCustomBuildTool");
        var CBTool =  oIdlFile.FileConfigurations("Debug").Tool;
        CBTool.CommandLine = "$(ProjectDir)" + strProjectName + ".bat $(InputFileName)";
        CBTool.Description = "正在创建头文件和xpt文件...";
        CBTool.Outputs = ".\\$(InputName).h";
        
        oIdlFile.FileConfigurations("Release").Tool = selProj.Object.Configurations("Release").Tools("VCCustomBuildTool");
        var CBTool =  oIdlFile.FileConfigurations("Release").Tool;
        CBTool.CommandLine = "$(ProjectDir)" + strProjectName + ".bat $(InputFileName)";
        CBTool.Description = "正在创建头文件和xpt文件...";
        CBTool.Outputs = ".\\$(InputName).h";    
         
		selProj.Object.Save();
	}
	catch(e)
	{
		if (e.description.length != 0)
			SetErrorInfo(e);
		return e.number
	}
}

function SetFileProperties(projfile, strName)
{
	return false;
}

function GetTargetName(strName, strProjectName, strResPath, strHelpPath)
{
	try
	{
		var strTarget = strName;

		if (strName.substr(0, 4) == "root")
		{	
		    strTarget = strProjectName + strName.substr(4);
		}
		return strTarget; 
	}
	catch(e)
	{
		throw e;
	}
}

function AddSpecificConfig(proj, strProjectName, strSDKPath)
{
	try
	{
		var config = proj.Object.Configurations("Debug");
		config.CharacterSet = charSetUNICODE;
		config.ConfigurationType = typeDynamicLibrary;

		var CLTool = config.Tools("VCCLCompilerTool");

		CLTool.RuntimeLibrary = rtMultiThreadedDebugDLL;

		var strDefines = CLTool.PreprocessorDefinitions;
		if (strDefines != "") strDefines += ";";
		strDefines += GetPlatformDefine(config);
		strDefines += "_DEBUG";

		strDefines += ";_WINDOWS;_USRDLL;XP_WIN;XP_WIN32;";
	
		CLTool.PreprocessorDefinitions = strDefines;
		CLTool.DebugInformationFormat = debugEditAndContinue;
        CLTool.AdditionalIncludeDirectories = strSDKPath + "include";

		var LinkTool = config.Tools("VCLinkerTool");
		LinkTool.GenerateDebugInformation = true;
		LinkTool.LinkIncremental = linkIncrementalYes;
		LinkTool.SubSystem = subSystemWindows;
		LinkTool.AdditionalDependencies = "nspr4.lib xpcom.lib xpcomglue_s.lib";
		LinkTool.AdditionalLibraryDirectories = strSDKPath + "lib";

        ///////////////////////////////////////////////////////////
		config = proj.Object.Configurations.Item("Release");
		config.CharacterSet = charSetUNICODE;

		config.ConfigurationType = typeDynamicLibrary;

		var CLTool = config.Tools("VCCLCompilerTool");

		CLTool.RuntimeLibrary = rtMultiThreadedDLL;

		var strDefines = CLTool.PreprocessorDefinitions;
		if (strDefines != "") strDefines += ";";
		strDefines += GetPlatformDefine(config);
		strDefines += "NDEBUG";

		CLTool.DebugInformationFormat = debugEnabled;

		strDefines += ";_WINDOWS;_USRDLL;XP_WIN;XP_WIN32;";

		CLTool.PreprocessorDefinitions = strDefines;
        CLTool.AdditionalIncludeDirectories = strSDKPath + "include";

		var LinkTool = config.Tools("VCLinkerTool");
		LinkTool.GenerateDebugInformation = true;
		LinkTool.LinkIncremental = linkIncrementalNo;
		LinkTool.SubSystem = subSystemWindows;
		LinkTool.AdditionalDependencies = "nspr4.lib xpcom.lib xpcomglue_s.lib";
		LinkTool.AdditionalLibraryDirectories = strSDKPath + "lib";
	}
	catch(e)
	{
		throw e;
	}
}

