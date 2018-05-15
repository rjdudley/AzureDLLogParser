using System;
using Microsoft.Analytics.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogParser
{
    [SqlUserDefinedExtractor(AtomicFileProcessing = true)]
    public class LogFileReader : IExtractor

    {
        private Encoding _encoding;
        private byte[] _row_delim;

        public LogFileReader(Encoding encoding, string row_delim)
        {
            this._encoding = ((encoding == null) ? Encoding.UTF8 : encoding);
            this._row_delim = this._encoding.GetBytes(row_delim);
        }

        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            string line;
            //Read the input line by line
            foreach (Stream current in input.Split(_row_delim))
            {
                using (StreamReader streamReader = new StreamReader(current, this._encoding))
                {
                    line = streamReader.ReadToEnd().Trim();

                    LogRowParser splitter = new LogRowParser();
                    LogRowElements parts = new LogRowElements();

                    parts = splitter.ParseElements(line);

                    output.Set<string>(0, parts.IP);
                    output.Set<string>(1, parts.Identity);
                    output.Set<string>(2, parts.UserId);
                    output.Set<string>(3, parts.Timestamp);
                    output.Set<string>(4, parts.Offset);
                    output.Set<string>(5, parts.RequestMessage);
                    output.Set<string>(6, parts.StatusCode);
                    output.Set<string>(7, parts.Size);
                    output.Set<string>(8, parts.Referer);
                    output.Set<string>(9, parts.URL);
                    output.Set<string>(10, parts.UserAgent);
                    output.Set<string>(11, parts.Forwarded);

                    yield return output.AsReadOnly();
                }
            }
        }
    }
}