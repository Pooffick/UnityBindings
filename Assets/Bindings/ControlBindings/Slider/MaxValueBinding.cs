namespace Pooffick.Bindings.ControlBindings.Slider
{
    public class MaxValueBinding : ControlBinding
    {
        private readonly UnityEngine.UI.Slider _slider;

        public MaxValueBinding(UnityEngine.UI.Slider slider, Bindable owner, string property) : base(owner, property)
        {
            _slider = slider;

            _slider.maxValue = (float)_propertyInfo.GetValue(owner);

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

            _slider.maxValue = (float)e.NewValue;
        }
    }
}