namespace Pooffick.Bindings.ControlBindings.Slider
{
    public class InteractableBinding : ControlBinding
    {
        private readonly UnityEngine.UI.Slider _slider;

        public InteractableBinding(UnityEngine.UI.Slider slider, Bindable owner, string property) : base(owner, property)
        {
            _slider = slider;

            _slider.interactable = (bool)_propertyInfo.GetValue(owner);

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

            _slider.interactable = (bool)e.NewValue;
        }
    }
}