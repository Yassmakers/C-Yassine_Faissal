# C# Opdracht Bibliotheekbeheersysteem

#### Het C# Bibliotheekbeheersysteem is een WPF MVVM-applicatie ontworpen voor bibliotheekbeheerders en medewerkers. Het systeem helpt bij het beheren van items (zoals boeken, cd's, dvd's, Blu-rays, games) en auteurs in een bibliotheek. Deze handleiding zal u door de basisfuncties en bedieningselementen van de applicatie leiden.

Inhoudsopgave

- <a href="#inloggegevens" target="_new">Inloggegevens</a>
- <a href="#systeemvereisten" target="_new">Systeemvereisten</a>
- <a href="#installatie" target="_new">Installatie</a>
- <a href="#database-migratie" target="_new">Database migratie</a>
- <a href="#gebruikershandleiding" target="_new">Gebruikershandleiding</a>
  - <a href="#inloggen" target="_new">Inloggen</a>
  - <a href="#navigatie" target="_new">Navigatie</a>
    - <a href="#items-zoeken" target="_new">Items zoeken</a>
    - <a href="#items-beheren" target="_new">Items beheren</a>
    - <a href="#auteurs-beheren" target="_new">Auteurs beheren</a>
    - <a href="#gebruikeraccounts-beheren" target="_new">Gebruikeraccounts beheren</a>
- <a href="#screenshots" target="_new">screenshots</a>    
- <a href="#aanvullende-opmerkingen" target="_new">Aanvullende opmerkingen</a>


## Inloggegevens

### Gebruik de volgende inloggegevens om toegang te krijgen tot het systeem

#### (hoofdlettergevoelig) :

#### Beheerder:

    Email = janzuur@gmail.nl
    Wachtwoord: jan123

Medewerker:

    Email = Robbergen@outlook.com
    Wachtwoord: emma123

Gast:

    Email = Jopie@Akkerlaan.com
    Wachtwoord: Jopie123

Systeemvereisten

- .NET 6
- WPF
- Entity Framework Core 7

# Installatie

- Kloon de Git-repository of download en pak het zip-bestand uit.
- Open het project in Visual Studio.
- Controleer of de juiste NuGet-pakketten zijn geïnstalleerd.
- Compileer en voer het project uit --> (bekijk eerst #Database Migratie en ##Inloggegevens).

# Database migratie

Voordat u de applicatie uitvoert, moet u de database migreren. Dit zijn de stappen om dat te doen:

- Open het project in Visual Studio.
- Open de NuGet Package Manager Console via Tools > NuGet Package Manager > Package Manager Console.
- Voer het volgende commando uit:

       Add-Migration InitialCreate

  Voer daarna dit commando uit:

      Update-Database

Na het uitvoeren van de database migratie, kunt u het project opnieuw compileren en uitvoeren.

## Gebruikershandleiding

### Inloggen

- #### Open de applicatie en u ziet het inlogscherm.

- #### Voer uw e-mailadres in het veld "Email" in.

- #### Voer uw wachtwoord in het veld "Password" in.

- #### Klik op de knop "Login" om in te loggen met uw gebruikersgegevens.

- #### Als u wilt inloggen als gast, klikt u op de knop "Login as Guest". Hierdoor kunt u de applicatie gebruiken zonder in te loggen met een gebruikersaccount. Houd er echter rekening mee dat sommige functies mogelijk niet beschikbaar zijn voor gastgebruikers.

### Navigatie

- #### Zoek / Filter Optie: Alle items opzoeken op titel of auteur in de zoekbalk, daarnaast de mogelijkheid om te kunnen filteren op itemtype.
- #### Items: Beheer alle items in de bibliotheek, zoals boeken, cd's, dvd's, Blu-rays en games.

- #### Auteurs: Beheer de auteurs van items in de bibliotheek.

- #### Gebruikers: Beheer de accounts van bibliotheekbeheerders en medewerkers.

### Items Zoeken

- Tik de zoekbalk aan en toets je gewenste titel of auteurnaam in.

### Items beheren

- Klik op het tabblad 'Items' om de lijst met items te bekijken.
- Klik op 'Nieuw item' om een nieuw item aan de lijst toe te voegen.
  Vul de gegevens van het nieuwe item in, selecteer een auteur uit de vervolgkeuzelijst en klik op 'Opslaan'.

- Klik op 'Bewerken' om de gegevens van een item bij te werken en klik op 'Opslaan'.
- Klik op 'Verwijderen' om een item uit de lijst te verwijderen.

**Tik als je klaar bent op de "Refresh" knop op het beginscherm om je wijzigingen te zien.**

### Auteurs beheren

- Klik op het tabblad 'Auteurs' om de lijst met auteurs te bekijken.
- Klik op 'Nieuwe auteur' om een nieuwe auteur aan de lijst toe te voegen.
  Vul de gegevens van de nieuwe auteur in en klik op 'Opslaan'.

- Klik op 'Bewerken' om de gegevens van een auteur bij te werken en klik op 'Opslaan'.
- Klik op 'Verwijderen' om een auteur uit de lijst te verwijderen.

**Tik als je klaar bent op de "Refresh" knop op het beginscherm om je wijzigingen te zien.**

### Gebruikeraccounts beheren 

- Klik op 'Nieuwe gebruiker' om een nieuw gebruikersaccount aan te maken.
  Vul de gegevens van de nieuwe gebruiker in en klik op 'Opslaan'.

- Klik op 'Bewerken' om de gegevens van een gebruiker bij te werken en klik op 'Opslaan'.
- Klik op 'Verwijderen' om een gebruiker uit de lijst te verwijderen.

**Tik als je klaar bent op de "Refresh" knop op het beginscherm om je wijzigingen te zien.**

# Screenshots
https://gyazo.com/2f55d3fd66e36700c8c162a6eeaec998
https://gyazo.com/f37f6c7461d007fc809bc09b052466f5

# Aanvullende opmerkingen

#### - De applicatie maakt gebruik van WPF voor de gebruikersinterface en MVVM-patroon voor de architectuur.

#### - Gebruikersinteractie gebeurt via de knoppen in de zijbalk, die verschillende CRUD (Create, Read, Update, Delete) operaties uitvoeren.

#### - Comments zijn in het Nederlands gezet

#### - De applicatie maakt gebruik van Entity Framework Core 7 voor het beheren van de database en het uitvoeren van query's.

> 👨‍🎓 **Yassine Messaoudi**
>
> - S-number: s1188088
> - Email: s1188088@student.windesheim.nl
> - GitLab: [@Yassineprojects](https://github.com/Yassmakers)
