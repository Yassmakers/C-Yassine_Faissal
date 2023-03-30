// Deze class bevat een enkele property genaamd "DatabaseFileName",
// die de bestandsnaam van de database bevat.
public static class Constants
{
    public static string DatabaseFileName => "library.db";


    // Op deze manier kan de naam van de database eenvoudig worden gewijzigd,
    // in de toekomst door simpelweg de waarde van de "DatabaseFileName" property te wijzigen,
    // Zonder de rest van de code te hoeven aanpassen.
}
