namespace Pooffick.Bindings.ControlBindings.Slider
{
    public class ValueBinding : ControlBinding
    {
        private readonly UnityEngine.UI.Slider _slider;

        public ValueBinding(UnityEngine.UI.Slider slider, Bindable owner, string property, BindMode mode) : base(owner, property)
        {
            _slider = slider;

            _slider.value = (float)_propertyInfo.GetValue(owner);

            if (mode != BindMode.OneWayToSource)
                _owner.PropertyChanged += OnPropertyChanged;

            if (mode != BindMode.OneWay)
                _slider.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void Disposing()
        {
            _owner.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName != _propertyInfo.Name)
                return;

            _slider.value = (float)e.NewValue;
        }

        private void OnValueChanged(float newValue)
        {
            _propertyInfo.SetValue(_owner, newValue);
        }
    }
}