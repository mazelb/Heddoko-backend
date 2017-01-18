/**
 * @file SplittedTraceListener.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DAL.Diagnostics
{
    /// <summary>
    ///     TraceListener class with ability of writing information into file with
    ///     timestamp in text and filename.
    /// </summary>
    public class SplittedTraceListener : TraceListener
    {
        /// <summary>
        ///     Size of writer's buffer.
        /// </summary>
        private const int BufferSize = 4096;

        private static int _maxLogSize = 8 * 1024 * 1024; // 8 Mb

        private readonly string _baseFileName;

        /// <summary>
        ///     For test purposes.
        /// </summary>
        private readonly Func<DateTime> _dateTimeNow = () => DateTime.Now;


        private int _checkSizeCounter;
        private DateTime _currentWriterCreationDate;
        private string _fileName;

        private TextWriter _writer;

        /// <summary>
        ///     Constructor. Filename of resulting file will: {pattern_path}{pattern_filename}_{YYYY-MM-DD}.{pattern_ext}.
        ///     If logfile is already opened by another process, tracer will try to open file with adding random GUID
        ///     before extention part. If log file is bigger than MaxLogSize an integer value will be added before
        ///     extention part.
        /// </summary>
        /// <param name="fileName">
        ///     The pattern of name of the file the <see cref="SplittedTraceListener" /> writes
        ///     to.
        /// </param>
        public SplittedTraceListener(string fileName)
        {
            _baseFileName = fileName;
            _currentWriterCreationDate = _dateTimeNow();
            _fileName = GetFileNameForCurrentDate();
        }

        /// <summary>
        ///     Gets or sets the text writer that receives the tracing or debugging output.
        /// </summary>
        protected TextWriter Writer
        {
            get
            {
                EnsureWriter();
                return _writer;
            }
            set { _writer = value; }
        }

        /// <summary>
        ///     Threshould for the one log's segment. If log exceeds this value, we will try next filename.
        ///     Checking filesize is expensive operation, so it happens not often and real log size can be
        ///     some bigger than MaxLogSize value.
        /// </summary>
        private static int MaxLogSize
        {
            get { return _maxLogSize; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("MaxLogSize", "MaxLogSize should be positive, but it has " + value + " value");
                }
                _maxLogSize = value;
            }
        }

        private string GetFileNameForCurrentDate(bool addGuid = false)
        {
            if (_baseFileName != null)
            {
                string leftPart = Path.Combine(Path.GetDirectoryName(_baseFileName), Path.GetFileNameWithoutExtension(_baseFileName));
                leftPart = $"{leftPart}_{_currentWriterCreationDate.ToString("yyyy-MM-dd")}";

                string rightPart = Path.GetExtension(_baseFileName);
                if (addGuid)
                {
                    Guid guid = Guid.NewGuid();
                    rightPart = "_" + guid + rightPart;
                }

                string res = leftPart + rightPart;
                int i = 1;
                while (!IsProperFileName(res))
                {
                    res = leftPart + "_" + i.ToString("D5") + rightPart;
                    ++i;
                }

                return res;
            }
            return null;
        }

        private static bool IsProperFileName(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    return true;
                }
                return new FileInfo(filename).Length < MaxLogSize;
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
            return true;
        }

        /// <summary>
        ///     Closes the <see cref="SplittedTraceListener.Writer" />.
        /// </summary>
        public override void Close()
        {
            if (_writer != null)
            {
                try
                {
                    Flush();
                    _writer.Close();
                }
                catch (ObjectDisposedException)
                {
                }
            }
            _writer = null;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!disposing)
                {
                    if (_writer != null)
                    {
                        try
                        {
                            _writer.Close();
                        }
                        catch (ObjectDisposedException)
                        {
                        }
                    }
                    _writer = null;
                }
                else
                {
                    Close();
                }
            }
            finally
            {
                Dispose(disposing);
            }
        }

        private bool EnsureWriter()
        {
            CheckDateChanging();
            CheckSizeLimits();
            bool flag = true;
            if (_writer != null)
            {
                return true;
            }

            flag = false;
            if (_baseFileName != null)
            {
                Encoding encodingWithFallback = GetEncodingWithFallback(new UTF8Encoding(false));
                _fileName = GetFileNameForCurrentDate();
                string fullPath = Path.GetFullPath(_fileName);
                for (int num = 0; num < 2; ++num)
                {
                    try
                    {
                        _writer = new StreamWriter(fullPath, true, encodingWithFallback, BufferSize);
                        flag = true;
                        break;
                    }
                    catch (IOException)
                    {
                        fullPath = GetFileNameForCurrentDate(true);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        break;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                if (!flag)
                {
                    _fileName = null;
                }
            }
            else
            {
                return false;
            }
            return flag;
        }

        /// <summary>
        ///     Flushes the output buffer for the <see cref="SplittedTraceListener.Writer" />.
        /// </summary>
        public override void Flush()
        {
            if (!EnsureWriter())
            {
                return;
            }
            try
            {
                _writer.Flush();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private static Encoding GetEncodingWithFallback(Encoding encoding)
        {
            Encoding replacementFallback = (Encoding) encoding.Clone();
            replacementFallback.EncoderFallback = EncoderFallback.ReplacementFallback;
            replacementFallback.DecoderFallback = DecoderFallback.ReplacementFallback;
            return replacementFallback;
        }

        private void CheckSizeLimits()
        {
            try
            {
                // FileSize checking is too expensive, so we do it not every time.
                ++_checkSizeCounter;
                if ((_checkSizeCounter & 0x3ff) != 0)
                {
                    return;
                }

                if (_writer == null
                    ||
                    _fileName == null)
                {
                    return;
                }

                if (new FileInfo(_fileName).Length >= MaxLogSize)
                {
                    Close();
                }
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }


        private void CheckDateChanging()
        {
            if (_writer == null)
            {
                return;
            }

            DateTime thisDate = _dateTimeNow();
            if (_currentWriterCreationDate.Day == thisDate.Day
                &&
                _currentWriterCreationDate.Month == thisDate.Month
                &&
                _currentWriterCreationDate.Year == thisDate.Year)
            {
                return;
            }

            _currentWriterCreationDate = thisDate;
            _fileName = GetFileNameForCurrentDate();
            Close();
        }


        public override void TraceEvent(
            TraceEventCache eventCache,
            string source,
            TraceEventType eventType,
            int id,
            string message)
        {
            if (Filter == null ||
                Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                WriteLine($"{eventType}: {message}");
            }
        }

        public override void TraceEvent(
            TraceEventCache eventCache,
            string source,
            TraceEventType eventType,
            int id,
            string format,
            params object[] args)
        {
            if (Filter == null
                ||
                Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
            {
                WriteLine(args.Length > 0 ? $"{eventType}: {string.Format(format, args)}" : $"{eventType}: {format}");
            }
        }

        /// <summary>
        ///     Writes a message to this instance's <see cref="SplittedTraceListener.Writer" />. If date is changed
        ///     since opening writer the logfile will be reopened with a new name.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(string message)
        {
            if (!EnsureWriter())
            {
                return;
            }
            if (NeedIndent)
            {
                WriteIndent();
            }
            try
            {
                _writer.Write(FormatMessage(message));
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <summary>
        ///     Writes a message to this instance's <see cref="SplittedTraceListener.Writer" /> followed by a
        ///     line terminator. The default line terminator is a carriage
        ///     return followed by a line feed (\r\n). If date is changed
        ///     since opening writer the logfile will be reopened with a new name.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string message)
        {
            if (!EnsureWriter())
            {
                return;
            }
            if (NeedIndent)
            {
                WriteIndent();
            }
            try
            {
                _writer.WriteLine(FormatMessageNewLine(message));
                NeedIndent = true;
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private string FormatMessage(string message)
        {
            return $"[{_dateTimeNow()}] {message}";
        }

        private string FormatMessageNewLine(string message)
        {
            return $"[{_dateTimeNow()}] {message}\r\n";
        }
    }
}