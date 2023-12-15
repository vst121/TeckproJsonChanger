namespace TeckproJsonChanger;

public static class FileManager
{
    const string refSchemaExtractFileName = "d:\\Test\\ExtractedRefSchemas.txt";

    const string jsonRawFileName = "d:\\Test\\TeckproRaw.json";
    const string jsonAddNulllableFileName = "d:\\Test\\TeckproMiddle1-AddNullable.json";
    const string jsonRemoveAllOfFileName = "d:\\Test\\TeckproMiddle2-RemoveAllOf.json";
    const string jsonFinalFileName = "d:\\Test\\TeckproFinal.json";

    const string refSchemaConst = "\"$ref\" : \"#/components/schemas/";
    const string allOfConst = "\"allOf\" : [ {";
    const string commaConst = ",";
    const string newNullableLine = "        \"nullable\": true, ";

    const string monetaryAmountLine = "\"#/components/schemas/MonetaryAmount\"";
    const string monetaryAmountNewLine = "            \"type\" : \"string\"";
    
    private static bool ExtractRefSchemaItems()
    {
        try
        {
            string? line;
            HashSet<string> refSchemas = new();

            StreamReader sr = new StreamReader(jsonRawFileName);
            StreamWriter sw = new StreamWriter(refSchemaExtractFileName);

            line = sr.ReadLine();
            while (line is not null)
            {
                if (line.Contains(refSchemaConst))
                {
                    line = line.Substring(line.LastIndexOf("/") + 1);

                    line = $"\"{ line} : {{";
                    sw.WriteLine(line);
                }

                line = sr.ReadLine();
            }

            sr.Close();
            sw.Close();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        return false;
    }

    private static HashSet<string> GetRefSchemaItems()
    {
        try
        {
            string? line;
            HashSet<string> refSchemas = new();

            StreamReader sr = new StreamReader(refSchemaExtractFileName);
            line = sr!.ReadLine();
            while (line is not null)
            {
                refSchemas.Add(line.Trim());
                line = sr.ReadLine();
            }

            sr.Close();

            Console.WriteLine($"refSchemas count : {refSchemas.Count}");

            return refSchemas;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        return null;
    }

    public static bool AddNullableToJsonFile()
    {
        try
        {
            var resExtract = ExtractRefSchemaItems();

            if (!resExtract)
            {
                throw new Exception("Extracting ref schemas encountered error!");
            }

            string? line;

            StreamReader sr = new StreamReader(jsonRawFileName);
            StreamWriter sw = new StreamWriter(jsonAddNulllableFileName);

            var refSchemas = GetRefSchemaItems();
            var foundSchema = "";

            line = sr.ReadLine();

            while (line is not null)
            {
                sw.WriteLine(line);

                foreach (var schema in refSchemas)
                {
                    if (line.Contains(schema))
                    {
                        foundSchema = schema;
                        sw.WriteLine(newNullableLine);
                        break;
                    }
                }

                refSchemas.Remove(foundSchema);

                line = sr.ReadLine();
            }

            sr.Close();
            sw.Close();

            Console.WriteLine($"refSchemas count that is not processed: {refSchemas.Count}");

            foreach (var refSchema in refSchemas)
            {
                Console.WriteLine($"ATTENTION: RefSchema that is not processed: {refSchema}");
            }

            Console.WriteLine("Add nullable property to json file was done!");

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        return false;
    }

    public static bool RemoveAllOfPropertyFromJsonFile()
    {
        try
        {
            string? line, previuosLine;

            StreamReader sr = new StreamReader(jsonAddNulllableFileName);
            StreamWriter sw = new StreamWriter(jsonRemoveAllOfFileName);

            line = sr.ReadLine();
            previuosLine = line;

            line = sr.ReadLine();

            while (line is not null)
            {
                if (line.Contains(allOfConst))
                {
                    // 1st Step - Ignore previous line that contains Type

                    // 2nd Step - Read next line that contains ref schema
                    line = sr.ReadLine();
                    sw.WriteLine(line.Substring(2));

                    // 3rd Step - Read next line that contains ref schema
                    line = sr.ReadLine();

                    if (line.Contains(commaConst))
                    {
                        line = commaConst;
                        sw.WriteLine(line);
                    }

                    line = sr.ReadLine();
                }
                else
                {
                    sw.WriteLine(previuosLine);
                }

                previuosLine = line;
                line = sr.ReadLine();
            }
            sw.WriteLine(previuosLine);

            sr.Close();
            sw.Close();

            Console.WriteLine("Remove allof property from json file was done!");

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        return false;
    }

    public static bool ChangeTypeOfErtragsanteilEuroInJsonFile()
    {
        try
        {
            string? line;

            StreamReader sr = new StreamReader(jsonRemoveAllOfFileName);
            StreamWriter sw = new StreamWriter(jsonFinalFileName);

            line = sr.ReadLine();

            while (line is not null)
            {
                if (line.Contains(monetaryAmountLine))
                {
                    line = monetaryAmountNewLine;
                }

                sw.WriteLine(line);

                line = sr.ReadLine();
            }

            sr.Close();
            sw.Close();

            Console.WriteLine("Changing ertragsanteilEuro type was done!");
            Console.WriteLine("Final json file was created!");

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }

        return false;
    }
}


