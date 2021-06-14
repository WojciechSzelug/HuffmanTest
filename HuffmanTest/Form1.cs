using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms;
using System.Runtime;


namespace HuffmanTest
{
    public partial class Form1 : Form
    {
        int sizeX = 35, sizeY = 25;
        int X = 0, Y = 320;
        private HuffmanTree huffmanTree = new HuffmanTree();
        Graphics g;
        Pen myPen = new Pen(Color.Black);
        Font drawFont = new Font("Arial", 8);
        SolidBrush drawBrush = new SolidBrush(Color.Black);

        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
        }



        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            string input, text, chars,num,code;
            int numer;
            huffmanTree = new HuffmanTree();
            BitArray encoded;
            List<Data> list = new List<Data>();
            input = richTextBox1.Text;
            huffmanTree.Build(input);


            g.Clear(this.BackColor);



            if (!IsSame(input))
            {
                ListOfChars(list, input, huffmanTree);
                encoded = huffmanTree.Encode(input);

                text = null;
                chars = null;
                num = null;
                code = null;
                foreach (bool bit in encoded)
                {
                    text = text + (bit ? 1 : 0);
                }
                richTextBox2.Text = text;

                foreach (Data data in list)
                {
                    chars = chars + WriteLine(data.Char.ToString(), 10)  + System.Environment.NewLine;
                    num = num + WriteLine(data.value.ToString(), 10) + System.Environment.NewLine;
                    code = code + WriteLine(data.code, 10) + System.Environment.NewLine;
                }
                charBox.Text = chars;
                numberBox.Text = num;
                codeBox.Text = code;

                
                
                int x=this.Width/2, y=Y;
                List<Data> orderedList = list.OrderBy(data => data.value).ToList<Data>();
                int left = 0, right = 0;
                //List<Data> orderedList = new List<Data>();
                // orderedList.Add(new Data(4, "w", "101001"));
                //   CreateTree(orderedList, x, y, 0);
                LabelEntropia.Text = Entropia(list).ToString();
                LabelWord.Text = LongWord(list).ToString();

            }
            else
            {
                richTextBox2.Text = "Proszę wpisać więcej niż jeden znak oraz przynajmnije dwa różne znaki";
            }
        }



        private double Entropia(List<Data> datas)
        {

            double result = 0;
            
            foreach(Data data in datas)
            {
                result = result + (((double)data.value / (double)datas.Count) * Math.Log(1 / (double)((double)data.value / (double)datas.Count),2));
            }
            return result;
        }
        private double LongWord(List<Data> datas)
        {
            double result = 0;

            foreach (Data data in datas)
            {
                result = result + (((double)data.value / (double)datas.Count) * (double)data.code.Length);
            }
            return result;
        }
        
        private void CreateTree(List<Data> list,int x,int y,int i ) {


            int left = 0, right = 0;
            int count=0;
            if (list.Count != 1)
            {
                g.DrawEllipse(myPen, x, y, sizeX, sizeY);
                PointF drawPoint = new PointF(x, y);

                foreach (Data dataa in list)
                {
                    count += dataa.value;
                }
                g.DrawString(count.ToString(), drawFont, drawBrush, drawPoint);
            }
            else
            {
                DrawLeaf(list[0].Char, list[0].value, list[0].code, x, y);
                return;
            }
            List<Data> leftData = new List<Data>();
            List<Data> rightData = new List<Data>();

            for (int z=0; z < list.Count; z++)
            {
                if (!(i + 1 > list[z].code.Length))
                {
                    if (list[z].code[i].Equals('1'))
                    {
                        left++;
                        leftData.Add(list[z]);
                    }
                    if (list[z].code[i].Equals('0'))
                    {
                        right++;
                        rightData.Add(list[z]);
                    }
                }
            }
            i++;
            y = y + 35;
            CreateTree(leftData, x - left * sizeX, y, i);
            CreateTree(rightData, x + left * sizeX, y, i);


        }



        private bool IsSame(string text) {

            bool same = true;
            if (text == "") return true;
            
            char first = text[0];
            
            foreach(char charr in text)
            {
                if (first != charr) same = false;

            }

            return  same;
        }

        private string WriteLine(string text,int lg) {

            if (text.Length < lg)
            {
                for (int i = text.Length; i < lg; i++)
                {
                    text += " ";
                }
            }
            
            return text;
        }


        private void ListOfChars(List<Data> list, String input, HuffmanTree huffmanTree)
        {
            char charValue;
            Boolean inList = false;
            string text;
            BitArray encoded;

            for (int i = 0; i < input.Length; i++)
            {
                inList = false;
                text = null;
                encoded = null;
                string czarek = input[i].ToString();//zmienna znaku
                if (czarek == " ") czarek = "SPACE";
                else if (czarek == ".") czarek = "DOT";
                else if (czarek == ",") czarek = "COMA";

                if (list != null)
                {
                    foreach (Data data in list)
                    {
                        if (data.Char == czarek)
                        {
                            data.incress();
                            inList = true;
                        }

                    }
                }


                if (!inList)
                {
                    encoded = huffmanTree.Encode(input[i].ToString());
                    foreach (bool bit in encoded)
                    {
                        text = text + (bit ? 1 : 0);
                    }
                    list.Add(new Data(1, czarek, text));
                }
                    
                    
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            
            List<bool> listBool = new List<bool>();
            string input, output;
            input  = richTextBox3.Text;
            foreach(char charr in input)
            {
                if (charr == '0') listBool.Add(false);
                if (charr == '1') listBool.Add(true);
            }
            BitArray binaryInput = new BitArray(listBool.ToArray());

            output = huffmanTree.Decode(binaryInput);
            richTextBinaryDecoded.Text = output;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DrawLeaf(char symbol,int count ,BitArray code ,int x,int y)
        {
            int X = 0, Y = 0;
            string text = null;
            foreach (bool bit in code)
            {
                text = text + (bit ? 1 : 0);
            }

            for (int i = 4; i < text.Length; i++)
            {
                X += 5;
            }

            Font drawFont = new Font("Arial", 8);


            SolidBrush drawBrush = new SolidBrush(Color.Black);
            
            Pen myPen = new Pen(Color.Black);
            g.DrawRectangle(myPen, x, y, X + 35, 25);
            g.DrawLine(myPen, x, y + 12, x + 35 + X, y + 12);
            g.DrawLine(myPen, x + 20, y, x + 20, y + 12);
            PointF drawPoint = new PointF(x + 4, y);
            g.DrawString(symbol.ToString(), drawFont, drawBrush, drawPoint);
            drawPoint = new PointF(x + 24, y);
            g.DrawString(count.ToString(), drawFont, drawBrush, drawPoint);
            drawPoint = new PointF(x + 4, y + 12);
            g.DrawString(text, drawFont, drawBrush, drawPoint);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void DrawLeaf(string symbol, int count, string code, int x, int y)
        {
            int X = 0, Y = 0;
            int middle = x+20;

            for (int i = 0; i < symbol.Length; i++)
            {
                middle += 5;
                X += 5;
            }
            for (int i = symbol.Length+3; i < code.Length; i++)
            {
                X += 5;
            }

            


            
            
            Pen myPen = new Pen(Color.Black);
            g.DrawRectangle(myPen, x, y, X + 35, 25);
            g.DrawLine(myPen, x, y + 12, x + 35 + X, y + 12);
            g.DrawLine(myPen, middle, y , middle, y + 12);
            PointF drawPoint = new PointF(x + 4, y);
            g.DrawString(symbol.ToString(), drawFont, drawBrush, drawPoint);
            drawPoint = new PointF(middle + 4, y);
            g.DrawString(count.ToString(), drawFont, drawBrush, drawPoint);
            drawPoint = new PointF(x + 4, y + 12);
            g.DrawString(code, drawFont, drawBrush, drawPoint);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
