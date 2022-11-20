using UnityEngine;

namespace Pooffick.Bindings.ControlBindings
{
    public class ActiveBinding : ControlBinding
    {
        private readonly GameObject _control;

        public ActiveBinding(GameObject control, Bindable owner, string property) : base(owner, property)
        {
            _control = control;

            _control.SetActive((bool)_propertyInfo.GetValue(owner));

            _owner.PropertyChanged += OnPropertyChanged;
        }

        protected override void Disposing()
        {
            _owner.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName != _propertyInfo.Name)
                return;

            _control.SetActive((bool)e.NewValue);
        }
    }
}