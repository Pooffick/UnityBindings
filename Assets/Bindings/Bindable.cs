using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pooffick.Bindings.ControlBindings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pooffick.Bindings
{
    public enum BindMode
    {
        OneWay,
        TwoWay,
        OneWayToSource
    }

    public abstract partial class Bindable : MonoBehaviour
    {
        private readonly List<ControlBinding> _bindings = new List<ControlBinding>();

        public event Action<PropertyChangedEventArgs> PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName]string property = null)
        {
            if (newValue.Equals(field))
                return false;

            field = newValue;
            OnPropertyChanged(property, newValue);
            return true;
        }

        protected void OnPropertyChanged(string property, object newValue)
        {
            PropertyChanged?.Invoke(new PropertyChangedEventArgs(property, newValue));
        }

        #region Methods

        protected void AddControlEnabledBinding(string property, UIBehaviour control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            if (control is TMP_InputField inputField)
            {
                _bindings.Add(new ControlBindings.InputField.InteractableBinding(inputField, this, property));
                return;
            }

            if (control is Slider slider)
            {
                _bindings.Add(new ControlBindings.Slider.InteractableBinding(slider, this, property));
                return;
            }

            if (control is TMP_Dropdown dropdown)
            {
                _bindings.Add(new ControlBindings.Dropdown.InteractableBinding(dropdown, this, property));
                return;
            }

            _bindings.Add(new EnabledBinding(control, this, property));
        }

        protected void AddControlActiveBinding(string property, GameObject control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            _bindings.Add(new ActiveBinding(control, this, property));
        }

        protected void AddTextColorBinding(string property, TMP_Text control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            _bindings.Add(new ControlBindings.Text.ColorBinding(control, this, property));
        }

        protected void AddTextTextBinding(string property, TMP_Text field = null)
        {
            TMP_Text component = field;
            if (component == null)
            {
                var control = GameObject.Find(property);
                if (control == null)
                    throw new ArgumentException($"Control named '{property}' not found.");
                component = control.GetComponent<TMP_Text>();
                if (component == null)
                    throw new ArgumentException($"Control named '{property}' does not contain TMP_Text component.");
            }

            _bindings.Add(new ControlBindings.Text.TextBinding(component, this, property));
        }

        protected void AddDropdownValueBinding(string property, BindMode mode, TMP_Dropdown field = null)
        {
            TMP_Dropdown component = field;
            if (component == null)
            {
                var control = GameObject.Find(property);
                if (control == null)
                    throw new ArgumentException($"Control named '{property}' not found.");
                component = control.GetComponent<TMP_Dropdown>();
                if (component == null)
                    throw new ArgumentException($"Control named '{property}' does not contain TMP_Dropdown component.");
            }

            _bindings.Add(new ControlBindings.Dropdown.ValueBinding(component, this, property, mode));
        }

        protected void AddDropdownOptionsBinding(string property, TMP_Dropdown control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            _bindings.Add(new ControlBindings.Dropdown.OptionsBinding(control, this, property));
        }

        protected void AddInputFieldTextBinding(string property, BindMode mode, TMP_InputField field = null)
        {
            TMP_InputField component = field;
            if (component == null)
            {
                var control = GameObject.Find(property);
                if (control == null)
                    throw new ArgumentException($"Control named '{property}' not found.");
                component = control.GetComponent<TMP_InputField>();
                if (component == null)
                    throw new ArgumentException($"Control named '{property}' does not contain TMP_InputField component.");
            }

            _bindings.Add(new ControlBindings.InputField.TextBinding(component, this, property, mode));
        }

        protected void AddToggleIsOnBinding(string property, BindMode mode, Toggle field = null)
        {
            Toggle component = field;
            if (component == null)
            {
                var control = GameObject.Find(property);
                if (control == null)
                    throw new ArgumentException($"Control named '{property}' not found.");
                component = control.GetComponent<Toggle>();
                if (component == null)
                    throw new ArgumentException($"Control named '{property}' does not contain Toggle component.");
            }

            _bindings.Add(new ControlBindings.Toggle.IsOnBinding(component, this, property, mode));
        }

        protected void AddSliderValueBinding(string property, BindMode mode, Slider field = null)
        {
            Slider component = field;
            if (component == null)
            {
                var control = GameObject.Find(property);
                if (control == null)
                    throw new ArgumentException($"Control named '{property}' not found.");
                component = control.GetComponent<Slider>();
                if (component == null)
                    throw new ArgumentException($"Control named '{property}' does not contain Slider component.");
            }

            _bindings.Add(new ControlBindings.Slider.ValueBinding(component, this, property, mode));
        }

        protected void AddSliderMinValueBinding(string property, Slider control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            _bindings.Add(new ControlBindings.Slider.MinValueBinding(control, this, property));
        }

        protected void AddSliderMaxValueBinding(string property, Slider control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            _bindings.Add(new ControlBindings.Slider.MaxValueBinding(control, this, property));
        }

        protected void RemoveBinding(string property)
        {
            var toRemove = new List<ControlBinding>();
            foreach (ControlBinding binding in _bindings)
            {
                if (binding.Property == property)
                    toRemove.Add(binding);
            }

            foreach (ControlBinding binding in toRemove)
            {
                _bindings.Remove(binding);
                binding.Dispose();
            }
        }

        #endregion

        private void OnDestroy()
        {
            foreach (ControlBinding binding in _bindings)
                binding.Dispose();
        }
    }

    public class PropertyChangedEventArgs
    {
        public string PropertyName { get; set; }

        public object NewValue { get; set; }

        public PropertyChangedEventArgs(string propertyName, object newValue)
        {
            PropertyName = propertyName;
            NewValue = newValue;
        }
    }
}