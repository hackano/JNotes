using System;
using System.Windows.Input;

namespace JNotes.Classes
{
	internal class DelegateCommand : ICommand


	{
		private readonly Func<object, bool> _canExecute;
		private readonly Action<object> _executeAction;
		private bool _canExecuteCache;

		public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
		{
			_executeAction = executeAction;
			_canExecute = canExecute;
		}

		#region ICommand Members

		public bool CanExecute(object parameter)
		{
			bool temp = _canExecute(parameter);
			if (_canExecuteCache != temp)
			{
				_canExecuteCache = temp;
				if (CanExecuteChanged != null)
				{
					CanExecuteChanged(this, new EventArgs());
				}
			}
			return _canExecuteCache;
		}


		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			_executeAction(parameter);
		}

		#endregion
	}
}