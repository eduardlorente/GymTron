namespace GymTron.App.Helpers;

public static class DateUtils
{


    public static string MonthNameInCatalan(int month)
    {
        string[] months =
        [
            "Gener", "Febrer", "Març", "Abril", "Maig", "Juny",
            "Juliol", "Agost", "Setembre", "Octubre", "Novembre", "Desembre"
        ];

        if (month < 1 || month > 12)
        {
            throw new ArgumentOutOfRangeException("El mes ha de ser un número entre 1 i 12.");
        }

        return months[month - 1];
    }
}
