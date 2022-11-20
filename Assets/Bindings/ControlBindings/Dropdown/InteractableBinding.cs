using TMPro;

namespace Pooffick.Bindings.ControlBindings.Dropdown
{
    public class InteractableBinding : ControlBinding
    {
        private readonly TMP_Dropdown _dropdown;

        public InteractableBinding(TMP_Dropdown dropdown, Bindable owner, string property) : base(owner, property)
        {
            _dropdown = dropdown;

            _dropdown.interactable = (bool)_propertyInfo.GetValue(owner);

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

            _dropdown.interactable = (bool)e.NewValue;
        }
    }
}