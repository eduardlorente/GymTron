using GymTron.App.Helpers;
using GymTron.App.ViewModels.Entities;
using GymTron.Application.BodyWeights.Commands;
using GymTron.Application.Logs.Commands;
using GymTron.Application.Trainings.Commands;
using GymTron.Application.Trainings.Queries;
using GymTron.Domain.Aggregates;
using GymTron.Domain.Projections;
using MediatR;
using Newtonsoft.Json;

namespace GymTron.App.Services;

internal class TrainingService(IMediator mediator) : ITrainingService
{


    private const string CURRENT_TRAINING_PREFERENCES_KEY = "current_training";


    private readonly IMediator _mediator = mediator;


    private TrainingViewModel? _currentTraining;


    public async Task StartTraining(int routineId, int dayOfWeek)
    {
        if (_currentTraining != null)
        {
            throw new InvalidOperationException("There is already a training in progress.");
        }

        Guid correlationId = Guid.NewGuid();

        StartTrainingCommand command = new(correlationId, routineId, dayOfWeek);

        await _mediator.Send(command);

        SaveCurrentTrainingIntoMemory(await GetCurrentTraining());
    }


    public async Task<TrainingViewModel?> GetCurrentTraining()
    {
        if (_currentTraining != null)
        {
            return _currentTraining;
        }

        _currentTraining = GetTrainingFromPreferences();

        if (_currentTraining == null)
        {
            Training training = await _mediator.Send(new CurrentTrainingQuery(Guid.NewGuid()));

            if (training != null)
            {
                SaveCurrentTrainingIntoMemory(new TrainingViewModel(training));
            }
        }

        return _currentTraining;
    }


    public async Task FinalizeTraining()
    {
        if (_currentTraining == null)
        {
            throw new InvalidOperationException("There is no training in progress.");
        }

        Guid correlationId = Guid.NewGuid();

        FinishTrainingCommand command = new(correlationId, _currentTraining.ToDomain());

        await _mediator.Send(command);

        RemoveCurrentTraining();
    }


    public async Task CompleteTrainingExercise(int exerciseParametersId, string name, decimal weight, int repetitions, List<string> observations)
    {
        if (_currentTraining == null)
        {
            throw new InvalidOperationException("There is no training in progress.");
        }

        Guid correlationId = Guid.NewGuid();

        AddExerciseToTrainingCommand command = AddExerciseToTrainingCommand.New(correlationId,
                                                                                _currentTraining.ToDomain(),
                                                                                exerciseParametersId,
                                                                                name,
                                                                                weight,
                                                                                repetitions,
                                                                                observations);

        Training training = await _mediator.Send(command);

        SaveCurrentTrainingIntoMemory(new TrainingViewModel(training));
    }


    public async Task CompleteTrainingExercise(int exerciseParametersId, string name, int durationInSeconds, List<string> observations)
    {
        if (_currentTraining == null)
        {
            throw new InvalidOperationException("There is no training in progress.");
        }

        Guid correlationId = Guid.NewGuid();

        AddExerciseToTrainingCommand command = AddExerciseToTrainingCommand.New(correlationId,
                                                                                _currentTraining.ToDomain(),
                                                                                exerciseParametersId,
                                                                                name,
                                                                                durationInSeconds,
                                                                                observations);

        Training training = await _mediator.Send(command);

        SaveCurrentTrainingIntoMemory(new TrainingViewModel(training));
    }


    public async Task<List<TrainingHistoryItemViewModel>> ListHistory()
    {
        List<TrainingHistoryProjection> trainings = await _mediator.Send(new HistoryTrainingsQuery(Guid.NewGuid()));

        List<(int Year, int Month, int count)> history = trainings.GroupBy(t => new { t.StartedOn.FullDate.Year, t.StartedOn.FullDate.Month })
                                                                  .Select(g => (g.Key.Year, g.Key.Month, g.Count()))
                                                                  .OrderByDescending(h => h.Year)
                                                                  .ThenByDescending(h => h.Month)
                                                                  .ToList();

        return history.Select(h => new TrainingHistoryItemViewModel(h.Year, DateUtils.MonthNameInCatalan(h.Month), h.count)).ToList();
    }


    private void SaveCurrentTrainingIntoMemory(TrainingViewModel? training)
    {
        _currentTraining = training;

        string trainingJson = JsonConvert.SerializeObject(training);
        Preferences.Set(CURRENT_TRAINING_PREFERENCES_KEY, trainingJson);
    }


    private void RemoveCurrentTraining()
    {
        _currentTraining = null;

        Preferences.Remove(CURRENT_TRAINING_PREFERENCES_KEY);
    }


    private static TrainingViewModel? GetTrainingFromPreferences()
    {
        if (Preferences.ContainsKey(CURRENT_TRAINING_PREFERENCES_KEY))
        {
            string trainingJson = Preferences.Get(CURRENT_TRAINING_PREFERENCES_KEY, string.Empty);
            if (!string.IsNullOrEmpty(trainingJson))
            {
                return JsonConvert.DeserializeObject<TrainingViewModel>(trainingJson);
            }
        }

        return null;
    }
}
