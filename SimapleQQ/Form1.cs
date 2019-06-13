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

namespace SimapleQQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            IVerify verify = VerifierFactory.GetVerifier("normal");
            MessageBox.Show(verify.Verify(nameTextbox.Text, passwordTextbox.Text) ? "登录成功！" : "登录失败！");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }

    public class VerifierFactory
    {
        public static IVerify GetVerifier(string type)
        {
            switch (type)
            {
                case "normal":
                    return new Verifier();
                case "file":
                    return new FileVerifier();
                case "net":
                    return new NetVerifier();
                default:
                    throw new Exception("无效的字符串！");
            }
        }
    }

    public interface IVerify
    {
        bool Verify(string name, string password);
    }

    public class NetVerifier : IVerify
    {
        public bool Verify(string name, string password)
        {
            //.....
            return false;
        }
    }

    public class FileVerifier : IVerify
    {
        public bool Verify(string name, string password)
        {
            List<VerifyData> verifyDatas = ReadVerifyDatas();

            for (int i = 0; i < verifyDatas.Count; i++)
            {
                if (verifyDatas[i].name == name && verifyDatas[i].password == password)
                {
                    return true;
                }
            }
            return false;
        }

        private static List<VerifyData> ReadVerifyDatas()
        {
            string datasText = File.ReadAllText(@"C:\Users\杨东雄\Desktop\VerifyDatas.txt");
            string[] datas = datasText.Split('\n');
            List<VerifyData> verifyDatas = new List<VerifyData>();
            foreach (var item in datas)
            {
                verifyDatas.Add(new VerifyData
                {
                    name = item.Split(' ')[0],
                    password = item.Split(' ')[1],
                });
            }

            return verifyDatas;
        }
    }

    public struct VerifyData
    {
        public string name;
        public string password;
    }

    public class Verifier : IVerify
    {
        VerifyData[] verifyDatas =
        {
            new VerifyData { name = "name", password = "123456" },
            new VerifyData { name = "xml", password = "123456" },
        };


        public bool Verify(string name, string password)
        {
            for (int i = 0; i < verifyDatas.Length; i++)
            {
                if (verifyDatas[i].name == name && verifyDatas[i].password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
