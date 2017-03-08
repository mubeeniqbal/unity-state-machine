/// @license MIT License <https://opensource.org/licenses/MIT>
/// @copyright Copyright (C) Turbo Labz 2017 - All rights reserved
/// Source from this file can be used as per the license agreement
/// Open source
/// 
/// @author Mubeen Iqbal <mubeen@turbolabz.com>
/// @company Turbo Labz <http://turbolabz.com>
/// @date 2017-02-23 15:20:19 UTC+05:00
/// 
/// @description
/// [add_description_here]

using System;
using System.Collections.Generic;

namespace TurboLabz.UnityStateMachine
{
    public interface IStateRepresentation<TState, TTrigger>
    {
        TState state { get; }
        IStateRepresentation<TState, TTrigger> superState { get; set; }
        ICollection<TTrigger> permittedTriggers { get; }

        bool CanHandle(TTrigger trigger);
        void AddTransition(TTrigger trigger, TState state);
        void RemoveTransition(TTrigger trigger);
        TState GetTransitionState(TTrigger trigger);
        void OnEnter();
        void OnExit();
        void AddEntryAction(Action action);
        void AddExitAction(Action action);
    }
}
