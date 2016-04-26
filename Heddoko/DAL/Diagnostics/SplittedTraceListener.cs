using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Diagnostics
{
    /// <summary>
    /// TraceListener class with ability of writing information into file with 
    /// timestamp in text and filename.
    /// </summary>
    public class SplittedTraceListener : TraceListener
    {
        /// <summary>
        /// Size of writer's buffer. 
        /// </summary>
        public const int BufferSize = 4096;

        private string _baseFileName;
        private DateTime _currentWriterCreationDate;
        private string _fileName;

        internal TextWriter _writer;

        /// <summary>
        /// For test purposes.
        /// </summary>
        internal Func<DateTime> DateTimeNow = () => DateTime.Now;

        /// <summary>
        /// Gets or sets the text writer that receives the tracing or debugging output. 
        /// </summary>
        protected TextWriter Writer
        {
            get
            {
                this.EnsureWriter();
                return this._writer;
            }
            set
            {
                this._writer = value;
            }
        }

        /// <summary>
        /// Threshould for the one log's segment. If log exceeds this value, we will try next filename.
        /// Checking filesize is expensive operation, so it happens not often and real log size can be
        /// some bigger than MaxLogSize value.
        /// </summary>
        public static int MaxLogSize
        {
            get
            {
                return _maxLogSize;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MaxLogSize",
                        "MaxLogSize should be positive, but it has " + value + " value");
                _maxLogSize = value;
            }
        }
        private static int _maxLogSize = 8 * 1024 * 1024; // 8 Mb

        /// <summary>
        /// Constructor. Filename of resulting file will: {pattern_path}{pattern_filename}_{YYYY-MM-DD}.{pattern_ext}.
        /// If logfile is already opened by another process, tracer will try to open file with adding random GUID
        /// before extention part. If log file is bigger than MaxLogSize an integer value will be added before 
        /// extention part.
        /// </summary>
        /// <param name="fileName">The pattern of name of the file the <see cref="SplittedTraceListener"/> writes
        ///     to.</param>
        public SplittedTraceListener(string fileName)
        {
            _baseFileName = fileName;
            _currentWriterCreationDate = DateTimeNow();
            _fileName = GetFileNameForCurrentDate();
        }

        private string GetFileNameForCurrentDate(bool addGuid = false)
        {
            string leftPart = Path.Combine(
                Path.GetDirectoryName(_baseFileName), Path.GetFileNameWithoutExtension(_baseFileName));
            leftPart = string.Format("{0}_{1}",
                leftPart,
                _currentWriterCreationDate.ToString("yyyy-MM-dd"));

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

        private static bool IsProperFileName(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                    return true;
                if (new FileInfo(filename).Length < MaxLogSize)
                    return true;
                return false;
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            return true;
        }

        /// <summary>
        /// Closes the <see cref="SplittedTraceListener.Writer"/>.
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

        internal bool EnsureWriter()
        {
            CheckDateChanging();
            CheckSizeLimits();
            bool flag = true;
            if (_writer == null)
            {
                flag = false;
                if (_baseFileName != null)
                {
                    Encoding encodingWithFallback = SplittedTraceListener.GetEncodingWithFallback(new UTF8Encoding(false));
                    _fileName = GetFileNameForCurrentDate();
                    string fullPath = Path.GetFullPath(this._fileName);
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
                    return flag;
                }
            }
            return flag;
        }

        /// <summary>
        /// Flushes the output buffer for the <see cref="SplittedTraceListener.Writer"/>.
        /// </summary>
        public override void Flush()
        {
            if (EnsureWriter())
            {
                try
                {
                    _writer.Flush();
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        private static Encoding GetEncodingWithFallback(Encoding encoding)
        {
            Encoding replacementFallback = (Encoding)encoding.Clone();
            replacementFallback.EncoderFallback = EncoderFallback.ReplacementFallback;
            replacementFallback.DecoderFallback = DecoderFallback.ReplacementFallback;
            return replacementFallback;
        }


        private int _checkSizeCounter = 0;
        private void CheckSizeLimits()
        {
            try
            {
                // FileSize checking is too expensive, so we do it not every time.
                ++_checkSizeCounter;
                if ((_checkSizeCounter & 0x3ff) == 0)
                    if (_writer != null && _fileName != null)
                    {
                        if (new FileInfo(_fileName).Length >= MaxLogSize)
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
            if (_writer != null)
            {
                DateTime thisDate = DateTimeNow();
                if (_currentWriterCreationDate.Day != thisDate.Day ||
                    _currentWriterCreationDate.Month != thisDate.Month ||
                    _currentWriterCreationDate.Year != thisDate.Year)
                {
                    _currentWriterCreationDate = thisDate;
                    _fileName = GetFileNameForCurrentDate();
                    Close();
                }
            }
        }


        public override void TraceEvent(TraceEventCache eventCache, string source,
            TraceEventType eventType, int id, string message)
        {
            if (Filter == null || Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                WriteLine(string.Format("{0}: {1}", eventType, message));
            }
        }

        public override void TraceEvent(TraceEventCache eventCache, string source,
            TraceEventType eventType, int id, string format, params object[] args)
        {
            if (Filter == null || Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
            {
                if (args != null)
                {
                    WriteLine(string.Format("{0}: {1}", eventType, string.Format(format, args)));
                }
                else
                {
                    WriteLine(string.Format("{0}: {1}", eventType, format));
                }
            }
        }

        /// <summary>
        /// Writes a message to this instance's <see cref="SplittedTraceListener.Writer"/>. If date is changed 
        /// since opening writer the logfile will be reopened with a new name.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(string message)
        {
            if (!EnsureWriter())
            {
                return;
            }
            if (base.NeedIndent)
            {
                base.WriteIndent();
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
        /// Writes a message to this instance's <see cref="SplittedTraceListener.Writer"/> followed by a 
        /// line terminator. The default line terminator is a carriage
        /// return followed by a line feed (\r\n). If date is changed 
        /// since opening writer the logfile will be reopened with a new name.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string message)
        {
            if (!EnsureWriter())
            {
                return;
            }
            if (base.NeedIndent)
            {
                base.WriteIndent();
            }
            try
            {
                _writer.WriteLine(FormatMessageNewLine(message));
                base.NeedIndent = true;
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private string FormatMessage(string message)
        {
            return string.Format("[{0}] {1}", DateTimeNow(), message);
        }

        private string FormatMessageNewLine(string message)
        {
            return string.Format("[{0}] {1}\r\n", DateTimeNow(), message);
        }

    }
}
