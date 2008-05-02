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
        // �麯��    
        virtual protected ArrayList DocumentClass(CodeClass code) { return null; }
        virtual protected ArrayList DocumentFunction(CodeFunction code) { return null; }
        virtual protected ArrayList DocumentVariable(CodeVariable code) { return null; }

        // �������
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

        // ȱʡ˵��
        public string DefaultFunctionSummary(CodeFunction codeFunction)
        {
            if (0 != ( codeFunction.FunctionKind & vsCMFunction.vsCMFunctionConstructor ))
                return GetParentFullName(codeFunction.Parent) + "�Ĺ��캯��";
            else if (0 != ( codeFunction.FunctionKind & vsCMFunction.vsCMFunctionDestructor ))
                return GetParentFullName(codeFunction.Parent) + "����������";
            else if (0 != (codeFunction.FunctionKind & vsCMFunction.vsCMFunctionComMethod))
                return "COM����" + codeFunction.Name + "��ժҪ˵��";
            else if (0 != (codeFunction.FunctionKind & vsCMFunction.vsCMFunctionVirtual))
                return "�麯��" + codeFunction.Name + "��ժҪ˵��";

            return "����" + codeFunction.Name + "��ժҪ˵��";
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
                    strRet.Add("����ֵ��˵��");
                    strRet.Add("TRUE ...");
                    strRet.Add("FALSE ...");
                    break; 
                case vsCMTypeRef.vsCMTypeRefPointer:
                    strRet.Add("����ֵ��˵��");
                    strRet.Add("NULL ...");
                    break; 
                default:
                    strRet.Add("����ֵ��˵��");
                    break;
            }

            return strRet;
        }

        // ��������
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
            arr.Add("/// " + codeClass.Name + "��ļ�Ҫ˵��");
            arr.Add("///");
            arr.Add("/// ����: " + System.Environment.UserName);
            arr.Add("/// �汾: 1.0");
            arr.Add("/// ����: " + System.DateTime.Today.ToShortDateString());
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
                arr.Add("///<param name=\"" + ce.Name + "\">����" + ce.Name + "��˵��</param>");
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
                        arr.Add("/// ����" + al[i].ToString());
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
            arr.Add("///<summary>����" + codeVariable.Name + "��˵��</summary>");
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
             arr.Add("/// " + codeClass.Name + "��ļ�Ҫ˵��");
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
                arr.Add("/// @param [in/out] " + ce.Name + " ����" + ce.Name + "��˵��");
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
            arr.Add("///����" + codeVariable.Name + "��˵��");
            return arr;
        }

    }
}
