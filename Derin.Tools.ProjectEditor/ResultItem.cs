using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FOFramework.Tools.ProjectEditor
{
    abstract public class ResultItem
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileRelativePath { get; set; }
        public Encoding FileEncoding { get; set; }
        public int NumMatches { get; set; }
        public List<LiteMatch> Matches { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsBinaryFile { get; set; }
        public bool FailedToOpen { get; set; }
        public string ErrorMessage { get; set; }

        internal bool IncludeFilesWithoutMatches { get; set; }

        public bool IncludeInResultsList
        {
            get
            {
                if (IsSuccess && NumMatches == 0 && IncludeFilesWithoutMatches)
                    return true;

                if (IsSuccess && NumMatches > 0)
                    return true;

                if (!IsSuccess && !String.IsNullOrEmpty(ErrorMessage))
                    return true;

                return false;
            }
        }

        public bool IsReplaced
        {
            get { return this.IsSuccess && this.NumMatches > 0; }  //Account for case when no matches found
        }
    }

    public class LiteMatch
    {
        public int Index { get; set; }
        public int Length { get; set; }
    }

    public static class Utils
    {
        public static RegexOptions GetRegExOptions(bool isCaseSensitive)
        {
            //Create a new option
            var options = new RegexOptions();
            options |= RegexOptions.Multiline;

            //Is the match case check box checked
            if (!isCaseSensitive)
                options |= RegexOptions.IgnoreCase;

            //Return the options
            return options;
        }

        public static string[] GetFilesInDirectory(string dir, string fileMask, bool includeSubDirectories, string excludeMask, string excludeDir)
        {
            SearchOption searchOption = includeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            var filesInDirectory = new List<string>();
            var fileMasks = fileMask.Split(',');
            foreach (var mask in fileMasks)
            {
                filesInDirectory.AddRange(Directory.GetFiles(dir, mask.Trim(), searchOption));
            }

            if (includeSubDirectories & !String.IsNullOrEmpty(excludeDir))
            {
                foreach (var path in filesInDirectory.ToList())
                {
                    foreach (var exclude in excludeDir.Split(','))
                    {
                        if (path.LastIndexOf('\\') != -1)
                        {


                            if (path.Substring(dir.Length - 1, (path.LastIndexOf('\\') + 1) - (dir.Length)).Contains(exclude.Trim()))
                            {
                                filesInDirectory.Remove(path);
                            }
                        }
                        else
                        {

                            if (path.Substring(dir.Length - 1, path.Length - (dir.Length)).Contains(exclude.Trim()))
                            {
                                filesInDirectory.Remove(path);
                            }
                        }
                    }
                }
            }

            filesInDirectory = filesInDirectory.Distinct().ToList();

            if (!String.IsNullOrEmpty(excludeMask))
            {
                var tempFilesInDirectory = new List<string>();
                List<string> excludeFileMasks = excludeMask.Split(',').ToList();
                excludeFileMasks = excludeFileMasks.Select(fm => WildcardToRegex(fm.Trim())).ToList();


                foreach (var excludeFileMaskRegExPattern in excludeFileMasks)
                {
                    foreach (string filePath in filesInDirectory)
                    {
                        string fileName = Path.GetFileName(filePath);
                        if (fileName == null) //Somehow it can be null. So add a check
                            continue;
                        fileName = filePath;

                        if (!Regex.IsMatch(fileName, excludeFileMaskRegExPattern))
                            tempFilesInDirectory.Add(filePath);
                    }

                    filesInDirectory = tempFilesInDirectory;
                    tempFilesInDirectory = new List<string>();
                }
            }

            filesInDirectory.Sort();
            return filesInDirectory.ToArray();
        }


        //public static FileGetter CreateFileGetter(string dir, string fileMask, bool includeSubDirectories, string excludeMask)
        //{
        //    SearchOption searchOption = includeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        //    var fileMasks = fileMask.Split(',').ToList();
        //    fileMasks = fileMasks.Select(fm => fm.Trim()).ToList();

        //    List<string> excludeFileMasks = null;
        //    if (!String.IsNullOrEmpty(excludeMask))
        //    {
        //        excludeFileMasks = excludeMask.Split(',').ToList();
        //        excludeFileMasks = excludeFileMasks.Select(fm => fm.Trim()).ToList();
        //    }

        //    var fileGetter = new FileGetter
        //    {
        //        DirPath = dir,
        //        FileMasks = fileMasks,
        //        ExcludeFileMasks = excludeFileMasks,
        //        SearchOption = searchOption,
        //        UseBlockingCollection = false
        //    };

        //    return fileGetter;
        //}


        public static bool IsBinaryFile(string fileContent)
        {
            //http://stackoverflow.com/questions/910873/how-can-i-determine-if-a-file-is-binary-or-text-in-c
            if (fileContent.Contains("\0\0\0\0"))
                return true;

            return false;
        }


        public static bool IsBinaryFile(byte[] bytes)
        {
            string text = System.Text.Encoding.Default.GetString(bytes);
            return IsBinaryFile(text);
        }


        public static List<MatchPreviewLineNumber> GetLineNumbersForMatchesPreview(string fileContent, List<LiteMatch> matches,
                                                                                   int replaceStrLength = 0,
                                                                                   bool isReplace = false)
        {
            var separator = Environment.NewLine;
            var lines = fileContent.Split(new string[] { separator }, StringSplitOptions.None);

            var temp = new List<MatchPreviewLineNumber>();

            int replacedTextLength = 0;

            foreach (LiteMatch match in matches)
            {
                var lineIndexStart = DetectMatchLine(lines.ToArray(), GetMatchIndex(match.Index, replacedTextLength, isReplace));
                var lineIndexEnd = DetectMatchLine(lines.ToArray(),
                                                   GetMatchIndex(match.Index + replaceStrLength, replacedTextLength, isReplace));

                replacedTextLength += match.Length;

                for (int i = lineIndexStart - 2; i <= lineIndexEnd + 2; i++)
                {
                    if (i >= 0 && i < lines.Count())
                    {
                        var lineNumber = new MatchPreviewLineNumber();
                        lineNumber.LineNumber = i;
                        lineNumber.HasMatch = (i >= lineIndexStart && i <= lineIndexEnd) ? true : false;
                        temp.Add(lineNumber);
                    }
                }
            }

            return temp.Distinct(new LineNumberComparer()).OrderBy(ln => ln.LineNumber).ToList();
        }

        public static string FormatTimeSpan(TimeSpan timeSpan)
        {
            string result = String.Empty;

            int h = timeSpan.Hours;
            int m = timeSpan.Minutes;
            int s = timeSpan.Seconds;

            if (h > 0)
            {
                result += String.Format("{0}h ", h);

                if (m > 0)
                {
                    result += String.Format("{0}m ", m);

                    if (s > 0) result += String.Format("{0}s ", s);
                }
                else
                {
                    if (s > 0)
                    {
                        result += String.Format("{0}m ", m);

                        result += String.Format("{0}s ", s);
                    }
                }

            }
            else
            {
                if (m > 0) result += String.Format("{0}m ", m);

                if (s > 0) result += String.Format("{0}s ", s);
            }

            return result;
        }

        private static int DetectMatchLine(string[] lines, int position)
        {
            var separatorLength = 2;
            int i = 0;
            int charsCount = lines[0].Length + separatorLength;

            while (charsCount <= position)
            {
                i++;
                charsCount += lines[i].Length + separatorLength;
            }

            return i;
        }

        //from http://www.roelvanlisdonk.nl/?p=259
        internal static string WildcardToRegex(string pattern)
        {
            return string.Format("^{0}$", Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", "."));
        }

        public static byte[] ReadFileContentSample(string filePath, int maxSize = 10240)
        {
            byte[] buffer;
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                long streamLength = stream.Length;
                long bufferSize = Math.Min(streamLength, maxSize);

                buffer = new byte[bufferSize];

                stream.Read(buffer, 0, (int)bufferSize);
            }

            return buffer;
        }


        private static int GetMatchIndex(int originalIndex, int replacedTextLength, bool isReplace = false)
        {
            if (!isReplace) return originalIndex;

            var newIndex = originalIndex - replacedTextLength;

            return newIndex;
        }


        public static List<LiteMatch> FindMatches(string fileContent, string findText, bool findTextHasRegEx, bool useEscapeChars,
                                                  RegexOptions regexOptions)
        {
            MatchCollection matches;

            if (!findTextHasRegEx && !useEscapeChars)
                matches = Regex.Matches(fileContent, Regex.Escape(findText), regexOptions);
            else
                matches = Regex.Matches(fileContent, findText, regexOptions);

            List<LiteMatch> liteMatches = new List<LiteMatch>();
            foreach (Match match in matches)
            {
                liteMatches.Add(new LiteMatch { Index = match.Index, Length = match.Length });
            }

            return liteMatches;
        }

        public static Encoding GetEncodingByName(string encodingName)
        {
            if (String.IsNullOrEmpty(encodingName))
                return null;

            return Encoding.GetEncoding(encodingName);
        }
    }

    public class EncodingDetector
    {

        [Flags]
        public enum Options
        {
            KlerkSoftBom = 1,
            KlerkSoftHeuristics = 2,
            MLang = 4
        }

        public static Encoding Detect(byte[] bytes, EncodingDetector.Options opts = Options.KlerkSoftBom | Options.MLang, Encoding defaultEncoding = null)
        {
            Encoding encoding = null;

            if ((opts & Options.KlerkSoftBom) == Options.KlerkSoftBom)
            {
                //StopWatch.Start("DetectEncoding: UsingKlerksSoftBom");

                encoding = DetectEncodingUsingKlerksSoftBom(bytes);

                //StopWatch.Stop("DetectEncoding: UsingKlerksSoftBom");
            }

            if (encoding != null)
                return encoding;

            if ((opts & Options.KlerkSoftHeuristics) == Options.KlerkSoftHeuristics)
            {
                StopWatch.Start("DetectEncoding: UsingKlerksSoftHeuristics");
                encoding = DetectEncodingUsingKlerksSoftHeuristics(bytes);
                StopWatch.Stop("DetectEncoding: UsingKlerksSoftHeuristics");
            }

            if (encoding != null)
                return encoding;

            if ((opts & Options.MLang) == Options.MLang)
            {
                StopWatch.Start("DetectEncoding: UsingMLang");
                encoding = DetectEncodingUsingMLang(bytes);
                StopWatch.Stop("DetectEncoding: UsingMLang");
            }

            if (encoding == null)
                encoding = defaultEncoding;

            return encoding;
        }

        private static Encoding DetectEncodingUsingKlerksSoftBom(byte[] bytes)
        {
            Encoding encoding = null;
            if (bytes.Count() >= 4)
                encoding = KlerksSoftEncodingDetector.DetectBOMBytes(bytes);

            return encoding;
        }


        private static Encoding DetectEncodingUsingKlerksSoftHeuristics(byte[] bytes)
        {
            Encoding encoding = KlerksSoftEncodingDetector.DetectUnicodeInByteSampleByHeuristics(bytes);

            return encoding;
        }

        private static Encoding DetectEncodingUsingMLang(Byte[] bytes)
        {
            try
            {
                Encoding[] detected = EncodingTools.DetectInputCodepages(bytes, 1);
                if (detected.Length > 0)
                {
                    return detected[0];
                }
            }
            catch //(COMException ex)
            {
                // return default codepage on error
            }

            return null;
        }

    }

    public static class EncodingTools
    {
        // this only contains ascii, default windows code page and unicode
        public static int[] PreferedEncodingsForStream;

        // this contains all codepages, sorted by preference and byte usage 
        public static int[] PreferedEncodings;

        // this contains all codepages, sorted by preference and byte usage 
        public static int[] AllEncodings;



        /// <summary>
        /// Static constructor that fills the default preferred codepages
        /// </summary>
        static EncodingTools()
        {

            List<int> streamEcodings = new List<int>();
            List<int> allEncodings = new List<int>();
            List<int> mimeEcodings = new List<int>();

            // asscii - most simple so put it in first place...
            streamEcodings.Add(Encoding.ASCII.CodePage);
            mimeEcodings.Add(Encoding.ASCII.CodePage);
            allEncodings.Add(Encoding.ASCII.CodePage);


            // add default 2nd for all encodings
            allEncodings.Add(Encoding.Default.CodePage);
            // default is single byte?
            if (Encoding.Default.IsSingleByte)
            {
                // put it in second place
                streamEcodings.Add(Encoding.Default.CodePage);
                mimeEcodings.Add(Encoding.Default.CodePage);
            }



            // prefer JIS over JIS-SHIFT (JIS is detected better than JIS-SHIFT)
            // this one does include cyrilic (strange but true)
            allEncodings.Add(50220);
            mimeEcodings.Add(50220);


            // always allow unicode flavours for streams (they all have a preamble)
            streamEcodings.Add(Encoding.Unicode.CodePage);
            foreach (EncodingInfo enc in Encoding.GetEncodings())
            {
                if (!streamEcodings.Contains(enc.CodePage))
                {
                    Encoding encoding = Encoding.GetEncoding(enc.CodePage);
                    if (encoding.GetPreamble().Length > 0)
                        streamEcodings.Add(enc.CodePage);
                }
            }


            // stream is done here
            PreferedEncodingsForStream = streamEcodings.ToArray();


            // all singlebyte encodings
            foreach (EncodingInfo enc in Encoding.GetEncodings())
            {


                if (!enc.GetEncoding().IsSingleByte)
                    continue;

                if (!allEncodings.Contains(enc.CodePage))
                    allEncodings.Add(enc.CodePage);

                // only add iso and IBM encodings to mime encodings 
                if (enc.CodePage <= 1258)
                {
                    mimeEcodings.Add(enc.CodePage);
                }
            }

            // add the rest (multibyte)
            foreach (EncodingInfo enc in Encoding.GetEncodings())
            {
                if (!enc.GetEncoding().IsSingleByte)
                {
                    if (!allEncodings.Contains(enc.CodePage))
                        allEncodings.Add(enc.CodePage);

                    // only add iso and IBM encodings to mime encodings 
                    if (enc.CodePage <= 1258)
                    {
                        mimeEcodings.Add(enc.CodePage);
                    }
                }
            }

            // add unicodes
            mimeEcodings.Add(Encoding.Unicode.CodePage);


            PreferedEncodings = mimeEcodings.ToArray();
            AllEncodings = allEncodings.ToArray();
        }


        /// <summary>
        /// Checks if specified string data is acii data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsAscii(string data)
        {
            // assume empty string to be ascii
            if ((data == null) || (data.Length == 0))
                return true;
            foreach (char c in data)
            {
                if ((int)c > 127)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the best Encoding for usage in mime encodings
        /// </summary>
        /// <param name="input">text to detect</param>
        /// <returns>the suggested encoding</returns>
        public static Encoding GetMostEfficientEncoding(string input)
        {
            return GetMostEfficientEncoding(input, PreferedEncodings);
        }

        /// <summary>
        /// Gets the best ISO Encoding for usage in a stream
        /// </summary>
        /// <param name="input">text to detect</param>
        /// <returns>the suggested encoding</returns>
        public static Encoding GetMostEfficientEncodingForStream(string input)
        {
            return GetMostEfficientEncoding(input, PreferedEncodingsForStream);
        }

        /// <summary>
        /// Gets the best fitting encoding from a list of possible encodings
        /// </summary>
        /// <param name="input">text to detect</param>
        /// <param name="preferedEncodings">an array of codepages</param>
        /// <returns>the suggested encoding</returns>
        public static Encoding GetMostEfficientEncoding(string input, int[] preferedEncodings)
        {
            Encoding enc = DetectOutgoingEncoding(input, preferedEncodings, true);
            // unicode.. hmmm... check for smallest encoding
            if (enc.CodePage == Encoding.Unicode.CodePage)
            {
                int byteCount = Encoding.UTF7.GetByteCount(input);
                enc = Encoding.UTF7;
                int bestByteCount = byteCount;

                // utf8 smaller?
                byteCount = Encoding.UTF8.GetByteCount(input);
                if (byteCount < bestByteCount)
                {
                    enc = Encoding.UTF8;
                    bestByteCount = byteCount;
                }

                // unicode smaller?
                byteCount = Encoding.Unicode.GetByteCount(input);
                if (byteCount < bestByteCount)
                {
                    enc = Encoding.Unicode;
                    bestByteCount = byteCount;
                }
            }
            else
            {

            }
            return enc;
        }

        public static Encoding DetectOutgoingEncoding(string input)
        {
            return DetectOutgoingEncoding(input, PreferedEncodings, true);
        }

        public static Encoding DetectOutgoingStreamEncoding(string input)
        {
            return DetectOutgoingEncoding(input, PreferedEncodingsForStream, true);
        }

        public static Encoding[] DetectOutgoingEncodings(string input)
        {
            return DetectOutgoingEncodings(input, PreferedEncodings, true);
        }

        public static Encoding[] DetectOutgoingStreamEncodings(string input)
        {
            return DetectOutgoingEncodings(input, PreferedEncodingsForStream, true);
        }

        private static Encoding DetectOutgoingEncoding(string input, int[] preferedEncodings, bool preserveOrder)
        {

            if (input == null)
                throw new ArgumentNullException("input");

            // empty strings can always be encoded as ASCII
            if (input.Length == 0)
                return Encoding.ASCII;

            Encoding result = Encoding.ASCII;

            // get the IMultiLanguage3 interface
            MultiLanguage.IMultiLanguage3 multilang3 = new MultiLanguage.CMultiLanguageClass();
            if (multilang3 == null)
                throw new System.Runtime.InteropServices.COMException("Failed to get IMultilang3");
            try
            {
                int[] resultCodePages =
                    new int[preferedEncodings != null ? preferedEncodings.Length : Encoding.GetEncodings().Length];
                uint detectedCodepages = (uint)resultCodePages.Length;
                ushort specialChar = (ushort)'?';


                // get unmanaged arrays
                IntPtr pPrefEncs = preferedEncodings == null
                                       ? IntPtr.Zero
                                       : Marshal.AllocCoTaskMem(sizeof(uint) * preferedEncodings.Length);
                IntPtr pDetectedEncs = Marshal.AllocCoTaskMem(sizeof(uint) * resultCodePages.Length);

                try
                {
                    if (preferedEncodings != null)
                        Marshal.Copy(preferedEncodings, 0, pPrefEncs, preferedEncodings.Length);

                    Marshal.Copy(resultCodePages, 0, pDetectedEncs, resultCodePages.Length);

                    MultiLanguage.MLCPF options = MultiLanguage.MLCPF.MLDETECTF_VALID_NLS;
                    if (preserveOrder)
                        options |= MultiLanguage.MLCPF.MLDETECTF_PRESERVE_ORDER;

                    if (preferedEncodings != null)
                        options |= MultiLanguage.MLCPF.MLDETECTF_PREFERRED_ONLY;

                    multilang3.DetectOutboundCodePage(options,
                                                      input, (uint)input.Length,
                                                      pPrefEncs, (uint)(preferedEncodings == null ? 0 : preferedEncodings.Length),

                                                      pDetectedEncs, ref detectedCodepages,
                                                      ref specialChar);

                    // get result
                    if (detectedCodepages > 0)
                    {
                        int[] theResult = new int[detectedCodepages];
                        Marshal.Copy(pDetectedEncs, theResult, 0, theResult.Length);
                        result = Encoding.GetEncoding(theResult[0]);
                    }

                }
                finally
                {
                    if (pPrefEncs != IntPtr.Zero)
                        Marshal.FreeCoTaskMem(pPrefEncs);
                    Marshal.FreeCoTaskMem(pDetectedEncs);
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(multilang3);
            }
            return result;
        }

        public static Encoding[] DetectOutgoingEncodings(string input, int[] preferedEncodings, bool preserveOrder)
        {

            if (input == null)
                throw new ArgumentNullException("input");

            // empty strings can always be encoded as ASCII
            if (input.Length == 0)
                return new Encoding[] { Encoding.ASCII };

            List<Encoding> result = new List<Encoding>();

            // get the IMultiLanguage3 interface
            MultiLanguage.IMultiLanguage3 multilang3 = new MultiLanguage.CMultiLanguageClass();

            if (multilang3 == null)
                throw new System.Runtime.InteropServices.COMException("Failed to get IMultilang3");
            try
            {
                int[] resultCodePages = new int[preferedEncodings.Length];
                uint detectedCodepages = (uint)resultCodePages.Length;
                ushort specialChar = (ushort)'?';


                // get unmanaged arrays
                IntPtr pPrefEncs = Marshal.AllocCoTaskMem(sizeof(uint) * preferedEncodings.Length);
                IntPtr pDetectedEncs = preferedEncodings == null
                                           ? IntPtr.Zero
                                           : Marshal.AllocCoTaskMem(sizeof(uint) * resultCodePages.Length);

                try
                {
                    if (preferedEncodings != null)
                        Marshal.Copy(preferedEncodings, 0, pPrefEncs, preferedEncodings.Length);

                    Marshal.Copy(resultCodePages, 0, pDetectedEncs, resultCodePages.Length);

                    MultiLanguage.MLCPF options = MultiLanguage.MLCPF.MLDETECTF_VALID_NLS |
                                                  MultiLanguage.MLCPF.MLDETECTF_PREFERRED_ONLY;
                    if (preserveOrder)
                        options |= MultiLanguage.MLCPF.MLDETECTF_PRESERVE_ORDER;

                    if (preferedEncodings != null)
                        options |= MultiLanguage.MLCPF.MLDETECTF_PREFERRED_ONLY;

                    // finally... call to DetectOutboundCodePage
                    multilang3.DetectOutboundCodePage(options,
                                                      input, (uint)input.Length,
                                                      pPrefEncs, (uint)(preferedEncodings == null ? 0 : preferedEncodings.Length),
                                                      pDetectedEncs, ref detectedCodepages,
                                                      ref specialChar);

                    // get result
                    if (detectedCodepages > 0)
                    {
                        int[] theResult = new int[detectedCodepages];
                        Marshal.Copy(pDetectedEncs, theResult, 0, theResult.Length);


                        // get the encodings for the codepages
                        for (int i = 0; i < detectedCodepages; i++)
                            result.Add(Encoding.GetEncoding(theResult[i]));

                    }

                }
                finally
                {
                    if (pPrefEncs != IntPtr.Zero)
                        Marshal.FreeCoTaskMem(pPrefEncs);
                    Marshal.FreeCoTaskMem(pDetectedEncs);
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(multilang3);
            }
            // nothing found
            return result.ToArray();
        }


        /// <summary>
        /// Detect the most probable codepage from an byte array
        /// </summary>
        /// <param name="input">array containing the raw data</param>
        /// <returns>the detected encoding or the default encoding if the detection failed</returns>
        public static Encoding DetectInputCodepage(byte[] input)
        {
            try
            {
                Encoding[] detected = DetectInputCodepages(input, 1);
                if (detected.Length > 0)
                    return detected[0];
                return Encoding.Default;
            }
            catch (COMException)
            {
                // return default codepage on error
                return Encoding.Default;
            }
        }

        /// <summary>
        /// Rerurns up to maxEncodings codpages that are assumed to be apropriate
        /// </summary>
        /// <param name="input">array containing the raw data</param>
        /// <param name="maxEncodings">maxiumum number of encodings to detect</param>
        /// <returns>an array of Encoding with assumed encodings</returns>
        public static Encoding[] DetectInputCodepages(byte[] input, int maxEncodings)
        {

            StopWatch.Start("DetectInputCodepages_" + Thread.CurrentThread.ManagedThreadId);

            if (maxEncodings < 1)
                throw new ArgumentOutOfRangeException("at least one encoding must be returend", "maxEncodings");

            if (input == null)
                throw new ArgumentNullException("input");

            // empty strings can always be encoded as ASCII
            if (input.Length == 0)
                return new Encoding[] { Encoding.ASCII };

            // expand the string to be at least 256 bytes
            if (input.Length < 256)
            {
                byte[] newInput = new byte[256];
                int steps = 256 / input.Length;
                for (int i = 0; i < steps; i++)
                    Array.Copy(input, 0, newInput, input.Length * i, input.Length);

                int rest = 256 % input.Length;
                if (rest > 0)
                    Array.Copy(input, 0, newInput, steps * input.Length, rest);
                input = newInput;
            }



            List<Encoding> result = new List<Encoding>();

            // get the IMultiLanguage" interface
            MultiLanguage.IMultiLanguage2 multilang2 = new MultiLanguage.CMultiLanguageClass();

            if (multilang2 == null)
                throw new System.Runtime.InteropServices.COMException("Failed to get IMultilang2");
            try
            {
                MultiLanguage.DetectEncodingInfo[] detectedEncdings = new MultiLanguage.DetectEncodingInfo[maxEncodings];

                int scores = detectedEncdings.Length;
                int srcLen = input.Length;

                // setup options (none)   
                MultiLanguage.MLDETECTCP options = MultiLanguage.MLDETECTCP.MLDETECTCP_NONE;


                StopWatch.Start("multilang2.DetectInputCodepage_" + Thread.CurrentThread.ManagedThreadId);

                // finally... call to DetectInputCodepage
                multilang2.DetectInputCodepage(options, 0,
                                               ref input[0], ref srcLen, ref detectedEncdings[0], ref scores);

                StopWatch.Stop("multilang2.DetectInputCodepage_" + Thread.CurrentThread.ManagedThreadId);

                // get result
                if (scores > 0)
                {
                    for (int i = 0; i < scores; i++)
                    {
                        // add the result
                        result.Add(Encoding.GetEncoding((int)detectedEncdings[i].nCodePage));
                    }
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(multilang2);
            }

            //Stopwatch.Stop("DetectInputCodepages_" + Thread.CurrentThread.ManagedThreadId);

            // nothing found
            return result.ToArray();
        }


        /*  Eric made no difference
	    public static MultiLanguage.IMultiLanguage2 _multilang2;

		public static void PreDetectInputCodepages2()
	    {
			StopWatch.Start("PreDetectInputCodepages2_" + Thread.CurrentThread.ManagedThreadId);

			// get the IMultiLanguage" interface
			_multilang2 = new MultiLanguage.CMultiLanguageClass();

			if (_multilang2 == null)
				throw new System.Runtime.InteropServices.COMException("Failed to get IMultilang2");

			StopWatch.Stop("PreDetectInputCodepages2_" + Thread.CurrentThread.ManagedThreadId);

	    }

	    public static Encoding[] DetectInputCodepages2(byte[] input, int maxEncodings)
		{
			StopWatch.Start("DetectInputCodepages2_" + Thread.CurrentThread.ManagedThreadId);

			if (maxEncodings < 1)
				throw new ArgumentOutOfRangeException("at least one encoding must be returend", "maxEncodings");

			if (input == null)
				throw new ArgumentNullException("input");

			// empty strings can always be encoded as ASCII
			if (input.Length == 0)
				return new Encoding[] { Encoding.ASCII };
		
			// expand the string to be at least 256 bytes
			if (input.Length < 256)
			{
				byte[] newInput = new byte[256];
				int steps = 256 / input.Length;
				for (int i = 0; i < steps; i++)
					Array.Copy(input, 0, newInput, input.Length * i, input.Length);

				int rest = 256 % input.Length;
				if (rest > 0)
					Array.Copy(input, 0, newInput, steps * input.Length, rest);
				input = newInput;
			}

			
			MultiLanguage.DetectEncodingInfo[] detectedEncdings = new MultiLanguage.DetectEncodingInfo[maxEncodings];
	
			// setup options (none)   
			MultiLanguage.MLDETECTCP options = MultiLanguage.MLDETECTCP.MLDETECTCP_NONE;

			List<Encoding> result = new List<Encoding>();
			int scores = detectedEncdings.Length;
			int srcLen = input.Length;

			StopWatch.Start("multilang2.DetectInputCodepage2_" + Thread.CurrentThread.ManagedThreadId);

			// finally... call to DetectInputCodepage
			_multilang2.DetectInputCodepage(options, 0,
				ref input[0], ref srcLen, ref detectedEncdings[0], ref scores);

			StopWatch.Stop("multilang2.DetectInputCodepage2_" + Thread.CurrentThread.ManagedThreadId);

			// get result
			if (scores > 0)
			{
				for (int i = 0; i < scores; i++)
				{
					// add the result
					result.Add(Encoding.GetEncoding((int)detectedEncdings[i].nCodePage));
				}
			}
			
			StopWatch.Stop("DetectInputCodepages2_" + Thread.CurrentThread.ManagedThreadId);
			
			// nothing found
			return result.ToArray();
		}


		public static void PostDetectInputCodepages2()
		{
			Marshal.FinalReleaseComObject(_multilang2);
		}

		 */

        /// <summary>
        /// Opens a text file and returns the content 
        /// encoded in the most probable encoding
        /// </summary>
        /// <param name="path">path to the souce file</param>
        /// <returns>the text content of the file</returns>
        public static string ReadTextFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            using (Stream fs = File.Open(path, FileMode.Open))
            {
                byte[] rawData = new byte[fs.Length];
                Encoding enc = DetectInputCodepage(rawData);
                return enc.GetString(rawData);
            }
        }

        /// <summary>
        /// Returns a stream reader for the given
        /// text file with the best encoding applied
        /// </summary>
        /// <param name="path">path to the file</param>
        /// <returns>a StreamReader for the file</returns>
        public static StreamReader OpenTextFile(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            return OpenTextStream(File.Open(path, FileMode.Open));
        }

        /// <summary>
        /// Creates a stream reader from a stream and detects
        /// the encoding form the first bytes in the stream
        /// </summary>
        /// <param name="stream">a stream to wrap</param>
        /// <returns>the newly created StreamReader</returns>
        public static StreamReader OpenTextStream(Stream stream)
        {
            // check stream parameter
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanSeek)
                throw new ArgumentException("the stream must support seek operations", "stream");

            // assume default encoding at first place
            Encoding detectedEncoding = Encoding.Default;

            // seek to stream start
            stream.Seek(0, SeekOrigin.Begin);

            // buffer for preamble and up to 512b sample text for dection
            byte[] buf = new byte[System.Math.Min(stream.Length, 512)];

            stream.Read(buf, 0, buf.Length);
            detectedEncoding = DetectInputCodepage(buf);
            // seek back to stream start
            stream.Seek(0, SeekOrigin.Begin);


            return new StreamReader(stream, detectedEncoding);

        }

    }

    public class KlerksSoftEncodingDetector
    {

        /*
         * Simple class to handle text file encoding woes (in a primarily English-speaking tech 
         *      world).
         * 
         *  - This code is fully managed, no shady calls to MLang (the unmanaged codepage
         *      detection library originally developed for Internet Explorer).
         * 
         *  - This class does NOT try to detect arbitrary codepages/charsets, it really only
         *      aims to differentiate between some of the most common variants of Unicode 
         *      encoding, and a "default" (western / ascii-based) encoding alternative provided
         *      by the caller.
         *      
         *  - As there is no "Reliable" way to distinguish between UTF-8 (without BOM) and 
         *      Windows-1252 (in .Net, also incorrectly called "ASCII") encodings, we use a 
         *      heuristic - so the more of the file we can sample the better the guess. If you 
         *      are going to read the whole file into memory at some point, then best to pass 
         *      in the whole byte byte array directly. Otherwise, decide how to trade off 
         *      reliability against performance / memory usage.
         *      
         *  - The UTF-8 detection heuristic only works for western text, as it relies on 
         *      the presence of UTF-8 encoded accented and other characters found in the upper 
         *      ranges of the Latin-1 and (particularly) Windows-1252 codepages.
         *  
         *  - For more general detection routines, see existing projects / resources:
         *    - MLang - Microsoft library originally for IE6, available in Windows XP and later APIs now (I think?)
         *      - MLang .Net bindings: http://www.codeproject.com/KB/recipes/DetectEncoding.aspx
         *    - CharDet - Mozilla browser's detection routines
         *      - Ported to Java then .Net: http://www.conceptdevelopment.net/Localization/NCharDet/
         *      - Ported straight to .Net: http://code.google.com/p/chardetsharp/source/browse
         *  
         * Copyright Tao Klerks, Jan 2010, tao@klerks.biz
         * Licensed under the modified BSD license:
         * 

Redistribution and use in source and binary forms, with or without modification, are 
permitted provided that the following conditions are met:

 - Redistributions of source code must retain the above copyright notice, this list of 
conditions and the following disclaimer.
 - Redistributions in binary form must reproduce the above copyright notice, this list 
of conditions and the following disclaimer in the documentation and/or other materials
provided with the distribution.
 - The name of the author may not be used to endorse or promote products derived from 
this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, 
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR 
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY 
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
OF SUCH DAMAGE.

         * 
         */

        //private const long _defaultHeuristicSampleSize = 0x10000;
        private const long _defaultHeuristicSampleSize = 10240;
        //completely arbitrary - inappropriate for high numbers of files / high speed requirements

        public static Encoding DetectTextFileEncoding(string InputFilename, Encoding DefaultEncoding)
        {
            using (FileStream textfileStream = File.OpenRead(InputFilename))
            {
                return DetectTextFileEncoding(textfileStream, DefaultEncoding);
            }
        }

        public static Encoding DetectTextFileEncoding(FileStream InputFileStream, Encoding DefaultEncoding)
        {
            //return DetectTextFileEncoding(InputFileStream, DefaultEncoding, InputFileStream.Length);
            return DetectTextFileEncoding(InputFileStream, DefaultEncoding, _defaultHeuristicSampleSize);
        }


        public static Encoding DetectTextFileEncoding(FileStream InputFileStream, Encoding DefaultEncoding,
                                                      long HeuristicSampleSize)
        {
            if (InputFileStream == null)
                throw new ArgumentNullException("Must provide a valid Filestream!", "InputFileStream");

            if (!InputFileStream.CanRead)
                throw new ArgumentException("Provided file stream is not readable!", "InputFileStream");

            if (!InputFileStream.CanSeek)
                throw new ArgumentException("Provided file stream cannot seek!", "InputFileStream");

            Encoding encodingFound = null;

            long originalPos = InputFileStream.Position;

            InputFileStream.Position = 0;


            //First read only what we need for BOM detection
            byte[] bomBytes = new byte[InputFileStream.Length > 4 ? 4 : InputFileStream.Length];
            InputFileStream.Read(bomBytes, 0, bomBytes.Length);

            encodingFound = DetectBOMBytes(bomBytes);

            if (encodingFound != null)
            {
                InputFileStream.Position = originalPos;
                return encodingFound;
            }


            //BOM Detection failed, going for heuristics now.
            //  create sample byte array and populate it
            byte[] sampleBytes =
                new byte[HeuristicSampleSize > InputFileStream.Length ? InputFileStream.Length : HeuristicSampleSize];
            Array.Copy(bomBytes, sampleBytes, bomBytes.Length);
            if (InputFileStream.Length > bomBytes.Length)
                InputFileStream.Read(sampleBytes, bomBytes.Length, sampleBytes.Length - bomBytes.Length);
            InputFileStream.Position = originalPos;

            //test byte array content
            encodingFound = DetectUnicodeInByteSampleByHeuristics(sampleBytes);

            if (encodingFound != null)
                return encodingFound;
            else
                return DefaultEncoding;
        }

        public static Encoding DetectTextByteArrayEncoding(byte[] TextData, Encoding DefaultEncoding)
        {
            if (TextData == null)
                throw new ArgumentNullException("Must provide a valid text data byte array!", "TextData");

            Encoding encodingFound = null;

            encodingFound = DetectBOMBytes(TextData);

            if (encodingFound != null)
            {
                return encodingFound;
            }
            else
            {
                //test byte array content
                encodingFound = DetectUnicodeInByteSampleByHeuristics(TextData);

                if (encodingFound != null)
                    return encodingFound;
                else
                    return DefaultEncoding;
            }


        }

        public static Encoding DetectBOMBytes(byte[] BOMBytes)
        {
            if (BOMBytes == null)
                throw new ArgumentNullException("Must provide a valid BOM byte array!", "BOMBytes");

            if (BOMBytes.Length < 2)
                return null;

            if (BOMBytes[0] == 0xff
                && BOMBytes[1] == 0xfe
                && (BOMBytes.Length < 4
                    || BOMBytes[2] != 0
                    || BOMBytes[3] != 0
                   )
                )
                return Encoding.Unicode;

            if (BOMBytes[0] == 0xfe
                && BOMBytes[1] == 0xff
                )
                return Encoding.BigEndianUnicode;

            if (BOMBytes.Length < 3)
                return null;

            if (BOMBytes[0] == 0xef && BOMBytes[1] == 0xbb && BOMBytes[2] == 0xbf)
                return Encoding.UTF8;

            if (BOMBytes[0] == 0x2b && BOMBytes[1] == 0x2f && BOMBytes[2] == 0x76)
                return Encoding.UTF7;

            if (BOMBytes.Length < 4)
                return null;

            if (BOMBytes[0] == 0xff && BOMBytes[1] == 0xfe && BOMBytes[2] == 0 && BOMBytes[3] == 0)
                return Encoding.UTF32;

            if (BOMBytes[0] == 0 && BOMBytes[1] == 0 && BOMBytes[2] == 0xfe && BOMBytes[3] == 0xff)
                return Encoding.GetEncoding(12001);

            return null;
        }

        public static Encoding DetectUnicodeInByteSampleByHeuristics(byte[] SampleBytes)
        {
            long oddBinaryNullsInSample = 0;
            long evenBinaryNullsInSample = 0;
            long suspiciousUTF8SequenceCount = 0;
            long suspiciousUTF8BytesTotal = 0;
            long likelyUSASCIIBytesInSample = 0;

            //Cycle through, keeping count of binary null positions, possible UTF-8 
            //  sequences from upper ranges of Windows-1252, and probable US-ASCII 
            //  character counts.

            long currentPos = 0;
            int skipUTF8Bytes = 0;

            while (currentPos < SampleBytes.Length)
            {
                //binary null distribution
                if (SampleBytes[currentPos] == 0)
                {
                    if (currentPos % 2 == 0)
                        evenBinaryNullsInSample++;
                    else
                        oddBinaryNullsInSample++;
                }

                //likely US-ASCII characters
                if (IsCommonUSASCIIByte(SampleBytes[currentPos]))
                    likelyUSASCIIBytesInSample++;

                //suspicious sequences (look like UTF-8)
                if (skipUTF8Bytes == 0)
                {
                    int lengthFound = DetectSuspiciousUTF8SequenceLength(SampleBytes, currentPos);

                    if (lengthFound > 0)
                    {
                        suspiciousUTF8SequenceCount++;
                        suspiciousUTF8BytesTotal += lengthFound;
                        skipUTF8Bytes = lengthFound - 1;
                    }
                }
                else
                {
                    skipUTF8Bytes--;
                }

                currentPos++;
            }

            //1: UTF-16 LE - in english / european environments, this is usually characterized by a 
            //  high proportion of odd binary nulls (starting at 0), with (as this is text) a low 
            //  proportion of even binary nulls.
            //  The thresholds here used (less than 20% nulls where you expect non-nulls, and more than
            //  60% nulls where you do expect nulls) are completely arbitrary.

            if (((evenBinaryNullsInSample * 2.0) / SampleBytes.Length) < 0.2
                && ((oddBinaryNullsInSample * 2.0) / SampleBytes.Length) > 0.6
                )
                return Encoding.Unicode;


            //2: UTF-16 BE - in english / european environments, this is usually characterized by a 
            //  high proportion of even binary nulls (starting at 0), with (as this is text) a low 
            //  proportion of odd binary nulls.
            //  The thresholds here used (less than 20% nulls where you expect non-nulls, and more than
            //  60% nulls where you do expect nulls) are completely arbitrary.

            if (((oddBinaryNullsInSample * 2.0) / SampleBytes.Length) < 0.2
                && ((evenBinaryNullsInSample * 2.0) / SampleBytes.Length) > 0.6
                )
                return Encoding.BigEndianUnicode;


            //3: UTF-8 - Martin Dürst outlines a method for detecting whether something CAN be UTF-8 content 
            //  using regexp, in his w3c.org unicode FAQ entry: 
            //  http://www.w3.org/International/questions/qa-forms-utf-8
            //  adapted here for C#.
            string potentiallyMangledString = Encoding.ASCII.GetString(SampleBytes);
            Regex UTF8Validator = new Regex(@"\A("
                                            + @"[\x09\x0A\x0D\x20-\x7E]"
                                            + @"|[\xC2-\xDF][\x80-\xBF]"
                                            + @"|\xE0[\xA0-\xBF][\x80-\xBF]"
                                            + @"|[\xE1-\xEC\xEE\xEF][\x80-\xBF]{2}"
                                            + @"|\xED[\x80-\x9F][\x80-\xBF]"
                                            + @"|\xF0[\x90-\xBF][\x80-\xBF]{2}"
                                            + @"|[\xF1-\xF3][\x80-\xBF]{3}"
                                            + @"|\xF4[\x80-\x8F][\x80-\xBF]{2}"
                                            + @")*\z");
            if (UTF8Validator.IsMatch(potentiallyMangledString))
            {
                //Unfortunately, just the fact that it CAN be UTF-8 doesn't tell you much about probabilities.
                //If all the characters are in the 0-127 range, no harm done, most western charsets are same as UTF-8 in these ranges.
                //If some of the characters were in the upper range (western accented characters), however, they would likely be mangled to 2-byte by the UTF-8 encoding process.
                // So, we need to play stats.

                // The "Random" likelihood of any pair of randomly generated characters being one 
                //   of these "suspicious" character sequences is:
                //     128 / (256 * 256) = 0.2%.
                //
                // In western text data, that is SIGNIFICANTLY reduced - most text data stays in the <127 
                //   character range, so we assume that more than 1 in 500,000 of these character 
                //   sequences indicates UTF-8. The number 500,000 is completely arbitrary - so sue me.
                //
                // We can only assume these character sequences will be rare if we ALSO assume that this
                //   IS in fact western text - in which case the bulk of the UTF-8 encoded data (that is 
                //   not already suspicious sequences) should be plain US-ASCII bytes. This, I 
                //   arbitrarily decided, should be 80% (a random distribution, eg binary data, would yield 
                //   approx 40%, so the chances of hitting this threshold by accident in random data are 
                //   VERY low). 

                if ((suspiciousUTF8SequenceCount * 500000.0 / SampleBytes.Length >= 1) //suspicious sequences
                    && (
                       //all suspicious, so cannot evaluate proportion of US-Ascii
                       SampleBytes.Length - suspiciousUTF8BytesTotal == 0
                       ||
                       likelyUSASCIIBytesInSample * 1.0 / (SampleBytes.Length - suspiciousUTF8BytesTotal) >= 0.8
                       )
                    )
                    return Encoding.UTF8;
            }

            return null;
        }

        private static bool IsCommonUSASCIIByte(byte testByte)
        {
            if (testByte == 0x0A //lf
                || testByte == 0x0D //cr
                || testByte == 0x09 //tab
                || (testByte >= 0x20 && testByte <= 0x2F) //common punctuation
                || (testByte >= 0x30 && testByte <= 0x39) //digits
                || (testByte >= 0x3A && testByte <= 0x40) //common punctuation
                || (testByte >= 0x41 && testByte <= 0x5A) //capital letters
                || (testByte >= 0x5B && testByte <= 0x60) //common punctuation
                || (testByte >= 0x61 && testByte <= 0x7A) //lowercase letters
                || (testByte >= 0x7B && testByte <= 0x7E) //common punctuation
                )
                return true;
            else
                return false;
        }

        private static int DetectSuspiciousUTF8SequenceLength(byte[] SampleBytes, long currentPos)
        {
            int lengthFound = 0;

            if (SampleBytes.Length > currentPos + 1
                && SampleBytes[currentPos] == 0xC2
                )
            {
                if (SampleBytes[currentPos + 1] == 0x81
                    || SampleBytes[currentPos + 1] == 0x8D
                    || SampleBytes[currentPos + 1] == 0x8F
                    )
                    lengthFound = 2;
                else if (SampleBytes[currentPos + 1] == 0x90
                         || SampleBytes[currentPos + 1] == 0x9D
                    )
                    lengthFound = 2;
                else if (SampleBytes[currentPos + 1] >= 0xA0
                         && SampleBytes[currentPos + 1] <= 0xBF
                    )
                    lengthFound = 2;
            }
            else if (SampleBytes.Length > currentPos + 1
                     && SampleBytes[currentPos] == 0xC3
                )
            {
                if (SampleBytes[currentPos + 1] >= 0x80
                    && SampleBytes[currentPos + 1] <= 0xBF
                    )
                    lengthFound = 2;
            }
            else if (SampleBytes.Length > currentPos + 1
                     && SampleBytes[currentPos] == 0xC5
                )
            {
                if (SampleBytes[currentPos + 1] == 0x92
                    || SampleBytes[currentPos + 1] == 0x93
                    )
                    lengthFound = 2;
                else if (SampleBytes[currentPos + 1] == 0xA0
                         || SampleBytes[currentPos + 1] == 0xA1
                    )
                    lengthFound = 2;
                else if (SampleBytes[currentPos + 1] == 0xB8
                         || SampleBytes[currentPos + 1] == 0xBD
                         || SampleBytes[currentPos + 1] == 0xBE
                    )
                    lengthFound = 2;
            }
            else if (SampleBytes.Length > currentPos + 1
                     && SampleBytes[currentPos] == 0xC6
                )
            {
                if (SampleBytes[currentPos + 1] == 0x92)
                    lengthFound = 2;
            }
            else if (SampleBytes.Length > currentPos + 1
                     && SampleBytes[currentPos] == 0xCB
                )
            {
                if (SampleBytes[currentPos + 1] == 0x86
                    || SampleBytes[currentPos + 1] == 0x9C
                    )
                    lengthFound = 2;
            }
            else if (SampleBytes.Length > currentPos + 2
                     && SampleBytes[currentPos] == 0xE2
                )
            {
                if (SampleBytes[currentPos + 1] == 0x80)
                {
                    if (SampleBytes[currentPos + 2] == 0x93
                        || SampleBytes[currentPos + 2] == 0x94
                        )
                        lengthFound = 3;
                    if (SampleBytes[currentPos + 2] == 0x98
                        || SampleBytes[currentPos + 2] == 0x99
                        || SampleBytes[currentPos + 2] == 0x9A
                        )
                        lengthFound = 3;
                    if (SampleBytes[currentPos + 2] == 0x9C
                        || SampleBytes[currentPos + 2] == 0x9D
                        || SampleBytes[currentPos + 2] == 0x9E
                        )
                        lengthFound = 3;
                    if (SampleBytes[currentPos + 2] == 0xA0
                        || SampleBytes[currentPos + 2] == 0xA1
                        || SampleBytes[currentPos + 2] == 0xA2
                        )
                        lengthFound = 3;
                    if (SampleBytes[currentPos + 2] == 0xA6)
                        lengthFound = 3;
                    if (SampleBytes[currentPos + 2] == 0xB0)
                        lengthFound = 3;
                    if (SampleBytes[currentPos + 2] == 0xB9
                        || SampleBytes[currentPos + 2] == 0xBA
                        )
                        lengthFound = 3;
                }
                else if (SampleBytes[currentPos + 1] == 0x82
                         && SampleBytes[currentPos + 2] == 0xAC
                    )
                    lengthFound = 3;
                else if (SampleBytes[currentPos + 1] == 0x84
                         && SampleBytes[currentPos + 2] == 0xA2
                    )
                    lengthFound = 3;
            }

            return lengthFound;
        }

    }
}
