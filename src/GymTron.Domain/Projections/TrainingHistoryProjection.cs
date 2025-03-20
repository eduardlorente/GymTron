﻿using GymTron.Domain.ValueObjects;

namespace GymTron.Domain.Projections;

public class TrainingHistoryProjection
{
    public TrainingDate StartedOn { get; set; } = new TrainingDate(DateTime.MinValue);
}
