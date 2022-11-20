using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using TMPro;

namespace Pooffick.Bindings.ControlBindings.Dropdown
{
    public class OptionsBinding : ControlBinding
    {
        private readonly TMP_Dropdown _dropdown;
        private readonly OptionCollection _collection;

        public OptionsBinding(TMP_Dropdown dropdown, Bindable owner, string property) : base(owner, property)
        {
            _dropdown = dropdown;
            _collection = (OptionCollection)_propertyInfo.GetValue(owner);

            FillOptions();

            _collection.CollectionChanged += OnCollectionChanged;
        }

        protected override void Disposing()
        {
            _collection.CollectionChanged -= OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FillOptions();
        }

        private void FillOptions()
        {
            int selectedIndex = _dropdown.value;
            _dropdown.ClearOptions();
            foreach (TMP_Dropdown.OptionData option in _collection)
                _dropdown.options.Add(option);
            _dropdown.SetValueWithoutNotify(selectedIndex);
            _dropdown.RefreshShownValue();
        }
    }

    public class OptionCollection : ObservableCollection<TMP_Dropdown.OptionData>
    {
        public OptionCollection(IEnumerable<string> options) : base(options.Select(x => new TMP_Dropdown.OptionData(x)))
        {
        }

        public OptionCollection(IEnumerable<UnityEngine.Sprite> options) : base(options.Select(x => new TMP_Dropdown.OptionData(x)))
        {
        }

        public OptionCollection(IEnumerable<TMP_Dropdown.OptionData> options) : base(options)
        {
        }

        public OptionCollection() : base()
        {
        }

        public void Add(string option)
        {
            Add(new TMP_Dropdown.OptionData(option));
        }

        public void Add(UnityEngine.Sprite option)
        {
            Add(new TMP_Dropdown.OptionData(option));
        }
    }
}