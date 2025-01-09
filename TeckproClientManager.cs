namespace TeckproJsonChanger;

public static class TeckproClientManager
{
    const string teckproClientRawFileName = "C:\\DotnetProjects8\\Test\\TeckproClientRaw.cs";
    const string teckproClientFinalFileName = "C:\\DotnetProjects8\\Test\\TeckproClient.cs";

    const string logger1Line = "                    LoggerService.Verbose(LoggerService.GetCallerInformation(), \"Entering method\");";
    const string logger2Line = "                    LoggerService.Verbose(LoggerService.GetCallerInformation(), \"json is: \" + json_);";

    const string StringFromUtf8ByteArray1Line = "                    #if DEBUG";
    const string StringFromUtf8ByteArray2Line = "                    var json__ = Encoding.UTF8.GetString(json_);";
    const string StringFromUtf8ByteArray3Line = "                    #endif";

    const string jsonPhraseConst = "var json_ =";

    public static bool AddGetStringFromUtf8ByteArray()
    {
        try
        {
            string? line;

            StreamReader sr = new StreamReader(teckproClientRawFileName);
            StreamWriter sw = new StreamWriter(teckproClientFinalFileName);

            line = sr.ReadLine();

            while (line is not null)
            {
                sw.WriteLine(line);

                if (line.Contains(jsonPhraseConst))
                {
                    sw.WriteLine();
                    sw.WriteLine(StringFromUtf8ByteArray1Line);
                    sw.WriteLine(StringFromUtf8ByteArray2Line);
                    sw.WriteLine(StringFromUtf8ByteArray3Line);
                    sw.WriteLine();
                }

                line = sr.ReadLine();
            }

            sr.Close();
            sw.Close();

            Console.WriteLine("StringFromUtf8ByteArray part added to the Client!");
            Console.WriteLine("Final TeckproClient was created!");

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        return false;
    }
}


