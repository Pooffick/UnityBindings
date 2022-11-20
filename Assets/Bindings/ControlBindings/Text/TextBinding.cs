using TMPro;

namespace Pooffick.Bindings.ControlBindings.Text
{
    public class TextBinding : ControlBinding
    {
        private readonly TMP_Text _text;

        public TextBinding(TMP_Text text, Bindable owner, string property) : base(owner, property)
        {
            _text = text;

            _text.text = (string)_propertyInfo.GetValue(owner);

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

            _text.text = (string)e.NewValue;
        }
    }
}