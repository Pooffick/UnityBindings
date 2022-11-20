using UnityEngine.EventSystems;

namespace Pooffick.Bindings.ControlBindings
{
    public class EnabledBinding : ControlBinding
    {
        private readonly UIBehaviour _control;

        public EnabledBinding(UIBehaviour control, Bindable owner, string property) : base(owner, property)
        {
            _control = control;

            _control.enabled = (bool)_propertyInfo.GetValue(owner);

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

            _control.enabled = (bool)e.NewValue;
        }
    }
}