# Unity State Machine

Create _state machines_ and lightweight _state machine-based workflows_ for Unity using C# .NET version 3.5.

```cs
IStateMachine<State, Trigger> phoneCall = new StateMachine<State, Trigger>(State.OffHook);

phoneCall.Configure(State.OffHook)
         .Permit(Trigger.CallDialed, State.Ringing);

phoneCall.Configure(State.Ringing)
         .Permit(Trigger.HungUp, State.OffHook)
         .Permit(Trigger.CallConnected, State.Connected);

phoneCall.Configure(State.Connected)
         .OnEnter(() => { StartCallTimer(); })
         .OnExit(() => { StopCallTimer(); })
         .Permit(Trigger.LeftMessage, State.OffHook)
         .Permit(Trigger.HungUp, State.OffHook)
         .Permit(Trigger.PlacedOnHold, State.OnHold);

phoneCall.Configure(State.OnHold)
         .Permit(Trigger.TakenOffHold, State.Connected)
         .Permit(Trigger.HungUp, State.OffHook)
         .Permit(Trigger.PhoneHurledAgainstWall, State.PhoneDestroyed);

phoneCall.Fire(Trigger.CallDialed);
Logger.Log(State.Ringing == phoneCall.currentState); // Must be True
```

This project is inspired by [Stateless](https://github.com/dotnet-state-machine/stateless).

## Features

Most standard state machine constructs are supported:

 * Generic support for states and triggers of any .NET type (numbers, strings, enums, etc.)
 * Entry/exit events for states
 * Guard clauses to support conditional transitions
 * Introspection

### Entry/Exit Events

In the example, the `StartCallTimer()` method will be executed when a call is connected. The `StopCallTimer()` will be executed when call completes (by either hanging up or hurling the phone against the wall.)

The call can move between the `Connected` and `OnHold` states without the `StartCallTimer()` and `StopCallTimer()` methods being called repeatedly because the `OnHold` state is a substate of the `Connected` state.

### Guard Clauses

The state machine will choose between multiple transitions based on guard clauses, e.g.:

```cs
phoneCall.Configure(State.OffHook)
         .PermitIf(Trigger.CallDialled, State.Ringing, () => IsValidNumber)
         .PermitIf(Trigger.CallDialled, State.Beeping, () => !IsValidNumber);
```

Guard clauses within a state must be mutually exclusive (multiple guard clauses cannot be valid at the same time.)

### Introspection

The state machine can provide a list of the triggers that can be successfully fired within the current state via the `StateMachine.permittedTriggers` property.

## Building

Unity State Machine runs on Unity using C# .NET 3.5. Just use it in your project and you are good to go.

## Project Goals

This page is an almost-complete description of Unity State Machine, and its explicit aim is to remain minimal. The project was built in an effort to create an easy to use framework for use in the Unity game engine.

Please use the issue tracker if you'd like to report problems or discuss features.
