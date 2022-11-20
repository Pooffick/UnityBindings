using TMPro;
using UnityEngine;

namespace Pooffick.Bindings.ControlBindings.Text
{
    public class ColorBinding : ControlBinding
    {
        private readonly TMP_Text _text;

        public ColorBinding(TMP_Text text, Bindable owner, string property) : base(owner, property)
        {
            _text = text;

            _text.color = (Color)_propertyInfo.GetValue(owner);

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

            _text.color = (Color)e.NewValue;
        }
    }
}