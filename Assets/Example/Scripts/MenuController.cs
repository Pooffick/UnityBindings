using System.Collections.ObjectModel;
using Pooffick.Bindings;
using Pooffick.Bindings.ControlBindings.Dropdown;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : Bindable
{
    [SerializeField]
    private TMP_InputField _inputField;
    [SerializeField]
    private TMP_Text _toggleLabel;
    [SerializeField]
    private TMP_Text _timerControl;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private TMP_Dropdown _dropdown;

    private int _index = 0;

    private bool _inputFieldEnabled = true;
    private string _toggleLabelText = "InputField is Enabled";
    private string _inputFieldText;
    private float _timer;
    private bool _timerVisible = false;
    private float _sliderMinValue = 0;
    private float _sliderMaxValue = 1;
    private float _sliderValue = 0;
    private int _dropdownSelected = 0;

    public bool InputFieldEnabled
    {
        get => _inputFieldEnabled;
        set
        {
            SetProperty(ref _inputFieldEnabled, value);
            ToggleLabelText = _inputFieldEnabled ? "InputField is Enabled" : "InputField is Disabled";
        }
    }

    public string ToggleLabelText
    {
        get => _toggleLabelText;
        set => SetProperty(ref _toggleLabelText, value);
    }

    public string InputFieldText
    {
        get => _inputFieldText;
        set => SetProperty(ref _inputFieldText, value);
    }

    public string Timer => $"{(int)_timer / 60}:{_timer % 60:00.000}";

    public float RealTimer
    {
        get => _timer;
        set
        {
            SetProperty(ref _timer, value);
            OnPropertyChanged(nameof(Timer), Timer);
        }
    }

    public bool TimerVisible
    {
        get => _timerVisible;
        set => SetProperty(ref _timerVisible, value);
    }

    public float SliderMinValue
    {
        get => _sliderMinValue;
        set => SetProperty(ref _sliderMinValue, value);
    }

    public float SliderMaxValue
    {
        get => _sliderMaxValue;
        set => SetProperty(ref _sliderMaxValue, value);
    }

    public float SliderValue
    {
        get => _sliderValue;
        set => SetProperty(ref _sliderValue, value);
    }

    public int DropdownSelected
    {
        get => _dropdownSelected;
        set
        {
            SetProperty(ref _dropdownSelected, value);
            OnPropertyChanged(nameof(DropdownSelectedText), DropdownSelectedText);
        }
    }

    public string DropdownSelectedText => DropdownOptions[DropdownSelected].text;

    public OptionCollection DropdownOptions { get; } = new OptionCollection();

    private void Start()
    {
        InitBindings();

        SliderMinValue = 0;
        SliderMaxValue = 100;
        SliderValue = 0;
    }

    private void Update()
    {
        RealTimer += Time.deltaTime;
        SliderValue += Time.deltaTime * 10;
        if (SliderValue >= SliderMaxValue)
        {
            SliderValue = 0;
            _index++;
            DropdownOptions.Add($"Option #{_index}");
            Debug.Log(DropdownSelectedText);
        }
    }

    private void InitBindings()
    {
        AddTextTextBinding(nameof(ToggleLabelText), _toggleLabel);
        AddToggleIsOnBinding(nameof(InputFieldEnabled), BindMode.TwoWay);
        AddControlEnabledBinding(nameof(InputFieldEnabled), _inputField);
        AddInputFieldTextBinding(nameof(InputFieldText), BindMode.TwoWay, _inputField);

        AddTextTextBinding(nameof(Timer));
        AddToggleIsOnBinding(nameof(TimerVisible), BindMode.OneWayToSource);
        AddControlActiveBinding(nameof(TimerVisible), _timerControl.gameObject);

        AddSliderMinValueBinding(nameof(SliderMinValue), _slider);
        AddSliderMaxValueBinding(nameof(SliderMaxValue), _slider);
        AddSliderValueBinding(nameof(SliderValue), BindMode.TwoWay, _slider);

        AddDropdownOptionsBinding(nameof(DropdownOptions), _dropdown);
        AddDropdownValueBinding(nameof(DropdownSelected), BindMode.TwoWay, _dropdown);
    }
}
