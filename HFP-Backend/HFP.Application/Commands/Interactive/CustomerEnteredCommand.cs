﻿using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Interactive
{
    public record CustomerEnteredCommand(string BuyerId) : ICommand;
}
