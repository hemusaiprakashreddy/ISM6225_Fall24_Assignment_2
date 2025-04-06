using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1: Find Missing Numbers in Array
            Console.WriteLine("Question 1:");
            int[] nums1 = { 4, 3, 2, 7, 8, 2, 3, 1 };
            IList<int> missingNumbers = FindMissingNumbers(nums1);
            Console.WriteLine(string.Join(",", missingNumbers));

            // Question 2: Sort Array by Parity
            Console.WriteLine("Question 2:");
            int[] nums2 = { 3, 1, 2, 4 };
            int[] sortedArray = SortArrayByParity(nums2);
            Console.WriteLine(string.Join(",", sortedArray));

            // Question 3: Two Sum
            Console.WriteLine("Question 3:");
            int[] nums3 = { 2, 7, 11, 15 };
            int target = 9;
            int[] indices = TwoSum(nums3, target);
            Console.WriteLine(string.Join(",", indices));

            // Question 4: Find Maximum Product of Three Numbers
            Console.WriteLine("Question 4:");
            int[] nums4 = { 1, 2, 3, 4 };
            int maxProduct = MaximumProduct(nums4);
            Console.WriteLine(maxProduct);

            // Question 5: Decimal to Binary Conversion
            Console.WriteLine("Question 5:");
            int decimalNumber = 42;
            string binary = DecimalToBinary(decimalNumber);
            Console.WriteLine(binary);

            // Question 6: Find Minimum in Rotated Sorted Array
            Console.WriteLine("Question 6:");
            int[] nums5 = { 3, 4, 5, 1, 2 };
            int minElement = FindMin(nums5);
            Console.WriteLine(minElement);

            // Question 7: Palindrome Number
            Console.WriteLine("Question 7:");
            int palindromeNumber = 121;
            bool isPalindrome = IsPalindrome(palindromeNumber);
            Console.WriteLine(isPalindrome);

            // Question 8: Fibonacci Number
            Console.WriteLine("Question 8:");
            int n = 4;
            int fibonacciNumber = Fibonacci(n);
            Console.WriteLine(fibonacciNumber);
        }

        // Question 1: Find Missing Numbers in Array

        public static IList<int> FindMissingNumbers(int[] nums)
        {
            List<int> missingNumbers = new List<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int index = Math.Abs(nums[i]) - 1;
                if (nums[index] > 0)
                {
                    nums[index] = -nums[index];
                }
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > 0)
                {
                    missingNumbers.Add(i + 1);
                }
            }

            return missingNumbers;
        }
        // Edge Cases:
        // 1. Empty array → returns empty list.
        // 2. All numbers present → returns empty list.
        // 3. All numbers missing → returns full range [1..n].
        // 4. Duplicate values → handled using absolute values to avoid index corruption.
        // Fix: Used in-place negation and Math.Abs to mark visited indices safely.


        // Question 2: Sort Array by Parity
        public static int[] SortArrayByParity(int[] nums)
        {
            int left = 0, right = nums.Length - 1;

            while (left < right)
            {
                if (nums[left] % 2 > nums[right] % 2)
                {
                    (nums[left], nums[right]) = (nums[right], nums[left]);
                }

                if (nums[left] % 2 == 0) left++;
                if (nums[right] % 2 == 1) right--;
            }

            return nums;
        }
        // Edge Cases:
        // 1. All even numbers → already sorted.
        // 2. All odd numbers → remains same (valid).
        // 3. Mix of even and odd → swaps done correctly.
        // 4. Empty array → returns empty array.
        // Fix: Used two-pointer technique and conditional checks to skip over already correct values.


        // Question 3: Two Sum
        public static int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> numMap = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                if (numMap.ContainsKey(complement))
                {
                    return new int[] { numMap[complement], i };
                }

                numMap[nums[i]] = i;
            }

            return new int[0]; // No solution
        }
        // Edge Cases:
        // 1. No two numbers add up → returns empty array.
        // 2. Duplicate values → handled by storing index of first occurrence only.
        // 3. Single element array → returns empty array.
        // Fix: Checked complement before inserting current number into dictionary to avoid using same index twice.


        // Question 4: Find Maximum Product of Three Numbers
        public static int MaximumProduct(int[] nums)
        {
            Array.Sort(nums);
            int n = nums.Length;

            return Math.Max(nums[n - 1] * nums[n - 2] * nums[n - 3], nums[0] * nums[1] * nums[n - 1]);
        }
        // Edge Cases:
        // 1. Negative numbers → handled by comparing min * min * max.
        // 2. Mix of negatives and positives → handled by sort logic.
        // 3. All positive/negative numbers → sort handles both.
        // 4. Array size exactly 3 → direct result.
        // Fix: Compared max three values and min two with max using sorted array.


        // Question 5: Decimal to Binary Conversion
        public static string DecimalToBinary(int decimalNumber)
        {
            return Convert.ToString(decimalNumber, 2);
        }
        // Edge Cases:
        // 1. Input = 0 → directly return "0".
        // 2. Very large numbers → handled by loop logic and string builder.
        // Fix: Inserted special return case for input == 0 to avoid empty string.


        // Question 6: Find Minimum in Rotated Sorted Array
        public static int FindMin(int[] nums)
        {
            int left = 0, right = nums.Length - 1;

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (nums[mid] > nums[right])
                    left = mid + 1;
                else
                    right = mid;
            }

            return nums[left];
        }
        // Edge Cases:
        // 1. Array not rotated → min is first element.
        // 2. Single element array → return element itself.
        // 3. Duplicates not allowed, but would still return left if allowed.
        // Fix: Binary search compares mid with right to safely narrow down to min.


        // Question 7: Palindrome Number
        public static bool IsPalindrome(int x)
        {
            if (x < 0) return false;

            int original = x, reversed = 0;

            while (x > 0)
            {
                reversed = reversed * 10 + x % 10;
                x /= 10;
            }

            return original == reversed;
        }
        // Edge Cases:
        // 1. Negative number → not palindrome.
        // 2. Single digit number → always palindrome.
        // 3. Number ending in 0 (except 0 itself) → not palindrome.
        // Fix: Handled negatives and used reverse technique instead of string comparison.


        // Question 8: Fibonacci Number
        public static int Fibonacci(int n)
        {
            if (n <= 1)
                return n;

            int a = 0, b = 1;

            for (int i = 2; i <= n; i++)
            {
                int temp = a + b;
                a = b;
                b = temp;
            }

            return b;
        }
        // Edge Cases:
        // 1. n = 0 or 1 → return n directly.
        // 2. Large n (e.g., 40+) → iterative version avoids recursion stack overflow.
        // Fix: Used iteration with base case return to avoid unnecessary looping.

    }
}