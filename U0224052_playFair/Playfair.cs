using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U0224052_playFair
{
    public class Playfair//加密的核心程式碼
    {
        //多圖替代法(PolygramSubstitution)的基本觀念是將一組字母一起加密，如此可使破密者更加困難，Playfair密碼法就是一個例子 其基本精神是將明文每一對字母(m1, m2)一起加密成密文字母(c1,c2)。
        /*
     'Prepare' removes all characters that are not letters i.e. all numbers, punctuation,
     spaces etc. are removed

     If you want numbers, punctuation etc. you must spell it out e.g.
     'stop' for period, 'one', 'two' etc.
 */
        public static string Prepare(string originalText) 
        {
            //假設使用者輸入的一定是字母
            uint length = (uint)originalText.Length; //會找出使用者輸入的文字長度 長度假定一定是非負整數
            originalText = originalText.ToLower(); //會將使用者輸入的字串轉換為小寫
            StringBuilder sb = new StringBuilder(); //在System.text內 建構子:StringBuilder() 表示可變動的字元字串 宣告產生StringBuilder的class

            //目前使用者輸入的字串都會是小寫
            for (uint i = 0; i < length; i++) //使用者輸入多少字元就執行幾次
            {
                char c = originalText[(int)i]; //取出字元 並放到暫存的變數c
                if ((int)c >= 97 && (int)c <= 122) //檢測是否符合規範 97是小寫a 122是小寫z 也就是檢測是否是字母
                {
                    //因為是一組一起加密 為了避免前一個和這次的字元一樣 造成無法在金鑰表上找到可配對的加密規則 所以過濾掉兩個連續相同的字母 避免發生問題
                    if (sb.Length % 2 == 1 && sb[sb.Length - 1] == c) //因為是兩個兩個一起加密 所以只要奇數的陣列位址不會和前面一個相同即可(例如 第[1]的不會和[0]一樣即可
                    {
                        sb.Append('x'); //如果發生的話 就直接插進一個字元補足 預設是'x'來加入要處理的字串中
                    }
                    sb.Append(c); //然後再插入到這個字元到要處理的字串中
                }
            }

            // 到最終上面都處理完成後 如果陣列長度是奇數的話就在插入一個'x"
            if (sb.Length % 2 == 1)
            {
                sb.Append('x');
            }

            return sb.ToString(); //回傳前置的處理結果 
        } //輸入是使用者輸入的字串 回傳是經過處理準備要拿去與金鑰表作比對的結果

        /*
            'Encipher' uses the Playfair cipher to encipher some text.
            The key is a string containing all 26 letters in the alphabet, except one'.
        */
        public static string Encipher(string key, string plainText)
        {
            //參數plainText的意思是經過Prepare函式處理過後的字串並非使用者輸入的那個 使用者輸入的需要經過prepare處理過後才會產生
            uint length = (uint)plainText.Length; //計算參數plainText的長度
            char a, b; //a是用於暫時儲存加密所要處理的兩個字母 假設位址是[0] 和 [1] 則a是儲存[0]  b是儲存[1]
            int a_ind, b_ind, a_row, b_row, a_col, b_col;
            StringBuilder sb = new StringBuilder();  //在System.text內 建構子:StringBuilder() 表示可變動的字元字串 宣告產生StringBuilder的class

            for (int i = 0; i < length; i += 2) //經過prepare後的字串有多長就處理多少次
            {
                a = plainText[i]; //暫時儲存準備要處理的[0] 
                b = plainText[i + 1]; //暫時儲存準備要處理的[1]

                //虛擬的方形金鑰表
                a_ind = key.IndexOf(a);
                b_ind = key.IndexOf(b);
                a_row = a_ind / 5;
                b_row = b_ind / 5;
                a_col = a_ind % 5;
                b_col = b_ind % 5;

                if (a_row == b_row)
                {
                    if (a_col == 4)
                    {
                        sb.Append(key[a_ind - 4]);
                        sb.Append(key[b_ind + 1]);
                    }
                    else if (b_col == 4)
                    {
                        sb.Append(key[a_ind + 1]);
                        sb.Append(key[b_ind - 4]);
                    }
                    else
                    {
                        sb.Append(key[a_ind + 1]);
                        sb.Append(key[b_ind + 1]);
                    }
                }
                else if (a_col == b_col)
                {
                    if (a_row == 4)
                    {
                        sb.Append(key[a_ind - 20]);
                        sb.Append(key[b_ind + 5]);
                    }
                    else if (b_row == 4)
                    {
                        sb.Append(key[a_ind + 5]);
                        sb.Append(key[b_ind - 20]);
                    }
                    else
                    {
                        sb.Append(key[a_ind + 5]);
                        sb.Append(key[b_ind + 5]);
                    }
                }
                else
                {
                    sb.Append(key[5 * a_row + b_col]);
                    sb.Append(key[5 * b_row + a_col]);
                }
            }
            return sb.ToString();
        }

        public static string Decipher(string key, string cipherText)
        {
            int length = cipherText.Length;
            char a, b;
            int a_ind, b_ind, a_row, b_row, a_col, b_col;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i += 2)
            {
                a = cipherText[i];
                b = cipherText[i + 1];

                a_ind = key.IndexOf(a);
                b_ind = key.IndexOf(b);
                a_row = a_ind / 5;
                b_row = b_ind / 5;
                a_col = a_ind % 5;
                b_col = b_ind % 5;

                if (a_row == b_row)
                {
                    if (a_col == 0)
                    {
                        sb.Append(key[a_ind + 4]);
                        sb.Append(key[b_ind - 1]);
                    }
                    else if (b_col == 0)
                    {
                        sb.Append(key[a_ind - 1]);
                        sb.Append(key[b_ind + 4]);
                    }
                    else
                    {
                        sb.Append(key[a_ind - 1]);
                        sb.Append(key[b_ind - 1]);
                    }
                }
                else if (a_col == b_col)
                {
                    if (a_row == 0)
                    {
                        sb.Append(key[a_ind + 20]);
                        sb.Append(key[b_ind - 5]);
                    }
                    else if (b_row == 0)
                    {
                        sb.Append(key[a_ind - 5]);
                        sb.Append(key[b_ind + 20]);
                    }
                    else
                    {
                        sb.Append(key[a_ind - 5]);
                        sb.Append(key[b_ind - 5]);
                    }
                }
                else
                {
                    sb.Append(key[5 * a_row + b_col]);
                    sb.Append(key[5 * b_row + a_col]);
                }
            }
            return sb.ToString();
        }

        public static String keyGenerator() //為編寫完成所以為void 目標為string
        {
            Random randMake = new Random();
            StringBuilder key = new StringBuilder(); //全部空間為26個
            String tempString = "";
            char findTarget;
            for (uint counter = 0; counter < 26; ++counter)
            {
                findTarget = (char)randMake.Next(97, 122); //產生ASCII 97~122 小寫字母的字元亂數產生
                tempString = key.ToString();
                if ((tempString.IndexOf(findTarget) == -1) && (findTarget != 'x')) //如果這次的字元不在目前字串內
                {
                    key.Append(findTarget);
                }
                else if (tempString.IndexOf('x') != -1)
                {
                    --counter;
                }
                else
                {
                    --counter;
                }
            }
            return key.ToString(); 
        }
    }
}
