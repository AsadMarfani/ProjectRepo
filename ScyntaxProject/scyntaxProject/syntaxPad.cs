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
        string code, tempStr = null, viewTokens, remainCode;
        string tokenFile = @"TokensFile.txt";
        char[] codeCharArr;
        char hold = '0', holdBreaker = '0';
        string tempBreaker = null;
        int count = 0, index;

        string keywordFile = @"keywords List.txt";
        string[] lines;
        char[] breakers;
        string[] keywords_ValuePart;
        string[] keywords_ClassPart;
        string[] Arth_OP;
        string[] Inc_Dec_OP;
        string[] Assignment_OP;
        char[] Logical;
        char[] Relational;
        char[] Punctuators;
        int line_no;

        public syntaxPad()
        {
            InitializeComponent();
        }

        private void syntaxPad_Load(object sender, EventArgs e)
        {
            //Deleted '+','-' and '.' to check the RE of float_constant. We should build some logic for it.
            breakers = new char[] { '+','-','*', '/', '%',';','=', '&', '|', '!', '<', '>', ':', ',', '?', '(', ')', '{', '}', '[', ']',' ','\r','\n',';',' '};
            
            Arth_OP = new string[]{"+","-","*","/","%"};
            Inc_Dec_OP = new string[] { "++", "--" };
            Assignment_OP = new string[] { "=", "+=", "-=", "*=", "/=" };
            // load the value part and class part of keywords in keyword_Array
            int i = 0;
            
            
            

            lines = File.ReadAllLines(keywordFile);
            keywords_ValuePart = new string[lines.Length];
            keywords_ClassPart = new string[lines.Length];


            while (i < lines.Length)
            {
                int index = lines[i].IndexOf(",");
                keywords_ValuePart[i] = lines[i].Substring(0, index);
                keywords_ClassPart[i] = lines[i].Substring(index + 1); //Done! value part and class part of keywords      
                i++;                
            }

            File.WriteAllText(@"TokensFile.txt", String.Empty);
        }

        private void CompileButton_Click(object sender, EventArgs e)
        {
            int count = 0;
            File.WriteAllText(@"TokensFile.txt", String.Empty);
            line_no = 1;
            

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
                            //if (count < 3 && (holdBreaker=='+'||holdBreaker=='-'||holdBreaker=='='||holdBreaker=='&'||holdBreaker=='|'))
                            if (i + 1 != code.Length)
                            {
                                if (codeCharArr[i + 1] == '+' || codeCharArr[i + 1] == '-' || codeCharArr[i + 1] == '=' || codeCharArr[i + 1] == '&' || codeCharArr[i + 1] == '|')
                                {
                                    tempBreaker = holdBreaker.ToString() + codeCharArr[i + 1].ToString();
                                    //count++;

                                }
                            }
                            break;
                        }

                    }
                    Console.WriteLine(holdBreaker);
                    Console.WriteLine(tempBreaker);
                    if (codeCharArr[i] == holdBreaker)
                    {
                        if (holdBreaker == '\n')
                        {
                            //Console.WriteLine("Line no= " + line_no);
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

                        if (IncDec_Operator(tempBreaker) != true)
                        {

                            if (Arithmatic_Operator(holdBreaker.ToString()) != true)
                            Assignment_Operator(tempBreaker);
                        }
                        tempBreaker = null;
                        tempStr = null;
                        i = -1;
                    }
                    else
                    {
                        tempStr += hold;
                        count++;
                    }

                }
            //Console.WriteLine("Line no= "+line_no);



                
                

                viewTokens = File.ReadAllText(@"TokensFile.txt");
                TokenTextBox.Text = viewTokens;


        }

        // Method for creating tokens
        private static void createToken(string classPart, string valuePart, int line)
        {
            FileStream fs = new FileStream(@"TokensFile.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Sw = new StreamWriter(fs);
            {
                Sw.WriteLine("("+classPart+" , "+valuePart+" , "+line+")");
            }
            Sw.Close();
            fs.Close();
        }

        // method for checking the tempstr is keyword or not
        public bool KeywordCheck(string saveStr)
        {
            for (int i = 0; i < keywords_ValuePart.Length; i++)
            {

                if (saveStr == keywords_ValuePart[i])
                {
                    //Console.WriteLine(keywords_ValuePart[i]);
                    //Console.WriteLine(keywords_ClassPart[i]);
                    //Console.WriteLine(line_no);
                    createToken(keywords_ClassPart[i],keywords_ValuePart[i],line_no);
                    return true;
                }
                    
            }
            return false;
        }

        // method for checking the tempstr is identifier/variable or not
        public bool IdentifierCheck(string CheckID) {

            Match mID = Regex.Match(CheckID, @"^([a-zA-Z]|_)(\w*[a-zA-Z0-9])*$");
          if (mID.Success)
          {
              createToken("I.D", mID.Value, line_no);
              //Console.WriteLine(mc.Value+"\nID");
              return true;
          }
          else
              return false;
        }
        public bool IntConst(string IntegerConstant)
        {

            Match mInt = Regex.Match(IntegerConstant, @"^(\+|\-|\s*)\d+$");
            if (mInt.Success)
            {
                createToken("Int_const", mInt.Value, line_no);
                //Console.WriteLine(mc1.Value + "\nInt_Constant");
                return true;
            }
            else
                return false;   
        }

        public bool FltConst(string FloatConstant)
        {

            Match mFloat = Regex.Match(FloatConstant, @"^(\+|\-|\s*)\d*\.\d+$");
            if (mFloat.Success)
            {
                createToken("Float_const", mFloat.Value, line_no);
                //Console.WriteLine(mFloat.Value + "\nFloat_Constant");
                return true;
            }
            else
                return false;
        }

        public bool CharConst(string CharacterConstant)
        {

            Match mChar = Regex.Match(CharacterConstant, @"^\^(\w|\\0|\\n|\\t|\\^|\\)\^$");
            if (mChar.Success)
            {
                createToken("Char_const", mChar.Value, line_no);
                Console.WriteLine(mChar.Value + "\nChar_Constant");
                return true;
            }
            else
                return false;
        }
        public bool StrConst(string StringConstant)
        {
            //Cannot take space between words! Will solve it later.
            Match mString = Regex.Match(StringConstant, @"^\^\^(\w*|\s*|\\n|\\t|\\^)*\^\^$");
            if (mString.Success)
            {
                createToken("String_const", mString.Value, line_no);
                Console.WriteLine(mString.Value + "\nString_Constant");
                return true;
            }
            else
                return false;
        }

        public bool Arithmatic_Operator(string checkOP)
        {
            for(int i=0; i<Arth_OP.Length;i++)
            {
                if (checkOP == Arth_OP[i])
                {
                    createToken("A.S_Op", checkOP, line_no);

                    //Console.WriteLine( + "\nString_Constant");

                    return true;
                 }
            
            }
                return false;
            
        }

        public bool IncDec_Operator(string incdec)

        {
            if (incdec == "++" || incdec == "--")
            {
                createToken("Inc_Dec", incdec, line_no);
                return true;
            }
            else
                return false;
            }

        public bool Assignment_Operator(string assign)
        {
            for (int i=0; i<Assignment_OP.Length; i++)
            {
                if (assign == "=")
                {
                    createToken("Assign_Op", assign, line_no);
                    return true;
                }
                else if (assign == Assignment_OP[i])
                {
                    createToken("Assign_Equal_Op", assign, line_no);
                    return true;  
                }
                
            }
            return false;
        }

        public bool Logical_Operator()
        {
            return true;
        }

        public bool Relational_Operator()
        {
            return true;
        }

        public bool Punctuator_Operator()
        {
            return true;
        }
    }
}
