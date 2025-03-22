using Questao2.Api;
using Questao2.Services;

public class Program
{
    public static void Main()
    {
        ServicesBuilder.BuildHost("https://jsonmock.hackerrank.com/api/");

        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string teamName, int year)
    {
        var api = ServicesBuilder.GetService<IResultadoPartidasApi>();
        var result = api.GetResultadosPorTime(teamName, year).GetAwaiter().GetResult();
        return result;
    }

}