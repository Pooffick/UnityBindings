using TMPro;

namespace Pooffick.Bindings.ControlBindings.InputField
{
    public class TextBinding : ControlBinding
    {
        private readonly TMP_InputField _inputField;

        public TextBinding(TMP_InputField inputField, Bindable owner, string property, BindMode mode) : base(owner, property)
        {
            _inputField = inputField;

            _inputField.text = (string)_propertyInfo.GetValue(owner);

            if (mode != BindMode.OneWayToSource)
                _owner.PropertyChanged += OnPropertyChanged;

            if (mode != BindMode.OneWay)
                _inputField.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void Disposing()
        {
            _owner.PropertyChanged -= OnPropertyChanged;
            _inputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName != _propertyInfo.Name)
                return;

            _inputField.text = (string)e.NewValue;
        }

        private void OnValueChanged(string newValue)
        {
            _propertyInfo.SetValue(_owner, newValue);
        }
    }
}