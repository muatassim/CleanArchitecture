using System.Xml.Linq;
using System.Text.Json;
namespace CleanArchitecture.ApiTests.Data
{
    public static class DataHelper
    {
        public static void WriteJson(JsonDocument jsonDocument, string jsonFileName)
        {
            using var fs = new FileStream(jsonFileName, FileMode.Create, FileAccess.Write);
            using var writer = new Utf8JsonWriter(fs, new JsonWriterOptions { Indented = true });
            jsonDocument.WriteTo(writer);
        }
        public static JsonDocument ReadJson(string jsonFileName)
        {
            using var fs = new FileStream(jsonFileName, FileMode.Open, FileAccess.Read);
            return JsonDocument.Parse(fs);
        }
        static readonly string RandomStringsChar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public static string RandomString(int size, bool lowerCase = false)
        {
            var res = new Random();
            var randomString = string.Empty;
            for (var i = 0; i < size; i++)
            {
                var x = res.Next(RandomStringsChar.Length);
                randomString += RandomStringsChar[x];
            }
            if (lowerCase)
                return randomString.ToLower();
            return randomString;
        }
        public static int RandomNumber(int min, int max)
        {
            Random random = new();
            return random.Next(min, max);
        }
        public static short RandomShort()
        {
            Random random = new();
            return (short) random.Next(short.MinValue, short.MaxValue + 1); 
        }
        public static byte[] FileToByteArray(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
        // Method to convert a byte array to an image file
        public static void ByteArrayToFile(string filePath, byte[] byteArray)
        {
            File.WriteAllBytes(filePath, byteArray);
        }
        public static async Task<byte[]> UrlToByteArrayAsync(string imageUrl)
        {
            using HttpClient httpClient = new ();
            return await httpClient.GetByteArrayAsync(imageUrl);
        }
        public static byte[] HexStringToByteArray(string hexString)
        {
            if (hexString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                hexString = hexString[2..];
            }
            int length = hexString.Length;
            byte[] byteArray = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return byteArray;
        }
        public static XElement GetXElement(string elementName)
        {
            //XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
            XElement xElement = new(elementName,
                new XElement("FirstName", "John"),
                new XElement("LastName", "Doe"),
                new XElement("Email", "john.doe@example.com"),
                new XElement("PhoneNumber", "123-456-7890"),
                new XElement("Address",
                    new XElement("Street", "123 Main St"),
                    new XElement("City", "Anytown"),
                    new XElement("State", "Anystate"),
                    new XElement("ZipCode", "12345")
                ),
                new XElement("Position", "Software Developer"),
                new XElement("Department", "Engineering"),
                new XElement("HireDate", DateTime.Now.ToString("yyyy-MM-dd"))
            );
            //XDocument xDocument = new XDocument(declaration, xElement);
            return xElement;
        }
        public static decimal GenerateRandomDecimal(int maxDigitsBeforeDecimal, int digitsAfterDecimal)
        {
            Random random = new ();
          decimal multiplier = (decimal)Math.Pow(10, maxDigitsBeforeDecimal - digitsAfterDecimal);
          decimal randomDecimal = (decimal)(random.NextDouble() * (double)multiplier);
          return Math.Round(randomDecimal, digitsAfterDecimal);
        }
    }
}
