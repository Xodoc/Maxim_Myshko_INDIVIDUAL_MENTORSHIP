﻿using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface ICommand
    {
        Task Execute();
    }
}
