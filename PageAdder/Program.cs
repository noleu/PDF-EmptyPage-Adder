// See https://aka.ms/new-console-template for more information

using Fclp;

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
    }   
}
 