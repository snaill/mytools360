using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using EnvDTE;
using EnvDTE80;

namespace VSComment
{
    public class DocumentThisCommand
    {
        // 虚函数    
        virtual protected ArrayList DocumentClass(CodeClass code) { return null; }
        virtual protected ArrayList DocumentFunction(CodeFunction code) { return null; }
        virtual protected ArrayList DocumentVariable(CodeVariable code) { return null; }

        // 调用入口
        public void DocumentThis(TextSelection ts, vsCMElement vse, Object code)
        {
            ArrayList al = null;

            switch (vse)
            {
                case vsCMElement.vsCMElementInterface:
                case vsCMElement.vsCMElementStruct:
                case vsCMElement.vsCMElementClass:
                    al = DocumentClass((CodeClass)code);
                    break;
                case vsCMElement.vsCMElementFunction:
                    al = DocumentFunction((CodeFunction)code);
                    break;
                case vsCMElement.vsCMElementVariable:
                    al = DocumentVariable((CodeVariable)code);
                    break;
                default:
                    return;
            }

            if (null != al)
                InsertString(ts, al); 
        }

        // 缺省说明
        public string DefaultFunctionSummary(CodeFunction codeFunction)
        {
            if (0 != ( codeFunction.FunctionKind & vsCMFunction.vsCMFunctionConstructor ))
                return GetParentFullName(codeFunction.Parent) + "的构造函数";
            else if (0 != ( codeFunction.FunctionKind & vsCMFunction.vsCMFunctionDestructor ))
                return GetParentFullName(codeFunction.Parent) + "的析构函数";
            else if (0 != (codeFunction.FunctionKind & vsCMFunction.vsCMFunctionComMethod))
                return "COM函数" + codeFunction.Name + "的摘要说明";
            else if (0 != (codeFunction.FunctionKind & vsCMFunction.vsCMFunctionVirtual))
                return "虚函数" + codeFunction.Name + "的摘要说明";

            return "函数" + codeFunction.Name + "的摘要说明";
        }

        public ArrayList DefaultFunctionReturn(CodeFunction codeFunction)
        {
            string strReturn = codeFunction.Type.AsFullName;
            if (null == strReturn || "" == strReturn)
                return null;

            ArrayList strRet = new ArrayList();
            switch (codeFunction.Type.TypeKind)
            {
                case vsCMTypeRef.vsCMTypeRefVoid:
                    return null;
                case vsCMTypeRef.vsCMTypeRefBool:
                    strRet.Add("返回值的说明");
                    strRet.Add("TRUE ...");
                    strRet.Add("FALSE ...");
                    break; 
                case vsCMTypeRef.vsCMTypeRefPointer:
                    strRet.Add("返回值的说明");
                    strRet.Add("NULL ...");
                    break; 
                default:
                    strRet.Add("返回值的说明");
                    break;
            }

            return strRet;
        }

        // 辅助函数
        protected string GetParentFullName(Object code)
        {
            try
            {
                CodeClass cc = (CodeClass)code;
                return cc.FullName;
            }
            catch { }

            return "";
        }
        protected void InsertString(TextSelection ts, ArrayList strList)
        {
            ts.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText, false);
            int col = ts.CurrentColumn;

            ts.NewLine(strList.Count);
            ts.LineUp(false, strList.Count);

            foreach (object str in strList)
            {
                ts.MoveTo( ts.CurrentLine, col, false );
                ts.Insert(str.ToString(), 0);
                ts.LineDown(false, 1);
            }
        }
    }

    class DocumentVsXmlCommand : DocumentThisCommand
    {
        protected override ArrayList DocumentClass(CodeClass codeClass)
        {
            if (null != codeClass.DocComment && "" != codeClass.DocComment)
                return null;

            System.Collections.ArrayList arr = new System.Collections.ArrayList();
            arr.Add("///<summary>");
            arr.Add("/// " + codeClass.Name + "类的简要说明");
            arr.Add("///");
            arr.Add("/// 作者: " + System.Environment.UserName);
            arr.Add("/// 版本: 1.0");
            arr.Add("/// 日期: " + System.DateTime.Today.ToShortDateString());
            arr.Add("///</summary>");
            return arr;
        }

        protected override ArrayList DocumentFunction(CodeFunction codeFunction) 
        {
            if (null != codeFunction.DocComment && "" != codeFunction.DocComment)
                return null;

            System.Collections.ArrayList arr = new System.Collections.ArrayList();
            arr.Add("///<summary>");
            arr.Add("/// " + DefaultFunctionSummary(codeFunction));
            arr.Add("///</summary>");
            foreach( CodeElement ce in codeFunction.Parameters )
            {
                arr.Add("///<param name=\"" + ce.Name + "\">参数" + ce.Name + "的说明</param>");
            }

            ArrayList al = DefaultFunctionReturn(codeFunction);
            if (null != al)
            {
                if (1 == al.Count)
                    arr.Add("///<returns>" + al[0].ToString() + "</returns>");
                else
                {
                    arr.Add("///<returns>");
                    arr.Add("/// " + al[0].ToString());
                    for (int i = 1; i < al.Count; i++)
                        arr.Add("/// 返回" + al[i].ToString());
                    arr.Add("///</returns>");
                }
            }

            return arr;
        }

        protected override ArrayList DocumentVariable(CodeVariable codeVariable) 
        {
            if (null != codeVariable.DocComment && "" != codeVariable.DocComment)
                return null;

            System.Collections.ArrayList arr = new System.Collections.ArrayList();
            arr.Add("///<summary>变量" + codeVariable.Name + "的说明</summary>");
            return arr;
        }

    }

    class DocumentDoxygenCommand : DocumentThisCommand
    {
        protected override ArrayList DocumentClass(CodeClass codeClass)
        {
            if (null != codeClass.Comment && "" != codeClass.Comment)
                return null;

            System.Collections.ArrayList arr = new System.Collections.ArrayList();
             arr.Add("/// " + codeClass.Name + "类的简要说明");
            arr.Add("///");
            arr.Add("/// @author: " + System.Environment.UserName);
            arr.Add("/// @version: 1.0");
            arr.Add("/// @date: " + System.DateTime.Today.ToShortDateString());
            return arr;
        }

        protected override ArrayList DocumentFunction(CodeFunction codeFunction)
        {
            if (null != codeFunction.Comment && "" != codeFunction.Comment)
                return null;

            System.Collections.ArrayList arr = new System.Collections.ArrayList();
            arr.Add("/// " + DefaultFunctionSummary(codeFunction));
            foreach (CodeElement ce in codeFunction.Parameters)
            {
                arr.Add("/// @param [in/out] " + ce.Name + " 参数" + ce.Name + "的说明");
            }

            ArrayList al = DefaultFunctionReturn(codeFunction);
            if (null != al)
            {
               arr.Add( "/// @return " + al[0].ToString() );
               for ( int i = 1; i < al.Count; i ++ )
                    arr.Add( "/// @retval" + al[i].ToString());
            }

            return arr;
        }

        protected override ArrayList DocumentVariable(CodeVariable codeVariable)
        {
            if (null != codeVariable.Comment && "" != codeVariable.Comment)
                return null;

            System.Collections.ArrayList arr = new System.Collections.ArrayList();
            arr.Add("///变量" + codeVariable.Name + "的说明");
            return arr;
        }

    }
}
