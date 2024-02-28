using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc.Formatters;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;

namespace Common.Helper
{
    public static class CommonHelper
    {
        public static object Currency { get; private set; }

        private static readonly Random random = new Random();
        private static readonly HttpClient client = new HttpClient();
        public static decimal GetDecimalValue(object inputValue, decimal defaultValue)
        {
            try
            {
                defaultValue = Convert.ToDecimal(inputValue);
            }
            catch
            {
                //Damp Error
            }
            return defaultValue;
        }
        public static long GetLongValue(string inputValue, long defaultValue = 0)
        {
            try
            {
                defaultValue = long.Parse(inputValue);
            }
            catch
            {
                //Damp Error
            }
            return defaultValue;
        }
        public static string GetStringValue(object inputValue, string defaultValue = "")
        {
            try
            {
                defaultValue = Convert.ToString(inputValue);
            }
            catch
            {
                //Damp Error
            }
            return defaultValue;
        }
        public static int GetIntegerValue(object inputValue, int defaultValue = 0)
        {
            try
            {
                defaultValue = Convert.ToInt32(inputValue);
            }
            catch
            {
                //Damp Error
            }
            return defaultValue;
        }
        public static bool GetBooleanValue(object inputValue, bool defaultValue = false)
        {
            try
            {
                defaultValue = Convert.ToBoolean(inputValue);
            }
            catch
            {
                //Damp Error
            }
            return defaultValue;
        }
        public static DateTime GetPreviousWeekDate(DateTime currentDateTime)
        {
            DateTime lastWeekDateTime = new DateTime();
            try
            {
                lastWeekDateTime = currentDateTime.AddDays(-7);
            }
            catch
            {
                //Damp Error
            }
            return lastWeekDateTime;
        }
        public static DateTime GetPreviousMonthDate(DateTime currentDateTime)
        {
            DateTime lastMonthDateTime = new DateTime();
            try
            {
                lastMonthDateTime = currentDateTime.AddMonths(-1);
            }
            catch
            {
                //Damp Error
            }
            return lastMonthDateTime;
        }
        public static DateTime GetPreviousYearDate(DateTime currentDateTime)
        {
            DateTime lastYearDateTime = new DateTime();
            try
            {
                lastYearDateTime = currentDateTime.AddYears(-1);
            }
            catch
            {
                //Damp Error
            }
            return lastYearDateTime;
        }
        public static bool DeleteFile(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        //public static string GenerateRandomPassword(PasswordOptions opts = null)
        //{
        //    if (opts == null) opts = new PasswordOptions()
        //    {
        //        RequiredLength = 8,
        //        RequiredUniqueChars = 1,
        //        RequireDigit = true,
        //        RequireLowercase = true,
        //        RequireNonAlphanumeric = true,
        //        RequireUppercase = true
        //    };

        //    string[] randomChars = new[] {
        //                                    "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
        //                                    "abcdefghijkmnopqrstuvwxyz",    // lowercase
        //                                    "0123456789",                   // digits
        //                                    "!@$?_-"                        // non-alphanumeric
        //                                 };
        //    Random rand = new Random(Environment.TickCount);
        //    List<char> chars = new List<char>();

        //    if (opts.RequireUppercase)
        //        chars.Insert(rand.Next(0, chars.Count),
        //            randomChars[0][rand.Next(0, randomChars[0].Length)]);

        //    if (opts.RequireLowercase)
        //        chars.Insert(rand.Next(0, chars.Count),
        //            randomChars[1][rand.Next(0, randomChars[1].Length)]);

        //    if (opts.RequireDigit)
        //        chars.Insert(rand.Next(0, chars.Count),
        //            randomChars[2][rand.Next(0, randomChars[2].Length)]);

        //    if (opts.RequireNonAlphanumeric)
        //        chars.Insert(rand.Next(0, chars.Count),
        //            randomChars[3][rand.Next(0, randomChars[3].Length)]);

        //    for (int i = chars.Count; i < opts.RequiredLength
        //        || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
        //    {
        //        string rcs = randomChars[rand.Next(0, randomChars.Length)];
        //        chars.Insert(rand.Next(0, chars.Count),
        //            rcs[rand.Next(0, rcs.Length)]);
        //    }

        //    return new string(chars.ToArray());
        //}
        public static string DecimalThousandSeparator(dynamic value)
        {
            try
            {
                return string.Format("{0:n}", value);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static byte[] ExportToExcel(DataTable table, string fileName)
        {
            var tempFilesDirectory = "wwwroot\\TempExcelFiles";
            if (!Directory.Exists(tempFilesDirectory))
            {
                Directory.CreateDirectory(tempFilesDirectory);
            }
            var filePath = Path.Combine(tempFilesDirectory, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);

                Row headerRow = new Row();

                List<String> columns = new List<string>();
                foreach (System.Data.DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);

                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(column.ColumnName);
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dsrow in table.Rows)
                {
                    Row newRow = new Row();
                    foreach (String col in columns)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(dsrow[col].ToString());
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }
            var byteArray = System.IO.File.ReadAllBytes(filePath);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return byteArray;
        }
        public static string SetStatementDescriptorLength(string descriptor)
        {
            var outputString = string.Empty;
            try
            {
                outputString = descriptor.Length > 22 ? descriptor.Substring(0, 21) : descriptor.Length < 5 ? $"{descriptor}-Tzedakah" : descriptor;
            }
            catch
            {
                //Damp
            }
            return outputString;
        }
        public static string Replace(string input, char[] separators)
        {
            var outputString = string.Empty;
            try
            {
                string[] temp;
                temp = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                return string.Join(outputString, temp);
            }
            catch
            {
                //Damp
            }
            return outputString;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //public static string GetLanguage(string keyword)
        //{
        //    string language = string.Empty;
        //    LanguageDetector languageDetector = new LanguageDetector();
        //    languageDetector.AddLanguages("en", "he");
        //    if (!string.IsNullOrEmpty(keyword))
        //    {
        //        language = languageDetector.Detect(keyword);
        //    }
        //    return language;
        //}
        public static byte[] ConvertHtmlToPdfByteArray(string html)
        {
            StringReader sr = new StringReader(html.ToString());
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            using MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            var attachment = memoryStream.ToArray();
            memoryStream.Close();
            return attachment;
        }
        //public static byte[] CreatePdf(string html)
        //{
        //    Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //    using MemoryStream memoryStream = new MemoryStream();
        //    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        //    document.Open();
        //    XMLWorkerHelper worker = XMLWorkerHelper.GetInstance();
        //    StringReader sr = new StringReader(html.ToString());
        //    worker.ParseXHtml(writer, document, sr);
        //    document.Close();
        //    var attachment = memoryStream.ToArray();
        //    memoryStream.Close();
        //    return attachment;
        //}
        public static byte[] CreatePdfUsingSelectHtmlToPdf(string html)
        {
            var htmlToPdf = new SelectPdf.HtmlToPdf();
            var pdfDoc = htmlToPdf.ConvertHtmlString(html);
            var pdf = pdfDoc.Save();
            pdfDoc.Close();
            return pdf;
        }
        public static DataTable ImportExcel(string filePath)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(filePath, false))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();
                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    }
                    foreach (Row row in rows) //this will also include your header row...
                    {
                        DataRow tempRow = dt.NewRow();
                        int columnIndex = 0;
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            // Gets the column index of the cell with data
                            int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cell.CellReference));
                            cellColumnIndex--; //zero based index
                            if (columnIndex < cellColumnIndex)
                            {
                                do
                                {
                                    tempRow[columnIndex] = ""; //Insert blank data here;
                                    columnIndex++;
                                }
                                while (columnIndex < cellColumnIndex);
                            }
                            tempRow[columnIndex] = GetCellValue(spreadSheetDocument, cell);

                            columnIndex++;
                        }
                        dt.Rows.Add(tempRow);
                    }
                }
                dt.Rows.RemoveAt(0); //...so i'm taking it out here.
            }
            catch
            {
            }
            return dt;
        }
        /// <summary>
        /// Given a cell name, parses the specified cell to get the column name.
        /// </summary>
        /// <param name="cellReference">Address of the cell (ie. B2)</param>
        /// <returns>Column Name (ie. B)</returns>
        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }
        /// <summary>
        /// Given just the column name (no row index), it will return the zero based column index.
        /// Note: This method will only handle columns with a length of up to two (ie. A to Z and AA to ZZ). 
        /// A length of three can be implemented when needed.
        /// </summary>
        /// <param name="columnName">Column Name (ie. A or AB)</param>
        /// <returns>Zero based index if the conversion was successful; otherwise null</returns>
        public static int? GetColumnIndexFromName(string columnName)
        {

            //return columnIndex;
            string name = columnName;
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return number;
        }
        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            if (cell.CellValue == null)
            {
                return "";
            }
            string value = cell.CellValue.InnerXml;
            return cell.DataType != null && cell.DataType.Value == CellValues.SharedString
                ? stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText
                : value;
        }
        public static string GenerateSlug(string phrase)
        {
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private static string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
        public static string GetLetterPressedFromDigits(string digits)
        {
            string letter = string.Empty;
            var characters = digits.ToCharArray();
            int twoCount = 0;
            int threeCount = 0;
            int fourCount = 0;
            int fiveCount = 0;
            int sixCount = 0;
            int sevenCount = 0;
            int eightCount = 0;
            int nineCount = 0;
            for (int index = 0; index < characters.Length; index++)
            {
                var character = characters[index].ToString();
                switch (character)
                {
                    case "2":
                        twoCount++;
                        threeCount = 0;
                        fourCount = 0;
                        fiveCount = 0;
                        sixCount = 0;
                        sevenCount = 0;
                        eightCount = 0;
                        nineCount = 0;
                        letter = twoCount switch
                        {
                            1 => "A",
                            2 => "B",
                            3 => "C",
                            _ => string.Empty,
                        };
                        break;
                    case "3":
                        twoCount = 0;
                        threeCount++;
                        fourCount = 0;
                        fiveCount = 0;
                        sixCount = 0;
                        sevenCount = 0;
                        eightCount = 0;
                        nineCount = 0;

                        letter = threeCount switch
                        {
                            1 => "D",
                            2 => "E",
                            3 => "F",
                            _ => string.Empty,
                        };
                        break;
                    case "4":
                        twoCount = 0;
                        threeCount = 0;
                        fourCount++;
                        fiveCount = 0;
                        sixCount = 0;
                        sevenCount = 0;
                        eightCount = 0;
                        nineCount = 0;
                        letter = fourCount switch
                        {
                            1 => "G",
                            2 => "H",
                            3 => "I",
                            _ => string.Empty,
                        };
                        break;
                    case "5":
                        twoCount = 0;
                        threeCount = 0;
                        fourCount = 0;
                        fiveCount++;
                        sixCount = 0;
                        sevenCount = 0;
                        eightCount = 0;
                        nineCount = 0;
                        letter = fiveCount switch
                        {
                            1 => "J",
                            2 => "K",
                            3 => "L",
                            _ => string.Empty,
                        };
                        break;
                    case "6":
                        twoCount = 0;
                        threeCount = 0;
                        fourCount = 0;
                        fiveCount = 0;
                        sixCount++;
                        sevenCount = 0;
                        eightCount = 0;
                        nineCount = 0;
                        letter = sixCount switch
                        {
                            1 => "M",
                            2 => "N",
                            3 => "O",
                            _ => string.Empty,
                        };
                        break;
                    case "7":
                        twoCount = 0;
                        threeCount = 0;
                        fourCount = 0;
                        fiveCount = 0;
                        sixCount = 0;
                        sevenCount++;
                        eightCount = 0;
                        nineCount = 0;
                        letter = sevenCount switch
                        {
                            1 => "P",
                            2 => "Q",
                            3 => "R",
                            4 => "S",
                            _ => string.Empty,
                        };
                        break;
                    case "8":
                        twoCount = 0;
                        threeCount = 0;
                        fourCount = 0;
                        fiveCount = 0;
                        sixCount = 0;
                        sevenCount = 0;
                        eightCount++;
                        nineCount = 0;
                        letter = eightCount switch
                        {
                            1 => "T",
                            2 => "U",
                            3 => "V",
                            _ => string.Empty,
                        };
                        break;
                    case "9":
                        twoCount = 0;
                        threeCount = 0;
                        fourCount = 0;
                        fiveCount = 0;
                        sixCount = 0;
                        sevenCount = 0;
                        eightCount = 0;
                        nineCount++;
                        letter = nineCount switch
                        {
                            1 => "W",
                            2 => "X",
                            3 => "Y",
                            4 => "Z",
                            _ => string.Empty,
                        };
                        break;
                    default:
                        break;
                }
            }
            return letter;
        }

        public static bool IsRequestAjax(HttpRequest request)
        {
            return request.Headers["x-requested-with"] == "XMLHttpRequest";
        }

        //public static int CheckMediaFileType(string filePath)
        //{
        //    var imageMediaExtensions = new string[] { ".PNG", ".JPG", ".JPEG", ".GIF" };
        //    var videoMediaExtensions = new string[] { ".MP4", ".MKV", ".AVI" };
        //    if (Array.IndexOf(imageMediaExtensions, Path.GetExtension(filePath).ToUpperInvariant()) != -1)
        //    {
        //        return (int)MediaType.Image;
        //    }
        //    else
        //    {
        //        return Array.IndexOf(videoMediaExtensions, Path.GetExtension(filePath).ToUpperInvariant()) != -1 ? (int)MediaType.Video : 0;
        //    }
        //}
        public static bool ValidateCreditCardNumber(string creditCardNumber)
        {
            Regex regex = new Regex(@"^\d{12,16}$");
            return regex.IsMatch(creditCardNumber);
        }
        public static DateTime GetPreviousthirtyDaysDate(DateTime currentDateTime)
        {
            DateTime lastThirtyDaysDate = new DateTime();
            try
            {
                lastThirtyDaysDate = currentDateTime.AddDays(-30);
            }
            catch
            {
                //Damp Error
            }
            return lastThirtyDaysDate;
        }
        public static DateTime GetPreviousSixtyDaysDate(DateTime currentDateTime)
        {
            DateTime lastSixtyDaysDate = new DateTime();
            try
            {
                lastSixtyDaysDate = currentDateTime.AddDays(-60);
            }
            catch
            {
                //Damp Error
            }
            return lastSixtyDaysDate;
        }
        public static DateTime GetPreviousNinetyDaysDate(DateTime currentDateTime)
        {
            DateTime lastNinetyDaysDate = new DateTime();
            try
            {
                lastNinetyDaysDate = currentDateTime.AddDays(-90);
            }
            catch
            {
                //Damp Error
            }
            return lastNinetyDaysDate;
        }
        public static string GetFormattedDateTime(DateTime statDate, DateTime endDate)
        {
            string formattedDateTime = string.Empty;
            TimeSpan span = endDate.Subtract(statDate);

            if (span.Days > 365)
            {
                int years = Convert.ToInt32(span.Days / 365);
                if (years == 1)
                {
                    formattedDateTime = "1 year ago";
                }
                else
                {
                    formattedDateTime = $"{years} years ago";
                }
            }
            else if (span.Days > 30 && span.Days <= 365)
            {
                int months = Convert.ToInt32(span.Days / 31);
                if (months == 1)
                {
                    formattedDateTime = "1 month ago";
                }
                else
                {
                    formattedDateTime = $"{months} months ago";
                }
            }
            else if (span.Days > 0 && span.Days <= 30)
            {
                if (span.Days <= 1)
                {
                    formattedDateTime = $"{span.Days} day ago";
                }
                else
                {
                    formattedDateTime = $"{span.Days} days ago";
                }
            }
            else if (span.Hours > 0 && span.Hours <= 24)
            {
                if (span.Hours <= 1)
                {
                    formattedDateTime = $"{span.Hours} hour ago";
                }
                else
                {
                    formattedDateTime = $"{span.Hours} hours ago";
                }
            }
            else if (span.Minutes > 0 && span.Minutes <= 59)
            {
                if (span.Minutes <= 1)
                {
                    formattedDateTime = $"{span.Minutes} minute ago";
                }
                else
                {
                    formattedDateTime = $"{span.Minutes} minutes ago";
                }
            }
            else if (span.Minutes <= 0)
            {
                formattedDateTime = "Just Now";
            }
            return formattedDateTime;
        }
        public static string StripHtmlTags(string htmlTags)
        {
            return !string.IsNullOrEmpty(htmlTags) ? Regex.Replace(htmlTags, "<.*?>|&.*?;", string.Empty) : string.Empty;
        }

        public static async Task<decimal> GetExchangedValue(string baseUrl, string key, string symbolCurrency, byte? currencyType)
        {
            decimal exchangedRate = 1;
            if (currencyType != null && currencyType != (int) Currency)
        {
            //HttpResponseMessage response = await client.GetAsync($"{baseUrl}?access_key={key}&base={(Currency)currencyType}&symbols={symbolCurrency}");
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}?access_key={key}&from={symbolCurrency}&to=USD&amount=1");
            string json = await response.Content.ReadAsStringAsync();
            var exchangedRates = JsonConvert.DeserializeObject<ExchangedRatesHelper>(json);
            exchangedRate = (decimal)exchangedRates.result;
        }
            return exchangedRate;
        }

    public static decimal GetExchangedAmount(decimal exchangedRate, decimal amount)
        {
            return exchangedRate * amount;
        }

        public static String TranslateLanguages(string word, string fromLanguage, string toLanguage)
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }

        public static CultureInfo[] GetAllCountryLanguages()
        {
            try
            {
                CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
                return cinfo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string CopyDirectoryFiles(string sourcePath, string targetPath)
        {
            string fileName = string.Empty;
            string destFile = string.Empty;
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            // To copy all the files in one directory to another directory. 
            // Get the files in the source folder. (To recursively iterate through 
            // all subfolders under the current directory, see 
            // "How to: Iterate Through a Directory Tree.")
            // Note: Check for target path was performed previously 
            //       in this code example. 
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist. 
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }
            return destFile;
        }
        public static string CopyFiles(string sourcePath, string targetPath, string fileName)
        {
            string destFile = string.Empty;
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            destFile = System.IO.Path.Combine(targetPath, fileName);
            System.IO.File.Copy(sourcePath, destFile, true);
            return destFile;
        }
        public static bool ValidateEmail(string email)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isEmail;
        }



    }
}
