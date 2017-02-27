/// @license MIT License <https://opensource.org/licenses/MIT>
/// @copyright Copyright (C) Turbo Labz 2017 - All rights reserved
/// Open source
/// 
/// @author Mubeen Iqbal <mubeen@turbolabz.com>
/// @company Turbo Labz <http://turbolabz.com>
/// @date 2017-02-23 15:21:49 UTC+05:00
/// 
/// @description
/// [add_description_here]

using System;
using System.Collections.Generic;

namespace TurboLabz.UnityStateMachine
{
    public class StateRepresentation<TState, TTrigger> : IStateRepresentation<TState, TTrigger>
    {
        // Remember that transitions are always outgoing for a state. They
        // can never be incoming. Every state can only control where it can
        // go next, not from where the state was reached.
        private IDictionary<TTrigger, TState> _transitions = new Dictionary<TTrigger, TState>();
        private IList<Action> _activationActions = new List<Action>();
        private IList<Action> _deactivationActions = new List<Action>();
        private IList<Action> _entryActions = new List<Action>();
        private IList<Action> _exitActions = new List<Action>();
        private bool _isActive;

        public TState state { get; private set; }

        public ICollection<TTrigger> permittedTriggers
        {
            get 
            {
                return _transitions.Keys;
            }
        }

        public StateRepresentation(TState state)
        {
            this.state = state;
        }

        public bool CanHandle(TTrigger trigger)
        {
            return _transitions.ContainsKey(trigger);
        }

        public void AddTransition(TTrigger trigger, TState state)
        {
            _transitions.Add(trigger, state);
        }

        public void RemoveTransition(TTrigger trigger)
        {
            if (!_transitions.ContainsKey(trigger))
            {
                throw new NotSupportedException("No transition present for trigger " + trigger);
            }

            _transitions.Remove(trigger);
        }

        public TState GetTransitionState(TTrigger trigger)
        {
            if (!_transitions.ContainsKey(trigger))
            {
                throw new KeyNotFoundException("No transition present for trigger " + trigger);
            }

            return _transitions[trigger];
        }

        public void Activate()
        {
            if (_isActive)
            {
                return;
            }

            foreach (Action action in _activationActions)
            {
                action();
            }

            _isActive = true;
        }

        public void Deactivate()
        {
            if (!_isActive)
            {
                return;
            }

            foreach (Action action in _deactivationActions)
            {
                action();
            }

            _isActive = false;
        }

        public void Enter()
        {
            foreach (Action action in _entryActions)
            {
                action();
            }
        }

        public void Exit()
        {
            foreach (Action action in _exitActions)
            {
                action();
            }
        }

        public void AddActivationAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "Action parameter must not be null");
            }

            if (_activationActions.Contains(action))
            {
                throw new NotSupportedException("Action " + action + " is already added to activationActions");
            }

            _activationActions.Add(action);
        }

        public void AddDeactivationAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "Action parameter must not be null");
            }

            if (_deactivationActions.Contains(action))
            {
                throw new NotSupportedException("Action " + action + " is already added to deactivationActions");
            }

            _deactivationActions.Add(action);
        }

        public void AddEntryAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "Action parameter must not be null");
            }

            if (_entryActions.Contains(action))
            {
                throw new NotSupportedException("Action " + action + " is already added to entryActions");
            }

            _entryActions.Add(action);
        }

        public void AddExitAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "Action parameter must not be null");
            }

            if (_exitActions.Contains(action))
            {
                throw new NotSupportedException("Action " + action + " is already added to exitActions");
            }

            _exitActions.Add(action);
        }
    }
}
