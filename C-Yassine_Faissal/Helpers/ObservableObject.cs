using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// Het is niet direct gerelateerd aan de projectvereisten, maar het helpt bij het
// maken van een betere en meer responsieve gebruikersinterface.
// Deze klasse is een basis klasse voor alle ViewModels en implementeert
// INotifyPropertyChanged. Dit zorgt ervoor dat de UI automatisch wordt
// bijgewerkt als de onderliggende data verandert

public abstract class ObservableObject : INotifyPropertyChanged
{    // PropertyChanged event voor het bijwerken van de UI
    public event PropertyChangedEventHandler PropertyChanged;

    // Deze methode wordt aangeroepen wanneer een property verandert
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Deze methode stelt een nieuwe waarde in voor een property en roept
    // OnPropertyChanged aan als de waarde is veranderd
    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
