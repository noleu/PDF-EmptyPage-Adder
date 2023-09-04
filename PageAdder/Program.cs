// See https://aka.ms/new-console-template for more information

using Fclp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PageAdder;

public class Program
{
    public static void Main(string[] args)
    {
        var commandLineParser = new FluentCommandLineParser<ApplicationArguments>();

        commandLineParser
            .Setup(arg => arg.PathToPdfFile)
            .As('p', "pathToPdf")
            .Required();

        var result = commandLineParser.Parse(args);

        if (result.HasErrors)
        {
            Console.WriteLine(
                "Parsing the command line arguments throw the following error. The program will be terminated");
            Console.WriteLine(result.Errors);
        }

        AddEmptyPages(commandLineParser.Object.PathToPdfFile);
    }

    private static void AddEmptyPages(string pathToPdfFile)
    {
        if (!File.Exists(pathToPdfFile))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The given file does not exist");
            Console.ResetColor();
            throw new FileNotFoundException();
        }

        PdfDocument originalPdf = PdfReader.Open(pathToPdfFile, PdfDocumentOpenMode.Import);

        PdfDocument notesPdf = new();

        notesPdf.PageLayout = PdfPageLayout.SinglePage;

        for (int i = 0; i < originalPdf.PageCount; i++)
        {
            notesPdf.AddPage(originalPdf.Pages[i]);
            notesPdf.AddPage(new PdfPage());
        }
        
        string notesFileName = pathToPdfFile.Split(".pdf")[0] + ".notes.pdf";
        
        notesPdf.Save(notesFileName);
    }
    
}