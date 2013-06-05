using System;
using System.Linq;

namespace UnitTestProject1
{
    /// <summary>
    ///     This is a calculator for string.
    /// </summary>
    public class Calculator
    {
        private const char CustomDelimitorStart = '[';
        private const char CustomDelimotorEnd = ']';
        private const char DefaultDelimtor = ',';
        private const char NewLine = '\n';

        private string m_numbers;

        public int Add(string numbers)
        {
            m_numbers = numbers;
            return string.IsNullOrEmpty(numbers) ? 0 : SumAnyNumbersAndCheckForNewLines();
        }

        private int SumAnyNumbersAndCheckForNewLines()
        {
            CheckForNegativeValue();
            FindAndReplaceMultipleDelimitors();
            ReplaceDelimitersAndGetStandardDelimitedNumbers();

            return SumNumbers();
        }

        private void CheckForNegativeValue()
        {
            for (int i = 0; i < m_numbers.Length; i++)
            {
                if (m_numbers[i].Equals('-') && char.IsNumber(m_numbers[i + 1]))
                {
                    throw new ArgumentException("Negatives not allowed: " + m_numbers[i] + m_numbers[i + 1]);
                }
            }
        }

        private void FindAndReplaceMultipleDelimitors()
        {
            int numberOfBrackets = m_numbers.Count(number => number.Equals(CustomDelimitorStart));
            if (numberOfBrackets > 1)
            {
                FindAndReplaceDelimiter(DefaultDelimtor, m_numbers.LastIndexOf);
            }
        }

        private void ReplaceDelimitersAndGetStandardDelimitedNumbers()
        {
            bool shouldUseCustomDelimitor = m_numbers.Length <= 4 || !m_numbers.StartsWith("//");
            if (shouldUseCustomDelimitor)
            {
                return;
            }

            char separator = m_numbers[2];
            if (separator == CustomDelimitorStart)
            {
                FindAndReplaceDelimiter(NewLine, m_numbers.IndexOf);
            }
            else
            {
                SeparateAndReplace(separator);
            }
        }

        private int SumNumbers()
        {
            string[] split = m_numbers.Split(new[] { DefaultDelimtor, NewLine });
            return split.Select(int.Parse).Where(number => number < 1000).Sum();
        }

        private string getDelimiters(int startIndex, int endIndex)
        {
            int delimitorLength = ((endIndex - startIndex) - 1);
            return m_numbers.Substring(startIndex + 1, delimitorLength);
        }

        private void FindAndReplaceDelimiter(char delimiter, Func<char, int> indexOf)
        {
            string numberArea = m_numbers.Substring(indexOf(delimiter) + 1);
            m_numbers = numberArea.Replace(getDelimiters(indexOf(CustomDelimitorStart), indexOf(CustomDelimotorEnd)), DefaultDelimtor.ToString());
        }

        private void SeparateAndReplace(char separator)
        {
            m_numbers = m_numbers.Substring(4);
            m_numbers = m_numbers.Replace(separator, DefaultDelimtor);
        }
    }
}