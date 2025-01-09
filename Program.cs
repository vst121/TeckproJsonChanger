using TeckproJsonChanger;

// Step 1 - Remove Examples with ref
var resultRemoveExamples = FileManager.RemoveExamplesWithRefFromJsonFile();

// Step 2 - Add nullable property to ref schemas
var resultAddNullable = FileManager.AddNullableToJsonFile();

// Step 3 - Remove allOf property and set ref schemas directly
var resultRemoveAllOf = FileManager.RemoveAllOfPropertyFromJsonFile();

// Step 4 - Change Type of ertragsanteilEuro to string
var resultFinalFile = FileManager.ChangeTypeOfErtragsanteilEuroInJsonFile();

Console.WriteLine("Do you want to add GetStringFromUtf8ByteArray to your TeckproClientRaw?");
var changeTeckproClient = Console.ReadLine();

if (changeTeckproClient == "Y")
{
    var resultAddLogToTeckproClient = TeckproClientManager.AddGetStringFromUtf8ByteArray();

    Console.ReadLine();
}

