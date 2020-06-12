
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGBTEST
{
    public class Program
    {
        static void Main()
        {
            Program p = new Program();
            p.realMain();
        }

        [STAThread]
        public void realMain()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
