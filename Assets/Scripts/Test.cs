/// @license MIT License <https://opensource.org/licenses/MIT>
/// @copyright Copyright (C) Turbo Labz 2017 - All rights reserved
/// Source from this file can be used as per the license agreement
/// Open source
/// 
/// @author Mubeen Iqbal <mubeen@turbolabz.com>
/// @company Turbo Labz <http://turbolabz.com>
/// @date 2017-02-24 07:53:06 UTC+05:00
/// 
/// @description
/// [add_description_here]

using System;

using UnityEngine;

namespace TurboLabz.UnityStateMachine
{
    public class Test : MonoBehaviour
    {
        enum Trigger
        {
            CallDialed,
            HungUp,
            CallConnected,
            LeftMessage,
            PlacedOnHold,
            TakenOffHold,
            PhoneHurledAgainstWall
        }

        enum State
        {
            OffHook,
            Ringing,
            Connected,
            OnHold,
            PhoneDestroyed
        }

        void Start()
        {
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
                     .SubstateOf(State.Connected)
                     .Permit(Trigger.TakenOffHold, State.Connected)
                     .Permit(Trigger.HungUp, State.OffHook)
                     .Permit(Trigger.PhoneHurledAgainstWall, State.PhoneDestroyed);

            Print(phoneCall);

            Fire(phoneCall, Trigger.CallDialed);
            Print(phoneCall);

            Fire(phoneCall, Trigger.CallConnected);
            Print(phoneCall);

            Fire(phoneCall, Trigger.PlacedOnHold);
            Print(phoneCall);

            Fire(phoneCall, Trigger.TakenOffHold);
            Print(phoneCall);

            Fire(phoneCall, Trigger.HungUp);
            Print(phoneCall);
        }

        static void StartCallTimer()
        {
            Debug.Log("[Timer:] Call started at " + DateTime.Now);
        }

        static void StopCallTimer()
        {
            Debug.Log("[Timer:] Call ended at " + DateTime.Now);
        }

        static void Fire(IStateMachine<State, Trigger> phoneCall, Trigger trigger)
        {
            Debug.Log("[Firing:] " + trigger);
            phoneCall.Fire(trigger);
        }

        static void Print(IStateMachine<State, Trigger> phoneCall)
        {
            Debug.Log("[Status:] " + phoneCall);
        }
    }
}
