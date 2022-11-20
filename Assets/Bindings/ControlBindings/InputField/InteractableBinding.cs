using TMPro;

namespace Pooffick.Bindings.ControlBindings.InputField
{
    public class InteractableBinding : ControlBinding
    {
        private readonly TMP_InputField _inputField;

        public InteractableBinding(TMP_InputField inputField, Bindable owner, string property) : base(owner, property)
        {
            _inputField = inputField;

            _inputField.interactable = (bool)_propertyInfo.GetValue(owner);

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

            _inputField.interactable = (bool)e.NewValue;
        }
    }
}