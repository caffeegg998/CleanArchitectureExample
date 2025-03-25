using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Utils
{
    public class ShippingInfoGenerator
    {
        private static readonly Random _random = new Random();

        public static string GenerateTrackingNumber()
        {
            // Bước 1: Tạo phần tiền tố (2 ký tự chữ cái viết hoa)
            string prefix = GenerateRandomLetters(2);

            // Bước 2: Tạo phần số giữa (9 chữ số ngẫu nhiên)
            string number = GenerateRandomNumbers(9);

            // Bước 3: Tạo phần hậu tố (2 ký tự chữ cái viết hoa)
            string suffix = GenerateRandomLetters(2);

            // Bước 4: Ghép các phần lại với nhau
            return $"{prefix}{number}{suffix}";
        }

        private static string GenerateRandomLetters(int length)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = _random.Next(letters.Length);
                result.Append(letters[index]);
            }

            return result.ToString();
        }

        private static string GenerateRandomNumbers(int length)
        {
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int digit = _random.Next(0, 10); // Sinh số từ 0 đến 9
                result.Append(digit);
            }

            return result.ToString();
        }
    }
}
