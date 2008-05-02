using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;

namespace VSComment
{
    class Manager
    {
        public const string CommentType_VsXmlDoc = "Visual Stdio XMLDoc";
        public const string CommentType_Doxygen = "Doxygen";
        public const string RegistryKey_Path = "Software\\Snail Studio\\VSComment";
        protected vsCMElement[] vse = new vsCMElement[] {
                vsCMElement.vsCMElementIncludeStmt,
                vsCMElement.vsCMElementImportStmt,
                vsCMElement.vsCMElementVariable, 
                vsCMElement.vsCMElementEnum,

                vsCMElement.vsCMElementTypeDef,
                vsCMElement.vsCMElementFunction, 
                vsCMElement.vsCMElementClass,
                vsCMElement.vsCMElementNamespace};

        public static bool DefaultConfig()  
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryKey_Path, true);

            if (null != key)
                return false;

            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegistryKey_Path);                 
            key.SetValue( "CommentType", CommentType_VsXmlDoc );

            return true;
        }

        public static string GetCommentType()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryKey_Path, false);
           
            System.Diagnostics.Debug.Assert(null != key);
            return key.GetValue("CommentType" ).ToString();
        }

        public static void SetCommentType(string strType)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Manager.RegistryKey_Path, true);

            System.Diagnostics.Debug.Assert(null != key);
            key.SetValue("CommentType", strType);
        }

        public DocumentThisCommand GetCommand()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryKey_Path, false);

            if (null == key)
                return null;

            string strCommentType = key.GetValue("CommentType").ToString();
            if (strCommentType == CommentType_VsXmlDoc)
                return new DocumentVsXmlCommand();
            else if (strCommentType == CommentType_Doxygen)
                return new DocumentDoxygenCommand();
            else
                return null;
        }

        public void Exec(DTE2 application)
        {
            TextSelection ts = null;
            CodeElement element = null;

            application.UndoContext.Open("VSComment.Manager.Exec", false);
            try
            {
                ts = (TextSelection)application.ActiveWindow.Selection;
                DocumentThisCommand dtc = GetCommand();
                for ( int i = 0; i < vse.Length; i ++ )
                {
                    element = ts.ActivePoint.get_CodeElement( vse[i] );
                    if (element != null)
                    {
                       dtc.DocumentThis(ts, vse[i], element);
                       break;
                    }
                }
            }
            catch
            {
                application.UndoContext.Close();
                return;
            }
            application.UndoContext.Close();
        }

        public void AutoComment(DTE2 application)
        {
            application.UndoContext.Open("VSComment.Manager.AutoComment", false);
            try
            {
                DocumentThisCommand dtc = GetCommand();
                TextSelection ts = (TextSelection)application.ActiveDocument.Selection;
                foreach (CodeElement ce in application.ActiveDocument.ProjectItem.FileCodeModel.CodeElements)
                {
                    DocumentCodeElement(ts, dtc, ce); 
                }
            }
            catch 
            {
                application.UndoContext.Close();
                return;
            }
            application.UndoContext.Close();
        }

        protected void DocumentCodeElement(TextSelection ts, DocumentThisCommand dtc, CodeElement ce)
        {
            try
            {
                TextPoint tp = ce.GetStartPoint(vsCMPart.vsCMPartWhole);
                ts.MoveToPoint(tp, true);
                dtc.DocumentThis(ts, ce.Kind, ce);

                foreach (CodeElement cesub in ce.Children)
                {
                    DocumentCodeElement(ts, dtc, cesub);
                }
            }
            catch 
            {
                return;
            }
        }
    }
}
