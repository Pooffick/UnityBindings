using System;
using System.Reflection;

namespace Pooffick.Bindings.ControlBindings
{
    public abstract class ControlBinding : IDisposable
    {
        protected readonly Bindable _owner;
        protected readonly PropertyInfo _propertyInfo;

        public ControlBinding(Bindable owner, string property)
        {
            _owner = owner;
            _propertyInfo = GetPropertyInfo(owner, property);
        }

        public void Dispose()
        {
            Disposing();
        }

        protected abstract void Disposing();

        protected virtual PropertyInfo GetPropertyInfo(Bindable owner, string property)
        {
            foreach (PropertyInfo propertyInfo in owner.GetType().GetProperties())
            {
                if (propertyInfo.Name == property)
                    return propertyInfo;
            }

            throw new ArgumentException($"Property '{property}' not found in '{owner.GetType().Name}/{owner.name}'.");
        }
    }
}