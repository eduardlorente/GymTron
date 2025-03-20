using System.ComponentModel;

namespace GymTron.App.ViewModels.Entities;

public class BodyWeightHistoryItemViewModel(decimal weight, decimal imc, DateTime createdOn) : INotifyPropertyChanged
{


    private decimal _weight = weight;
    public string Weight
    {
        get => $"{_weight} kg";
        set
        {
            if (decimal.TryParse(value.Replace(" kg", ""), out decimal parsedWeight))
            {
                _weight = parsedWeight;
                OnPropertyChanged(nameof(Weight));
            }
        }
    }

    private decimal _imc = imc;
    public string IMC
    {
        get => $"{_imc} %";
        set
        {
            if (decimal.TryParse(value.Replace(" %", ""), out decimal parsedIMC))
            {
                _imc = parsedIMC;
                OnPropertyChanged(nameof(IMC));
            }
        }
    }

    private DateTime _createdOn = createdOn;
    public string CreatedOn
    {
        get => _createdOn.ToString("dd/MM/yyyy");
        set
        {
            if (DateTime.TryParse(value, out DateTime parsedDate))
            {
                _createdOn = parsedDate;
                OnPropertyChanged(nameof(CreatedOn));
            }
        }
    }



    public string WeightColor { get; set; } = "Black";
    public string IMCColor { get; set; } = "Black";


    public void SetColor(BodyWeightHistoryItemViewModel nextItem)
    {
        if (nextItem != null)
        {
            if(_weight > 0)
                WeightColor = _weight > nextItem._weight ? "Red" : "Green";

            if(_imc > 0)
                IMCColor = _imc > nextItem._imc ? "Red" : "Green";
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;


    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}