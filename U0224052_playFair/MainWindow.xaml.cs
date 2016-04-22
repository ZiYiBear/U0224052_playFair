using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace U0224052_playFair
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * string originalText = "Defend the east wall of the castle.";
        Console.WriteLine(originalText);         
        string plainText = Playfair.Prepare(originalText);
        Console.WriteLine(plainText);
        string key = "cdefghiklmnopqrstuvwxyzab";  
        string cipherText = Playfair.Encipher(key, plainText);
        Console.WriteLine(cipherText);
        plainText = Playfair.Decipher(key, cipherText);
        Console.WriteLine(plainText);
        Console.WriteLine();
        originalText = "Hide the gold in the tree stump.";
        Console.WriteLine(originalText);         
        plainText = Playfair.Prepare(originalText);
        Console.WriteLine(plainText);
        key = "playfirexmbcdghjknostuvwz";     
        cipherText = Playfair.Encipher(key, plainText);
        Console.WriteLine(cipherText);
        plainText = Playfair.Decipher(key, cipherText);
        Console.WriteLine(plainText);
        Console.ReadLine(); 
        */

        uint counter = 1; //從第一個開始 計算"準備"輸入的是第幾個字串
        String key = Playfair.keyGenerator();

        public MainWindow()
        {
            //這裡要設定一個Key的字串來當作金鑰
            InitializeComponent();
        }

        private void sendUserStringbutton_Click(object sender, RoutedEventArgs e) //如果壓下確認輸入按鈕之後
        {
            ++counter;

        }

        private void resetbutton_Click(object sender, RoutedEventArgs e) //如果壓下去重新輸入按鈕之後
        {
            //壓下去之後 輸入的方格內的文字(text)要變成空白
            tellUserToInputStringBlock.Text = String.Format("請輸入您第{0:D}個想要加密的字串", counter);
            userInputStringBox.Text = "";
            hideForShowOutComeBlock.Text = "熊熊已成功幫你清空輸入欄位，可以重新輸入";
        }

        private void askIfShowKeycheck_Checked(object sender, RoutedEventArgs e)
        {
            hideForShowOutComeBlock.Text = key;
        }
    }
}
