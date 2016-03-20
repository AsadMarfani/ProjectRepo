using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace scyntaxProject
{
    public partial class syntaxPad : Form
    {
        char[] breakers;
        string[] keywords_ValuePart;
        string[] keywords_ClassPart;
        int line_no;

        public syntaxPad()
        {
            InitializeComponent();
        }

        private void syntaxPad_Load(object sender, EventArgs e)
        {
            //Deleted '+','-' and '.' to check the RE of float_constant. We should build some logic for it.
            breakers = new char[] { '*', '/', '%',';','=', '&', '|', '!', '<', '>', ':', ',', '?', '(', ')', '{', '}', '[', ']',' ','\r','\n',';'};

            // load the value part and class part of keywords in keyword_Array
            int i = 0;
            string keywordFile = @"keywords List.txt";
            
            string[] lines;

            lines = File.ReadAllLines(keywordFile);
            keywords_ValuePart = new string[lines.Length];
            keywords_ClassPart = new string[lines.Length];

            while (i < lines.Length)
            {
                int index = lines[i].IndexOf(",");
                keywords_ValuePart[i] = lines[i].Substring(0, index);
                keywords_ClassPart[i] = lines[i].Substring(index + 1);
                //Console.WriteLine("{0} , {1}", keywords_ValuePart[i], keywords_ClassPart[i]); //Done! value part and class part of keywords
                i++;
                
            }

            File.WriteAllText(@"TokensFile.txt", String.Empty);
        }

        private void CompileButton_Click(object sender, EventArgs e)
        {
            line_no = 1;
            string code, tempStr=null, viewTokens,remainCode;
            string tokenFile = @"TokensFile.txt";
            char[] codeCharArr;
            char hold= '0',holdBreaker='0';
            int count = 0,index;

            code = CodeTextBox.Text.ToString();
            codeCharArr = new char[code.Length];
            codeCharArr = code.ToCharArray();

                for (int i = 0; i < code.Length; i++)
                {
                    for (int j = 0; j < breakers.Length; j++)
                    {
                        if (codeCharArr[i] != breakers[j])
                        {
                            hold = codeCharArr[i];
                        }
                        else if (codeCharArr[i] == breakers[j])
                        {
                            holdBreaker = codeCharArr[i];
                            
                            break;
                        }
                    }
                    if (codeCharArr[i] == holdBreaker)
                    {
                        if (holdBreaker == '\n')
                        {
                            Console.WriteLine("Line no= " + line_no);
                            line_no++;
                        }
                        index = code.IndexOf(holdBreaker);

                        remainCode = code.Substring(index + 1);
                        code = remainCode;
                        codeCharArr = null;
                        codeCharArr = code.ToCharArray();
                        if (tempStr != null && KeywordCheck(tempStr) != true)
                        {
                            if (IdentifierCheck(tempStr) != true)
                                if (IntConst(tempStr) != true)
                                    if (FltConst(tempStr) != true)
                                        if (CharConst(tempStr) != true)
                                                StrConst(tempStr);
                        }
                        tempStr = null;
                        i = -1;
                    }
                    else
                    {
                        tempStr += hold;
                        count++;
                    }

                }
            Console.WriteLine("Line no= "+line_no);
            //for (int i = 0; i < code.Length; i++)
            //{
            //    FileStream fs = new FileStream(@"TokensFile.txt", FileMode.Append, FileAccess.Write);
            //    StreamWriter Sw = new StreamWriter(fs);
            //    {
            //        Sw.WriteLine(codeCharArr[i]);
            //    }
            //    Sw.Close();
            //    fs.Close();
            //}

            //viewTokens = File.ReadAllText(@"TokensFile.txt");
            //TokenTextBox.Text = viewTokens;
        }
        public bool KeywordCheck(string saveStr)
        {
            for (int i = 0; i < keywords_ValuePart.Length; i++)
            {

                if (saveStr == keywords_ValuePart[i])
                {
                    Console.WriteLine(keywords_ValuePart[i]);
                    Console.WriteLine(keywords_ClassPart[i]);
                    return true;
                }
                    
            }
            return false;
        }
        public bool IdentifierCheck(string CheckID) {

          Match mc = Regex.Match(CheckID, @"^([a-zA-Z]|_)(\w*[a-zA-Z0-9])*$");
          if (mc.Success)
          {

              Console.WriteLine(mc.Value+"\nID");
              return true;
          }
          else
              return false;
        }
        public bool IntConst(string IntegerConstant)
        {

            Match mc1 = Regex.Match(IntegerConstant, @"^(\+|\-|\s*)\d+$");
            if (mc1.Success)
            {

                Console.WriteLine(mc1.Value + "\nInt_Constant");
                return true;
            }
            else
                return false;   
        }
        public bool FltConst(string FloatConstant)
        {

            Match mc1 = Regex.Match(FloatConstant, @"^(\+|\-|\s*)\d*\.\d+$");
            if (mc1.Success)
            {

                Console.WriteLine(mc1.Value + "\nFloat_Constant");
                return true;
            }
            else
                return false;
        }
        public bool CharConst(string CharacterConstant)
        {

            Match mc1 = Regex.Match(CharacterConstant, @"^\^(\w|\\0|\\n|\\t|\\^|\\)\^$");
            if (mc1.Success)
            {

                Console.WriteLine(mc1.Value + "\nChar_Constant");
                return true;
            }
            else
                return false;
        }
        public bool StrConst(string StringConstant)
        {
            //Cannot take space between words! Will solve it later.
            Match mc1 = Regex.Match(StringConstant, @"^\^\^(\w*|\s*|\\n|\\t|\\^)*\^\^$");
            if (mc1.Success)
            {

                Console.WriteLine(mc1.Value + "\nString_Constant");
                return true;
            }
            else
                return false;
        }
     
    }
}
