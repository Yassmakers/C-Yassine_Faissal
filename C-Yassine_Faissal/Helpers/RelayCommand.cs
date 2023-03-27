using System;
using System.Windows.Input;

namespace C_Yassine_Faissal.Commands
{
    // Deze klasse implementeert ICommand en stelt ons in staat om commando's
    // uit te voeren vanuit de UI en de ViewModel.
    public class RelayCommand : ICommand, IRaiseCanExecuteChanged
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        // Constructor
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        private event EventHandler _canExecuteChanged;
        // Event dat wordt aangeroepen wanneer de CanExecute status verandert
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                _canExecuteChanged += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                _canExecuteChanged -= value;
            }
        }
        // Bepaalt of het commando kan worden uitgevoerd op basis van een parameter
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        // Voert het commando uit met de gegeven parameter
        public void Execute(object parameter) => _execute(parameter);
        // Roept het CanExecuteChanged event aan om de UI bij te werken
        public void RaiseCanExecuteChanged()
        {
            _canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public interface IRaiseCanExecuteChanged
    {
        void RaiseCanExecuteChanged();
    }
}
