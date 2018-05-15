using System.Text.RegularExpressions;

namespace LogParser
{
    public class LogRowParser
    {

        private string EntryPattern = @"^(\S+) (\S+) (\S+) \[([\w:\/]+\s[+\-]\d{4})\] ""(\S+)\s?(\S+)?\s?(\S+)?"" (\d{3}|-) (\d+|-) (\S+) ""(\S+)"" ""(.+)"" ""-""$";

        private string MatchPattern = @"(""[^""]+"")|\S+"; 

        public bool DoesEntryMatchPattern(string entry)
        {
            // learn more about match.Groups and see how both tests are true.
            Regex rgx = new Regex(EntryPattern, RegexOptions.IgnoreCase);
            Match match = rgx.Match(entry);
            return match.Success;
        }


        public LogRowElements ParseElements(string entry)
        {
            LogRowElements elements = new LogRowElements();

            Regex rgx = new Regex(MatchPattern, RegexOptions.IgnoreCase);
            var parts = rgx.Matches(entry);

            elements.PartsCount = parts.Count;
            elements.IP = parts[0].ToString();
            elements.Identity = parts[1].ToString();
            elements.UserId = parts[2].ToString();
            elements.Timestamp = parts[3].ToString();
            elements.Offset = parts[4].ToString();
            elements.RequestMessage = parts[5].ToString();
            elements.StatusCode = parts[6].ToString();
            elements.Size = parts[7].ToString();
            elements.Referer = parts[8].ToString();
            elements.URL = parts[9].ToString();
            elements.UserAgent = parts[10].ToString();
            elements.Forwarded = parts[11].ToString();

            if (parts.Count != 12)
            {
                elements.Error = true;
            }

            return elements;
        }
    }
}
