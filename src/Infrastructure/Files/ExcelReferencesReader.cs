using Azure.Storage.Blobs;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using OfficeOpenXml;
using SmartRef.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace SmartRef.Infrastructure.Files;

public class ExcelReferencesReader
{
    private string connectionString;
    private string containerName;
    private const string EXCEL_NAME = "ReferenceOverview.xlsx";

    private readonly ExcelWorksheet _sheet;
    private const string FOOTER_PPTX = "DO NEW | DO BETTER  |  DO MORE  |  DO RIGHT ";

    public ExcelReferencesReader(IConfiguration config)
    {
        this.connectionString = config.GetValue<string>("ContainerConnection");
        this.containerName = config.GetValue<string>("ContainerName");
        BlobServiceClient blobServiceClient = new(this.connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(this.containerName);
        BlobClient blobClient = containerClient.GetBlobClient(EXCEL_NAME);
        MemoryStream memoryStream = new();
        blobClient.DownloadTo(memoryStream);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        ExcelPackage excel = new(memoryStream);
        _sheet = excel.Workbook.Worksheets.Count == 0
            ? throw new InvalidOperationException("A problem appear : excel is empty because of 'Sequence contains no elements'")
            : excel.Workbook.Worksheets.First();
    }

    public List<Vertical> GetVerticals() // E
    {
        List<Vertical> verticals = new();

        ExcelRange cells = _sheet.Cells["E2:E113"];
        foreach (var name in GetCellsTextFromExcelRange(cells))
            verticals.Add(new Vertical(name));

        return verticals;
    }

    public List<Sector> GetSectorsByVertical(Vertical vertical) // F
    {
        List<Sector> sectors = new();

        ExcelRange cells = _sheet.Cells["E2:F113"];
        foreach (var name in GetCellsSectorsTextByVerticalFromExcelRange(cells, vertical.Name))
            sectors.Add(new Sector(name, vertical.Id));

        return sectors;
    }

    public List<Customer> GetCustomersBySector(Sector sector) // A
    {
        List<Customer> customers = new();

        foreach (var name in GetCellsCustomersTextBySectors(sector.Name))
            customers.Add(new Customer(name, sector.VerticalId, sector.Id));

        return customers;
    }

    public List<Contact> GetContacts() // O-P
    {
        List<Contact> contacts = new();

        ExcelRange cells = _sheet.Cells["O2:P113"];
        foreach (var name in GetCellsTextFromExcelRange(cells))
            contacts.Add(new Contact(name));

        return contacts;
    }

    public List<Technology> GetTechnologies() // M
    {
        List<Technology> technologies = new();

        ExcelRange cells = _sheet.Cells["M2:M113"];
        foreach (var name in GetCellsTextFromExcelRange(cells))
            technologies.Add(new Technology(name));

        return technologies;
    }

    public List<Country> GetCountries() // N
    {
        List<Country> countries = new();

        ExcelRange cells = _sheet.Cells["N2:N113"];
        foreach (var initial in GetCellsTextFromExcelRange(cells))
        {
            string name = GetCorrespondingName(initial);
            countries.Add(new Country(name, initial));
        }
        return countries;
    }

    public List<Project> GetProjectsByCustomer(Customer customer)
    {
        List<Project> projects = new();

        foreach (var cell in _sheet.Cells["A2:A113"])
        {
            var cellText = cell.Text;
            if (!string.IsNullOrWhiteSpace(cellText) && cellText.Equals(customer.Name))
            {
                string projectNameCellAddress = "D" + cell.Address.Split('A')[1];
                string projectEndDateCellAddress = "I" + cell.Address.Split('A')[1];
                string priceCellAddress = "L" + cell.Address.Split('A')[1];
                string isPublicCellAddress = "J" + cell.Address.Split('A')[1];

                string endDateText = _sheet.Cells[projectEndDateCellAddress].Text;

                string name = _sheet.Cells[projectNameCellAddress].Text;
                int endYear = !string.IsNullOrWhiteSpace(endDateText) ? GetCorrespondingYear(endDateText) : 2150;
                bool isFinished = !string.IsNullOrWhiteSpace(endDateText);
                decimal price = decimal.Parse(_sheet.Cells[priceCellAddress].Text);
                bool isPublic = _sheet.Cells[isPublicCellAddress].Text.Equals("Public");

                string pptxCellAddress = "B" + cell.Address.Split('A')[1];
                Uri hyperlink = _sheet.Cells[pptxCellAddress].Hyperlink;
                string[]? dataExtracted = ExtractDataFromPowerPoint(hyperlink);
                string? narative = ExtractNarativeFromComment(hyperlink);

                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (dataExtracted != null)
                    {
                        projects.Add(new Project(name, endYear, isFinished, price, 0, dataExtracted[1], dataExtracted[0], dataExtracted[2], narative,
                            isPublic, null, customer.Id));
                    }
                    else
                    {
                        projects.Add(new Project(name, endYear, isFinished, price, 0, narative, isPublic,
                            null, customer.Id));
                    }
                }
            }
        }
        return projects;
    }

    public bool IsProjectLinkWith(string projectName, string otherName, string columnLetter)
    {
        foreach (var cell in _sheet.Cells["D2:D113"])
        {
            var cellText = cell.Text;
            if (!string.IsNullOrWhiteSpace(cellText) && cellText.Equals(projectName))
            {
                string tribeOfferCellAddress = columnLetter + cell.Address.Split('D')[1];
                foreach (var tribeOfferText in GetCellsTextFromExcelRange(_sheet.Cells[tribeOfferCellAddress]))
                {
                    if (!string.IsNullOrWhiteSpace(tribeOfferText) && tribeOfferText.Equals(otherName))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private static List<string> GetCellsTextFromExcelRange(ExcelRange cells) // in the Excel file, the caracter ',' is always the separator
    {
        List<string> cellsName = new();

        foreach (var cell in cells)
        {
            string[] texts = cell.Text.Split(',');
            foreach (var text in texts)
            {
                if (!string.IsNullOrWhiteSpace(text) && !cellsName.Contains(text))
                {
                    cellsName.Add(text);
                }
            }
        }
        return cellsName;
    }

    private static List<string> GetCellsSectorsTextByVerticalFromExcelRange(ExcelRange cells, string verticalName)
    {
        List<string> sectorsName = new();

        foreach (var cell in cells)
        {
            var cellText = cell.Text;
            string[] adresseSplitted = cell.Address.Split('E', 'F');
            if (!string.IsNullOrWhiteSpace(cellText) && cellText.Equals(verticalName) && cell.Address.Equals("E" + adresseSplitted[1]))
            {
                string nextCellAdresse = "F" + adresseSplitted[1];
                string[] texts = cells[nextCellAdresse].Text.Split(',');
                foreach (var text in texts)
                {
                    if (!sectorsName.Contains(text) && !string.IsNullOrWhiteSpace(text))
                    {
                        sectorsName.Add(text);
                    }
                }
            }
        }
        return sectorsName;
    }

    private List<string> GetCellsCustomersTextBySectors(string sectorName)
    {
        List<string> customersName = new();

        foreach (var cell in _sheet.Cells["F2:F113"]) // sectors name
        {
            var cellText = cell.Text;
            if (!string.IsNullOrWhiteSpace(cellText) && cellText.Equals(sectorName))
            {
                string customerCellAdresse = "A" + cell.Address.Split('F')[1]; // customers name
                string customerCellText = _sheet.Cells[customerCellAdresse].Text;
                if (!customersName.Contains(customerCellText) && !string.IsNullOrWhiteSpace(customerCellText))
                {
                    customersName.Add(customerCellText);
                }
            }
        }
        return customersName;
    }

    private static string GetCorrespondingName(string initial) // based on ISO Country Codes
    {
        return initial switch
        {
            "BE" => "Belgium",
            "LS" => "Life Sciences",
            "LU" => "Luxembourg",
            "NL" => "Netherlands",
            "FR" => "France",
            "CH" => "Switzerland",
            "RU" => "Russian Federation",
            "TN" => "Tunisia",
            "MA" => "Morocco",
            "MU" => "Mauritius",
            "ES" => "Spain",
            _ => "Undifined",
        };
    }

    private static int GetCorrespondingYear(string endDate)
    {
        string[] texts = endDate.Split('-');
        if(texts.Length != 3 || string.IsNullOrWhiteSpace(texts[2])){ 
            return 2150; 
        };
        string yearText = "20" + texts[2];
        return int.Parse(yearText);
    }

    // POWERPOINT PART

    private string[]? ExtractDataFromPowerPoint(Uri hyperlink)
    {
        try
        {
            SlidePart slidePart = GetSlidePart(hyperlink);
            return GetAllTextsByParagraphInSlide(slidePart);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private string? ExtractNarativeFromComment(Uri hyperlink)
    {
        try
        {
            SlidePart slidePart = GetSlidePart(hyperlink);
            return GetNarativeComment(slidePart);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private SlidePart? GetSlidePart(Uri hyperlink)
    {
        try
        {
            //Get the name of the PowerPoint
            string pptName = "";
            string hyperlinkText = hyperlink.ToString();
            if (hyperlinkText.First() == 'R' && hyperlinkText.Last() == 'x')
            {
                if (hyperlinkText.Contains("%20"))
                {
                    hyperlinkText = hyperlinkText.Replace("%20", " ");
                }
                string[] hyperlinkSplitted = hyperlinkText.Split("Ref_");
                pptName = hyperlinkSplitted[1];
            }
            else
            {
                string[] hyperlinkSplittedOnce = hyperlinkText.Split("FRef_");
                string[] hyperlinkSplittedTwice = hyperlinkSplittedOnce[1].Split("&baseUrl");
                pptName = hyperlinkSplittedTwice[0];
            }

            // Open ppt
            pptName = "Ref_" + pptName;
            BlobServiceClient blobServiceClient = new(this.connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(this.containerName);
            BlobClient blobClient = containerClient.GetBlobClient(pptName);
            MemoryStream memoryStream = new();
            blobClient.DownloadTo(memoryStream);
            PresentationDocument presentationDocument = PresentationDocument.Open(memoryStream, false);

            // Access the slidepart
            PresentationPart? presentationPart = presentationDocument.PresentationPart;
            Presentation presentation = presentationPart.Presentation;
            var slideIds = presentation.SlideIdList.ChildElements;
            string? slidePartRelationshipId = (slideIds[0] as SlideId).RelationshipId;

            // return SlidePart used to read pptx 
            return (SlidePart)presentationPart.GetPartById(slidePartRelationshipId);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static string? GetNarativeComment(SlidePart slidePart)
    {
        string text = "";

        if (slidePart.Slide != null && slidePart.NotesSlidePart != null)
        {
            foreach (TextBody textBody in slidePart.NotesSlidePart.NotesSlide.Descendants<TextBody>()) // the text area
            {
                if (!string.IsNullOrWhiteSpace(textBody.InnerText)
                    && textBody.InnerText.Length > 0
                    && !textBody.InnerText.Equals("1"))
                {
                    foreach (var paragraph in textBody.Descendants<DocumentFormat.OpenXml.Drawing.Paragraph>()) // one line of text
                    {
                        text += paragraph.InnerText;
                    }
                }
            }
        }
        return text.Length > 0 ? text : null; ;
    }

    private static string[]? GetAllTextsByParagraphInSlide(SlidePart slidePart)
    {
        string[] texts = new string[3];
        int textBodyNumber = 0;
        int cpt = 0; // to don't set the delimiter on the first 

        if (slidePart.Slide != null)
        {
            foreach (TextBody textBody in slidePart.Slide.Descendants<TextBody>()) // the text area
            {
                if (!string.IsNullOrWhiteSpace(textBody.InnerText)
                    && textBody.InnerText.Length > 0
                    && !slidePart.Slide.Descendants<TextBody>().Last().InnerText.Equals(textBody.InnerText) // the title
                    && !textBody.InnerText.Equals("1")
                    && !textBody.InnerText.Equals(FOOTER_PPTX))
                {
                    foreach (var paragraph in textBody.Descendants<DocumentFormat.OpenXml.Drawing.Paragraph>()) // one line of text
                    {
                        if (cpt == 0)
                        {
                            texts[textBodyNumber] += paragraph.InnerText;
                            ++cpt;
                        }
                        else
                        {
                            texts[textBodyNumber] += '\n' + paragraph.InnerText;
                        }
                    }
                    cpt = 0;
                    ++textBodyNumber;
                }
            }
        }
        return texts.Length > 0 ? texts : null;
    }

}