using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace FOFramework.Tools.ProjectEditor
{
    public class RemoveBinding
    {
        public void Process()
        {
            var sccFilesToDelete = new List<string>();
            var projFilesToModify = new List<string>();
            var slnFilesToModify = new List<string>();

            

            var files = new List<string>(System.IO.Directory.GetDirectories(@"C:\xGeneratorOutput\Temp\", "*", System.IO.SearchOption.AllDirectories));

            foreach (var filename in files)
            {
                string normalizedFilename = filename.ToLower();
                if (normalizedFilename.Contains(".") && normalizedFilename.EndsWith("proj") && !normalizedFilename.EndsWith("vdproj"))
                {
                    projFilesToModify.Add(filename);
                }
                else if (normalizedFilename.EndsWith(".sln"))
                {
                    slnFilesToModify.Add(filename);
                }
                else if (normalizedFilename.EndsWith(".vssscc") || normalizedFilename.EndsWith(".vspscc"))
                {
                    sccFilesToDelete.Add(filename);
                }
                else
                {
                    // do nothing
                }
            }

            if ((projFilesToModify.Count + slnFilesToModify.Count + sccFilesToDelete.Count < 1))
            {
                Console.WriteLine(@"No files to modify or delete. Exiting.");
                return;
            }

            ProcessFile(ModifySolutionFile, slnFilesToModify);
            ProcessFile(ModifyProjectFile, projFilesToModify);
            ProcessFile(DeleteFile, sccFilesToDelete);

            Console.WriteLine(@"Done.");
        }

        public static void ModifySolutionFile(string filename)
        {
            if (!filename.ToLower().EndsWith(".sln"))
            {
                throw new ArgumentException("Internal Error: ModifySolutionFile called with a file that is not a solution");
            }

            Console.WriteLine(@"Modifying Solution: {0}", filename);

            // Remove the read-only flag
            var originalAttr = File.GetAttributes(filename);
            File.SetAttributes(filename, FileAttributes.Normal);

            var outputLines = new List<string>();

            bool inSourcecontrolSection = false;

            Encoding encoding;
            var lines = ReadAllLines(filename, out encoding);

            foreach (string line in lines)
            {
                var lineTrimmed = line.Trim();

                // lines can contain separators which interferes with the regex
                // escape them to prevent regex from having problems
                var lineTrimmedForRegex = Uri.EscapeDataString(lineTrimmed);

                if (lineTrimmed.StartsWith("GlobalSection(SourceCodeControl)")
                    || lineTrimmed.StartsWith("GlobalSection(TeamFoundationVersionControl)")
                    || System.Text.RegularExpressions.Regex.IsMatch(lineTrimmedForRegex, @"GlobalSection\(.*Version.*Control", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    // this means we are starting a Source Control Section
                    // do not copy the line to output
                    inSourcecontrolSection = true;
                }
                else if (inSourcecontrolSection && lineTrimmed.StartsWith("EndGlobalSection"))
                {
                    // This means we were Source Control section and now see the ending marker
                    // do not copy the line containing the ending marker 
                    inSourcecontrolSection = false;
                }
                else if (lineTrimmed.StartsWith("Scc"))
                {
                    // These lines should be ignored completely no matter where they are seen
                }
                else
                {
                    // No handle every other line
                    // Basically as long as we are not in a source control section
                    // then that line can be copied to output
                    if (!inSourcecontrolSection)
                    {
                        outputLines.Add(line);
                    }
                }
            }

            // Write the file back out
            File.WriteAllLines(filename, outputLines, encoding);

            // Restore the original file attributes
            File.SetAttributes(filename, originalAttr);
        }

        public static void ModifyProjectFile(string filename)
        {
            if (!filename.ToLower().EndsWith("proj"))
            {
                throw new ArgumentException("Internal Error: ModifyProjectFile called with a file that is not a project");
            }

            Console.WriteLine(@"Modifying Project : {0}", filename);

            // Load the Project file
            XDocument doc;
            Encoding encoding = new UTF8Encoding(false);
            using (StreamReader reader = new StreamReader(filename, encoding))
            {
                doc = XDocument.Load(reader);
                encoding = reader.CurrentEncoding;
            }

            // Modify the Source Control Elements
            RemoveSccElementsAttributes(doc.Root);

            // Remove the read-only flag
            var originalAttr = File.GetAttributes(filename);
            File.SetAttributes(filename, FileAttributes.Normal);

            //if the original document doesn't include the encoding attribute 
            //in the declaration then do not write it to the outpu file.
            if (doc.Declaration == null || String.IsNullOrEmpty(doc.Declaration.Encoding))
                encoding = null;

            //else if its not utf (i.e. utf-8, utf-16, utf32) format which use a BOM
            //then use the encoding identified in the XML file.
            else if (!doc.Declaration.Encoding.StartsWith("utf", StringComparison.OrdinalIgnoreCase))
                encoding = Encoding.GetEncoding(doc.Declaration.Encoding);

            // Write out the XML
            using (var writer = new XmlTextWriter(filename, encoding))
            {
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
                writer.Close();
            }

            // Restore the original file attributes
            File.SetAttributes(filename, originalAttr);
        }

        private static void RemoveSccElementsAttributes(XElement el)
        {
            el.Elements().Where(x => x.Name.LocalName.StartsWith("Scc")).Remove();
            el.Attributes().Where(x => x.Name.LocalName.StartsWith("Scc")).Remove();

            foreach (var child in el.Elements())
            {
                RemoveSccElementsAttributes(child);
            }
        }

        public static void DeleteFile(string filename)
        {
            File.SetAttributes(filename, FileAttributes.Normal);
            File.Delete(filename);
        }

        /// <summary>
        /// Reads all the lines from a test file into an array.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <param name="encoding">The file encoding.</param>
        /// <returns>A string array containing all the lines from the file</returns>
        /// <remarks>UTF-8 encoded files optionally include a byte order mark (BOM) at the beginning of the file.
        /// If the mark is detected by the StreamReader class, it will modify it's encoding property so that it
        /// reflects that file was written with a BOM. However, if no BOM is detected the StreamReader will not
        /// modify it encoding property. The determined UTF-8 encoding (UTF-8 with BOM or UTF-8 without BOM) is
        /// returned as an output parameter.
        /// </remarks>
        private static string[] ReadAllLines(string path, out Encoding encoding)
        {
            List<string> lines = new List<string>();

            Encoding encodingNoBom = new UTF8Encoding(false);
            using (StreamReader reader = new StreamReader(path, encodingNoBom))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }

                encoding = reader.CurrentEncoding;
            }

            return lines.ToArray();
        }

        /// <summary>
        /// Processes a list of files based on the porcessing method.
        /// </summary>
        /// <param name="processMethod">The method for processing the files.</param>
        /// <param name="files">The list of files.</param>
        private static void ProcessFile(Action<string> processMethod, IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                try
                {
                    processMethod(file);
                }
                catch (Exception e)
                {
                    string message = String.Format("Unable to process {0}: {1}", file, e.Message);
                    WriteLine(ConsoleColor.Red, message);
                }
            }
        }

        /// <summary>
        /// Writes a line to console in the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">The foreground color.</param>
        /// <param name="value">The value that is written to the console.</param>
        private static void WriteLine(ConsoleColor foregroundColor, string value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }
    }
}