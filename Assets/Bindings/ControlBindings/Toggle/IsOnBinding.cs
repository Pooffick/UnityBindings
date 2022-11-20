namespace Pooffick.Bindings.ControlBindings.Toggle
{
    public class IsOnBinding : ControlBinding
    {
        private readonly UnityEngine.UI.Toggle _toggle;

        public IsOnBinding(UnityEngine.UI.Toggle toggle, Bindable owner, string property, BindMode mode) : base(owner, property)
        {
            _toggle = toggle;

            _toggle.isOn = (bool)_propertyInfo.GetValue(owner);

            if (mode != BindMode.OneWayToSource)
                _owner.PropertyChanged += OnPropertyChanged;

            if (mode != BindMode.OneWay)
                _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void Disposing()
        {
            _owner.PropertyChanged -= OnPropertyChanged;
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName != _propertyInfo.Name)
                return;

            _toggle.isOn = (bool)e.NewValue;
        }

        private void OnValueChanged(bool newValue)
        {
            _propertyInfo.SetValue(_owner, newValue);
        }
    }
}