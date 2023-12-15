using TeckproJsonChanger;

// Step 1 - Add nullable property to ref schemas
var resultAddNullable = FileManager.AddNullableToJsonFile();

// Step 2 - Remove allOf property and set ref schemas directly
var resultRemoveAllOf = FileManager.RemoveAllOfPropertyFromJsonFile();

// Step 3 - Change Type of ertragsanteilEuro to string
var resultFinalFile = FileManager.ChangeTypeOfErtragsanteilEuroInJsonFile();

var changeTeckproClient = Console.ReadLine();

if (changeTeckproClient == "Y")
{
    var resultAddLogToTeckproClient = TeckproClientManager.AddLogs();

    Console.ReadLine();
}

