using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOFramework.Tools.ProjectEditor
{
    public enum Status
    {
        Processing,
        Completed,
        Cancelled
    }

    public class Stats
    {
        public class StatsFiles
        {
            public int Total { get; set; }

            public int Processed { get; set; }

            public int Binary { get; set; }

            public int WithMatches { get; set; }

            public int WithoutMatches { get; set; }

            public int FailedToRead { get; set; }

            public int FailedToWrite { get; set; }
        }

        public class StatsMatches
        {
            public int Found { get; set; }

            public int Replaced { get; set; }
        }

        public class StatsTime
        {
            public TimeSpan Passed { get; set; }

            public TimeSpan Remaining { get; set; }
        }

        public StatsFiles Files { get; set; }

        public StatsMatches Matches { get; set; }

        public StatsTime Time { get; set; }

        public Stats()
        {
            Files = new StatsFiles();

            Matches = new StatsMatches();

            Time = new StatsTime();
        }

        public void UpdateTime(DateTime startTime, DateTime startTimeProcessingFiles)
        {
            DateTime now = DateTime.Now;
            Time.Passed = now.Subtract(startTime);

            //Use startTimeProcessingFiles to figure out remaining time
            TimeSpan passedProcessingFiles = now.Subtract(startTimeProcessingFiles);

            double passedSeconds = passedProcessingFiles.TotalSeconds;

            int remainingFiles = Files.Total - Files.Processed;
            var remainingSeconds = (passedSeconds / Files.Processed) * remainingFiles;

            Time.Remaining = TimeSpan.FromSeconds(remainingSeconds);
        }
    }

    [Serializable]
    public class StopWatch
    {
        private static ConcurrentDictionary<string, StopWatch> _stopWatches;
        private DateTime _endTime;
        private DateTime _startTime;

        private bool _started;
        private int _stopCount;
        private TimeSpan _timeSpan;

        /// <summary>Instantiate a new instance of XStopWatch.</summary>
        public StopWatch()
        {
            Reset();
        }

        #region Class Properties

        public TimeSpan ElapsedSpan
        {
            get
            {
                if (!IsStarted())
                    throw new InvalidOperationException("Stopwatch needs to be running");

                TimeSpan timeSpan = _timeSpan.Add(DateTime.Now.Subtract(_startTime));
                return timeSpan;
            }
        }

        /// <summary>Total number of milliseconds that elapsed since start.  This does not effect the stopwatch.</summary>
        public int ElapsedMilliseconds
        {
            get
            {
                if (!IsStarted())
                    throw new InvalidOperationException("Stopwatch needs to be running");

                TimeSpan timeSpan = _timeSpan.Add(DateTime.Now.Subtract(_startTime));

                return (int)timeSpan.TotalMilliseconds;
            }
        }

        /// <summary>Total number of milliseconds that elapsed between every call to Start() method and corresponding call to Stop() method.</summary>
        public int Milliseconds
        {
            get { return (int)_timeSpan.TotalMilliseconds; }
            set { }
        }

        /// <summary>Divides milliseconds by the number of stops done thus far.</summary>
        /// <remarks>Assumes one stop has been made if none have occurred.</remarks>
        public int AverageMilliseconds
        {
            get
            {
                int stops = _stopCount;
                if (_stopCount < 1)
                    stops = 1;
                return (Milliseconds / stops);
            }
            set { }
        }

        /// <summary>Total number of times Stop() has been called.</summary>
        public int StopCount
        {
            get { return _stopCount; }
            set { _stopCount = value; }
        }

        /// <summary>The time span representation of time elapsed on this watch.  Updated when Stop() is called.</summary>
        public TimeSpan Span
        {
            get { return _timeSpan; }
            set { _timeSpan = value; }
        }

        /// <summary>The time span representation of time elapsed on this watch.  Updated when Stop() is called.</summary>
        public static ConcurrentDictionary<string, StopWatch> Collection
        {
            get { return _stopWatches; }
            set { _stopWatches = value; }
        }

        #endregion

        /// <summary>Resets the stopwatch, setting internal elapsed counter to 0.</summary>
        public void Reset()
        {
            _started = false;
            _stopCount = 0;
            _timeSpan = new TimeSpan(0);
        }

        /// <summary>Indicates if stopwatch has been started.</summary>
        /// <returns>True if stopwatch was started and false, otherwise. </returns>
        public bool IsStarted()
        {
            return _started;
        }

        /// <summary>Starts the stopwatch.</summary>
        public void Start()
        {
            _startTime = DateTime.Now;
            _started = true;
        }

        /// <summary>Stops the stopwatch.</summary>
        /// <remarks>Adds elapsed time from calling Start() to the StopWatch internal counter.</remarks>
        public void Stop()
        {
            if (!_started)
                throw new InvalidOperationException("Timer could not be stopped.  Start() function must be called before Stop().");

            _endTime = DateTime.Now;

            _timeSpan = _timeSpan.Add(_endTime.Subtract(_startTime));

            _stopCount++;

            _started = false;
        }

        //Static collection related methods
        public static void Start(string key, bool reset = false)
        {
            if (_stopWatches == null)
                _stopWatches = new ConcurrentDictionary<string, StopWatch>();

            if (!_stopWatches.ContainsKey(key))
                _stopWatches[key] = new StopWatch();

            if (reset)
                _stopWatches[key].Reset();

            _stopWatches[key].Start();
        }

        public static void Stop(string key)
        {
            _stopWatches[key].Stop();
        }

        public static void PrintCollection(int? totalMilliseconds)
        {
            var sb = new StringBuilder();

            PrintCollection(totalMilliseconds, sb);

            Console.Write(sb.ToString());
        }



        public static void PrintCollection(int? totalMilliseconds, StringBuilder sb)
        {
            sb.AppendLine("Print Stop Watches: " + DateTime.Now);
            StopWatch stopWatch;
            string line;
            decimal secs;
            decimal percent;

            foreach (string key in _stopWatches.Keys)
            {
                stopWatch = _stopWatches[key];
                secs = Math.Round((((decimal)stopWatch.Milliseconds) / 1000), 1);

                line = key + ": " + secs + " secs, " + stopWatch.Milliseconds + " millisecs";

                decimal avgDuration = Math.Round((((decimal)stopWatch.Milliseconds) / stopWatch.StopCount), 1);
                line += ", " + stopWatch.StopCount + " stops, Avg Duration " + avgDuration + " millisecs";

                if (totalMilliseconds != null)
                {
                    percent = Math.Round((stopWatch.Milliseconds / (decimal)totalMilliseconds.Value) * 100, 1);
                    line += ", " + percent + "%";
                }
                sb.AppendLine(line);
            }

            sb.AppendLine("Total: " + totalMilliseconds + " millisecs");
        }
    }

    public class MatchPreviewLineNumber
    {
        public int LineNumber { get; set; }

        public bool HasMatch { get; set; }
    }

    public class LineNumberComparer : IEqualityComparer<MatchPreviewLineNumber>
    {
        public bool Equals(MatchPreviewLineNumber x, MatchPreviewLineNumber y)
        {
            return x.LineNumber == y.LineNumber;
        }

        public int GetHashCode(MatchPreviewLineNumber obj)
        {
            return obj.LineNumber.GetHashCode();
        }
    }

    [Serializable]
    public class Verify
    {
        internal Verify()
        {
        }

        [Serializable]
        public class Argument
        {
            internal Argument()
            {
            }

            [DebuggerStepThrough]
            public static void IsNotEmpty(Guid argument, string argumentName)
            {
                if (argument == Guid.Empty)
                    throw new ArgumentException(argumentName + " cannot be empty guid.", argumentName);
            }

            [DebuggerStepThrough]
            public static void IsNotEmpty(string argument, string argumentName)
            {
                if (string.IsNullOrEmpty((argument ?? string.Empty).Trim()))
                    throw new ArgumentException(argumentName + " cannot be empty.", argumentName);
            }

            [DebuggerStepThrough]
            public static void IsWithinLength(string argument, int length, string argumentName)
            {
                if (argument.Trim().Length > length)
                    throw new ArgumentException(argumentName + " cannot be more than " + length + " characters", argumentName);
            }


            [DebuggerStepThrough]
            public static void IsNull(object argument, string argumentName)
            {
                if (argument != null)
                    throw new ArgumentException(argumentName + " must be null", argumentName);
            }


            [DebuggerStepThrough]
            public static void IsNotNull(object argument, string argumentName)
            {
                if (argument == null)
                    throw new ArgumentNullException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositiveOrZero(int argument, string argumentName)
            {
                if (argument < 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }


            [DebuggerStepThrough]
            public static void IsPositive(int argument, string argumentName)
            {
                if (argument <= 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }


            [DebuggerStepThrough]
            public static void IsPositiveOrZero(long argument, string argumentName)
            {
                if (argument < 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositive(long argument, string argumentName)
            {
                if (argument <= 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositiveOrZero(float argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsPositive(float argument, string argumentName)
            {
                if (argument <= 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositiveOrZero(decimal argument, string argumentName)
            {
                if (argument < 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositive(decimal argument, string argumentName)
            {
                if (argument <= 0)
                    throw new ArgumentOutOfRangeException(argumentName);
            }
            /*
            [DebuggerStepThrough]
            public static void IsValidDate(DateTime argument, string argumentName)
            {
                if (!argument.IsValid())
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsInFuture(DateTime argument, string argumentName)
            {
                if (argument < SystemTime.Now())
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsInPast(DateTime argument, string argumentName)
            {
                if (argument > SystemTime.Now())
                    throw new ArgumentOutOfRangeException(argumentName);
            }
            */

            [DebuggerStepThrough]
            public static void IsPositiveOrZero(TimeSpan argument, string argumentName)
            {
                if (argument < TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsPositive(TimeSpan argument, string argumentName)
            {
                if (argument <= TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException(argumentName);
            }

            [DebuggerStepThrough]
            public static void IsNotEmpty<T>(IList<T> argument, string argumentName)
            {
                IsNotNull(argument, argumentName);

                if (argument.Count == 0)
                    throw new ArgumentException("List cannot be empty.", argumentName);
            }


            [DebuggerStepThrough]
            public static void IsInRange(int argument, int min, int max, string argumentName)
            {
                if ((argument < min) || (argument > max))
                    throw new ArgumentOutOfRangeException(argumentName, argumentName + "must be between " + min + "-" + max + ".");
            }

            public static void AreEqual<T>(T expected, T actual, string argumentName)
            {
                if (!EqualityComparer<T>.Default.Equals(expected, actual))
                    throw new ArgumentOutOfRangeException(argumentName, argumentName + " must be " + expected + ", but was " + actual + ".");
            }

            public static void IsTrue(bool actual, string argumentName)
            {
                AreEqual(true, actual, argumentName);
            }

            public static void IsFalse(bool actual, string argumentName)
            {
                AreEqual(false, actual, argumentName);
            }

            public static void AreNotEqual<T>(T expected, T actual, string argumentName)
            {
                if (EqualityComparer<T>.Default.Equals(expected, actual))
                    throw new ArgumentOutOfRangeException(argumentName, argumentName + " must not be equal to " + expected + ".");
            }

            /*
           [DebuggerStepThrough]
           public static void IsNotInvalidEmail(string argument, string argumentName)
           {
               IsNotEmpty(argument, argumentName);

               if (!argument.IsEmail())
               {
                   throw new ArgumentException("\"{0}\" is not a valid email address.".FormatWith(argumentName), argumentName);
               }
           }


           [DebuggerStepThrough]
           public static void IsNotInvalidWebUrl(string argument, string argumentName)
           {
               IsNotEmpty(argument, argumentName);

               if (!argument.IsWebUrl())
               {
                   throw new ArgumentException("\"{0}\" is not a valid web url.".FormatWith(argumentName), argumentName);
               }
           }
            */

            [DebuggerStepThrough]
            public static void IsValidID(int? id, string argumentName)
            {
                IsNotNull(id, argumentName);
                IsPositive(id.Value, argumentName);
            }

        }
    }
}
