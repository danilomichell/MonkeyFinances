namespace MonkeyFinances.Core.Exception
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// Get message from Excpetion and his all InnerException messages
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>List of exception messages</returns>
        public static IEnumerable<string> GetAllMessages(this System.Exception ex)
        {
            if (ex == null)
                yield break;
            yield return ex.Message;
            var innerExceptions = Enumerable.Empty<System.Exception>();
            if (ex is AggregateException && (ex as AggregateException).InnerExceptions.Any())
                innerExceptions = (ex as AggregateException).InnerExceptions;
            else if (ex.InnerException != null)
                innerExceptions = new System.Exception[] { ex.InnerException };
            foreach (var innerEx in innerExceptions)
            {
                foreach (string msg in innerEx.GetAllMessages())
                    yield return msg;
            }
        }

        /// <summary>
        /// Get stackTrace from Excpetion and his all InnerException StackTraces
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>List of exception StackTraces</returns>
        public static IEnumerable<string> GetAllStackTraces(this System.Exception ex)
        {
            if (ex == null)
                yield break;
            if (string.IsNullOrEmpty(ex.StackTrace))
                yield break;
            yield return ex.StackTrace;
            var innerExceptions = Enumerable.Empty<System.Exception>();
            if (ex is AggregateException && (ex as AggregateException).InnerExceptions.Any())
                innerExceptions = (ex as AggregateException).InnerExceptions;
            else if (ex.InnerException != null)
                innerExceptions = new System.Exception[] { ex.InnerException };
            foreach (var innerEx in innerExceptions)
            {
                foreach (string msg in innerEx.GetAllStackTraces())
                    yield return msg;
            }
        }

        /// <summary>
        /// Get message from Excpetion and his all InnerException messages as a unique string
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>String with all exception messages</returns>
        public static string GetAllMessagesAsString(this System.Exception ex, string separator = "\n") => string.Join(separator, ex.GetAllMessages());

        /// <summary>
        /// Get stackTrace from Excpetion and his all InnerException StackTraces as a unique string
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>String with all exception StackTraces</returns>
        public static string GetAllStackTracesAsString(this System.Exception ex, string separator = "\n") => string.Join(separator, ex.GetAllStackTraces());
    }
}
