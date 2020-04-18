using System;
using System.Collections.Generic;
using System.Text;

namespace com.protectsoft.SqlStatementParser
{
    public class SqlServerStatementParser : SqlStatementParser
    {
        public SqlServerStatementParser(string sql) : base(sql)
        {
        }

        unsafe internal override void determineStatementRanges(char* sql, int length, string initial_delimiter, List<StatementRange> ranges, string line_break)
        {
            char[] delimiterarr = { 'D', 'E', 'L', 'I', 'M', 'I', 'T', 'E', 'R' };
            bool _stop = false;
            string delimiter = string.IsNullOrEmpty(initial_delimiter) ? ";" : initial_delimiter;
            char* delimiter_head;
            fixed (char* delimiter_head_alloc = delimiter)
            {
                delimiter_head = delimiter_head_alloc;
            }
            char* start = (char*)(sql);
            char* head = sql;
            char* tail = head;
            char* end = head + length;
            char* new_line;
            fixed (char* new_line_alloc = line_break)
            {
                new_line = new_line_alloc;
            }
            bool have_content = false; // Set when anything else but comments were found for the current statement.
            int statementStart = 0;
            int currentLine = 0;
            char* safe_exit = head;
            while (!_stop && tail < end && safe_exit < end)
            {
                switch (*tail)
                {
                    case '/': // Possible multi line comment or hidden (conditional) command.
                        if (*(tail + 1) == '*')
                        {
                            tail += 2;
                            bool is_hidden_command = (*tail == '!');
                            while (true)
                            {
                                while (tail < end && *tail != '*')
                                    tail++;
                                if (tail == end) // Unfinished comment.
                                    break;
                                else
                                {
                                    if (*++tail == '/')
                                    {
                                        tail++; // Skip the slash too.
                                        break;
                                    }
                                }
                            }

                            if (!is_hidden_command && !have_content)
                                head = tail; // Skip over the comment.
                        }
                        else
                            tail++;

                        break;

                    case '-': // Possible single line comment.
                        {
                            char* end_char = tail + 2;
                            if (*(tail + 1) == '-' && (*end_char == ' ' || *end_char == '\t' || isLineBreak(end_char, new_line)))
                            {
                                // Skip everything until the end of the line.
                                tail += 2;
                                while (tail < end && !isLineBreak(tail, new_line))
                                    tail++;
                                if (!have_content)
                                    head = tail;
                            }
                            else
                                tail++;

                            break;
                        }

                    case '#': // MySQL single line comment.
                        while (tail < end && !isLineBreak(tail, new_line))
                            tail++;
                        if (!have_content)
                            head = tail;
                        break;

                    case '"':
                    case '\'':
                    case '`': // Quoted string/id. Skip this in a local loop.
                        {
                            have_content = true;
                            char quote = *tail++;
                            while (tail < end && *tail != quote)
                            {
                                // Skip any escaped character too.
                                if (*tail == '\\')
                                    tail++;
                                tail++;
                            }
                            if (*tail == quote)
                                tail++; // Skip trailing quote char to if one was there.

                            break;
                        }
                    case 'd':
                    case 'D':
                        {
                            have_content = true;
                            // Possible start of the keyword DELIMITER. Must be at the start of the text or a character,
                            char* run = tail;
                            bool isDelimiter = true;
                            for (int i = 0; i < delimiterarr.Length; i++)
                            {
                                if (char.ToLower(delimiterarr[i]) == char.ToLower(*run))
                                    ++run;
                                else
                                    isDelimiter = false;
                            }
                            if (*run == ' ' && isDelimiter)
                            {
                                // Delimiter keyword found. Get the new delimiter (everything until the end of the line).
                                StringBuilder delimiterBuilder = new StringBuilder();
                                while (run < end && *run != '\n' && *run != '\0')
                                {
                                    if (*run != ' ' && *run != 13)
                                        delimiterBuilder.Append(*run);
                                    run++;
                                }
                                delimiter = delimiterBuilder.ToString();
                                fixed (char* dhead = delimiter)
                                {
                                    delimiter_head = dhead;
                                }
                                while (isLineBreak(run, new_line))
                                {
                                    ++currentLine;
                                    ++run;
                                }
                                tail = run;
                                head = tail;
                                statementStart = currentLine;
                            }
                            else
                                ++tail;
                            break;
                        }
                    default:
                        if (isLineBreak(tail, new_line))
                        {
                            ++currentLine;
                            if (!have_content)
                                ++statementStart;
                        }
                        if (*tail > ' ')
                            have_content = true;
                        tail++;
                        break;
                }

                if (*tail == *delimiter_head)
                {
                    // Found possible start of the delimiter. Check if it really is.
                    int count = delimiter.Length;
                    if (count == 1)
                    {
                        // Most common case. Trim the statement and check if it is not empty before adding the range.
                        head = skip_leading_whitespace(head, tail);
                        if (head < tail)
                        {
                            long startT = head - (char*)sql;
                            long endT = tail - head;
                            if (includeInRange(startT, endT))
                            {
                                ranges.Add(new StatementRange(startT, endT));
                            }
                        }
                        head = ++tail;
                        have_content = false;
                    }
                    else
                    {
                        char* run = tail + 1;
                        char* del = delimiter_head + 1;
                        while (count-- > 1 && (*run++ == *del++))
                            ;
                        if (count == 0)
                        {
                            // Multi char delimiter is complete. Tail still points to the start of the delimiter.
                            // Run points to the first character after the delimiter.
                            head = skip_leading_whitespace(head, tail);
                            if (head < tail)
                            {
                                long startT = head - (char*)sql;
                                long endT = tail - head;
                                if (includeInRange(startT, endT))
                                {
                                    ranges.Add(new StatementRange(startT, endT));
                                }
                            }
                            tail = run;
                            head = run;
                            have_content = false;
                        }
                    }
                }
                safe_exit++;
            }

            // Add remaining text to the range list.
            head = skip_leading_whitespace(head, tail);
            if (head < tail)
            {
                long startT = head - (char*)sql;
                long endT = tail - head;
                if (includeInRange(startT, endT))
                {
                    ranges.Add(new StatementRange(startT, endT));
                }
            }
        }

    }
}
