using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace C_Yassine_Faissal.ViewModels
{
    // ViewModelBase is een abstracte klasse die erft van ObservableObject.
    // Hierdoor kunnen alle ViewModels die ervan erven, automatisch de implementatie van INotifyPropertyChanged van ObservableObject gebruiken.
    public abstract class ViewModelBase : ObservableObject
    {
    }
}

