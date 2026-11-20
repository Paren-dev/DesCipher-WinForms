using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //ТАБЛИЦЫ DES

        // Таблица 1. Начальная перестановка IP
        private static readonly byte[] IP = {
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9, 1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };

        // Таблица 8. Конечная перестановка IP^-1
        private static readonly byte[] IP_INV = {
            40, 8, 48, 16, 56, 24, 64, 32,
            39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30,
            37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28,
            35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26,
            33, 1, 41, 9, 49, 17, 57, 25
        };

        // Таблица 2. Функция расширения E (32 -> 48)
        private static readonly byte[] E = {
            32, 1, 2, 3, 4, 5,
            4, 5, 6, 7, 8, 9,
            8, 9, 10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1
        };

        // Таблица 4. Перестановка битов P (32 бита)
        private static readonly byte[] P = {
            16, 7, 20, 21, 29, 12, 28, 17,
            1, 15, 23, 26, 5, 18, 31, 10,
            2, 8, 24, 14, 32, 27, 3, 9,
            19, 13, 30, 6, 22, 11, 4, 25
        };

        // Таблица 5. Перестановка ключа PC-1 (64 -> 56)
        private static readonly byte[] PC1 = {
            57, 49, 41, 33, 25, 17, 9,
            1, 58, 50, 42, 34, 26, 18,
            10, 2, 59, 51, 43, 35, 27,
            19, 11, 3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
            7, 62, 54, 46, 38, 30, 22,
            14, 6, 61, 53, 45, 37, 29,
            21, 13, 5, 28, 20, 12, 4
        };

        // Таблица 7. Сжатие ключа PC-2 (56 -> 48)
        private static readonly byte[] PC2 = {
            14, 17, 11, 24, 1, 5,
            3, 28, 15, 6, 21, 10,
            23, 19, 12, 4, 26, 8,
            16, 7, 27, 20, 13, 2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32
        };

        // Таблица 6. Количество циклических сдвигов для каждого из 16 раундов
        private static readonly int[] SHIFTS = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        // Таблица 3. Восемь S-блоков (S1 - S8)
        private static readonly byte[,,] S_BOXES = {
            // S1
            {
                {14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7},
                {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},
                {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},
                {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}
            },
            // S2
            {
                {15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10},
                {3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5},
                {0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15},
                {13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9}
            },
            // S3
            {
                {10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8},
                {13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1},
                {13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7},
                {1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12}
            },
            // S4
            {
                {7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15},
                {13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9},
                {10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4},
                {3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14}
            },
            // S5
            {
                {2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9},
                {14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6},
                {4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14},
                {11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3}
            },
            // S6
            {
                {12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11},
                {10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8},
                {9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6},
                {4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13}
            },
            // S7
            {
                {4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1},
                {13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6},
                {1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2},
                {6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12}
            },
            // S8
            {
                {13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7},
                {1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2},
                {7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8},
                {2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11}
            }
        };

        public Form1()
        {
            InitializeComponent();
            textBoxKey.Text = GenerateDesKey();
        }

        /*ГЕНЕРАЦИЯ КЛЮЧА DES
        64 бита (8 символов ASCII). Биты 8, 16, 24, 32, 40, 48, 56, 64 выставляются так,
         чтобы в каждом байте было нечётное число единиц (контроль чётности).*/
        private string GenerateDesKey()
        {
            Random rnd = new Random();
            char[] keyChars = new char[8];

            for (int i = 0; i < 8; i++)
            {
                // Берём 7 случайных значащих бит в печатном диапазоне ASCII
                int val = rnd.Next(32, 126);
                val = val & 0xFE; // Очищаем младший 8-й бит

                // Считаем единицы в первых 7 битах
                int countOnes = 0;
                for (int b = 1; b < 8; b++)
                {
                    if (((val >> b) & 1) == 1) countOnes++;
                }

                // Дополняем до нечётности
                if (countOnes % 2 == 0)
                {
                    val |= 1;
                }

                keyChars[i] = (char)val;
            }

            return new string(keyChars);
        }

        //ПРЕОБРАЗОВАНИЯ БИТОВ

        private bool[] BytesToBits(byte[] bytes)
        {
            bool[] bits = new bool[bytes.Length * 8];
            for (int i = 0; i < bytes.Length; i++)
            {
                for (int b = 0; b < 8; b++)
                {
                    bits[i * 8 + b] = ((bytes[i] >> (7 - b)) & 1) == 1;
                }
            }
            return bits;
        }

        private byte[] BitsToBytes(bool[] bits)
        {
            byte[] bytes = new byte[bits.Length / 8];
            for (int i = 0; i < bytes.Length; i++)
            {
                byte val = 0;
                for (int b = 0; b < 8; b++)
                {
                    if (bits[i * 8 + b])
                    {
                        val |= (byte)(1 << (7 - b));
                    }
                }
                bytes[i] = val;
            }
            return bytes;
        }

        private bool[] Permute(bool[] input, byte[] table)
        {
            bool[] output = new bool[table.Length];
            for (int i = 0; i < table.Length; i++)
            {
                output[i] = input[table[i] - 1]; // таблицы нумеруются с 1
            }
            return output;
        }

        private bool[] LeftShift(bool[] bits, int count)
        {
            bool[] res = new bool[bits.Length];
            for (int i = 0; i < bits.Length; i++)
            {
                res[i] = bits[(i + count) % bits.Length];
            }
            return res;
        }

        //ГЕНЕРАЦИЯ 16 РАУНДОВЫХ КЛЮЧЕЙ
        private bool[][] GenerateSubkeys(byte[] keyBytes)
        {
            bool[] keyBits = BytesToBits(keyBytes);
            bool[] pc1Bits = Permute(keyBits, PC1); // 64 бита -> 56 бит

            bool[] c = new bool[28];
            bool[] d = new bool[28];
            Array.Copy(pc1Bits, 0, c, 0, 28);
            Array.Copy(pc1Bits, 28, d, 0, 28);

            bool[][] subkeys = new bool[16][];

            for (int i = 0; i < 16; i++)
            {
                c = LeftShift(c, SHIFTS[i]);
                d = LeftShift(d, SHIFTS[i]);

                bool[] cd = new bool[56];
                Array.Copy(c, 0, cd, 0, 28);
                Array.Copy(d, 0, cd, 28, 28);

                subkeys[i] = Permute(cd, PC2); // 56 бит -> 48 бит
            }

            return subkeys;
        }

        // ФУНКЦИЯ ШИФРОВАНИЯ f(R, ki)
        private bool[] FeistelF(bool[] r, bool[] subkey)
        {
            // 1) Расширение E (32 -> 48 бит)
            bool[] eExpanded = Permute(r, E);

            // 2) Побитовое исключающее ИЛИ с 48-битным раундовым ключом ki
            bool[] xorResult = new bool[48];
            for (int i = 0; i < 48; i++)
            {
                xorResult[i] = eExpanded[i] ^ subkey[i];
            }

            // 3) Подстановка через 8 S-блоков (48 бит -> 32 бита)
            bool[] sOutput = new bool[32];
            for (int i = 0; i < 8; i++)
            {
                int baseIdx = i * 6;

                // Первый и последний разряды дают номер строки 'a' (0..3)
                int row = (xorResult[baseIdx] ? 2 : 0) + (xorResult[baseIdx + 5] ? 1 : 0);

                // Средние 4 разряда дают номер столбца 'b' (0..15)
                int col = (xorResult[baseIdx + 1] ? 8 : 0) +
                          (xorResult[baseIdx + 2] ? 4 : 0) +
                          (xorResult[baseIdx + 3] ? 2 : 0) +
                          (xorResult[baseIdx + 4] ? 1 : 0);

                byte sVal = S_BOXES[i, row, col];

                // Записываем 4 бита результата
                sOutput[i * 4 + 0] = ((sVal >> 3) & 1) == 1;
                sOutput[i * 4 + 1] = ((sVal >> 2) & 1) == 1;
                sOutput[i * 4 + 2] = ((sVal >> 1) & 1) == 1;
                sOutput[i * 4 + 3] = ((sVal >> 0) & 1) == 1;
            }

            //Перестановка P (32 бита)
            return Permute(sOutput, P);
        }

        // 16 РАУНДОВ СЕТИ ФЕЙСТЕBlock (64 бита)
        private byte[] ProcessDesBlock(byte[] blockBytes, bool[][] subkeys, bool decrypt)
        {
            bool[] blockBits = BytesToBits(blockBytes);

            // Начальная перестановка IP
            bool[] ipBlock = Permute(blockBits, IP);

            bool[] l = new bool[32];
            bool[] r = new bool[32];
            Array.Copy(ipBlock, 0, l, 0, 32);
            Array.Copy(ipBlock, 32, r, 0, 32);

            // 16 циклов
            for (int i = 0; i < 16; i++)
            {
                // При дешифровании ключи идут в обратном порядке: k16 -> k1
                bool[] currentSubkey = decrypt ? subkeys[15 - i] : subkeys[i];

                bool[] fResult = FeistelF(r, currentSubkey);

                bool[] nextR = new bool[32];
                for (int j = 0; j < 32; j++)
                {
                    nextR[j] = l[j] ^ fResult[j];
                }

                l = r;
                r = nextR;
            }

            // После 16-го раунда блок объединяется как R16 L16
            bool[] r16l16 = new bool[64];
            Array.Copy(r, 0, r16l16, 0, 32);
            Array.Copy(l, 0, r16l16, 32, 32);

            // Конечная обратная перестановка IP^-1
            bool[] finalBits = Permute(r16l16, IP_INV);
            return BitsToBytes(finalBits);
        }


        // Кнопка генерации случайного ключа
        private void buttonGenKey_Click(object sender, EventArgs e)
        {
            textBoxKey.Text = GenerateDesKey();
        }

        // Кнопка «Зашифровать» (ECB + Padding '\0' + Полный DES)
        private void buttonEN_Click(object sender, EventArgs e)
        {
            string originalText = textBox1.Text;
            string keyText = textBoxKey.Text;

            if (string.IsNullOrEmpty(originalText))
            {
                MessageBox.Show("Введите открытый текст!");
                return;
            }

            if (Encoding.ASCII.GetByteCount(keyText) != 8)
            {
                MessageBox.Show("Ключ DES должен состоять ровно из 8 символов (64 бита)!");
                return;
            }

            byte[] inputBytes = Encoding.UTF8.GetBytes(originalText);
            byte[] keyBytes = Encoding.ASCII.GetBytes(keyText);

            // Генерируем 16 подключей
            bool[][] subkeys = GenerateSubkeys(keyBytes);

            // Padding нуль-байтами до кратности 8
            int padLen = (8 - (inputBytes.Length % 8)) % 8;
            byte[] padded = new byte[inputBytes.Length + padLen];
            Array.Copy(inputBytes, padded, inputBytes.Length);

            byte[] result = new byte[padded.Length];

            // Режим ECB: каждый блок по 8 байт шифруется независимо
            for (int i = 0; i < padded.Length; i += 8)
            {
                byte[] block = new byte[8];
                Array.Copy(padded, i, block, 0, 8);

                byte[] cipherBlock = ProcessDesBlock(block, subkeys, decrypt: false);
                Array.Copy(cipherBlock, 0, result, i, 8);
            }

            textBox2.Text = Convert.ToBase64String(result);
        }

        // Кнопка «Дешифровать»
        private void buttonde_Click(object sender, EventArgs e)
        {
            try
            {
                string base64Cipher = textBox2.Text.Trim();
                string keyText = textBoxKey.Text;

                if (string.IsNullOrEmpty(base64Cipher)) return;

                if (Encoding.ASCII.GetByteCount(keyText) != 8)
                {
                    MessageBox.Show("Ключ DES должен состоять ровно из 8 символов (64 бита)!", "Ошибка");
                    return;
                }

                byte[] cipherBytes = Convert.FromBase64String(base64Cipher);
                if (cipherBytes.Length % 8 != 0)
                {
                    MessageBox.Show("Длина зашифрованных данных должна быть кратна 8 байтам!", "Ошибка");
                    return;
                }

                byte[] keyBytes = Encoding.ASCII.GetBytes(keyText);
                bool[][] subkeys = GenerateSubkeys(keyBytes);

                byte[] decryptedBytes = new byte[cipherBytes.Length];

                for (int i = 0; i < cipherBytes.Length; i += 8)
                {
                    byte[] block = new byte[8];
                    Array.Copy(cipherBytes, i, block, 0, 8);

                    byte[] plainBlock = ProcessDesBlock(block, subkeys, decrypt: true);
                    Array.Copy(plainBlock, 0, decryptedBytes, i, 8);
                }

                textBox1.Text = Encoding.UTF8.GetString(decryptedBytes).TrimEnd('\0');
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректная зашифрованная строка (ожидается Base64).", "Ошибка");
            }
        }

        // Кнопка «Загрузить файл»
        private void buttonLoadClick(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                ofd.Title = "Выберите файл для загрузки";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBox1.Text = File.ReadAllText(ofd.FileName, Encoding.UTF8);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка чтения файла: {ex.Message}", "Ошибка");
                    }
                }
           }
        }
        private void buttonSaveKey_Click(object sender, EventArgs e)
        {
            string key = textBoxKey.Text;
            string cipher = textBox2.Text;

            if (string.IsNullOrEmpty(cipher))
            {
                MessageBox.Show("Шифротекст пуст! Сначала зашифруйте текст.", "Предупреждение");
                return;
            }

            if (Encoding.ASCII.GetByteCount(key) != 8)
            {
                MessageBox.Show("Ключ некорректен (должно быть ровно 8 символов).", "Ошибка");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                sfd.Title = "Сохранить ключ и зашифрованный текст в файл";
                sfd.DefaultExt = "txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Записываем две строки: сначала ключ, затем шифротекст
                        string[] lines = { key, cipher };
                        File.WriteAllLines(sfd.FileName, lines, Encoding.UTF8);

                        MessageBox.Show("Ключ и шифротекст успешно сохранены в один файл!", "Успех");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
                    }
                }
            }
        }
        private void buttonLoadKey_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                ofd.Title = "Выберите файл с ключом и шифротекстом";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);

                        if (lines.Length >= 2)
                        {
                            // Первая строка — ключ, вторая — зашифрованный текст Base64
                            textBoxKey.Text = lines[0].Trim();
                            textBox2.Text = lines[1].Trim();
                            MessageBox.Show("Ключ и зашифрованный текст успешно загружены из файла!", "Успех");
                        }
                        else if (lines.Length == 1)
                        {
                            // Если в файле всего одна строка, загружаем её как исходный открытый текст
                            textBox1.Text = lines[0];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка чтения файла: {ex.Message}", "Ошибка");
                    }
                }
            }
        }


    }
}
    
