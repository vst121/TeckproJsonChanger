namespace TeckproJsonChanger;

public static class TeckproClientManager
{
    const string teckproClientRawFileName = "d:\\Test\\TeckproClientRaw.cs";
    const string teckproClientFinalFileName = "d:\\Test\\TeckproClient.cs";

    const string logger1Line = "                    _loggerService!.Verbose(\"Entering method\");";
    const string logger2Line = "                    _loggerService!.Verbose(\"json is: \" + json_);";

    const string jsonPhraseConst = "var json_ =";
    

    public static bool AddLogs()
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
                    sw.WriteLine(logger1Line);
                    sw.WriteLine(logger2Line);
                    sw.WriteLine();
                }

                line = sr.ReadLine();
            }

            sr.Close();
            sw.Close();

            Console.WriteLine("Logs added to the Client!");
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


