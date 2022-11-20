using TMPro;

namespace Pooffick.Bindings.ControlBindings.Dropdown
{
    public class ValueBinding : ControlBinding
    {
        private readonly TMP_Dropdown _dropdown;

        public ValueBinding(TMP_Dropdown dropdown, Bindable owner, string property, BindMode mode) : base(owner, property)
        {
            _dropdown = dropdown;

            _dropdown.value = (int)_propertyInfo.GetValue(owner);
            _dropdown.RefreshShownValue();

            if (mode != BindMode.OneWayToSource)
                _owner.PropertyChanged += OnPropertyChanged;

            if (mode != BindMode.OneWay)
                _dropdown.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void Disposing()
        {
            _owner.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName != _propertyInfo.Name)
                return;

            _dropdown.value = (int)e.NewValue;
            _dropdown.RefreshShownValue();
        }

        private void OnValueChanged(int newValue)
        {
            _propertyInfo.SetValue(_owner, newValue);
        }
    }
}